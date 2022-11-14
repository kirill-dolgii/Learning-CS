using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class NoteListVm
	{
		public NoteListVm() { }

		public NoteListVm(IList<NoteLookupDto> notes)
		{
			Notes = notes;
		}

		public IList<NoteLookupDto> Notes { get; set; }
	}
}
