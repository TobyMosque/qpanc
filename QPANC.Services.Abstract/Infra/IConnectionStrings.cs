namespace QPANC.Services.Abstract
{
    public interface IConnectionStrings
    {
        string DefaultConnection { get; }
        string AuditConnection { get; }
    }
}
