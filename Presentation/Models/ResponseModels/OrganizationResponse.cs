using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models.ResponseModels
{
    public class OrganizationResponse
    {
        public int Id { get; set; }
        public string Organization_type { get; set; }
        public string Delete { get; set; }
        public string Edite { get; set; }
    }
}
