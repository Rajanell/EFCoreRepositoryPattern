using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rajanell.Core.Model
{
    public class Player
    {
        public Guid PlayerId { get; set; }
        public Guid? TeamId { get; set; }

        public string Name { get; set; }
        public int ShirtNumber { get; set; }
        public Position Position { get; set; }

        public virtual Team Team { get; set; }
    }
}
