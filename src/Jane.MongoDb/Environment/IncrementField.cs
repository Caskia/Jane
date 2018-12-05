using Jane.Entities;
using MongoDB.Bson;

namespace Jane
{
    public class IncrementField : Entity<ObjectId>
    {
        public string Key { get; set; }

        public int Value { get; set; }
    }
}