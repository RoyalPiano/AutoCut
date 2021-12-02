using ProjectZavod.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZavod.Services
{
    public interface IPathsService
    {
        //Task<RootPaths> LoadPaths();
        RootPaths LoadPaths();
        void AddOrdersPath(string value);
        void AddResultsPath(string value);
    }
}
