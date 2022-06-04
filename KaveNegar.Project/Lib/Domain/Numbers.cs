using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaveNegar.Project.Domain
{
    [Table(nameof(Numbers), Schema = "dbo")]
    public class Numbers
    {
        [Key]
        public long Id { get; set; }
        public string Number { get; set; }
    }
}
