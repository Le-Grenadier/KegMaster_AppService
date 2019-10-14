using Microsoft.Azure.Mobile.Server;
using System;

namespace KegMasterAppService.DataObjects
{
    public class KegItem : EntityData
    {
        public int TapId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateAvailable { get; set; }
        public DateTime DateKegged { get; set; }
        public float Quantity { get; set; }
        public bool Complete { get; set; }
        public bool Dispense { get; set; }
    }
}