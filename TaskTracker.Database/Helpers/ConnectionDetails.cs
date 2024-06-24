namespace TaskTracker.Database.Helpers
{
    public class ConnectionStringHelper
    {
        public static string BuildConnectionString(ConnectionDetails connectionDetails)
        {
            return @$"
                    Server={connectionDetails.Server},1433;
                    Initial Catalog={connectionDetails.Database_Name};
                    Persist Security Info=False;
                    User ID={connectionDetails.User};
                    Password={connectionDetails.Password};
                    MultipleActiveResultSets=False;
                    Encrypt=True;
                    TrustServerCertificate=True;
                    Connection Timeout=30;";
        }
    }
}