using netDxf;
using netDxf.Entities;
using netDxf.Header;
using netDxf.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectZavod.ViewModels
{
    public static class DxfExtensions
    {
        const int constWidth = 860;
        const int constHeight = 2050;
        const int topElementColor = 255;
        const int bottomElementColor = 254;

        public static DxfDocument ChangeSize(this DxfDocument ourFile, double needWidth, double needHeight)
        {
            double newWidth = needWidth - constWidth;
            double newHeigh = needHeight - constHeight;
            foreach (var x in ourFile.Lines)
            {
                LineChangeSize(x, new Vector3(newWidth, 0, 0), x.Color.G == topElementColor, x.Color.B == topElementColor);
                LineChangeSize(x, new Vector3(0, newHeigh, 0), x.Color.R == topElementColor, x.Color.R == bottomElementColor);
            }
            foreach (var x in ourFile.Circles)
            {
                if (x.Color.B == topElementColor)
                    x.Center += new Vector3(newWidth, 0, 0);
            }
            return ourFile;
        }

        private static void LineChangeSize(Line x, Vector3 vectorWidth, bool firstArg, bool secondArg)
        {
            if (firstArg)
                x.EndPoint += vectorWidth;
            if (secondArg)
            {
                x.StartPoint += vectorWidth;
                x.EndPoint += vectorWidth;
            }
        }

        public static DxfDocument AddModelWithShift(this DxfDocument ourModel, DxfDocument additionalModel, Vector3 deltaVector)
        {
            var listEntites = new List<EntityObject>();
            foreach (Layout layout in additionalModel.Layouts)
            {
                List<DxfObject> entities = additionalModel.Layouts.GetReferences(layout);

                foreach (DxfObject o in entities)
                {
                    EntityObject entity = o as EntityObject;
                    listEntites.Add(entity.ShiftEntityPosition(deltaVector));
                }
            }
            return ourModel.AddEntitiesToDxf(listEntites); 
        }

        private static EntityObject ShiftEntityPosition(this EntityObject entity, Vector3 deltaVector)
        {
            switch (entity.Type)
            {
                case EntityType.Arc:
                    var arc = (Arc)entity;
                    arc.Center += deltaVector;
                    break;
                case EntityType.Circle:
                    var cir = (Circle)entity;
                    cir.Center += deltaVector;
                    break;
                case EntityType.Line:
                    var line = (Line)entity;
                    line.StartPoint += deltaVector;
                    line.EndPoint += deltaVector;
                    break;
            }
            return entity;
        }

        private static DxfDocument AddEntitiesToDxf(this DxfDocument ourModel, List<EntityObject> listEntites)
        {
            foreach (var y in listEntites)
            {
                var x = y.Clone();
                ourModel.AddEntity((EntityObject)x);
            }
            return ourModel;
        }

        public static void CheckLoadError(this DxfDocument ourFile, string filePath)
        {
            if (ourFile == null)
            {
                MessageBox.Show($"{ourFile} File is not loaded, incorrect format");
            }

            DxfVersion dxfVersion = DxfDocument.CheckDxfFileVersion(filePath, out _);
            if (dxfVersion < DxfVersion.AutoCad2000)
                MessageBox.Show($"you are using an old AutoCad Version, may be mistakes in result file. Please, update DxfVersion of file {filePath}");
        }
    }
}
