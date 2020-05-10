using System;

namespace ServerlessObservability.Models
{
    public abstract class Item
    {
        public Guid Id { get; set; }
        public ItemType Type { get; set; }
        public string Message { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.UtcNow;

        public Item() {}

        public Item(Guid id, ItemType itemType, string message)
        {
            Id = id;
            Type = itemType;
            Message = message;
        }
    }
}
