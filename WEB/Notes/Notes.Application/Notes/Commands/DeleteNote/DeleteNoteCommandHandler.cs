using MediatR;
using Notes.Application.Interfaces;

namespace Notes.Application.Notes.Commands.DeleteNote;

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
{
	public INotesDbContext _DbContext { get; set; }

	public async Task<Unit> Handle(DeleteNoteCommand request, 
							 CancellationToken cancellationToken)
	{
		var note = await _DbContext.Notes.FindAsync(new object[] { request.Id }, cancellationToken);

		if (note == null || note.User != request.User) throw new Exception("note is not found");

		_DbContext.Notes.Remove(note);
		await _DbContext.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}