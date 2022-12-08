﻿using FluentValidation;

namespace Notes.Application.Notes.Commands.UpdateNote;

public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
	public UpdateNoteCommandValidator()
	{
		RuleFor(updateNoteCommand => updateNoteCommand.Title).NotEmpty().MaximumLength(250);
		RuleFor(updateNoteCommand => updateNoteCommand.Id).NotEqual(Guid.Empty);
		RuleFor(updateNoteCommand => updateNoteCommand.User).NotEqual(Guid.Empty);
	}
}