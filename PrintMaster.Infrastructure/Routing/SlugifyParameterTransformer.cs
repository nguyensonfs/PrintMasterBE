using Microsoft.AspNetCore.Routing;
using PrintMaster.Commons.Utils;

namespace PrintMaster.Infrastructure.Routing
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            return StringUltilities.Slugify(value?.ToString());
        }
    }
}
