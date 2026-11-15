using System;
using System.Collections.Generic;
using System.Text;

namespace Dll.Filters
{
    public class MovieFilter
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Genre { get; set; }
        public int? Year { get; set; }
        public string? Descr { get; set; }
        public string? Country { get; set; }
        public string? Key { get; set; }
    }
}
