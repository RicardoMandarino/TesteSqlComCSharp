using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testeBancoComC_.Entity
{
    public class ClienteEntity
    {
        public int ID { get; set; }
        public string NOME { get; set; }
        public string NOME_CACHORRO { get; set; }
        public string TELEFONE { get; set; }
        public void Mostrar()
        {
            Console.WriteLine($"{ID} - Nome: {NOME} Nome do Cachorro: {NOME_CACHORRO} Telefone: {TELEFONE}");
        }
    }
}
