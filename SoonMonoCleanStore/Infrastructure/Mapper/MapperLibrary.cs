﻿using Mapster;
using SharedKernel.UseCases.MapperInterface;
using System;
using System.Collections.Generic;

namespace Infrastructure.Mapper
{
    public class MapperLibrary : IMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return source.Adapt<TDestination>();
        }

        public IEnumerable<TDestination> MapEnumerable<TSource, TDestination>(IEnumerable<TSource> sourceList)
        {
            return sourceList.Adapt<IEnumerable<TDestination>>();
        }

        public List<TDestination> MapList<TSource, TDestination>(List<TSource> sourceList)
        {
            return sourceList.Adapt<List<TDestination>>();
        }

        // Add more mapping methods as needed
    }
}
