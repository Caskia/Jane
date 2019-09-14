namespace Jane.QCloud.Cos
{
    public interface IQCloudCosService
    {
        string BuildAuthorizationString(SignatureInput input);

        string GetHost();

        string GetPathPrefix();

        string Signature(SignatureInput input);
    }
}