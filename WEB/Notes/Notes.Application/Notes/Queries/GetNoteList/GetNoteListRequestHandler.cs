using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Notes.Application.Interfaces;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class GetNoteListRequestHandler : IRequestHandler<GetNoteListQuery, NoteListVm>
    {
		public GetNoteListRequestHandler(IMapper mapper, INotesDbContext notesDbContext)
		{
			_mapper = mapper;
			_notesDbContext = notesDbContext;
		}
		private IMapper         _mapper         { get; set; }
		private INotesDbContext _notesDbContext { get; set; }

		public async Task<NoteListVm> Handle(GetNoteListQuery request, 
											 CancellationToken cancellationToken)
		{
			var notes = await _notesDbContext.Notes.Where(note => note.User == request.UserId)
												   .ProjectTo<NoteLookupDto>(_mapper.ConfigurationProvider)
												   .ToListAsync(cancellationToken);
			//var dtos   = notes.Select(note => _mapper.Map<NoteLookupDto>(note)).ToList();
			return new NoteListVm(notes: notes);

			//return ret;

		}
	}
}
