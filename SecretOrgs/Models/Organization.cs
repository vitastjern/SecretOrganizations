using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecretOrgs.Models
{
    public class Organization
    {
        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }

        [Display(Name = "Organization ID")]
        public int Id { get; set; }

    }
}