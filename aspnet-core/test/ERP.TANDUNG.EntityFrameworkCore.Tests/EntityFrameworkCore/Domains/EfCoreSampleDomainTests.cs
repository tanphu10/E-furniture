using ERP.TANDUNG.Samples;
using Xunit;

namespace ERP.TANDUNG.EntityFrameworkCore.Domains;

[Collection(TANDUNGTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<TANDUNGEntityFrameworkCoreTestModule>
{

}
