using MediatR;

namespace Notes.Application.Notes.Queries.GetNoteList;

public class GetNoteListQuery : IRequest<NoteListVm>, IRequest<NoteDetailsVm>
{
	public Guid UserId { get; set; }
}