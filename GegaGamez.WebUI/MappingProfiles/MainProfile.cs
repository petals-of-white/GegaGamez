using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.WebUI.Models.Auth;
using GegaGamez.WebUI.Models.Display;
using GegaGamez.WebUI.Models.ModifyModels;

namespace GegaGamez.WebUI.MappingProfiles;

public class MainProfile : Profile
{
    private void Collections()
    {
        CreateMap<DefaultCollection, DefaultCollectionModel>()
    .ForMember(dest => dest.Type, act => act.MapFrom(src => src.DefaultCollectionType));

        CreateMap<DefaultCollectionType, DefaultCollectionTypeModel>();
        CreateMap<UserCollection, UserCollectionModel>();
    }

    private void Comment()
    {
        CreateMap<NewCommentModel, Comment>();

        CreateMap<Comment, CommentModel>().ReverseMap();
    }

    private void Country()
    {
        CreateMap<Country, CountryModel>();
    }

    private void Developer()
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

    private void Game()
    {
        CreateMap<Game, GameModel>()
            .ForMember(dest => dest.ReleaseDate, act => act.MapFrom(src => DateOnly.FromDateTime(src.ReleaseDate)));

        CreateMap<EditGameModel, Game>()
            .ForMember(dest => dest.ReleaseDate, act =>
            {
                act.MapFrom(src => src.ReleaseDate.ToDateTime(TimeOnly.MinValue));
            })
            .ForMember(dest => dest.Genres, act =>
            {
                act.MapFrom(src => src.GenreIds.Select(gid => new Genre { Id = gid }).ToHashSet());
            })
            .ReverseMap()
                .ForMember(src => src.ReleaseDate, act => act.MapFrom(dest => DateOnly.FromDateTime(dest.ReleaseDate)))
                .ForMember(src => src.GenreIds, act => act.MapFrom(dest => dest.Genres.Select(g => g.Id).ToHashSet()));
    }

    private void Genre()
    {
        CreateMap<Genre, GenreModel>();
    }

    private void Rating()
    {
        CreateMap<UpdateRatingModel, Rating>();
        CreateMap<Rating, RatingModel>();
    }

    private void User()
    {
        CreateMap<UpdateProfileModel, User>();
        CreateMap<RegisterUserModel, User>();
        CreateMap<User, UserModel>();
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
