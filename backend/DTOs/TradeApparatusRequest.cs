using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vendingbackend.Core.DTOs
{
    public record TradeApparatusRequest(
        string Model,
        int PayType,
        decimal SummaryIncome,
        Guid SerialNumber,
        string FirmName,
        DateOnly? DateCreated,
        DateOnly? DateUpdated,
        DateOnly? LastCheckDate,
        int NextCheckInterval,
        uint Resource,
        DateOnly? NextReapirDate,
        uint ReapirTime,
        int Status,
        string CountryOfManufacturer,
        DateOnly? InventarizationTime,
        int CheckedByUserId
        );
}
