﻿using RestWithAspNetUdemy.Hypermedia.Abstract;
using System.Collections.Generic;

namespace RestWithAspNetUdemy.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}