using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Entities.ViewModel.Player
{
    public class UpdatePlayerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PlayerNumber { get; set; }
        public int TeamId { get; set; }

    }
}
