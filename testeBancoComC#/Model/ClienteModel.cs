using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using testeBancoComC_.Entity;

namespace testeBancoComC_.Model
{
    internal class ClienteModel : ICrud
    {
        public string conectionString = "Server=localhost;Database=PETSHOP;User=root;Password=root;";
        public void Create()
        {
            ClienteEntity cliente = new ClienteEntity();
            Console.WriteLine("Digite seu nome: ");
            cliente.NOME = Console.ReadLine();
            Console.WriteLine("Digite o nome do seu cachorro: ");
            cliente.NOME_CACHORRO = Console.ReadLine();
            Console.WriteLine("Digite seu telefone: ");
            cliente.TELEFONE = Console.ReadLine();

            using(MySqlConnection connection = new MySqlConnection(conectionString))
            {
                string sql = "INSERT INTO cliente VALUE (NULL, @NOME, @NOME_CACHORRO, @TELEFONE)";
                int linhas = connection.Execute(sql, cliente);
                Console.WriteLine($"Cliente inserido - {linhas} afetadas");
            }
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            using MySqlConnection con = new MySqlConnection(conectionString);
            {
                IEnumerable<ClienteEntity> clientes = con.Query<ClienteEntity>("SELECT * FROM cliente");
                foreach(ClienteEntity cliente in clientes)
                {
                    cliente.Mostrar();
                }
            }
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
