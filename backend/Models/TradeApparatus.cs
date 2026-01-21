using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.Models
{
    public class TradeApparatus
    {
        public int Id { get; set; }

        public string Model { get; set; } = string.Empty;

        public string Place { get; set; } = string.Empty;

        public PayType Type { get; set; }

        public decimal SummaryIncome { get; set; }

        public Guid SerialNumber { get; set; }

        public Guid InventoryNumber { get; set; }

        public string FirmName { get; set; } = string.Empty;

        public DateOnly? DateCreated { get; set; }

        public DateOnly? ManufacturedDate { get; set; }

        public DateOnly? DateUpdated { get; set; }

        public DateOnly? LastCheckDate { get; set; }

        public int NextCheckInterval { get; set; }

        public uint Resource { get; set; }

        public DateOnly? NextRepairDate { get; set; }

        public uint RepairTime { get; set; }

        public Status Status { get; set; }

        public string CountryOfManufacturer { get; set; } = string.Empty;

        public DateOnly? InventarizationTime { get; set; }

        public virtual User? CheckedByUser { get; set; }

        public int? CheckedByUserId { get; set; }

        public virtual ICollection<Sales> Sales { get; set; } = new List<Sales>();
        public virtual ICollection<Services> Services { get; set; } = new List<Services>();
    }
    public enum PayType
    {
        CardPay, CashPay
    }
    public enum Status
    {
        Working, OutOfOrder, InService
    }
}
