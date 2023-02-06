using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Player> Players { get; set; }
        public  Coach Coach { get; set; }
        public int CoachId { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDateOnUTC { get; set; }
        public DateTime? ModifiedDateOnUTC { get; set; }
    }
}
