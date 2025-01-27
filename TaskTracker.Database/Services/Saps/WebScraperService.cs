using HtmlAgilityPack;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Text.RegularExpressions;
using TaskTracker.Common.Enums;
using TaskTracker.Common.Models.Saps;
using TaskTracker.Database.Models.Saps;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace TaskTracker.Database.Services.Saps
{
    public class WebScraperService
    {
        private TaskTrackerContext _taskTrackerContext;
        private readonly MissingPersonsListService _missingPersonsListService;
        private readonly WantedPersonsListService _wantedPersonsListService;
        public ILogger<WebScraperService> _logger;

        public WebScraperService(TaskTrackerContext taskTrackerContext, MissingPersonsListService missingPersonsListService, WantedPersonsListService wantedPersonsListService, ILogger<WebScraperService> logger)
        {
            _taskTrackerContext = taskTrackerContext;
            _missingPersonsListService = missingPersonsListService;
            _wantedPersonsListService = wantedPersonsListService;
            _logger = logger;
        }


        public async Task<bool> FetchAllMissingPersonCasesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var html = await _missingPersonsListService.ExecuteAsync(cancellationToken)
                    .ConfigureAwait(false);

                Dictionary<string, string>? missingPersonsList = await GetList(html);

                if (missingPersonsList != null)
                {
                    //now we query each valid individual and then log them to to the table
                    return await UpsertPersons("missing", missingPersonsList);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return true;
        }

        public async Task<bool> FetchAllWantedPersonCasesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var html = await _wantedPersonsListService.ExecuteAsync(cancellationToken)
                    .ConfigureAwait(false);

                Dictionary<string, string>? wantedPersonsList = await GetList(html);

                if (wantedPersonsList != null)
                {
                    //now we query each valid individual and then log them to to the table
                    return await UpsertPersons("wanted", wantedPersonsList);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return true;
        }


        private static Task<Dictionary<string, string>?> GetList(string htmlContent)
        {
            try
            {
                string typeOfPersons;
                Dictionary<string, string> listOfPersons = [];
                int previousItemIndex = 350*2;

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlContent);

                var headerNode = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel-heading']");
                var tableNode = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='col-md-14 cust-td-border']");

                if (headerNode != null && tableNode != null)
                {
                    typeOfPersons = headerNode.InnerHtml.Trim()?.Split(':')[0] ?? string.Empty;

                    var rows = tableNode.SelectNodes(".//table//tr");

                    int rowsCounter = 0;

                    foreach (var row in rows)
                    {
                        var linkNode = row.SelectSingleNode(".//a");

                        if (linkNode != null)
                        {
                            string key = linkNode.GetAttributeValue("href", string.Empty);
                            string value = linkNode.InnerText.Trim();

                            if (key != string.Empty && value != string.Empty && !listOfPersons.ContainsKey(key))
                            {
                                if (rowsCounter < previousItemIndex)
                                {
                                    rowsCounter++;

                                    continue;
                                }

                                listOfPersons.Add(key, value);
                            }
                        }
                    }

                    return System.Threading.Tasks.Task.FromResult<Dictionary<string, string>?>(listOfPersons);
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }

            return System.Threading.Tasks.Task.FromResult<Dictionary<string, string>?>(null);
        }

        private async Task<bool> UpsertPersons(string typeOfPersons, Dictionary<string, string> persons)
        {

            try
            {
                foreach (var person in persons)
                {
                    var id = int.Parse(person.Key.Split("=")[1]);
                    var personDetailsQueryUrl = $"https://www.saps.gov.za/crimestop/{typeOfPersons}/detail.php?bid={id}";

                    string personPhoto = "";
                    string fullName = "";
                    string redText = "";
                    string caseNumber = "";
                    string unknown = "Unknown";

                    Dictionary<string, string> tableRows = [];

                    using HttpClient client = new();

                    try
                    {
                        // Fetch the page
                        HttpResponseMessage response = await client.GetAsync(personDetailsQueryUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            string htmlContent = await response.Content.ReadAsStringAsync();

                            var htmlDoc = new HtmlDocument();
                            htmlDoc.LoadHtml(htmlContent);

                            var contentNode = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel-body']");

                            var nameNode = contentNode.SelectSingleNode("//h2");
                            fullName = nameNode?.InnerText.Trim() ?? string.Empty;

                            var redTextNode = contentNode.SelectSingleNode("//p/font[@color='red']");
                            redText = redTextNode?.InnerText.Trim() ?? string.Empty;

                            var caseNumberNode = contentNode.SelectSingleNode(".//table//td[@align='center']");
                            caseNumber = Regex.Match(caseNumberNode?.InnerHtml?.Trim()?.Split(">")[1] ?? string.Empty, @"\d+").Value;

                            var imageNode = htmlDoc.DocumentNode.SelectSingleNode(".//table//td[@align='center']//img");
                            var imageSource = imageNode.GetAttributeValue("src", string.Empty);
                            personPhoto = imageSource == string.Empty ? "" : $"https://www.saps.gov.za/crimestop/{typeOfPersons}/{imageSource}";

                            var rows = contentNode.SelectNodes("//table[@class='table table-bordered table-hover table-striped']//tr");

                            if (rows != null)
                            {
                                foreach (var row in rows)
                                {
                                    var headerCell = row.SelectSingleNode("./td[1]/b")?.InnerText.Trim();
                                    var valueCell = row.SelectSingleNode("./td[2]")?.InnerText.Trim();
                                    if (!string.IsNullOrEmpty(headerCell) && !string.IsNullOrEmpty(valueCell))
                                    {
                                        Console.WriteLine($"{headerCell}: {valueCell}");
                                        tableRows.Add(headerCell, valueCell);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Exception {ex}");
                    }

                    var names = fullName.Split(" ");

                    CaseDTO @case = new()
                    {
                        // Ensure safe parsing of case number
                        CaseNumber = int.TryParse(caseNumber, out int caseNum) ? caseNum : 0,

                        // Enum values with fallback
                        CaseStatus = CaseStatusEnum.Active,

                        // Handle missing or null values gracefully
                        ContactEmail = tableRows.GetValueOrDefault("E-mail:") ?? unknown,
                        ContactNumber = tableRows.GetValueOrDefault("Contact nr:") ?? unknown,
                        InvestigatingOfficer = tableRows.GetValueOrDefault("Investigating Officer:") ?? unknown,
                        CaseStatusDescription = tableRows.GetValueOrDefault("Pending Reason:") ?? unknown,

                        // Station information with default empty list for telephone lines
                        Station = new()
                        {
                            StationName = tableRows.GetValueOrDefault("Station:") ?? unknown,
                            StationTelephoneLines = new List<string>(
                                (tableRows.GetValueOrDefault("Station Telephone:") ?? string.Empty)
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            ),
                        },

                        // Subject details with better middle name handling
                        Subject = new()
                        {
                            FirstName = names.FirstOrDefault() ?? string.Empty,
                            MiddleName = names.Length == 2 ? names[1] : names.Length == 3 ? names[1] : string.Join(" ", names[1..(names.Length - 1)]),
                            Surname = names.LastOrDefault() ?? string.Empty,
                            Status = SubjectStatusEnum.Missing,
                            Alias = tableRows.GetValueOrDefault("Aliases:") == "0"
                                ? new List<string>()
                                : new List<string>((tableRows.GetValueOrDefault("Aliases:") ?? string.Empty).Split(",", StringSplitOptions.RemoveEmptyEntries)),

                            // Optional personal information with defaults
                            Gender = tableRows.GetValueOrDefault("Gender:") ?? unknown,
                            EyeColor = tableRows.GetValueOrDefault("Eye Colour:") ?? unknown,
                            HairColor = tableRows.GetValueOrDefault("Hair Colour:") ?? unknown,
                            Height = tableRows.GetValueOrDefault("Height:") ?? unknown,
                            Weight = tableRows.GetValueOrDefault("Weight:") ?? unknown,
                            Build = tableRows.GetValueOrDefault("Build:") ?? unknown,
                            SubjectPhoto = personPhoto,

                            // Handling disappearance details for missing persons
                            DisappearanceDetails = typeOfPersons == "missing" ? new DisappearanceDetailsDTO
                            {
                                CirculationNumber = tableRows.GetValueOrDefault("Circulation Number:") ?? unknown,
                                DisappearanceAge = redText ?? unknown,
                                MissingCircumstances = tableRows.GetValueOrDefault("Missing Circumstances:") ?? unknown,
                                MissingDate = DateTime.TryParse(tableRows.GetValueOrDefault("Missing Date:"), out var missingDate) ? missingDate : DateTime.MinValue
                            } : null,

                            // Handling warrant details for wanted persons
                            SubjectWarrant = typeOfPersons == "wanted" ? new SubjectWarrantDTO
                            {
                                ChargeCircumstances = tableRows.GetValueOrDefault("Crime Circumstances:") ?? unknown,
                                ChargeDate = DateTime.TryParse(tableRows.GetValueOrDefault("Crime Date:"), out var chargeDate) ? chargeDate : DateTime.MinValue,
                                ChargeType = tableRows.GetValueOrDefault("Crime:") ?? unknown,
                            } : null
                        }
                    };

                    var dbCase = @case.Adapt<CaseEntity>();

                    var result = await _taskTrackerContext.Cases
                            .FirstOrDefaultAsync((v) =>
                                v.CaseNumber == dbCase.CaseNumber &&
                                v.ContactEmail == dbCase.ContactEmail &&
                                v.Subject.FirstName == dbCase.Subject.FirstName &&
                                v.Subject.Surname == dbCase.Subject.Surname &&
                                v.Subject.SubjectPhoto == dbCase.Subject.SubjectPhoto
                            );

                    if (result == null)
                    {
                        await _taskTrackerContext.AddAsync(dbCase);
                        await _taskTrackerContext.SaveChangesAsync();
                    }

                    Thread.Sleep(10000);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception {ex}");
            }            
        }
    }
}