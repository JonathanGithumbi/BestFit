using BestFit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Application.DTOs.RequestDTOs
{
    public class AddFeaturedContentRequestDTO
    {
        public string Name { get; set; }
        public string Heading { get; set; }
        public string SubHeading { get; set; }
        public Guid ImageId { get; set; }
        public DateTime RunFromDate { get; set; }
        public DateTime RunToDate { get; set; }


    }

}
