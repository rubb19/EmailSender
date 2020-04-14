using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace emailSender.Models
{
    public class EmailFormModel
    {
        //[Required, Display(Name = "Your name")]
        //public string FromName { get; set; }

        //[Required, Display(Name = "Your email"), EmailAddress]
        //public string FromEmail { get; set; }

        
        

        public int id { get; set; }
        public string nombre { get; set; }
        public string a_paterno { get; set; }
        public string a_materno { get; set; }
        public string email { get; set; }
        [Required, Display(Name = "Your message")]
        public string Message { get; set; }


    }
}
    


