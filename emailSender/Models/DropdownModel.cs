using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace emailSender.Models
{
    public class DropdownModel
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string a_paterno { get; set; }
        public string a_materno { get; set; }
        public string email { get; set; }
    }
}