using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneDriveIntergration.Models
{
    public class Document
    {
        public string Name { get; set; }
        public string CId { get; set; }
        public string ResId { get; set; }
        public string ParId { get; set; }
        public string Extention { get; set; }
        public string FileSize { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifedTime { get; set; }
        public string DownloadUrl { get; set; }
        public string OnlieEditingUrl
        {
            get
            {
                return string.Format("{0}?cid={1}&page=view&resid={2}&parId={3}&app={4}",
                    "https://onedrive.live.com/edit.aspx",
                    CId,
                    ResId,
                    ParId,
                    Extention);
            }
        }
    }
}