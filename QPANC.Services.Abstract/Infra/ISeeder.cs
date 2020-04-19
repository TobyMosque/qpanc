using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QPANC.Services.Abstract
{
    public interface ISeeder
    {
        Task Execute();
    }
}
