using Microsoft.AspNetCore.Mvc;
using RestWithAspNetUdemy.Hypermedia.Constants;
using RestWithAspNetUdemy.Model;
using System.Text;
using System.Threading.Tasks;

namespace RestWithAspNetUdemy.Hypermedia.Enricher
{
    public class BooksEnricher : ContentResponseEnricher<BooksVO>
    {
        private readonly object _lock = new object();

        protected override Task EnrichModel(BooksVO content, IUrlHelper urlHelper)
        {
            var path = "api/v1/books";
            string link = GetLink(content.Id, urlHelper, path);

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.GET,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGet
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.POST,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.PUT,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPut
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.DELETE,
                Href = link,
                Rel = RelationType.self,
                Type = "int"
            });

            return null;
        }

        private string GetLink(long id, IUrlHelper urlHelper, string path)
        {
            lock (_lock)
            {
                var url = new { Controller = path, id = id };
                return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();
            };
        }
    }
}
