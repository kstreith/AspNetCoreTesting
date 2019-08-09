using System;
using System.ComponentModel.DataAnnotations;

namespace MiddlewareSample.Models
{
    public class ValueModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int? Age { get; set; }
    }
}
