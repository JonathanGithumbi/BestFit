using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Shared.DTOs.RequestDTOs
{
    public class SearchContentRequestDTO
    {
        public string Keyword { get; set; }
        public IFormFile File { get; set; }//For image based searches
    }
}
