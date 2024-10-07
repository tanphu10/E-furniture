using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TANDUNG.Orders
{
    public enum TransactionType
    {
        ConfirmOrder=1,
        StartProcessing=2,
        FinishOrder=3,
        CancelOrder=4
    }
}