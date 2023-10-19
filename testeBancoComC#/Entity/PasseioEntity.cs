using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testeBancoComC_.Entity
{
    internal class PasseioEntity
    {
        public int ID { get; set; }
        public string NOME { get; set; }
        public string LOCAL_PASSEIO { get; set; }
        public double PRECO { get; set; }
        public int CLIENTE_ID { get; set; }
        
        public ClienteEntity CLIENTE { get; set; }
    }
}
