using System;

namespace ServerlessObservability.Models
{
    public class ItemMessage
    {
        public Guid ItemId { get; set; }

        public ItemMessage(Guid itemId)
        {
            ItemId = itemId;
        }
    }
}
