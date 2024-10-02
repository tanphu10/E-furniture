using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TANDUNG.Admin.Products
{
    public class ProductListFilterDto:BaseListFilterDto
    {
        public Guid? CategoryId { get; set; }
    }
}
