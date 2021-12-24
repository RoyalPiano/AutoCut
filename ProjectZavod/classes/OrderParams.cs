using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using ProjectZavod.classes.DTOs;
using ProjectZavod.Services;

namespace ProjectZavod.ViewModels
{
    public class OrderParams
    {
        public OrderParams(OrderParamsDTO orderParamsDTO)
        {
            Height = orderParamsDTO.Height;
            Width = orderParamsDTO.Width;
            KeyType1 = new KeyLock(orderParamsDTO.KeyType1);
            KeyType2 = new KeyLock(orderParamsDTO.KeyType2);
            DoorType = orderParamsDTO.DoorType;
            LatchSum = orderParamsDTO.LatchSum;
        }

        public double Height { get; private set; }
        public double Width { get; private set; }
        public KeyLock KeyType1 { get; private set; }
        public KeyLock KeyType2 { get; private set; }
        public string DoorType { get; private set; }
        public int LatchSum { get; private set; }
    }

    public class KeyLock
    {
        public KeyLock(string type)
        {
            Type = type;
            IsRequested = type != "Нет";
        }

        public string Type { get; private set; }
        public bool IsRequested { get; private set; }
    }
}