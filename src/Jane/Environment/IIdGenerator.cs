using Jane.Dependency;

namespace Jane
{
    public interface IIdGenerator
    {
        long NextId();

        string NextIdString();
    }
}