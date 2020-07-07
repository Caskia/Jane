namespace Jane.AWS.S3
{
    public interface IAWSS3Service
    {
        SignatureOut GetSignature(GetSignatureInput input);
    }
}