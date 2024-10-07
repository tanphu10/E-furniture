﻿using ERP.TANDUNG.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ERP.TANDUNG.Admin.Catalog.Products
{
    public class ProductInListDto : EntityDto<Guid>
    {
        public Guid ManufacturerID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Slug { get; set; }
        public ProductType ProductType { get; set; }
        public string SKU { get; set; }
        public int SortOrder { get; set; }
        public bool Visibility { get; set; }
        public bool IsActive { get; set; }
        public Guid CategoryId { get; set; }
        public string ThumbnailPicture { get; set; }
        public string CategoryName { get; set; }
        public string CategorySlug { get; set; }
    }
}
