using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class Organization
    {
        [Key]
        public int Id { get; set; }
        public string organization_id { get; set; }
        public string organization_type { get; set; }
        public string business_id { get; set; }
        public string business_type { get; set; }
    }
}
