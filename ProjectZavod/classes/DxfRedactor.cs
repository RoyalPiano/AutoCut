using netDxf;
using System.Linq;
using System.IO;
using ProjectZavod.classes;
using Ninject;
using ProjectZavod.classes.DTOs;

namespace ProjectZavod.ViewModels
{
    public class DxfRedactor
    {
        private static readonly double deltaZ = 243;
        private static readonly string widthTemplate = "860";
        private static readonly string heightTemplate = "2050";
        public DxfRedactor(StandardKernel container)
        {
            this.container = container;
            paths = container.Get<RootPaths>();
        }

        private readonly StandardKernel container;
        private readonly RootPaths paths;
        public void CreateTransformedTemplates()
        {
            var ordersPath = paths.OrdersPath;
            foreach (var orderPath in Directory.GetFiles(ordersPath))
            {
                var orderFileInf = new FileInfo(orderPath);
                Directory.CreateDirectory(Path.Combine(paths.ResultsPath, orderFileInf.Name));
                var orderParams = container.Get<OrderReader>().ReadParams(orderPath);
                var dxfDoorFiles = Directory.GetFiles(Path.Combine(paths.DoorModelsPath, orderParams.DoorType));
                foreach (var file in dxfDoorFiles)
                {
                    var dxfFile = DxfDocument.Load(file);
                    dxfFile.CheckLoadError(file);
                    dxfFile = dxfFile.ChangeSize(orderParams.Width, orderParams.Height);
                    var dxfFileInf = new FileInfo(file);
                    dxfFileInf = SetNewMeasurementsInFileName(dxfFileInf, orderParams);
                    var dxfFileDTO = new DxfFileDTO(dxfFile, dxfFileInf);

                    if (dxfFileInf.Name.Contains("зам"))
                    {
                        dxfFileDTO = HandlePresenceOfLocks(orderParams, dxfFileDTO);
                    }

                    if (dxfFileInf.Name.Contains("зд"))
                    {
                        dxfFileDTO = HandlePresenceOfLatches(orderParams, dxfFileDTO);
                    }

                    dxfFileDTO.DxfModel.Save(Path.Combine(paths.ResultsPath, orderFileInf.Name, dxfFileDTO.FileInfo.Name));
                }
            }
        }

        private DxfFileDTO HandlePresenceOfLatches(OrderParams orderParams, DxfFileDTO dxfFileDTO)
        {
            if (orderParams.LatchSum == 0)
            {
                dxfFileDTO.FileInfo = RemoveExtraElementFromName(dxfFileDTO.FileInfo);
            }
            else
            {
                for (int i = 0; i < orderParams.LatchSum; i++)
                {
                    dxfFileDTO.DxfModel = dxfFileDTO.DxfModel.AddModelWithShift(GetLatchModel(dxfFileDTO.FileInfo, paths.LatchModelsPath), new Vector3(deltaZ * i, 0, 0));
                }
            }
            return dxfFileDTO;
        }

        private DxfFileDTO HandlePresenceOfLocks(OrderParams orderParams, DxfFileDTO dxfFileDTO)
        {
            if (orderParams.KeyType1.IsRequested)
            {
                dxfFileDTO.DxfModel = dxfFileDTO.DxfModel.AddModelWithShift(GetLockModel(orderParams.KeyType1.Type, dxfFileDTO.FileInfo, paths.KeyHoleModelsPath), new Vector3(0, 0, 0));
                dxfFileDTO.FileInfo = AddKeyLockToFileName(dxfFileDTO.FileInfo, orderParams.KeyType1.Type);
            }

            if (orderParams.KeyType2.IsRequested)
            {
                dxfFileDTO.DxfModel = dxfFileDTO.DxfModel.AddModelWithShift(GetLockModel(orderParams.KeyType2.Type, dxfFileDTO.FileInfo, paths.KeyHoleModelsPath), new Vector3(deltaZ, 0, 0));
                dxfFileDTO.FileInfo = AddKeyLockToFileName(dxfFileDTO.FileInfo, orderParams.KeyType2.Type);
            }
            return dxfFileDTO;
        }

        private FileInfo RemoveExtraElementFromName(FileInfo fileInfo)
        {
            return new FileInfo(fileInfo.FullName.Replace("зд ", ""));
        }

        private FileInfo AddKeyLockToFileName(FileInfo fileInfo, string typeOfKeyLock)
        {
            return new FileInfo(fileInfo.FullName.Replace(".dxf", $" {typeOfKeyLock}.dxf"));
        }

        private FileInfo SetNewMeasurementsInFileName(FileInfo fileInfo, OrderParams orderParams)
        {
            var tempMeasure = fileInfo.Name;
            if (tempMeasure.Contains(widthTemplate))
                fileInfo = new FileInfo(fileInfo.Name.Replace(widthTemplate, $"{orderParams.Width}"));
            else if (tempMeasure.Contains(heightTemplate))
                fileInfo = new FileInfo(fileInfo.FullName.Replace(heightTemplate, $"{orderParams.Height}"));
            else fileInfo = new FileInfo(fileInfo.FullName.Replace($"{widthTemplate}x{heightTemplate}", $"{orderParams.Width}x{orderParams.Height}"));
            return fileInfo;
        }

        private DxfDocument GetLockModel(string type, FileInfo fileInf, string folderPath)
        {
            var lockFile = DxfDocument
                .Load(Directory.GetFiles(Path.Combine(folderPath, type))
                .First(w => new FileInfo(w).Name.StartsWith(fileInf.Name.Split(' ')[1])));
            return lockFile;
        }

        private DxfDocument GetLatchModel(FileInfo fileInf, string folderPath)
        {
            var latchFile = DxfDocument
                .Load(Directory.GetFiles(folderPath)
                .First(w => new FileInfo(w).Name.StartsWith(fileInf.Name.Split(' ')[1])));
            return latchFile;
        }
    }
}
