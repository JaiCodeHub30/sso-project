using Npgsql;

namespace SSOProject.Data
{
    public class PostgresHelper
    {  
        private string connectionString =
            "Host=localhost;Port=5432;Username=postgres;Password=Jaikumar30!;Database=sso_db";   //Database connection info

        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connectionString); //Returns database connection object
        }
    }
}

// connects the postgreSQL DB.