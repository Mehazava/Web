using System;
using System.Collections.Generic;

namespace DiskLib
{
    public class DiskInfo : InfoItem
    {
        public DiskInfo(int authorID, List<int> genreID, int publisherID, string name, 
            DateTime releaseDate) : base()
        {
            Type = ItemType.disk;
            AuthorID = authorID;
            PublisherID = publisherID;
            GenreID = genreID;
            Name = name;
            ReleaseDate = releaseDate;
        }
        public DiskInfo(string all) : base()
        {
            try
            {
                string[] wow = all.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (wow.Length > 4)
                {
                    Type = ItemType.disk;
                    Name = wow[0];
                    AuthorID = Convert.ToInt32(wow[1]);
                    PublisherID = Convert.ToInt32(wow[2]);
                    ReleaseDate = DateTime.Parse(wow[3]);
                    GenreID = new List<int>();
                    for (int owo = 4; owo < wow.Length; ++owo)
                    {
                        if (GenreID.Contains(Convert.ToInt32(wow[owo])))
                        {
                            throw new Exception("Genres are repeating.");
                        }
                        GenreID.Add(Convert.ToInt32(wow[owo]));
                    }
                }
                else
                {
                    throw new Exception("Wrong amount of arguments.");
                }
            }
            finally { }
        }
        public DiskInfo(DiskInfo from)
        {
            Type = ItemType.disk;
            AuthorID = from.AuthorID;
            PublisherID = from.PublisherID;
            GenreID = new List<int>(from.GenreID);
            Name = from.Name;
            ReleaseDate = from.ReleaseDate;
            Id = from.Id;
        }
        public List<int> GenreID;
        public int AuthorID { get; private set; }
        public int PublisherID { get; private set; }
        public string Name { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public override string GetInfo()
        {
            return ($"{Id}: '{Name}' by {AuthorID}, published by {PublisherID} {ReleaseDate:d};\n" +//add .padright
                $"Genres: ({String.Concat(GenreID)})");
        }
        public override string GetName()
        {
            return Name;
        }
        protected internal override void ChangeField(int Field, string value)
        {
            switch (Field){
                case 1:
                    if (String.IsNullOrWhiteSpace(value))
                    {
                        throw new Exception("Bad name.");
                    }
                    else
                    {
                        Name = value;
                    }
                    break;
                case 2:
                    try
                    {
                        AuthorID = Int32.Parse(value);
                    }
                    finally { }
                    break;
                case 3:
                    try
                    {
                        PublisherID = Int32.Parse(value);
                    }
                    finally { }
                    break;
                case 4:
                    try
                    {
                        ReleaseDate = DateTime.Parse(value);
                    }
                    finally { }
                    break;
                case 5:
                    try
                    {
                        int tempInt = Int32.Parse(value);
                        if (tempInt == -1)
                        {
                            GenreID.Clear();
                        }
                        else
                        {
                            if (tempInt < 1)
                            {
                                throw new Exception("Bad id");
                            }
                            if (GenreID.TrueForAll(UwU => UwU != tempInt))
                            {
                                GenreID.Add(tempInt);
                            }
                            else
                            {
                                throw new Exception("This id already present.");
                            }
                        }
                    }
                    finally { }
                    break;
                default:
                    throw new Exception("No field goes by this number.");
            }
        }
    }
}
