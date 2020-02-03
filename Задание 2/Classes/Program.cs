using System;
using System.Collections;
using System.Collections.Generic;

namespace Classes
{
    class Author
    {
        public Author(string fname, string sname, int id, DateTime birth)
        {
            FName = fname;
            SName = sname;
            ID = id;
            Birth = birth;
        }
        public string GetInfo()
        {
            return ($"Author[{ID}]: {FName} {SName}, DoB: {Birth:d}");
        }
        public int ID {get; private set;}
        public string FName { get; set; }
        public string SName { get; set; }
        public DateTime Birth { get; set; }
    }
    class Publisher
    {
        public Publisher(int id, string name)
        {
            ID = id;
            Name = name;
        }
        public string GetInfo()
        {
            return ($"Publisher[{ID}]: {Name}");
        }
        public int ID { get; set; }
        public string Name { get; set; }
    }
    class Genre
    {
        public Genre(int id, string name)
        {
            ID = id;
            Name = name;
        }
        public string GetInfo()
        {
            return ($"Genre[{ID}]: {Name}");
        }
        public int ID { get; set; }
        public string Name { get; set; }
    }
    class Disk
    {
        public Disk()
        {

        }
        public string GetInfo()
        {
            string result = $"Disk[{ID}]: {Name} by {AuthorID} published by {PublisherID} {Release:d}\nGenres:";
            for (int i = 0; i < GenreID.Count; ++i)
            {
                result += $" {GenreID[i]}";
            }
            return result;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Release { get; set; }
        public int AuthorID { get; set; }
        public int PublisherID { get; set; }
        public ArrayList GenreID { get; set; }
    }
    class GenericManager<T>
    {
        protected int LastID = 0;
        protected Dictionary<int, T> EList;
        public void Add(int id, T elem)
        {
            EList.Add(id, elem);
        }
        public void Delete(int id)
        {
            EList.Remove(id);
        }
        public Dictionary<int, T>.ValueCollection GetAll()
        {
            return EList.Values;
        }
        public T GetByID(int id)
        {
            return EList[id];
        }
    }
    class AuthorManager : GenericManager<Author>
    {
        public void AddNew()
        {
            string stemp;
            string[] vs;
            bool succ = false;
            DateTime Birth;
            Console.Write("Enter author's name: ");
            do
            {
                try
                {
                    stemp = Console.ReadLine();
                    vs = stemp.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message} Try again.");
                    succ = false;
                    vs = null;
                }
                if (succ && (vs.Length == 2))
                {
                    Console.WriteLine("Ur failed to give a proper name. Try again.");
                }
            } while (!succ);
            Console.Write("Enter author's birth day (day/month/year): ");
            do
            {
                try
                {
                    Birth = DateTime.Parse(Console.ReadLine());
                    succ = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message} Ur failed.");
                    Birth = DateTime.MinValue;
                    succ = false;
                }
            } while (!succ);
            try
            {
                LastID++;
                Add(LastID, new Author(vs[0], vs[1], LastID, Birth));
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message} Failed to add element.");
            }
            Console.WriteLine($"Author's name registered with id {1}.", LastID);
        }
        public void Edit(int id)
        {
            Author author;
            bool succ = false;
            string sinput;
            int choice = -1;
            try
            {
                author = EList[id];
                Console.WriteLine(author.GetInfo());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            Console.WriteLine("What do you want to change? (0:Cancel, 1: FName, 2:SName, 3:DoB)");
            do
            {
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch(Exception e)
                {
                    Console.WriteLine($"{e.Message} Try again.");
                }
                if ((choice < 4) && (choice >= 0))
                {
                    succ = true;
                }
                else
                {
                    Console.WriteLine("Not an option. Try again.");
                }
            } while (!succ);
            if(choice == 0)
            {
                return;
            }
            Console.WriteLine("New value:");
            do
            {
                sinput = Console.ReadLine();
            } while (!succ);
            Console.ReadLine();
        }
    }
    class GoodsInfo
    {
        private AuthorManager AuI;
        private GenericManager<Genre> GeI;
        private GenericManager<Publisher> PuI;
        private GenericManager<Disk> DiI;
    }
    class Shop
    {
        public void AddDisk(Disk disk)
        {

        }
        public void EditDisk()
        {

        }
        public void SellDisk()
        {

        }
        public void ListDisk()
        {

        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
