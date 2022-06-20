using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unico.Core.API.Data;
using Unico.Core.API.Models;

namespace Unico.Core.API.Profiles
{
    /// <summary>
    /// Class type profile
    /// </summary>
    public class MarketProfile : Profile
    {
        /// <summary>
        /// Method to create the Map between the models in service
        /// </summary>
        public MarketProfile()
        {
            CreateMap<MarketRequest, Market>()
                .ReverseMap();
            CreateMap<MarketRequest, MarketCsv>()
               .ReverseMap();
            CreateMap<MarketResponse, Market>()
                .ReverseMap();
            CreateMap<Market, MarketCsv>()
                .ReverseMap()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForSourceMember(x => x.Id, y => y.DoNotValidate());
        }
    }
}
