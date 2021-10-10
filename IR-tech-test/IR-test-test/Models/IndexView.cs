using System;
using System.ComponentModel.DataAnnotations;

namespace IR_tech_test.Models
{
  public class IndexView
  {
    [Required]
    [MinLength(1)]
    [RegularExpression("^[0-9]*$", ErrorMessage = "UPRN must be numeric")]
    public double Depth { get; set; }
  }
}
