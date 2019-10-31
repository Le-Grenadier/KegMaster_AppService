using System;
using System.Collections.Generic;
using Microsoft.Azure.Mobile.Server;

namespace KegMasterAppService.DataObjects
{
    public class KegItem : EntityData
    {
        public string Alerts { get; set; }
        public int TapNo { get; set; }
        public string Name { get; set; }
        public string Style { get; set; }
        public string Description { get; set; }
        public DateTime DateKegged { get; set; }
        public DateTime DateAvail { get; set; }
        public bool PourEn { get; set; }
        public bool PourNotification { get; set; }
        public float PourQtyGlass { get; set; }
        public float PourQtySample { get; set; }
        public float PressureCrnt { get; set; }
        public float PressureDsrd { get; set; }
        public float PressureDwellTime { get; set; }
        public bool PressureEn { get; set; }
        public float QtyAvailable { get; set; }
        public float QtyReserve { get; set; }
    }
}