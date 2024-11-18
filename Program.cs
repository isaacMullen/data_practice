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
            //DeleteData("Employees");
;           
            //CreateDatabase("test");

            CreateEmployeeTable("test.db", "Employees");


            Console.Write("How many rows would you like to enter?: ");
            int howManyEntries = Convert.ToInt32(Console.ReadLine());
            
            for (int i = 0; i < howManyEntries; i++)
            {
                Console.Write("Employee: ");
                string employeeName = Console.ReadLine();

                Console.Write("Department: ");
                string employeeDepartment = Console.ReadLine();

                Console.Write("Salary: ");
                int employeeSalary = Convert.ToInt32(Console.ReadLine());

                
                AddEmployee(employeeName, employeeDepartment, employeeSalary);
                
            }
            //Getting the data from table row by row with all columns present
            RetreiveData();
            //Finding the highest Salary inside the table
            FindHigestSalary("Employees");            
        }
        
        static void FindHigestSalary(string table)
        {
            using (var connection = new SQLiteConnection("Data source=test.db"))
            {
                
                connection.Open();

                string maxSalaryQuery = $"SELECT MAX(Salary) from {table}";
                int highestValue = 0;

                using (var command = new SQLiteCommand(maxSalaryQuery, connection))
                {
                    object result = command.ExecuteScalar();
                    
                    if(result != DBNull.Value)
                    {
                        highestValue = Convert.ToInt32(result);
                    }
                }

                string selectQuery = $"SELECT id, Name, Department, Salary FROM {table} WHERE Salary = @Salary";

                using (var command = new SQLiteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Salary", highestValue);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string department = reader.GetString(2);
                            int salary = reader.GetInt32(3);

                            Console.WriteLine($"Higest Paid Employee: | Id: {id}, Name: {name}, Department: {department}, Salary: {salary}");
                            Console.ReadKey();
                        }
                    }
                }

                
            }
        }
        static void CreateDatabase(string fileName)
        {
            string databaseFile = fileName + ".db";

            SQLiteConnection.CreateFile(databaseFile);

            Console.WriteLine("Database created!");
            Console.ReadKey();
        }
        static void CreateEmployeeTable(string dataBase, string tableName)
        {
            using (var connection = new SQLiteConnection("Data source=" + dataBase))
            {
                connection.Open();

                string createTableQuery = $"CREATE TABLE IF NOT EXISTS {tableName} (id INTEGER PRIMARY KEY, Name TEXT, Department TEXT, Salary INTEGER)";

                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                Console.WriteLine($"Table: '{tableName}' is ready.");
                
            }
        }
        static void AddEmployee(string name, string department, int salary)
        {
            using (var connection = new SQLiteConnection("Data Source=test.db"))
            {
                connection.Open();

                string insetQuery = $"INSERT INTO Employees (Name, Department, Salary) VALUES ('{name}', '{department}', '{salary}')";

                using (var command = new SQLiteCommand(insetQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                Console.WriteLine($"Data ({name}, {department}, {salary}) Entered");
                Console.ReadKey();
            }
        }
        static void CreateDepartmentTable(string dataBase, string tableName, string name)
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
        static void RetreiveData()
        {
            using (var connection = new SQLiteConnection("Data Source=test.db"))
            {
                connection.Open();

                // Retrieve data from the Users table
                string selectQuery = "SELECT Id, Name, Department, Salary FROM Employees";
                using (var command = new SQLiteCommand(selectQuery, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string department = reader.GetString(2);
                        int salary = reader.GetInt32(3);

                        Console.WriteLine($"ID: {id}, Name: {name}, Department: {department}, Salary: {salary}");
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

