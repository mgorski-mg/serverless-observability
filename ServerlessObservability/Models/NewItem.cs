using System;

namespace ServerlessObservability.Models
{
    public class NewItem : Item
    {
        public NewItem() {}

        public NewItem(Guid id, string message) : base(id, ItemType.New, message) {}

        public DoneItem Finish(DateTime finishTime) => new DoneItem(Id, Message, finishTime) { CreateTime = CreateTime };
    }
}
