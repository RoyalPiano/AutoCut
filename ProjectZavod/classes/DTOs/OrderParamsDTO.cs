using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZavod.classes.DTOs
{
    public class OrderParamsDTO
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public string KeyType1 { get; set; }
        public string KeyType2 { get; set; }
        public string DoorType { get; set; }
        public int LatchSum { get; set; }
    }
}
