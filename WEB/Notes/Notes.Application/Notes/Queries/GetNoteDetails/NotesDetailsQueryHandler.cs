using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;

namespace Notes.Application.Notes.Queries
{
    public class NotesDetailsQueryHandler : IRequestHandler<GetNoteDetailsQuery, NoteDetailsVm>
    {
		public NotesDetailsQueryHandler(INotesDbContext dbContext, IMapper mapper)
		{
			_DbContext = dbContext;
			_Mapper = mapper;
		}
		public INotesDbContext _DbContext { get; set; }
		public IMapper       _Mapper  { get; set; }

		public async Task<NoteDetailsVm> Handle(GetNoteDetailsQuery request, 
										  CancellationToken cancellationToken)
		{
			var note = await _DbContext.Notes.FirstOrDefaultAsync(note => note.Id == request.Id);
			if (note == null || note.User != request.User) throw new Exception("Note is not found");

			return _Mapper.Map<NoteDetailsVm>(note);
		}
	}
}
