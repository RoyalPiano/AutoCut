using netDxf;
using netDxf.Entities;
using netDxf.Header;
using netDxf.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZavod.ViewModels
{
    public static class DxfExtensions
    {
        const int constWidth = 860;
        const int constHeight = 2050;

        public static DxfDocument ChangeSize(this DxfDocument ourFile, double needWidth, double needHeight)
        {
            double newWidth = needWidth - constWidth;
            double newHeigh = needHeight - constHeight;
            foreach (var x in ourFile.Lines)
            {
                LineChangeSize(x, new Vector3(newWidth, 0, 0), x.Color.G == 255, x.Color.B == 255);
                LineChangeSize(x, new Vector3(0, newHeigh, 0), x.Color.R == 255, x.Color.R == 254);
            }
            foreach (var x in ourFile.Circles)
            {
                if (x.Color.B == 255)
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

        public static DxfDocument UniteModels(this DxfDocument ourFile, DxfDocument modelToAdd, Vector3 deltaVector)
        {
            var listEntites = new List<EntityObject>();
            foreach (Layout layout in modelToAdd.Layouts)
            {
                List<DxfObject> entities = modelToAdd.Layouts.GetReferences(layout);
                
                foreach (DxfObject o in entities)
                {
                    EntityObject entity = o as EntityObject;
                    switch (entity.Type)
                    {
                        case EntityType.Arc:
                            var arc = (Arc)entity;
                            arc.Center = arc.Center + deltaVector;
                            break;
                        case EntityType.Circle:
                            var cir = (Circle)entity;
                            cir.Center = cir.Center + deltaVector;
                            break;
                        case EntityType.Line:
                            var line = (Line)entity;
                            line.StartPoint = line.StartPoint + deltaVector;
                            line.EndPoint = line.EndPoint + deltaVector;
                            break;
                    }
                    listEntites.Add(entity);
                }
            }
            foreach (var y in listEntites)
            {
                var x = y.Clone();
                ourFile.AddEntity((EntityObject)x);
            }
            return ourFile;
        }

        public static void CheckLoadError(this DxfDocument ourFile, string filePath)
        {
            if (ourFile == null)
            {
                throw new Exception(ourFile + " File is not loaded, incorrect format");
            }

            DxfVersion dxfVersion = DxfDocument.CheckDxfFileVersion(filePath, out _);
            if (dxfVersion < DxfVersion.AutoCad2000)
                throw new Exception("you are using an old AutoCad Version");
        }
    }
}
