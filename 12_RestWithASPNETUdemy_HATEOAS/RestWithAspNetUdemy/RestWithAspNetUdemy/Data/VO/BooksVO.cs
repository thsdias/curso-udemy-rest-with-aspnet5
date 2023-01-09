﻿using RestWithAspNetUdemy.Hypermedia;
using RestWithAspNetUdemy.Hypermedia.Abstract;
using System;
using System.Collections.Generic;

namespace RestWithAspNetUdemy.Model
{
    public class BooksVO : ISupportsHyperMedia
    {
        public long Id { get; set; }

        public String Author { get; set; }

        public DateTime LaunchDate { get; set; }

        public decimal Price { get; set; }

        public string Title { get; set; }

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
