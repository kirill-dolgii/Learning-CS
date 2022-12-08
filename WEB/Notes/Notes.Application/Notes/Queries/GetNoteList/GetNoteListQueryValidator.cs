using FluentValidation;

namespace Notes.Application.Notes.Queries.GetNoteList;

public class GetNoteListQueryValidator : AbstractValidator<GetNoteListQuery>
{
	public GetNoteListQueryValidator()
	{
		RuleFor(getNoteDetailsQuery => getNoteDetailsQuery.UserId).NotEqual(Guid.Empty);
	}
}