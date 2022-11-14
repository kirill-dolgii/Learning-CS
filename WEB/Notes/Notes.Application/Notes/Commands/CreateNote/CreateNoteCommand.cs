using MediatR;

namespace Notes.Application.Notes.Commands.CreateNote;

public class CreateNoteCommand : IRequest<Guid>
{
	public Guid   User { get; set; }
	public string Title  { get; set; }
	public string      Description      { get; set; }

}