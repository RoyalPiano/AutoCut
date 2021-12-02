using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZavod.ViewModels
{
    public class RootPaths
    {
        private string _keyHoleModelsPath = @"..\..\templates\KeyHoles";
        public string KeyHoleModelsPath
        {
            get
            {
                return _keyHoleModelsPath;
            }
        }
        private string _doorModelsPath = @"..\..\templates\Doors";
        public string DoorModelsPath
        {
            get
            {
                return _doorModelsPath;
            }
        }


        public string OrdersPath;
        public string ResultsPath;
    }
}
