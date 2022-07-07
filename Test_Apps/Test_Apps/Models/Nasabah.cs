using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Test_Apps.Models
{
    public partial class Nasabah
    {
        public Nasabah()
        {
            Transaksis = new HashSet<Transaksi>();
        }

        [Required]
        [Display(Name = "Account Id")]
        public int AccountId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public virtual ICollection<Transaksi> Transaksis { get; set; }
    }
}
