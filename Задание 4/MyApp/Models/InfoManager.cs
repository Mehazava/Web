using System;
using System.Collections.Generic;

namespace DiskLib
{
    public class InfoManager
    {
        public InfoManager()
        {
            items = new Dictionary<ItemType, List<InfoItem>>();
            items.Add(ItemType.author, new List<InfoItem>());
            items.Add(ItemType.disk, new List<InfoItem>());
            items.Add(ItemType.genre, new List<InfoItem>());
            items.Add(ItemType.publisher, new List<InfoItem>());
        }
        public Dictionary<ItemType, List<InfoItem>> items { get; private set; }
        public void Add(InfoItem item, ItemInfoHandler createHandler = null, ItemInfoHandler modifyHandler = null, ItemInfoHandler destroyHandler = null)
        {
            string result = CheckItem(item);
            if (result == null){
                item.Created += createHandler;
                item.Modified += modifyHandler;
                item.Deleted += destroyHandler;
                item.Create();
                items[item.Type].Add(item);
            }
            else
            {
                throw new Exception(result);
            }
        }
        public void Remove(ItemType type, int id)
        {
            int OwO = GetIndexByID(type, id);
            if (OwO == -1)
            {
                throw new Exception("No element with this id.");
            }
            else
            {
                items[type][OwO].Delete();
                items[type].RemoveAt(OwO);
            }
        }
        public void Modify(ItemType type, int id, int field, string data)
        {
            InfoItem Modded = null;
            switch (type)
            {
                case ItemType.author:
                    Modded = new AuthorInfo(items[type][id] as AuthorInfo);
                    break;
                case ItemType.genre:
                    Modded = new GenreInfo(items[type][id] as GenreInfo);
                    break;
                case ItemType.publisher:
                    Modded = new PublisherInfo(items[type][id] as PublisherInfo);
                    break;
                case ItemType.disk:
                    Modded = new DiskInfo(items[type][id] as DiskInfo);
                    break;
            }
            if (Modded == null)
            {
                throw new Exception("Some problem occured.");
            }
            Modded.ChangeField(field, data);
            if (CheckItem(Modded) == null)
            {
                items[type].RemoveAt(id);
                items[type].Add(Modded);
                Modded.Modify();
            }
            else
            {
                throw new Exception("Invalid data.");
            }
        }
        public InfoItem GetByID(ItemType type, int id)
        {
            return items[type].Find(elem => elem.Id == id);
        }
        public int GetIndexByID(ItemType type, int id)
        {
            return items[type].FindIndex(elem => elem.Id == id);
        }
        public string CheckItem(InfoItem item)
        {
            switch (item.Type)
            {
                case ItemType.author:
                    AuthorInfo tempA = item as AuthorInfo;
                    if (tempA == null)
                    {
                        return "?";
                    }
                    if (String.IsNullOrWhiteSpace(tempA.FName))
                    {
                        return "Bad first name.";
                    }
                    if (String.IsNullOrWhiteSpace(tempA.SName))
                    {
                        return "Bad second name.";
                    }
                    if (tempA.BirthDate == null)
                    {
                        return "Bad birth date.";
                    }
                    if (DateTime.Today < tempA.BirthDate)
                    {
                        return "Author's birth day can't be in future.";
                    }
                    return null;
                case ItemType.genre:
                    GenreInfo tempG = item as GenreInfo;
                    if (tempG == null)
                    {
                        return "?";
                    }
                    if (String.IsNullOrWhiteSpace(tempG.Name))
                    {
                        return "Bad name.";
                    }
                    if (items[ItemType.genre].TrueForAll(Hah => ((GenreInfo)Hah).Name != tempG.Name))
                    {
                        return null;
                    }
                    return "Such genre already exists.";
                case ItemType.publisher:
                    PublisherInfo tempP = item as PublisherInfo;
                    if (tempP == null)
                    {
                        return "?";
                    }
                    if (String.IsNullOrWhiteSpace(tempP.Name))
                    {
                        return "Bad name.";
                    }
                    if (items[ItemType.publisher].TrueForAll(Hah => ((PublisherInfo)Hah).Name != tempP.Name))
                    {
                        return null;
                    }
                    return "Such publisher already exists.";
                case ItemType.disk:
                    DiskInfo tempD = item as DiskInfo;
                    if (tempD == null)
                    {
                        return "?";
                    }
                    if (String.IsNullOrWhiteSpace(tempD.Name))
                    {
                        return "Bad name.";
                    }
                    if (tempD.ReleaseDate == null)
                    {
                        return "Bad release date.";
                    }
                    if (GetByID(ItemType.author, tempD.AuthorID) == null)
                    {
                        return "Not defined author was used.";
                    }
                    if (GetByID(ItemType.publisher, tempD.PublisherID) == null)
                    {
                        return "Not defined publisher was used.";
                    }
                    if (tempD.ReleaseDate < ((AuthorInfo)GetByID(ItemType.author, tempD.AuthorID)).BirthDate)
                    {
                        return "Release must be after author's bday";
                    }
                    if (tempD.GenreID.TrueForAll(Hah => GetByID(ItemType.genre, Hah) != null))
                    {
                        return null;
                    }
                    else
                    {
                        return "Not defined genre was used.";
                    }
            }
            throw new Exception("?");
        }
    }
}
