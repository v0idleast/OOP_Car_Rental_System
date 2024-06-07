using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_Yampol.Models
{
  public class Car
  {
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; }

    public double Price { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
    public virtual ICollection<DeclineReason> DeclineReasons { get; set; }
  }
}
