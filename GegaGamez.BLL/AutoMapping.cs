﻿using AutoMapper;

namespace GegaGamez.BLL
{
    public static class AutoMapping
    {
        public static readonly Mapper Mapper;

        static AutoMapping()
        {
            throw new NotImplementedException();
            var mapConfig = new MapperConfiguration(config =>
            {
                //config.CreateMap<>();
            });

            Mapper = new Mapper(mapConfig);
        }
    }
}