using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rajanell.Core.Model
{
    public class Team
    {
        public Guid TeamId { get; set; }
        public Guid StadiumId { get; set; }
        public string Name { get; set; }

        public virtual Stadium Stadium { get; set; }
        public virtual List<Player> Players { get; set; }

    }
}
