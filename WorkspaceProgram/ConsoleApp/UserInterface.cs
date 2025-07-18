using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class UserInterface
    {
        public static void MainMenu()
        {
            Console.WriteLine("1.Database Management");
            Console.WriteLine("2.Table Management");
            Console.WriteLine("3.Execute Own Query");
            Console.WriteLine("4.Exit");
            string choice = Console.ReadLine();
            Console.Clear();
            Console.Clear();
            switch (choice)
            {
                case "1":
                    DataBaseManagementMenu();
                    break;
                case "2":
                    TableManagementMenu();
                    break;
                case "3":
                    DBManipulation.GetDatabasesFromPostgreSQLServer();
                    Console.WriteLine("Enter the database name:");
                    string dbName = Console.ReadLine();
                    DBManipulation.ExecuteQuery(dbName);
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    MainMenu();
                    break;
            }
        }

        public static void DataBaseManagementMenu()
        {
            Console.WriteLine("1. Create Database");
            Console.WriteLine("2. Drop Database");
            Console.WriteLine("3. Show Database Info");
            Console.WriteLine("4. Back to Main Menu");
            string choice = Console.ReadLine();
            Console.Clear();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter the name of the database to create:");
                    string dbName = Console.ReadLine();
                    DBManipulation.CreateDatabase(dbName);
                    DataBaseManagementMenu();
                    break;
                case "2":
                    Console.WriteLine("Available databases:");
                    DBManipulation.GetDatabasesFromPostgreSQLServer();
                    Console.WriteLine("Enter the name of the database to drop:");
                    string dbToDrop = Console.ReadLine();
                    DBManipulation.DropDatabase(dbToDrop);
                    DataBaseManagementMenu();
                    break;
                case "3":
                    Console.WriteLine("Available databases:");
                    DBManipulation.GetDatabasesFromPostgreSQLServer();
                    Console.WriteLine("Enter the name of the database to show info:");
                    string dbToShow = Console.ReadLine();
                    DBManipulation.GetDatabaseInfoFromPostgreSQLServer(dbToShow);
                    DataBaseManagementMenu();
                    break;
                case "4":
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    DataBaseManagementMenu();
                    break;
            }
        }

        public static void TableManagementMenu()
        {
            Console.WriteLine("1. Create Table");
            Console.WriteLine("2. Drop Table");
            Console.WriteLine("3. Select Table Data");
            Console.WriteLine("4. Update Table Data");
            Console.WriteLine("5. Delete Table Data");
            Console.WriteLine("6. Insert Into Table Data");
            Console.WriteLine("7. Back to Main Menu");
            string choice = Console.ReadLine();
            Console.Clear();
            switch (choice)
            {
                case "1":
                RetryTableManageOne:
                    try
                    {
                        DBManipulation.GetDatabasesFromPostgreSQLServer();
                        Console.WriteLine("Enter the name of the database where you want to create a table:");
                        string dbNameToCreate = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Enter the name of the table to create:");
                        string tableNameToCreate = Console.ReadLine();
                        Console.Clear();
                        DBManipulation.CreateTable(dbNameToCreate, tableNameToCreate);
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        goto RetryTableManageOne;
                    }
                case "2":
                RetryTableManageTwo:
                    try
                    {
                        DBManipulation.GetDatabasesFromPostgreSQLServer();
                        Console.WriteLine("Enter the name of the database where you want to drop a table:");
                        string dbNameToDrop = Console.ReadLine();
                        Console.Clear();
                        DBManipulation.GetDatabaseTablesFromPostgreSQLServer(dbNameToDrop);
                        Console.WriteLine("Enter the name of the table to drop:");
                        string tableToDrop = Console.ReadLine();
                        Console.Clear();
                        DBManipulation.DropTable(dbNameToDrop, tableToDrop);
                        TableManagementMenu();
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        goto RetryTableManageTwo;
                    }
                case "3":
                RetryTableManageThree:
                    try
                    {
                        DBManipulation.GetDatabasesFromPostgreSQLServer();
                        Console.WriteLine("Enter the name of the database where you want to select table data:");
                        string dbNameToSelect = Console.ReadLine();
                        Console.Clear();
                        DBManipulation.GetDatabaseTablesFromPostgreSQLServer(dbNameToSelect);
                        Console.WriteLine("Enter the name of the table to select data from:");
                        string tableToSelect = Console.ReadLine();
                        Console.Clear();
                        DBManipulation.SelectTableDataFromPostgreSQLDatabase(dbNameToSelect, tableToSelect);
                        TableManagementMenu();
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        goto RetryTableManageThree;
                    }
                case "4":
                RetryTableManageFour:
                    try
                    {
                        DBManipulation.GetDatabasesFromPostgreSQLServer();
                        Console.WriteLine("Enter the name of the database where you want to update table data:");
                        string dbNameToUpdate = Console.ReadLine();
                        Console.Clear();
                        DBManipulation.GetDatabaseTablesFromPostgreSQLServer(dbNameToUpdate);
                        Console.WriteLine("Enter the name of the table to update data in:");
                        string tableToUpdate = Console.ReadLine();
                        Console.Clear();
                        DBManipulation.UpdateTableDataFromPostgreSQLDatabase(dbNameToUpdate, tableToUpdate);
                        TableManagementMenu();
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        goto RetryTableManageFour;
                    }
                case "5":
                RetryTableManageFive:
                    try
                    {
                        DBManipulation.GetDatabasesFromPostgreSQLServer();
                        Console.WriteLine("Enter the name of the database where you want to delete table data:");
                        string dbNameToDelete = Console.ReadLine();
                        Console.Clear();
                        DBManipulation.GetDatabaseTablesFromPostgreSQLServer(dbNameToDelete);
                        Console.WriteLine("Enter the name of the table to delete data from:");
                        string tableToDelete = Console.ReadLine();
                        Console.Clear();
                        DBManipulation.DeleteTableDataFromPostgreSQLDatabase(dbNameToDelete, tableToDelete);
                        TableManagementMenu();
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        goto RetryTableManageFive;
                    }
                case "6":
                RetryTableManageSix:
                    try
                    {
                        DBManipulation.GetDatabasesFromPostgreSQLServer();
                        Console.WriteLine("Enter the name of the database where you want to insert into table data:");
                        string dbNameToInsert = Console.ReadLine();
                        Console.Clear();
                        DBManipulation.GetDatabaseTablesFromPostgreSQLServer(dbNameToInsert);
                        Console.WriteLine("Enter the name of the table to insert data into:");
                        string tableToUpdateInsert = Console.ReadLine();
                        Console.Clear();
                        DBManipulation.InsertIntoTableDataPostgreSQLDatabase(dbNameToInsert, tableToUpdateInsert);
                        TableManagementMenu();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        goto RetryTableManageSix;
                    }
                    break;
                case "7":
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    TableManagementMenu();
                    break;
            }
        }
    }
}
