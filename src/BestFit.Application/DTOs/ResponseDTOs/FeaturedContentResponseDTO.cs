using BestFit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Application.DTOs.ResponseDTOs
{
    public class FeaturedContentResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Heading { get; set; }
        public string SubHeading { get; set; }

        public string ImageUrl { get; set; }
    }
}
