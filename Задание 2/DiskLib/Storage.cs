using System;
using System.Collections.Generic;

namespace DiskLib
{
    public class Storage
    {
        public Storage()
        {
            Items = new Dictionary<int, int>();
        }
        public Dictionary<int, int> Items { get; private set; }
        public void Stock(int id, int amount)
        {
            if (!Items.ContainsKey(id))
            {
                Items.Add(id, amount);
            }
            else
            {
                Items[id] += amount;
            }
        }
        public void Take(int id, int amount)
        {
            if (!Items.ContainsKey(id))
            {
                throw new Exception("No such disk listed.");
            }
            if (Items[id] < amount)
            {
                throw new Exception("Not enough disks.");
            }
            Items[id] -= amount;
        }
        public int GetAmount(int id)
        {
            if (Items.ContainsKey(id))
            {
                return Items[id];
            }
            return 0;
        }
    }
}
