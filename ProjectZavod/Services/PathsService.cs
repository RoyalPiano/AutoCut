using ProjectZavod.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZavod.Services
{
    public class PathsService : IPathsService
    {
        private RootPaths paths;

        public PathsService()
        {
            paths = new RootPaths();
        }

        //public async Task<RootPaths> LoadPaths()
        //{
        //    await Task.Delay(1);
        //    return paths;
        //}

        public RootPaths LoadPaths()
        {
            return paths;
        }

        public void AddOrdersPath(string value)
        {
            paths.OrdersPath = value;
        }

        public void AddResultsPath(string value)
        {
            paths.ResultsPath = value;
        }
    }
}
