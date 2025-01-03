using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmbassyTokenizer.Models
{
    public class Token
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public int Number { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsCalled { get; set; }
    }

}