using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneDriveIntergration.Models
{
    public class GraphService
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string HttpRequest { get; set; }
        public string RequestBody { get; set; }
        public string HttpResponse { get; set; }
        public string Category { get; set; }
        public string Link { get; set; }
    }
}