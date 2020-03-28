namespace Jane.QCloud.Cos
{
    public interface IQCloudCosService
    {
        string BuildAuthorizationString(SignatureInput input);

        string GetHost(string region = null, string bucketName = null);

        string GetPathPrefix();

        string Signature(SignatureInput input);
    }
}