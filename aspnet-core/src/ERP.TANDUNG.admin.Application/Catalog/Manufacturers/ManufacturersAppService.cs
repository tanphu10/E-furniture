﻿using ERP.TANDUNG.Admin.Permissions;
using ERP.TANDUNG.Manufacturers;
using ERP.TANDUNG.ProductCategories;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ERP.TANDUNG.Admin.Catalog.Manufacturers
{
    [Authorize(TANDUNGPermissions.Manufacturer.Default,Policy ="AdminOnly")]
    public class ManufacturersAppService : CrudAppService<Manufacturer,
        ManufacturerDto, Guid, PagedResultRequestDto,
        CreateUpdateManufacturerDto, CreateUpdateManufacturerDto>, IManufacturersAppService
    {
        public ManufacturersAppService(IRepository<Manufacturer, Guid> repository) : base(repository)
        {
            GetPolicyName = TANDUNGPermissions.Manufacturer.Default;
            GetListPolicyName = TANDUNGPermissions.Manufacturer.Default;
            CreatePolicyName = TANDUNGPermissions.Manufacturer.Create;
            UpdatePolicyName = TANDUNGPermissions.Manufacturer.Update;
            DeletePolicyName = TANDUNGPermissions.Manufacturer.Delete;
        }
        [Authorize(TANDUNGPermissions.Manufacturer.Delete)]
        public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();

        }
        [Authorize(TANDUNGPermissions.Manufacturer.Default)]

        public async Task<List<ManufacturerInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Manufacturer>, List<ManufacturerInListDto>>(data);

        }
        [Authorize(TANDUNGPermissions.Manufacturer.Default)]

        public async Task<PagedResultDto<ManufacturerInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();

            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));
            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));
            return new PagedResultDto<ManufacturerInListDto>(totalCount, ObjectMapper.Map<List<Manufacturer>, List<ManufacturerInListDto>>(data));
        }
    }
}
