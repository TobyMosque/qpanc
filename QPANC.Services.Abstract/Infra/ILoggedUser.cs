using System;

namespace QPANC.Services.Abstract
{
    public interface ILoggedUser
    {
        Guid? SessionId { get; }
        Guid? UserId { get; }
    }
}
