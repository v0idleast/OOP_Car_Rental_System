using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_Yampol.Models
{
  public class DeclineReason
  {
    [Key]
    public int Id { get; set; }

    [ForeignKey("Car")]
    public int CarId { get; set; }

    [StringLength(100)]
    public string Reason { get; set; }

    public virtual Car Car { get; set; }
  }
}
