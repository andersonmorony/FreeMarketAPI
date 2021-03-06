using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unico.Core.API.Models
{
    public class MarketBase
    {
        public string LONG { get; set; }
        public string LAT { get; set; }
        public long SETCENS { get; set; }
        public long AREAP { get; set; }
        public int CODDIST { get; set; }
        public string DISTRITO { get; set; }
        public int CODSUBPREF { get; set; }
        public string SUBPREFE { get; set; }
        public string REGIAO5 { get; set; }
        public string REGIAO8 { get; set; }
        public string NOME_FEIRA { get; set; }
        public string REGISTRO { get; set; }
        public string LOGRADOURO { get; set; }
        public string NUMERO { get; set; }
        public string BAIRRO { get; set; }
        public string REFERENCIA { get; set; }
    }
}
