using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class Note
    {
		public Guid      User         { get; set; }
		public Guid      Id           { get; set; }
		public string    Title        { get; set; }
		public string    Details      { get; set; }
		public DateTime  CreationTime { get; set; }
		public DateTime? DateTime     { get; set; }

	}
}
