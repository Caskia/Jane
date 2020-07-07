namespace Jane.AWS.S3
{
    public interface IAWSS3Service
    {
        string GetHost(string region = null, string bucketName = null);

        string GetPathPrefix();

        SignatureOut GetSignature(GetSignatureInput input);
    }
}