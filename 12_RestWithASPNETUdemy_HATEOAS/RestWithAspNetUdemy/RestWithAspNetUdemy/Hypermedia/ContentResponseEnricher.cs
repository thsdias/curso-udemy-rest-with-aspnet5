using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using RestWithAspNetUdemy.Hypermedia.Abstract;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWithAspNetUdemy.Hypermedia
{
    public abstract class ContentResponseEnricher<T> : IResponseEnricher where T : ISupportsHyperMedia
    {
        public ContentResponseEnricher()
        { 
        }

        protected abstract Task EnrichModel(T content, IUrlHelper urlHelper);

        public virtual bool CanErich(Type contentType)
        {
            return contentType == typeof(T) || contentType == typeof(List<T>);
        }

        bool IResponseEnricher.CanErich(ResultExecutingContext response)
        {
            if (response.Result is OkObjectResult okObjectResult) 
            {
                return CanErich(okObjectResult.Value.GetType());
            }

            return false;
        }

        public async Task Enrich(ResultExecutingContext response)
        {
            var urlHelper = new UrlHelperFactory().GetUrlHelper(response);

            if (response.Result is OkObjectResult okObjectResult)
            {
                if(okObjectResult.Value is T model) 
                {
                    await EnrichModel(model, urlHelper);
                }
                else if(okObjectResult.Value is List<T> collection)
                {
                    ConcurrentBag<T> bag = new ConcurrentBag<T>(collection);
                    Parallel.ForEach(bag, (element) =>
                    {
                        EnrichModel(element, urlHelper);
                    });
                }
                await Task.FromResult<object>(null);
            }
        }
    }
}
