using QPANC.Services.Abstract;
using System;

namespace QPANC.Services
{
    public class SGuid : ISGuid
    {
        public Guid NewGuid()
        {
            return RT.Comb.Provider.PostgreSql.Create();
        }
    }
}
