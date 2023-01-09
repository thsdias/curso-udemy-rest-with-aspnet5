using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace RestWithAspNetUdemy.Hypermedia.Abstract
{
    public interface IResponseEnricher
    {
        bool CanErich(ResultExecutingContext context);

        Task Enrich(ResultExecutingContext context);
    }
}
