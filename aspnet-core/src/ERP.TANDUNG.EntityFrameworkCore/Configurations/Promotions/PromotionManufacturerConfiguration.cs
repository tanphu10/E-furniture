using ERP.TANDUNG.Promotions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TANDUNG.Configurations.Promotions
{
    public class PromotionManufacturerConfiguration : IEntityTypeConfiguration<PromotionManufacturer>
    {
        public void Configure(EntityTypeBuilder<PromotionManufacturer> builder)
        {
            builder.ToTable(TANDUNGConsts.DbTablePrefix + "PromotionManufacturers");
            builder.HasKey(x => x.Id);
        }
    }
}
