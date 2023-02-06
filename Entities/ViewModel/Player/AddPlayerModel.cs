using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel.Player
{
    public class AddPlayerModel
    {
        public string Name { get; set; }
        public int PlayerNumber { get; set; }
        public string Image { get; set; }
        public int TeamId { get; set; }
        public IFormFile Photo { get; set; }

    }
}
