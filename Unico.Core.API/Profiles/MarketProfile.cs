using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unico.Core.API.Data;
using Unico.Core.API.Models;

namespace Unico.Core.API.Profiles
{
    public class MarketProfile : Profile
    {
        public MarketProfile()
        {
            CreateMap<MarketRequest, Market>()
                .ReverseMap();
            CreateMap<MarketRequest, MarketCsv>()
               .ReverseMap();
            CreateMap<MarketResponse, Market>()
                .ReverseMap();
            CreateMap<Market, MarketCsv>()
               .ReverseMap();

        }
    }
}
