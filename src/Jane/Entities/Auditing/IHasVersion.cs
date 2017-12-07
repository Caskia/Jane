namespace Jane.Entities.Auditing
{
    public interface IHasVersion
    {
        int EventSequence { get; set; }

        int Version { get; set; }
    }
}