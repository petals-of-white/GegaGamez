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
                config.CreateMap<DAL.Entities.Comment, Models.Comment>().ReverseMap();
                config.CreateMap<DAL.Entities.Country, Models.Country>().ReverseMap();
                config.CreateMap<DAL.Entities.DefaultCollection, Models.DefaultCollection>().ReverseMap();
                config.CreateMap<DAL.Entities.DefaultCollectionType, Models.DefaultCollectionType>().ReverseMap();
                config.CreateMap<DAL.Entities.Developer, Models.Developer>().ReverseMap();
                config.CreateMap<DAL.Entities.Game, Models.Game>().ReverseMap();
                config.CreateMap<DAL.Entities.Genre, Models.Genre>().ReverseMap();
                config.CreateMap<DAL.Entities.Rating, Models.Rating>().ReverseMap();
                config.CreateMap<DAL.Entities.User, Models.User>().ReverseMap();
                config.CreateMap<DAL.Entities.UserCollection, Models.UserCollection>().ReverseMap();
            });

            Mapper = new Mapper(mapConfig);
        }
    }
}
