using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoredProcedCrud.Models
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public int Pincode { get; set; }
    }
}
