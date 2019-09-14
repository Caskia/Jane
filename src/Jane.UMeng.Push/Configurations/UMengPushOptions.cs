namespace Jane.UMeng.Push.Configurations
{
    public class UMengPushOptions
    {
        public UMengKey AndroidKey { get; set; } = new UMengKey();

        public UMengKey IOSKey { get; set; } = new UMengKey();
    }
}