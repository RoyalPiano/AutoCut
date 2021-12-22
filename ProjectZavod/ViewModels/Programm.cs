using netDxf;
using ProjectZavod.Services;
using System;
using ProjectZavod.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZavod.ViewModels
{
    public class Programm
    {
        private RootPaths paths;
        public void Start() //здесь пишется что должна делать программа при нажатии start
        {
            //dxfRedactor.ChangeSize(); пример того, как вызывать методы. Хочешь добавить метод - добавляй в класс DxfRedactor
            //dxfRedactor.MakeCut();
            //dxfRedactor.AddLock();
            paths = Service<IPathsService>.GetService().LoadPaths();
            if (paths.OrdersPath == null || paths.ResultsPath == null)
            {
                throw new Exception("не указан путь к папке");
            }

            var ordersPath = paths.OrdersPath;
            foreach (var order in ordersPath.GetFilesFromDirectory())
            {
                paths.ResultsPath.AddPath(order.Split('\\').Last()).CreateFolder();

                OrderParams orderParams = new OrderParams(order); // создаешь эту переменную, в ней есть поля ширина, высота и типы ключей, двери из excel файла.
                var dxfDoorFiles = paths.DoorModelsPath.AddPath(orderParams.DoorType).GetFilesFromDirectory();
                foreach (var file in dxfDoorFiles)
                {
                    var fileName = file;
                    var dxfFile = DxfDocument.Load(file);
                    dxfFile.CheckLoadError(fileName);
                    dxfFile = dxfFile.ChangeSize(orderParams.Width, orderParams.Height);
                    var temp = fileName.Split('\\').Last().Split(' ')[2].Replace(".dxf", "");
                    if (temp == "860")
                        fileName = fileName.Replace(temp, $"{orderParams.Width}");
                    else if (temp == "2050")
                        fileName = fileName.Replace(temp, $"{orderParams.Height}");
                    else fileName = fileName.Replace(temp, $"{orderParams.Width}x{orderParams.Height}");

                    //fileName = fileName.Replace(fileName.Split('\\').Last().Split(' ')[2].Replace(".dxf", ""), $"{orderParams.Width}x{orderParams.Height}");

                    if (file.Split('\\').Last().Contains("зам"))
                    {
                        if(orderParams.KeyType1 != "Нет")
                        {
                            dxfFile = dxfFile.UniteModels(GetLockModel(orderParams.KeyType1, file, paths.KeyHoleModelsPath), new Vector3(0, 0, 0));
                            fileName = fileName.Replace(".dxf", $" {orderParams.KeyType1}.dxf");
                        }

                        if (orderParams.KeyType2 != "Нет")
                        {
                            dxfFile = dxfFile.UniteModels(GetLockModel(orderParams.KeyType2, file, paths.KeyHoleModelsPath), new Vector3(0, 0, 243));
                            fileName = fileName.Replace(".dxf", $" {orderParams.KeyType2}.dxf");
                        }
                    }

                    if (file.Split('\\').Last().Contains("зд"))
                    {
                        var deltaZ = 243;
                        if (orderParams.LatchSum == 0)
                        {
                            fileName = fileName.Replace("зд", ""); // поправить, возможны ошибки
                        }
                        else
                        {
                            for (int i = 0; i < orderParams.LatchSum; i++)
                            {
                                dxfFile.UniteModels(GetLatchModel(file, paths.LatchModelsPath), new Vector3(0, 0, deltaZ * i));
                            }
                        }
                    }

                    dxfFile.Save(paths.ResultsPath.AddPath(order.Split('\\').Last()).AddPath(fileName.Split('\\').Last()));
                }
            }
        }

        private DxfDocument GetLockModel(string type, string file, string folderPath)
        {
            var lockFile = DxfDocument
                .Load(folderPath.AddPath(type)
                .GetFilesFromDirectory()
                .First(w => w.Split('\\')
                .Last()
                .StartsWith(file.Split('\\')
                .Last()
                .Split(' ')[1])));
            return lockFile;
        }

        private DxfDocument GetLatchModel(string file, string folderPath)
        {
            var latchFile = DxfDocument
                .Load(folderPath.GetFilesFromDirectory()
                    .First(w => w.Split('\\')
                    .Last()
                    .StartsWith(file.Split('\\')
                    .Last()
                    .Split(' ')[1])));
            return latchFile;
        }
    }
}
