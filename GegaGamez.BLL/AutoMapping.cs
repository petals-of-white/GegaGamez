using AutoMapper;

namespace GegaGamez.BLL
{
    internal static class AutoMapping
    {
        public static readonly Mapper Mapper;

        static AutoMapping()
        {
            var mapConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<DAL.Entities.Comment, Shared.BusinessModels.Comment>().ReverseMap();
                config.CreateMap<DAL.Entities.Country, Shared.BusinessModels.Country>().ReverseMap();
                config.CreateMap<DAL.Entities.DefaultCollection, Shared.BusinessModels.DefaultCollection>().ReverseMap();
                config.CreateMap<DAL.Entities.DefaultCollectionType, Shared.BusinessModels.DefaultCollectionType>().ReverseMap();
                config.CreateMap<DAL.Entities.Developer, Shared.BusinessModels.Developer>().ReverseMap();
                config.CreateMap<DAL.Entities.Game, Shared.BusinessModels.Game>().ReverseMap();
                config.CreateMap<DAL.Entities.Genre, Shared.BusinessModels.Genre>().ReverseMap();
                config.CreateMap<DAL.Entities.Rating, Shared.BusinessModels.Rating>().ReverseMap();
                config.CreateMap<DAL.Entities.User, Shared.BusinessModels.User>().ReverseMap();
                config.CreateMap<DAL.Entities.UserCollection, Shared.BusinessModels.UserCollection>().ReverseMap();
            });

            Mapper = new Mapper(mapConfig);
        }
    }
}
