using ERP.TANDUNG.Public.Samples;
using ERP.TANDUNG.Samples;
using Xunit;

namespace ERP.TANDUNG.EntityFrameworkCore.Applications;

[Collection(TANDUNGTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<TANDUNGEntityFrameworkCoreTestModule>
{

}
