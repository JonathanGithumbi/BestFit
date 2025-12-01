using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Application.DTOs
{
    public class CategoryResponseDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
