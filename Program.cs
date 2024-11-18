using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.ComponentModel.Design;
using System.Xml.Linq;
using System.IO;

namespace data_practice
{
    internal class Program
    {
        
        
        static void Main()
        {
        var employees = new (string Name, string Department, int Salary)[]
        {
            ("Benjamin Forbes", "Toys", 29549),
            ("Jason Ward", "Clothing", 21541),
            ("Matthew Andrews", "Clothing", 22520),
            ("Allen Hernandez", "Clothing", 26736),
            ("Mark Nunez", "Toys", 22525),
            ("Caleb Torres", "Clothing", 24117),
            ("Elizabeth Khan", "Technology", 29918),
            ("John Melton", "Sports", 24069),
            ("Chelsea Shaw", "Technology", 26113),
            ("Rebecca Smith", "Toys", 25088),
            ("Jenna Maldonado", "Toys", 24916),
            ("Kristine Hernandez", "Clothing", 28461),
            ("Allison Shannon", "Sports", 26730),
            ("Willie Rose", "Technology", 23819),
            ("Robert Snyder", "Technology", 21051),
            ("Christina Williams", "Clothing", 26152),
            ("Mark Park", "Clothing", 20024),
            ("Barry Kelly", "Sports", 25420),
            ("Christine Martinez", "Toys", 21040),
            ("Angela Smith", "Clothing", 25810),
            ("Makayla Pineda", "Technology", 25788),
            ("Michael Reyes", "Clothing", 20506),
            ("Chad Chapman", "Technology", 20851),
            ("Wanda Boone", "Clothing", 24229),
            ("Chelsey Brown", "Technology", 28402),
            ("Hannah Cabrera", "Sports", 20368),
            ("Darren Rodriguez", "Clothing", 26540),
            ("Wanda Simmons", "Toys", 23150),
            ("Jennifer Marshall", "Technology", 20720),
            ("Paula Miles", "Clothing", 23925),
            ("Brian Moyer", "Sports", 28295),
            ("Jason Huff", "Toys", 27282),
            ("Morgan Rivera", "Technology", 20413),
            ("Kyle Smith", "Technology", 28185),
            ("Joseph Nunez", "Sports", 29650),
            ("Kim Duran", "Technology", 29531),
            ("Michael Smith", "Sports", 26546),
            ("Brandon Cross", "Toys", 28106),
            ("Amanda Buck", "Toys", 24543),
            ("Natalie Hill", "Sports", 20036),
            ("Jason Pham", "Technology", 21403),
            ("Terri Drake", "Technology", 21030),
            ("Taylor Davis", "Sports", 27563),
            ("David Garcia", "Toys", 24550),
            ("Jared Gardner", "Technology", 27028),
            ("Hayden Fernandez", "Sports", 23140),
            ("Nathan Duncan", "Technology", 21137),
            ("Regina Mcgee", "Clothing", 22540),
            ("Christopher Logan", "Sports", 24691),
            ("Daisy Watson", "Clothing", 26976),
            ("Melanie Blake", "Toys", 24720),
            ("Vincent Newman", "Technology", 27072),
            ("Christy Vazquez", "Toys", 29867),
            ("Christopher Mendoza", "Sports", 27844),
            ("Joseph Robbins", "Clothing", 25201),
            ("Michael Fry", "Clothing", 28470),
            ("Richard Hamilton", "Clothing", 23055),
            ("Kyle Johnson", "Clothing", 29558),
            ("Katie Cole", "Technology", 29167),
            ("Linda Boyd", "Sports", 29225),
            ("Cynthia Smith", "Toys", 20516),
            ("Eric Garcia", "Toys", 20032),
            ("Javier Wilson", "Sports", 26805),
            ("Justin Hull", "Technology", 21352),
            ("Toni Williams", "Technology", 28787),
            ("Kathleen Lee", "Technology", 29955),
            ("Steven Hernandez", "Technology", 22883),
            ("Angela Hernandez", "Clothing", 25130),
            ("John Graves", "Toys", 21329),
            ("Theresa Smith", "Clothing", 25652),
            ("Mary Carter", "Sports", 29861),
            ("Diane Bryant", "Toys", 22135),
            ("Shane Landry", "Toys", 23019),
            ("Kathryn Davenport", "Technology", 24325),
            ("Yesenia Hensley", "Clothing", 27818),
            ("Tina Hansen", "Toys", 26815),
            ("Lisa Cummings", "Toys", 26684),
            ("Tracey Collins", "Toys", 21327),
            ("Amanda Thomas", "Sports", 23536),
            ("Ryan Powell", "Toys", 28728),
            ("Jennifer Santos", "Technology", 27469),
            ("Todd Lewis", "Clothing", 21384),
            ("Melissa Hall", "Toys", 20105),
            ("Craig Jones", "Toys", 28338),
            ("Miranda Kerr", "Clothing", 22971),
            ("Robert Atkins", "Clothing", 27113),
            ("John Patterson", "Clothing", 20954),
            ("Ethan Martinez", "Technology", 22352),
            ("Barry Mercer", "Clothing", 29333),
            ("Jennifer Sanchez", "Sports", 29780),
            ("Abigail Newton", "Technology", 26730),
            ("Miss Elizabeth Jones", "Toys", 25306),
            ("Zachary Rodriguez", "Technology", 26635),
            ("Madison Price", "Clothing", 21454),
            ("Jeffery Miller", "Clothing", 24635),
            ("Tonya Rodriguez", "Sports", 27303),
            ("Matthew Moyer", "Clothing", 27478),
            ("Jessica Parker", "Technology", 26541),
            ("Brianna Thomas", "Toys", 29736)
        };

            DeleteData("Employees");

            CreateDatabase("test");

            CreateEmployeeTable("Employees");

            foreach (var employee in employees)
            {
                AddEmployee(employee.Name, employee.Department, employee.Salary);
            }
            Console.WriteLine();
            Console.ReadKey();

            //Getting the data from table row by row with all columns present
            //RetreiveData();
            FindByDepartment("Clothing");
            FindByDepartment("Toys");
            FindByDepartment("Sports");
            FindByDepartment("Technology");
           
            //Finding the highest Salary inside the table            
            FindHigestSalary("Employees");

            SortBySalary();
            FindAverageSalaryByDepartment();
        }
        
        static void FindAverageSalaryByDepartment()
        {
            using (var connection = new SQLiteConnection("Data source=Database/test.db"))
            {
                connection.Open();

                string selectQuery = $"SELECT Department, AVG(Salary) AS averageSalary FROM Employees GROUP BY Department";

                using (var command = new SQLiteCommand(selectQuery, connection))
                {
                    Console.WriteLine("AVERAGE SALARY BY DEPARTMENT\n");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string department = reader.GetString(0);
                            double averageSalary = reader.GetDouble(1);

                            Console.WriteLine($"Department: {department} | Average Salary: {averageSalary}");
                        }
                        Console.ReadKey();
                    }                    
                }
            }
        }
        
        static void SortBySalary()
        {
            using (var connection = new SQLiteConnection("Data source=Database/test.db"))
            {
                connection.Open();

                string selectQuery = $"SELECT * FROM Employees ORDER BY Salary ASC";

                using (var command = new SQLiteCommand(selectQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine("SORTED BY SALARY");
                        while(reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string departmentName = reader.GetString(2);
                            int salary = reader.GetInt32(3);


                            Console.WriteLine($"Id: {id}, Name: {name}, Department: {departmentName}, Salary: {salary}");
                        }
                        Console.WriteLine();
                        Console.ReadKey();
                    }
                }
            }
        }
        
        static void FindByDepartment(string department)
        {
            using (var connection = new SQLiteConnection("Data source=Database/test.db"))
            {
                connection.Open();

                string selectQuery = $"SELECT * FROM Employees WHERE Department = @Department";

                using (var command = new SQLiteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Department", department);                   

                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine($"All Employees in the '{department}' department.");
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string departmentName = reader.GetString(2);
                            int salary = reader.GetInt32(3);


                            Console.WriteLine($"Id: {id}, Name: {name}, Department: {departmentName}, Salary: {salary}");
                        }
                        Console.WriteLine();
                        Console.ReadKey();
                    }
                }                                    
            }
        }
        
        static void FindHigestSalary(string table)
        {
            using (var connection = new SQLiteConnection("Data source=Database/test.db"))
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

                            Console.WriteLine($"Higest Paid Employee => Id: {id}, Name: {name}, Department: {department}, Salary: {salary}\n");                            
                            Console.ReadKey();
                        }
                    }
                }

                
            }
        }
        static void CreateDatabase(string fileName)
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string databaseFolder = Path.Combine(currentDirectory, "Database");

            // Define the 'Database' folder relative to the project root.
            //string databaseFolder = Path.Combine(projectRoot, "Database");

            if (!Directory.Exists(databaseFolder))
            {
                Directory.CreateDirectory(databaseFolder);
            }

            string databaseFile = Path.Combine(databaseFolder, fileName + ".db");

            if(File.Exists(databaseFile))
            {
                Console.WriteLine($"Database already exists at {databaseFile}\n");
                return;
            }
            
            SQLiteConnection.CreateFile(databaseFile);

            Console.WriteLine($"Database created at {databaseFile}!\n");
            Console.ReadKey();
        }
        static void CreateEmployeeTable(string tableName)
        {           
            using (var connection = new SQLiteConnection($"Data source=Database/test.db")) 
            {
                connection.Open();

                string checkTableQuery = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}'\n";

                using (var checkCommand = new SQLiteCommand(checkTableQuery, connection))
                {
                    var result = checkCommand.ExecuteScalar();

                    if(result != null)
                    {
                        Console.WriteLine($"Table '{tableName}' already exists.\n");
                        return;
                    }
                }

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
            using (var connection = new SQLiteConnection("Data Source=Database/test.db"))
            {
                connection.Open();

                string insetQuery = $"INSERT INTO Employees (Name, Department, Salary) VALUES ('{name}', '{department}', '{salary}')";

                using (var command = new SQLiteCommand(insetQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                Console.WriteLine($"Data Entered: ({name}, {department}, {salary})");
                
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
            using (var connection = new SQLiteConnection("Data Source=Database/test.db"))
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
            using (var connection = new SQLiteConnection("Data Source=Database/test.db"))
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
                    }
                    Console.ReadKey();
                }
            }
        }
        static void AddData(string name, int age)
        {
            using (var connection = new SQLiteConnection("Data Source=Database/test.db"))
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

