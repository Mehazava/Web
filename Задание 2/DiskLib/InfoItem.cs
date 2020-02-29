using System;
using System.Collections.Generic;

namespace DiskLib
{
    public enum ItemType
    {
        undefined,
        disk,
        author,
        publisher,
        genre
    }
    public abstract class InfoItem
    {
        protected internal event ItemInfoHandler Created;
        protected internal event ItemInfoHandler Modified;
        protected internal event ItemInfoHandler Deleted;
        private static int counter = 0;
        protected internal ItemType Type;
        public InfoItem()
        {
            Id = ++counter;
            Type = ItemType.undefined;
        }
        public int Id { get; protected set; }
        private void CallEvent(string Message, ItemInfoHandler handler)
        {
            if (Message != null)
            {
                handler?.Invoke(this, Message);
            }
        }
        protected virtual void OnCreated(string Message)
        {
            CallEvent(Message, Created);
        }
        protected virtual void OnModified(string Message)
        {
            CallEvent(Message, Modified);
        }
        protected virtual void OnDeleted(string Message)
        {
            CallEvent(Message, Deleted);
        }
        protected internal void Create()
        {
            OnCreated($"Added a new item of type {Type}. Id:{Id}.");
        }
        protected internal void Modify()
        {
            OnModified($"Modified a field for item of type {Type} with id {Id}.");
        }
        protected internal void Delete()
        {
            OnDeleted($"Deleted an item of type {Type} with id {Id}.");
        }
        protected internal abstract void ChangeField(int Field, string value);
        public abstract string GetInfo();
        public abstract string GetName();
    }
}
