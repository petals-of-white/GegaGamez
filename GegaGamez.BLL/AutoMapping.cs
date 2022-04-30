using AutoMapper;
using GegaGamez.Shared.Entities;

namespace GegaGamez.BLL
{
    internal static class AutoMapping
    {
        public static readonly Mapper Mapper;

        static AutoMapping()
        {
            var mapConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Comment, Shared.BusinessModels.Comment>().ReverseMap();
                config.CreateMap<Country, Shared.BusinessModels.Country>().ReverseMap();
                config.CreateMap<DefaultCollection, Shared.BusinessModels.DefaultCollection>().ReverseMap();
                config.CreateMap<DefaultCollectionType, Shared.BusinessModels.DefaultCollectionType>().ReverseMap();
                config.CreateMap<Developer, Shared.BusinessModels.Developer>().ReverseMap();
                config.CreateMap<Game, Shared.BusinessModels.Game>().ReverseMap();
                config.CreateMap<Genre, Shared.BusinessModels.Genre>().ReverseMap();
                config.CreateMap<Rating, Shared.BusinessModels.Rating>().ReverseMap();
                config.CreateMap<User, Shared.BusinessModels.User>().ReverseMap();
                config.CreateMap<UserCollection, Shared.BusinessModels.UserCollection>().ReverseMap();
            });

            Mapper = new Mapper(mapConfig);
        }
    }
}
