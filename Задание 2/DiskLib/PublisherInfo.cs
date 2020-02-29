using System;
using System.Collections.Generic;

namespace DiskLib
{
    public class PublisherInfo : InfoItem
    {
        public PublisherInfo(string name) : base()
        {
            if (name.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length != 1)
            {
                throw new Exception("Wrong amount of arguments.");
            }
            Type = ItemType.publisher;
            Name = name;
        }
        public PublisherInfo(PublisherInfo from)
        {
            Type = ItemType.publisher;
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
