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
    public class DxfRedactor
    {
        private RootPaths paths = new RootPaths();
        public DxfDocument ChangeSize(DxfDocument ourFile, double width, double height)
        {
            double newWidth = width - 860;
            double newHeigh = height - 2050;
            foreach (var x in ourFile.Lines)
            {
                if (x.Color.G == 255)
                    x.EndPoint = x.EndPoint + new Vector3(newWidth, 0, 0);
                if (x.Color.B == 255)
                {
                    x.StartPoint = x.StartPoint + new Vector3(newWidth, 0, 0);
                    x.EndPoint = x.EndPoint + new Vector3(newWidth, 0, 0);
                }
                if (x.Color.R == 255)
                    x.EndPoint = x.EndPoint - new Vector3(0, newHeigh, 0);
                if (x.Color.R == 254)
                {
                    x.StartPoint = x.StartPoint - new Vector3(0, newHeigh, 0);
                    x.EndPoint = x.EndPoint - new Vector3(0, newHeigh, 0);
                }
            }
            foreach (var x in ourFile.Circles)
            {
                if (x.Color.B == 255)
                    x.Center = x.Center + new Vector3(newWidth, 0, 0);
            }
            return ourFile;
        }

        public DxfDocument AddLock(DxfDocument ourFile, DxfDocument lockFile, DxfDocument lockFile2)
        {
            var listEntites = new List<EntityObject>();
            if (lockFile != null)
                foreach (Layout layout in lockFile.Layouts)
                {
                    List<DxfObject> entities = lockFile.Layouts.GetReferences(layout);
                    foreach (DxfObject o in entities)
                    {
                        EntityObject entity = o as EntityObject;
                        listEntites.Add(entity);
                    }
                }
            if (lockFile2 != null)
                foreach (Layout layout in lockFile2.Layouts)
                {
                    List<DxfObject> entities = lockFile2.Layouts.GetReferences(layout);
                    Vector3 lenghtToSecondHole = new Vector3(ourFile.Points.ToArray()[0].Position.X, 0, 0) - new Vector3(ourFile.Points.ToArray()[1].Position.X, 0, 0);
                    foreach (DxfObject o in entities)
                    {
                        EntityObject entity = o as EntityObject;
                        switch ((int)entity.Type)
                        {
                            case 1:
                                var cir = (Circle)entity;
                                cir.Center = cir.Center + lenghtToSecondHole;
                                break;
                            case 10:
                                var line = (Line)entity;
                                line.StartPoint = line.StartPoint + lenghtToSecondHole;
                                line.EndPoint = line.EndPoint + lenghtToSecondHole;
                                break;
                            case 0:
                                var arc = (Arc)entity;
                                arc.Center = arc.Center + lenghtToSecondHole;
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

        public void MakeCut()
        {
            var orderFiles = paths.OrdersPath.GetFilesFromDirectory();
            for (int i = 0; i < orderFiles.Length; i++)
            {
                string file = string.Format($"{paths.ResultsPath}\\createdFile{i}.dxf");
                DxfDocument ourFile = DxfDocument.Load(orderFiles[i]);
                if (ourFile == null)
                {
                    throw new Exception(orderFiles[i] + " File is not loaded, incorrect format");
                }

                double width = 880;
                double height = 2050;
                ChangeSize(ourFile, width, height);
                ourFile.Save(file);

                DxfVersion dxfVersion = DxfDocument.CheckDxfFileVersion(file, out _);
                if (dxfVersion < DxfVersion.AutoCad2000)
                    throw new Exception("you are using an old AutoCad Version");
            }
        }
    }
}
