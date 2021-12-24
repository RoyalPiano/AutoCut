using netDxf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZavod.classes.DTOs
{
    public class DxfFileDTO
    {
        public DxfFileDTO(DxfDocument dxfModel, FileInfo fileInfo)
        {
            DxfModel = dxfModel;
            FileInfo = fileInfo;
        }

        public DxfDocument DxfModel { get; set; }
        public FileInfo FileInfo { get; set; }
    }
}
