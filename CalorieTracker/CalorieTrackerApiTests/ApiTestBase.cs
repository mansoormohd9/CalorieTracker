using AutoMapper;
using CalorieTrackerApi.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalorieTrackerApiTests
{
    public class ApiTestBase
    {
        public Mapper _mapper;

        public ApiTestBase()
        {
            // Initialize mapper
            _mapper = new Mapper(
                new MapperConfiguration(
                    configure => { configure.AddProfile<MappingProfile>(); }
                )
            );
        }
    }
}
