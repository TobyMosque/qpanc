namespace QPANC.Services.Abstract
{
    public interface IJwtBearer
    {
        byte[] IssuerSigningKey { get; }
        byte[] TokenDecryptionKey { get; }
        string ValidIssuer { get; }
        string ValidAudience { get; }
    }
}
