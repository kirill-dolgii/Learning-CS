using MediatR;
using Notes.Application.Interfaces;

namespace Notes.Application.Notes.Commands.CreateNote
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
    {
		private INotesDbContext _context;

		public CreateNoteCommandHandler(INotesDbContext context) => _context = context;

		public async Task<Guid> Handle(CreateNoteCommand request, 
									   CancellationToken cancellationToken)
		{
			var note = new Note()
			{
				CreationTime = DateTime.Now,
				EditDate = null,
				Details = request.Description,
				Id = Guid.NewGuid(),
				Title = request.Title,
				User = request.User
			};

			await _context.Notes.AddAsync(note, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);
			return note.Id;
		}
	}
}
