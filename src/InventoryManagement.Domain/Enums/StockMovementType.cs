using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.Enums;

public enum StockMovementType
{
    Entry = 1,
    Exit = 2,
    TransferIn = 3,
    TransferOut = 4,
    CountAdjustment = 5
}