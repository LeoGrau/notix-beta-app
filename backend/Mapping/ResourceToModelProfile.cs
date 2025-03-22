using AutoMapper;
using Notix.Beta.API.Auth.Domain.Models;
using Notix.Beta.API.Notes.Domain.Models;
using Notix.Beta.API.Notes.Domain.Models.Intermediate;
using Notix.Beta.API.Notes.Resources.Create;
using Notix.Beta.API.Notes.Resources.Update;
using Notix.Beta.API.Notes.Resources.Update.Patch;
using RegisterRequest = Notix.Beta.API.Auth.Resources.Auth.RegisterRequest;

namespace Notix.Beta.API.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        // Notes
        CreateMap<UpdateNoteResource, Note>()
            .ForMember(model => model.NoteCategories, expression => expression.MapFrom(resource =>
                resource.NoteCategoriesId.Select(id => new NoteCategory
                {
                    CategoryId = id
                })));
        CreateMap<PatchNoteResource, Note>();
        CreateMap<CreateNoteResource, Note>()
            .ForMember(model => model.NoteCategories, expression => expression.MapFrom(resource =>
                resource.NoteCategoriesId.Select(id => new NoteCategory
                {
                    CategoryId = id
                })));
        
        // Category
        CreateMap<CreateCategoryResource, Category>();
        
        // Auth
        CreateMap<RegisterRequest, User>();
    }
}