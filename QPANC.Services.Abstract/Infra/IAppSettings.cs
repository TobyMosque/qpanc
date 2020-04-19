namespace QPANC.Services.Abstract
{
    public interface IAppSettings
    {
        IConnectionStrings ConnectionString { get; }
        IJwtBearer JwtBearer { get; }
        ICors Cors { get; }
    }
}
