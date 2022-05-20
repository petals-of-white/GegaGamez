using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.WebUI.Models.Auth;
using GegaGamez.WebUI.Models.Display;
using GegaGamez.WebUI.Models.ModifyModels;

namespace GegaGamez.WebUI.MappingProfiles;

public class MainProfile : Profile
{
    void User()
    {
        CreateMap<UpdateProfileModel, User>();
        CreateMap<RegisterUserModel, User>();
        CreateMap<User, UserModel>();

    }

    void Rating()
    {
        CreateMap<UpdateRatingModel, Rating>();
        CreateMap<Rating, RatingModel>();

    }
    void Game()
    {
        CreateMap<Game, GameModel>()
            .ForMember(dest => dest.ReleaseDate, act => act.MapFrom(src => DateOnly.FromDateTime(src.ReleaseDate)));

        CreateMap<EditGameModel, Game>()
            .ForMember(dest => dest.ReleaseDate, act => act.MapFrom(src => src.ReleaseDate.ToDateTime(TimeOnly.MinValue));

    }
    void Comment()
    {

        CreateMap<NewCommentModel, Comment>();

        CreateMap<Comment, CommentModel>().ReverseMap();
    }

    void Country()
    {
        CreateMap<Country, CountryModel>();
    }

    void Developer()
    {
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
    }

    void Collections()
    {
        CreateMap<DefaultCollection, DefaultCollectionModel>()
    .ForMember(dest => dest.Type, act => act.MapFrom(src => src.DefaultCollectionType));

        CreateMap<DefaultCollectionType, DefaultCollectionTypeModel>();
        CreateMap<UserCollection, UserCollectionModel>();

    }
    
    void Genre()
    {
        CreateMap<Genre, GenreModel>();
    }
    public MainProfile()
    {
        User();
        Rating();
        Game();
        Comment();
        Country();
        Developer();
        Collections();
        Genre();
    }

}
