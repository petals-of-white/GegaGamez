using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.WebUI.Models.Auth;
using GegaGamez.WebUI.Models.Display;
using GegaGamez.WebUI.Models.ModifyModels;

namespace GegaGamez.WebUI.MappingProfiles;

public class MainProfile : Profile
{
    public MainProfile()
    {
        // Create my maps here

        CreateMap<UpdateProfileModel, User>();

        CreateMap<RegisterUserModel, User>();

        CreateMap<User, UserModel>();


        CreateMap<NewCommentModel, Comment>();

        CreateMap<Comment, CommentModel>().ReverseMap();


        CreateMap<Country, CountryModel>();

        CreateMap<DefaultCollection, DefaultCollectionModel>()
            .ForMember(dest => dest.Type, act => act.MapFrom(src => src.DefaultCollectionType));

        CreateMap<DefaultCollectionType, DefaultCollectionTypeModel>();

        CreateMap<Developer, DeveloperModel>()
            .ForMember(dest => dest.BeginDate,
                       act => act.MapFrom(src => DateOnly.FromDateTime(src.BeginDate)))
            .ForMember(dest => dest.EndDate,
                       act => act.MapFrom(
                            delegate (Developer dev,
                                      DeveloperModel devModel)
                            {
                                DateOnly? result = dev.EndDate.HasValue ? DateOnly.FromDateTime(dev.EndDate.Value) : null;
                                return result;
                            }));

        CreateMap<Game, GameModel>()
            .ForMember(dest => dest.ReleaseDate, act => act.MapFrom(src => DateOnly.FromDateTime(src.ReleaseDate)));

        CreateMap<Genre, GenreModel>();

        CreateMap<Rating, RatingModel>();

        CreateMap<UserCollection, UserCollectionModel>();

    }
}
