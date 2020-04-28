namespace Jane.RongCloud.Im
{
    public class GetUserTokenResponse : RongCloudResponse
    {
        public string Token { get; set; }

        public string UserId { get; set; }
    }
}