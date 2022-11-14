using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand>
    {
		public UpdateNoteCommandHandler(INotesDbContext dbContext) => _DbContext = dbContext;
		public INotesDbContext _DbContext { get; set; }

		public async Task<Unit> Handle(UpdateNoteCommand request, 
								 CancellationToken cancellationToken)
		{
			var note = await _DbContext.Notes.FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);

			if (note == null || note.User != request.User) throw new Exception("note is not found");

			note.Title = request.Title;
			note.Details = request.Details;
			note.EditDate = DateTime.Now;

			await _DbContext.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
