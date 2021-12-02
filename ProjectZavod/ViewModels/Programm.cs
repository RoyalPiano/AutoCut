using ProjectZavod.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZavod.ViewModels
{
    public class Programm
    {
        private DxfRedactor dxfRedactor = new DxfRedactor();
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
                OrderParams orderParams = new OrderParams(order); // создаешь эту переменную, в ней есть поля ширина, высота и типы ключей, двери из excel файла.
            }
        }
    }
}
