using System.Data.OleDb;

public class AccessDatabaseService
{
    private readonly string _connectionString;

    public AccessDatabaseService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void InsertOperation(string operation)
    {
        using (var connection = new OleDbConnection(_connectionString))
        {
            connection.Open();
            using (var command = new OleDbCommand())
            {
                command.Connection = connection;
                command.CommandText = "INSERT INTO table (Operation) VALUES (@Operation)";
                command.Parameters.AddWithValue("@Operation", operation);
                command.ExecuteNonQuery();
            }
        }
    }

    public List<string> GetOperations()
    {
        List<string> operations = new List<string>();

        using (var connection = new OleDbConnection(_connectionString))
        {
            connection.Open();
            using (var command = new OleDbCommand())
            {
                command.Connection = connection;
                command.CommandText = "SELECT Operation FROM table";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string operation = reader.GetString(0);
                        operations.Add(operation);
                    }
                }
            }
        }

        return operations;
    }
}
