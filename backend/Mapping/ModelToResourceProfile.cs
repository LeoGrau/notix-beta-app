using AutoMapper;
using Notix.Beta.API.Notes.Domain.Models;
using Notix.Beta.API.Notes.Domain.Models.Intermediate;
using Notix.Beta.API.Notes.Resources.Show.Basic;
using Notix.Beta.API.Notes.Resources.Show.Detailed;

namespace Notix.Beta.API.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        // Note
        CreateMap<Note, BasicNoteResource>();
        CreateMap<Note, DetailedNoteResource>();
        
        // Category
        CreateMap<Category, BasicCategoryResource>();
        
        // Note Category
        CreateMap<NoteCategory, DetailedNoteCategoryResource>()
            .ForMember(resource => resource.Id, expression => expression.MapFrom(category => category.CategoryId))
            .ForMember(resource => resource.Name, expression => expression.MapFrom(category => category.Category.Name));
    }
}