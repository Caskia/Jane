using Jane.Entities;
using MongoDB.Bson;

namespace Jane.Environment
{
    public class JaneIncrementField : Entity<ObjectId>
    {
        public string Key { get; set; }

        public int Value { get; set; }
    }
}