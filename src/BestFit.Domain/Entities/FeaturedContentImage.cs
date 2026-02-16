using BestFit.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Domain.Entities
{
    public class FeaturedContentImage : Image
    {
        public ICollection<FeaturedContent> FeaturedContents { get; set; } = new List<FeaturedContent>();

    }
}
