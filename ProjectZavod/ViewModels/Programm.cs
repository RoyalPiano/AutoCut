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
        //private DxfRedactor dxfRedactor = new DxfRedactor();
        public void Start() //здесь пишется что должна делать программа при нажатии start
        {
            //dxfRedactor.ChangeSize(); пример того, как вызывать методы. Хочешь добавить метод - добавляй в класс DxfRedactor
            //dxfRedactor.MakeCut();
            //dxfRedactor.AddLock();
            var paths = Service<IPathsService>.GetService().LoadPaths();
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
                    var dxfFile = DxfDocument.Load(file);
                    dxfFile = dxfFile.ChangeSize(orderParams.Width, orderParams.Height);
                    if (file.Contains("зам"))
                    {
                        if (orderParams.KeyType1 != "Нет")
                        {
                            var lockFile1 = DxfDocument.Load(paths.KeyHoleModelsPath.AddPath(orderParams.KeyType1)
                                .GetFilesFromDirectory().First(w => w.StartsWith(file.Split(' ')[2])));
                            dxfFile = dxfFile.AddFirstPositionLocks(lockFile1);
                        }

                        if (orderParams.KeyType2 != "Нет")
                        {
                            var lockFile2 = DxfDocument.Load(paths.KeyHoleModelsPath.AddPath(orderParams.KeyType2)
                                .GetFilesFromDirectory().First(w => w.StartsWith(file.Split(' ')[2])));
                            dxfFile = dxfFile.AddSecondPositionLocks(lockFile2);
                        }
                    }

                    dxfFile.Save(paths.ResultsPath.AddPath(order).AddPath(file));
                }
            }
        }
    }
}
