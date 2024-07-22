using ERP.TANDUNG.ProductAttributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.TANDUNG.Configurations.Attributes
{
    public class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.ToTable(TANDUNGConsts.DbTablePrefix + "Attributes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code).HasMaxLength(50).IsUnicode(false).IsRequired();
            builder.Property(x => x.Label).HasMaxLength(50).IsRequired();
        }
    }
}
