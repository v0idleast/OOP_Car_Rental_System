using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_Yampol.Models
{
  public class Order
  {
    [Key]
    public int Id { get; set; }

    [ForeignKey("Car")]
    public int CarId { get; set; }

    [StringLength(100)]
    public string UserName { get; set; }

    public string PassportCode { get; set; }

    public decimal Days { get; set; }

    public bool IsBroken { get; set; }

    public bool IsDecline { get; set; }
    public virtual Car Car { get; set; }
  }

}
