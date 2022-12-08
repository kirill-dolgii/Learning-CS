using FluentValidation;

namespace Notes.Application.Notes.Queries.GetNoteDetails;

public class GetNotesDetailsQueryValidator : AbstractValidator<GetNoteDetailsQuery>
{
	public GetNotesDetailsQueryValidator()
	{
		RuleFor(getNoteDetailsQuery => getNoteDetailsQuery.Id).NotEqual(Guid.Empty);
		RuleFor(getNoteDetailsQuery => getNoteDetailsQuery.User).NotEqual(Guid.Empty);
	}
}