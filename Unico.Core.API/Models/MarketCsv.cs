using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unico.Core.API.Models
{
    public class MarketCsv
    {
        [Index(0)]
        [Optional]
        public int Id { get; set; }
        [Index(1)]
        [Optional]
        public string LONG { get; set; } = "";
        [Index(2)]
        [Optional]
        public string LAT { get; set; } = "";
        [Index(3)]
        [Optional]
        public long SETCENS { get; set; } = 0;
        [Index(4)]
        [Optional]
        public long AREAP { get; set; } = 0;
        [Index(5)]
        [Optional]
        public int CODDIST { get; set; } = 0;
        [Index(6)]
        [Optional]
        public string DISTRITO { get; set; } = "";
        [Index(7)]
        [Optional]
        public int CODSUBPREF { get; set; } = 0;
        [Index(8)]
        [Optional]
        public string SUBPREFE { get; set; } = "";
        [Index(9)]
        [Optional]
        public string REGIAO5 { get; set; } = "";
        [Index(10)]
        [Optional]
        public string REGIAO8 { get; set; } = "";
        [Index(11)]
        [Optional]
        public string NOME_FEIRA { get; set; } = "";
        [Index(12)]
        [Optional]
        public string REGISTRO { get; set; } = "";
        [Index(13)]
        [Optional]
        public string LOGRADOURO { get; set; } = "";
        [Index(14)]
        [Optional]
        public string NUMERO { get; set; } = "";
        [Index(15)]
        [Optional]
        public string BAIRRO { get; set; } = "";
        [Index(16)]
        [Optional]
        public string REFERENCIA { get; set; } = "";
    }
}
