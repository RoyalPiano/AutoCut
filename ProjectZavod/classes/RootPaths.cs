using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZavod.ViewModels
{
    public class RootPaths
    {
        public readonly string KeyHoleModelsPath = @"..\..\templates\KeyHoles";
        public readonly string DoorModelsPath = @"..\..\templates\Doors";
        public readonly string LatchModelsPath = @"..\..\templates\Latches";

        public string OrdersPath { get; set; }
        public string ResultsPath { get; set; }
    }
}
