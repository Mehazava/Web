using System;

namespace BasedOnDb
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
//"data source=(localdb)\MSSQLLocalDB;Initial Catalog=MusicDB;Integrated Security=True;"  --------- original database connection string
//Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=MusicDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer --- package console command
