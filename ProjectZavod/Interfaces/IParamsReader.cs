using ProjectZavod.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZavod.Interfaces
{
    public interface IParamsReader
    {
        OrderParams ReadParams(string file);
    }
}
