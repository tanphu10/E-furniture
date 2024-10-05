using Volo.Abp.Application.Dtos;

namespace ERP.TANDUNG.Admin
{
    public class BaseListFilterDto:PagedResultRequestDto
    {
        public string? Keyword { get; set; }
    }
}
