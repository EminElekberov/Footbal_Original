using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel.Team
{
    public class UpdateTeamModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CoachId { get; set; }
    }
}
