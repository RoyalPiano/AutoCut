using ProjectZavod.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZavod.Interfaces
{
    public interface IParamsReader
    {
        OrderParamsDTO ReadParams(FileInfo file);
    }
}
