using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleApp
{
    static class DBManipulation
    {
        public static void GetDatabaseInfoFromPostgreSQLServer(string databaseName)
        {
            string connectionString = $"{Connection.connectionString}Database={databaseName}";
            using var connection = new Npgsql.NpgsqlConnection(connectionString);
            connection.Open();

            using var getInfoSchemaColumnNamesCommand = new Npgsql.NpgsqlCommand("SELECT table_name, column_name, data_type\r\nFROM information_schema.columns\r\nWHERE table_schema = 'public'\r\nORDER BY table_name, ordinal_position;", connection);
            using var columnNamesReader = getInfoSchemaColumnNamesCommand.ExecuteReader();

            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            for (int i = 0; i < columnNamesReader.FieldCount; i++)
            {
                Console.Write($"{columnNamesReader.GetName(i),-35} | ");
            }
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");

            while (columnNamesReader.Read())
            {
                for (int i = 0; i < columnNamesReader.FieldCount; i++)
                {
                    if (!columnNamesReader.IsDBNull(i))
                        Console.Write($"{columnNamesReader.GetValue(i),-35} | ");
                    else
                        Console.Write($"{"NULL",-25} | ");
                }
                Console.WriteLine();
            }
            connection.Close();
        }

        public static void GetDatabaseTablesFromPostgreSQLServer(string databaseName)
        {
            string connectionString = $"{Connection.connectionString}Database={databaseName}";
            using var connection = new Npgsql.NpgsqlConnection(connectionString);
            connection.Open();

            using var getInfoSchemaColumnNamesCommand = new Npgsql.NpgsqlCommand("SELECT DISTINCT table_name\r\nFROM information_schema.columns\r\nWHERE table_schema = 'public'\r\nORDER BY table_name;\r\n", connection);
            using var columnNamesReader = getInfoSchemaColumnNamesCommand.ExecuteReader();

            Console.WriteLine("-------------------------------------");
            while (columnNamesReader.Read())
            {
                for (int i = 0; i < columnNamesReader.FieldCount; i++)
                {
                    if (!columnNamesReader.IsDBNull(i))
                        Console.Write($"{columnNamesReader.GetValue(i),-35} ");
                    else
                        Console.Write($"{"NULL",-25} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("-------------------------------------");
            connection.Close();
        }

        public static void GetDatabasesFromPostgreSQLServer()
        {
            string connectionString = $"{Connection.connectionString}";
            using var connection = new Npgsql.NpgsqlConnection(connectionString);
            connection.Open();
            using var getDatabasesCommand = new Npgsql.NpgsqlCommand("SELECT datname FROM pg_database WHERE datistemplate = false;", connection);
            using var reader = getDatabasesCommand.ExecuteReader();
            Console.WriteLine("-------------------------------------");
            while (reader.Read())
            {
                Console.WriteLine($"{reader.GetString(0)}");
            }
            Console.WriteLine("-------------------------------------");
            connection.Close();
        }

        public static void GetTableColumnsFromPostgreSQLDatabase(string databaseName, string tableName)
        {
            string connectionString = $"{Connection.connectionString}Database={databaseName}";
            using var connection = new Npgsql.NpgsqlConnection(connectionString);
            connection.Open();
            using var getColumnsCommand = new Npgsql.NpgsqlCommand(
                "SELECT column_name, data_type FROM information_schema.columns WHERE table_name = @tableName;",
                connection
            );
            getColumnsCommand.Parameters.AddWithValue("tableName", tableName);
            using var reader = getColumnsCommand.ExecuteReader();
            Console.WriteLine("--------------------------------------------------");
            while (reader.Read())
            {
                Console.WriteLine($"{reader.GetString(0),-25} | {reader.GetString(1)}");
            }
            Console.WriteLine("--------------------------------------------------");
            connection.Close();
        }


        public static void SelectTableDataFromPostgreSQLDatabase(string databaseName, string tableName)
        {
            string connectionString = $"{Connection.connectionString}Database={databaseName}";
            using var connection = new Npgsql.NpgsqlConnection(connectionString);
            connection.Open();

            using var getData = new Npgsql.NpgsqlCommand(
                $"SELECT * FROM {tableName};",
                connection
            );
            using var reader = getData.ExecuteReader();
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------");
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write($"{reader.GetName(i),-25} | ");
            }
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------");

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (!reader.IsDBNull(i))
                        Console.Write($"{reader.GetValue(i),-25} | ");
                    else
                        Console.Write($"{"NULL",-25} | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            connection.Close();

        }

        public static void UpdateTableDataFromPostgreSQLDatabase(string databaseName, string tableName)
        {
            string connectionString = $"{Connection.connectionString}Database={databaseName}";
            using var connection = new Npgsql.NpgsqlConnection(connectionString);
            connection.Open();
            GetTableColumnsFromPostgreSQLDatabase(databaseName, tableName);
            Console.WriteLine("Enter the column name you want to update:");
            string columnName = Console.ReadLine();
            Console.WriteLine("Enter the new value:");
            string newValue = Console.ReadLine();
            Console.WriteLine("Enter the condition for the update (e.g., id = 1):");
            string condition = Console.ReadLine();
            using var updateCommand = new Npgsql.NpgsqlCommand(
                $"UPDATE {tableName} SET {columnName} = @newValue WHERE {condition};",
                connection
            );
            updateCommand.Parameters.AddWithValue("newValue", newValue);
            int rowsAffected = updateCommand.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} rows updated.");
            Console.WriteLine();
            connection.Close();
        }

        public static void DeleteTableDataFromPostgreSQLDatabase(string databaseName, string tableName)
        {
            string connectionString = $"{Connection.connectionString}Database={databaseName}";
            using var connection = new Npgsql.NpgsqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Enter the condition for deletion (e.g., id = 1):");
            string condition = Console.ReadLine();
            using var deleteCommand = new Npgsql.NpgsqlCommand(
                $"DELETE FROM {tableName} WHERE {condition};",
                connection
            );
            int rowsAffected = deleteCommand.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} rows deleted.");
            Console.WriteLine();
            connection.Close();
        }

        public static void InsertIntoTableDataPostgreSQLDatabase(string databaseName, string tableName)
        {
            string connectionString = $"{Connection.connectionString}Database={databaseName}";
            using var connection = new Npgsql.NpgsqlConnection(connectionString);
            connection.Open();

            // Get column types
            var columnTypes = new Dictionary<string, string>();
            string sqlGetColumns = @"SELECT column_name, data_type
        FROM information_schema.columns
        WHERE table_schema = 'public' AND table_name = @tableName;";

            using (var cmd = new Npgsql.NpgsqlCommand(sqlGetColumns, connection))
            {
                cmd.Parameters.AddWithValue("tableName", tableName);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string columnName = reader.GetString(0);
                    string dataType = reader.GetString(1);
                    columnTypes[columnName] = dataType;
                }
            }

            GetTableColumnsFromPostgreSQLDatabase(databaseName, tableName);
            Console.WriteLine("Enter the column names (comma-separated):");
            string columns = Console.ReadLine();
            string[] columnArray = columns.Split(',', StringSplitOptions.RemoveEmptyEntries);

            var values = new List<string>();
            var parameters = new List<Npgsql.NpgsqlParameter>();

            for (int i = 0; i < columnArray.Length; i++)
            {
                string column = columnArray[i].Trim();
                Console.Write($"Enter value for '{column}': ");
                string valueInput = Console.ReadLine();

                string paramName = $"@p{i}";
                values.Add(paramName);

                if (!columnTypes.ContainsKey(column))
                {
                    Console.WriteLine($"Column '{column}' not found in DB. Skipping.");
                    continue;
                }

                string dataType = columnTypes[column];

                // Convert automatically based on type
                object finalValue;
                if (dataType == "integer" || dataType == "smallint" || dataType == "bigint")
                {
                    finalValue = int.Parse(valueInput);
                }
                else if (dataType == "numeric" || dataType == "double precision" || dataType == "real" || dataType == "decimal")
                {
                    finalValue = double.Parse(valueInput);
                }
                else if (dataType == "boolean")
                {
                    finalValue = bool.Parse(valueInput);
                }
                else
                {
                    // Default to text
                    finalValue = valueInput;
                }

                parameters.Add(new Npgsql.NpgsqlParameter(paramName, finalValue));
            }

            string columnsJoined = string.Join(", ", columnArray);
            string valuesJoined = string.Join(", ", values);

            string sql = $"INSERT INTO {tableName} ({columnsJoined}) VALUES ({valuesJoined});";
            using var insertCommand = new Npgsql.NpgsqlCommand(sql, connection);

            insertCommand.Parameters.AddRange(parameters.ToArray());

            int rowsAffected = insertCommand.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} rows inserted.");
            Console.WriteLine();
            connection.Close();
        }




        public static void CreateTable(string databaseName, string tableName)
        {
            Console.WriteLine("Enter the column definitions (e.g., id SERIAL PRIMARY KEY, name VARCHAR(100), age INT):");
            string columnDefinitions = Console.ReadLine();
            string connectionString = $"{Connection.connectionString}Database={databaseName}";
            using var connection = new Npgsql.NpgsqlConnection(connectionString);
            connection.Open();
            using var createTableCommand = new Npgsql.NpgsqlCommand(
                $"CREATE TABLE {tableName} ({columnDefinitions});",
                connection
            );
            createTableCommand.ExecuteNonQuery();
            Console.WriteLine($"Table '{tableName}' created successfully.");
            Console.WriteLine();
            connection.Close();
        }

        public static void DropTable(string databaseName, string tableName)
        {
            string connectionString = $"{Connection.connectionString}Database={databaseName}";
            using var connection = new Npgsql.NpgsqlConnection(connectionString);
            connection.Open();
            using var deleteTableCommand = new Npgsql.NpgsqlCommand(
                $"DROP TABLE IF EXISTS {tableName};",
                connection
            );
            deleteTableCommand.ExecuteNonQuery();
            Console.WriteLine($"Table '{tableName}' deleted successfully.");
            Console.WriteLine();
            connection.Close();
        }

        public static void CreateDatabase(string databaseName)
        {
            string connectionString = $"{Connection.connectionString}";
            using var connection = new Npgsql.NpgsqlConnection(connectionString);
            connection.Open();
            using var createDatabaseCommand = new Npgsql.NpgsqlCommand(
                $"CREATE DATABASE {databaseName};",
                connection
            );
            createDatabaseCommand.ExecuteNonQuery();
            Console.WriteLine($"Database '{databaseName}' created successfully.");
            connection.Close();
        }

        public static void DropDatabase(string databaseName)
        {
            string connectionString = $"{Connection.connectionString}";
            using var connection = new Npgsql.NpgsqlConnection(connectionString);
            connection.Open();
            using var deleteDatabaseCommand = new Npgsql.NpgsqlCommand(
                $"DROP DATABASE IF EXISTS {databaseName};",
                connection
            );
            deleteDatabaseCommand.ExecuteNonQuery();
            Console.WriteLine($"Database '{databaseName}' deleted successfully.");
            Console.WriteLine();
            connection.Close();
        }

        public static void ExecuteQuery(string databaseName)
        {
            Console.Clear();
            string connectionString = $"{Connection.connectionString}Database={databaseName}";
            using var connection = new Npgsql.NpgsqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Enter your SQL query:");
            try
            {
                string query = Console.ReadLine();
                using var executeQueryCommand = new Npgsql.NpgsqlCommand(query, connection);

                Console.Clear();

                int rowsAffected = executeQueryCommand.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine($"{rowsAffected} rows affected.");
                }
                else
                {
                    using var reader = executeQueryCommand.ExecuteReader();
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------");
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write($"{reader.GetName(i),-25} | ");
                    }
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------");

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (!reader.IsDBNull(i))
                                Console.Write($"{reader.GetValue(i),-25} | ");
                            else
                                Console.Write($"{"NULL",-25} | ");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
            }
            catch (Npgsql.NpgsqlException ex)
            {
                Console.WriteLine($"Error executing query: {ex.Message}");
                UserInterface.MainMenu();
            }
            finally
            {
                UserInterface.MainMenu();
            }
        }
    }
}

