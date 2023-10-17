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
            Read();
            Console.WriteLine("Digite o id para excluir");
            int id = Convert.ToInt32(Console.ReadLine());
            using MySqlConnection connection = new MySqlConnection(conectionString);
            {
                var parameters = new { Id = id };
                string sql = "DELETE FROM CLIENTE WHERE ID = @Id";
                connection.Execute(sql, parameters);
                Console.WriteLine("Cliente excluido com sucesso");
            }
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

        private ClienteEntity getById(int id)
        {
            using (MySqlConnection con = new MySqlConnection(conectionString))
            {
                string sql = "SELECT ID as Id, NOME as Nome FROM CLIENTE WHERE ID = @Id";
                var parameters = new { Id = id };
                return con.QueryFirst<ClienteEntity>(sql, parameters);
            }
        }

        private ClienteEntity getClienteEntity()
        {
            Console.WriteLine("Digite o id para editar");
            int id = Convert.ToInt32(Console.ReadLine());
            return getById(id);
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(conectionString);
        }
        private ClienteEntity updateClienteNome(ClienteEntity cliente)
        {
            Console.WriteLine($"Digite o novo nome para o cliente {cliente.NOME}");
            cliente.NOME = Console.ReadLine();
            return cliente;
        }

        private int Execute(string sql, object obj)
        {
            using (MySqlConnection con = GetConnection())
            {
                return con.Execute(sql, obj);
            }
        }

        public void Update()
        {
            Read();
            ClienteEntity cliente = getClienteEntity();           
            cliente = updateClienteNome(cliente);

            string sql = "UPDATE CLIENTE SET NOME = @Nome WHERE ID = @Id";
            Execute(sql, cliente);
            Console.WriteLine("Tipo alterado com sucesso!");
        }
    }
}
