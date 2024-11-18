using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.ComponentModel.Design;
using System.Xml.Linq;

namespace data_practice
{
    internal class Program
    {
        static void Main()
        {
            DeleteData("Users");
;           
            CreateDatabase("test");
            
            for(int i =0; i < 3; i++)
            {
                Console.WriteLine("Employee: ")
                string employee = Console.ReadLine();

                Console.WriteLine("Department: ");
                string department = Console.ReadLine();
            }
            
            //Reduce these functions. Handle data adding in a seperate function..
            CreateDepartmentTable("test.db", "Departments");
            CreateEmployeeTable("test.db","Employees");
            


            

            //Getting all entries in the users table with the age above 25
            
        }
        static void FindOlderThan(int minAge, string table)
        {
            Console.Write($"Enter a minimum age: ");           
            
            using (var connection = new SQLiteConnection("Data Source=test.db"))
            {
                connection.Open();

                string selectQuery = $"SELECT id, Name, Age FROM {table} WHERE Age > {minAge}";

                using (var command = new SQLiteCommand(selectQuery, connection))
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine();
                    Console.WriteLine($"Users older than {minAge}");

                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        int age = reader.GetInt32(2);

                        Console.WriteLine($"Id: {id} | Age: {age} | Name: {name} | ");
                    }
                }
            }
            Console.ReadKey();
        }
        static void CreateDatabase(string fileName)
        {
            string databaseFile = fileName + ".db";

            SQLiteConnection.CreateFile(databaseFile);

            Console.WriteLine("Database created!");
            Console.ReadKey();
        }
        static void CreateEmployeeTable(string dataBase, string tableName, string name, string department, int salary)
        {
            using (var connection = new SQLiteConnection("Data source=" + dataBase))
            {
                connection.Open();

                string createTableQuery = $"CREATE TABLE IF NOT EXISTS {tableName} (id INTEGER PRIMARY KEY, {name} TEXT, {department} TEXT, INTEGER {salary})";

                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Table Created");
                Console.ReadKey();
            }
        }
        static void CreateDepartmentTable(string datastring tableName, string name)
        {
            using (var connection = new SQLiteConnection("Data source=" + dataBase))
            {
                connection.Open();

                string createTableQuery = $"CREATE TABLE IF NOT EXISTS {tableName} (id INTEGER PRIMARY KEY, {name} TEXT)";

                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Table Created");
                Console.ReadKey();
            }
        }
        static void DeleteData(string table)
        {
            using (var connection = new SQLiteConnection("Data Source=test.db"))
            {
                connection.Open();

                // SQL command to delete all rows from the table
                string deleteQuery = $"DELETE FROM {table}";

                using (var command = new SQLiteCommand(deleteQuery, connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} rows deleted from {table}");
                }
            }
            Console.ReadKey();
        }
        static void RetrieveNameAndAge()
        {
            using (var connection = new SQLiteConnection("Data Source=test.db"))
            {
                connection.Open();

                // Retrieve data from the Users table
                string selectQuery = "SELECT Id, Name, Age FROM Users";
                using (var command = new SQLiteCommand(selectQuery, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        int age = reader.GetInt32(2);

                        Console.WriteLine($"ID: {id}, Name: {name}, Age: {age}");
                        Console.ReadKey();
                    }
                }
            }
        }
        static void AddData(string name, int age)
        {
            using (var connection = new SQLiteConnection("Data Source=test.db"))
            {
                connection.Open();


                string insetQuery = $"INSERT INTO Users (Name, Age) VALUES ('{name}', {age})";

                using (var command = new SQLiteCommand(insetQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("Data Entered");
                Console.ReadKey();
            }
        }
    }
}

