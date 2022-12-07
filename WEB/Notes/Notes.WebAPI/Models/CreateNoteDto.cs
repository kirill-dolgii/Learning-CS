using AutoMapper;
using Notes.Application.Common.Mapping;
using Notes.Application.Notes.Commands.CreateNote;

namespace Notes.WebAPI.Models;

public class CreateNoteDto : IMapWith<CreateNoteCommand>
{
    public string Title { get; set; }
    public string Details { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateNoteDto, CreateNoteCommand>()
            .ForMember(command => command.Title, opt => opt.MapFrom(dto => dto.Title))
            .ForMember(command => command.Details, opt => opt.MapFrom(dto => dto.Details)); 

    }
}