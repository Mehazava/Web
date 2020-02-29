using System;
using System.Collections.Generic;

namespace DiskLib
{
    public class GenreInfo : InfoItem
    {
        public GenreInfo(string name) : base()
        {
            if (name.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length != 1)
            {
                throw new Exception("Wrong amount of arguments.");
            }
            Type = ItemType.genre;
            Name = name;
        }
        public GenreInfo(GenreInfo from)
        {
            Type = ItemType.genre;
            Name = from.Name;
            Id = from.Id;
        }
        public string Name { get; private set; }
        public override string GetInfo()
        {
            return ($"{Id}: '{Name}'");
        }
        public override string GetName()
        {
            return Name;
        }
        protected internal override void ChangeField(int Field, string value)
        {
            switch (Field)
            {
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
                default:
                    throw new Exception("No field goes by this number.");
            }
        }
    }
}
