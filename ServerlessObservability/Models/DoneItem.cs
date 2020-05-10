using System;

namespace ServerlessObservability.Models
{
    public class DoneItem : Item
    {
        public DateTime FinishTime { get; set; }

        public DoneItem(Guid id, string message, DateTime finishTime) : base(id, ItemType.Done, message)
        {
            FinishTime = finishTime;
        }
    }
}
