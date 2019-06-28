using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOLayer
{
    public interface IConnection
    {
        void Connect();
        void Close();
    }
}
