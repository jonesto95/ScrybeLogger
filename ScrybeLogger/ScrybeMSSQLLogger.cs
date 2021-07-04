using System.Data.SqlClient;

namespace ScrybeLogger
{
    public class ScrybeMSSQLLogger : ScrybeLogger
    {
        private SqlConnection SqlConnection { get; set; }

        private string TableName { get; set; }

        public ScrybeMSSQLLogger(decimal loggingLevel, string connectionString, string tableName)
            : base(loggingLevel)
        {
            SqlConnection = new SqlConnection(connectionString);
            SqlConnection.Open();
            TableName = tableName;
            TableCheck();
        }


        private void TableCheck()
        {
            string tableCheck =
                $"IF NOT EXISTS ( SELECT 1 FROM sys.tables WHERE [name] = '{TableName}' ) " +
                " BEGIN " +
                $" CREATE TABLE [{TableName}] ( " +
                " LogId BIGINT NOT NULL IDENTITY(1, 1), " +
                " LogTime DATETIME NOT NULL, " +
                " LoggingLevel  VARCHAR(255) NOT NULL, " +
                " LogText NVARCHAR(MAX)," +
                " PRIMARY KEY (LogId) ) " +
                " END ";
            ExecuteSqlText(tableCheck);
        }


        public override void LogMessage(object message)
        {
            string messageString = message.ToString().Replace("'", "''");
            string logInsert = $"INSERT INTO [{TableName}] VALUES " +
                $"(GETDATE(), '{CurrentLoggingLevel}', '{messageString}')";
            ExecuteSqlText(logInsert);
        }


        private void ExecuteSqlText(string sql)
        {
            var sqlCommand = new SqlCommand(sql, SqlConnection);
            sqlCommand.ExecuteNonQuery();
        }
    }
}
