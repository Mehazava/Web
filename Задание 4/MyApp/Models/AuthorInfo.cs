using System;
using System.Collections.Generic;

namespace DiskLib
{
    public class AuthorInfo : InfoItem
    {
        public AuthorInfo(string sName, string fName, DateTime birthDate) : base()
        {
            Type = ItemType.author;
            FName = fName;
            SName = sName;
            BirthDate = birthDate;
        }
        public AuthorInfo(string sName, string fName, DateTime birthDate, int id)
        {
            Type = ItemType.author;
            FName = fName;
            SName = sName;
            BirthDate = birthDate;
            Id = id;
        }
        public AuthorInfo(string all) : base()
        {
            try
            {
                string[] wow = all.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (wow.Length == 3)
                {
                    Type = ItemType.author;
                    FName = wow[0];
                    SName = wow[1];
                    BirthDate = DateTime.Parse(wow[2]);
                }
                else
                {
                    throw new Exception("Wrong amount of arguments.");
                }
            }
            finally { }
        }
        public AuthorInfo(AuthorInfo from)
        {
            Type = ItemType.author;
            FName = from.FName;
            SName = from.SName;
            BirthDate = from.BirthDate;
            Id = from.Id;
        }
        public string FName { get; private set; }
        public string SName { get; private set; }
        public DateTime BirthDate { get; private set; }
        public override string GetInfo()
        {
            return ($"{Id}: {FName} {SName}, born {BirthDate:d}");
        }
        public override string GetName()
        {
            return $"{FName} {SName}";
        }
        protected internal override void ChangeField(int Field, string value)
        {
            switch (Field)
            {
                case 1:
                    if (String.IsNullOrWhiteSpace(value))
                    {
                        throw new Exception("Bad first name.");
                    }
                    else
                    {
                        FName = value;
                    }
                    break;
                case 2:
                    if (String.IsNullOrWhiteSpace(value))
                    {
                        throw new Exception("Bad second name.");
                    }
                    else
                    {
                        SName = value;
                    }
                    break;
                case 3:
                    try
                    {
                        BirthDate = DateTime.Parse(value);
                    }
                    finally { }
                    break;
                default:
                    throw new Exception("No field goes by this number.");
            }
        }
    }
}
