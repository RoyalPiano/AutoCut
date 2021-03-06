//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectZavod.Data.orderDBModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int Id { get; set; }
        public string Leaf { get; set; }
        public string SteelThickness { get; set; }
        public Nullable<double> DoorWidth { get; set; }
        public Nullable<double> DoorHeight { get; set; }
        public string DoorOpeningType { get; set; }
        public Nullable<double> SashWidth { get; set; }
        public Nullable<double> SashHeight { get; set; }
        public bool JambRight { get; set; }
        public bool JambLeft { get; set; }
        public bool JambUp { get; set; }
        public string PolymerCoating { get; set; }
        public string Lock1 { get; set; }
        public string Сylinder1 { get; set; }
        public string Lock2 { get; set; }
        public string Сylinder2 { get; set; }
        public string Handle { get; set; }
        public string HardwareColor { get; set; }
        public Nullable<int> LatchesCount { get; set; }
        public string Soundproofing { get; set; }
        public string Peephole { get; set; }
        public string InteriorDecoration { get; set; }
        public string Color { get; set; }
        public string Payment { get; set; }
        public bool OnWorkingSide { get; set; }
        public bool OnSecondSide { get; set; }
        public bool MufflePart { get; set; }
        public string Side { get; set; }
        public string SlopeCorner { get; set; }
        public string Сomments { get; set; }
        public string Customer { get; set; }
        public string OrderNumber { get; set; }
        public string NumberDD { get; set; }
        public string Date { get; set; }
        public string Price { get; set; }
        public string CustomerContact { get; set; }
        public string CustomerAdress { get; set; }
    }
}
