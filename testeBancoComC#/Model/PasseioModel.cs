using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testeBancoComC_.Entity;
using testeBancoComC_.Helpers;

namespace testeBancoComC_.Model
{
    internal class PasseioModel : DataBase, ICrud
    {   
        private string ChangeValue(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                Console.WriteLine($"Atual = {value} deseja alterar ? S/N");
                char resposta = Convert.ToChar(Console.ReadLine().ToUpper());
                if (resposta == 'S')
                {
                    Console.WriteLine("Digite o novo valor");
                    value = Console.ReadLine();
                }
            }
            else
            {
                value = Console.ReadLine();
            }
            return value;
        }
        private double ChangeValue(double value)
        {
            if (value == null)
            {
                Console.WriteLine($"Atual = {value} deseja alterar ? S/N");
                char resposta = Convert.ToChar(Console.ReadLine().ToUpper());
                if (resposta == 'S')
                {
                    Console.WriteLine("Digite o novo valor");
                    value = Convert.ToDouble(Console.ReadLine());
                }
               
            }
            else value = Convert.ToDouble(Console.ReadLine());
            return value;
        }
        private int ChangeCliente(PasseioEntity passeio)
        {   
            ClienteModel cliente = new();
            if (passeio.CLIENTE_ID > 0)
            {
                Console.WriteLine($"Atual = {passeio.CLIENTE.NOME}  deseja alterar ? S/N");
                char resposta = Convert.ToChar(Console.ReadLine().ToUpper());
                if (resposta == 'S')
                {
                    cliente.Read();
                    Console.WriteLine("Digite o id do cliente");
                    passeio.CLIENTE.ID = Convert.ToInt32(Console.ReadLine());
                }
                
            }
            else
            {
                cliente.Read();
                Console.WriteLine("Digite o id do cliente");
                passeio.CLIENTE_ID = Convert.ToInt32(Console.ReadLine());
            }
            return passeio.CLIENTE_ID;
        }
        private PasseioEntity Popular(PasseioEntity passeio)
        {
            Console.WriteLine("Digite o nome do passeador:");
            passeio.NOME = ChangeValue(passeio.NOME);
            Console.WriteLine("Digite o local do passeio:");
            passeio.LOCAL_PASSEIO = ChangeValue(passeio.LOCAL_PASSEIO);
            Console.WriteLine("Digite o preço do passeio:");
            passeio.PRECO = ChangeValue(passeio.PRECO);

            passeio.CLIENTE_ID = ChangeCliente(passeio);
            return passeio;
        }
        public void Create()
        {
            PasseioEntity passeio = new();
            passeio = Popular(passeio);
            string sql = "INSERT INTO PASSEADOR VALUE (NULL, @NOME, @LOCAL_PASSEIO, @PRECO, @CLIENTE_ID)";
            int linhas = this.Execute(sql, passeio);
            Console.WriteLine($"Passeio incluido com sucesso - {linhas} linhas afetadas");
        }

        public void Delete()
        {
            var parameters = new { Id = GetIndex() };
            string sql = "DELETE FROM PASSEADOR WHERE ID = @ID";
            this.Execute(sql, parameters);
            Console.WriteLine("Passeio deletado com sucesso");
        }
        private IEnumerable<PasseioEntity> ListPasseioEntity()
        {
            string sql = "SELECT * FROM PASSEADOR P JOIN CLIENTE C WHERE P.CLIENTE_ID = C.ID ";
            return this.GetConnection().Query<PasseioEntity, ClienteEntity, PasseioEntity>(
                sql,
                (passeio, cliente) =>
                {
                    passeio.CLIENTE = cliente;
                    return passeio;
                }
             );
        }
        
        public void Read()
        {
            foreach(var passeio in ListPasseioEntity())
            {
                Console.WriteLine($"{passeio.ID} - Passeador: {passeio.NOME} - Local: {passeio.LOCAL_PASSEIO} - Preço: {passeio.PRECO}R$ - Cliente: {passeio.CLIENTE.NOME} - Cachorro: {passeio.CLIENTE.NOME_CACHORRO}");
            }
        }
        private PasseioEntity GetById(int id = 0)
        {
            if (id == 0)
            {
                id = GetIndex();
            }
            return ListPasseioEntity().Where(o => o.ID == id).ToList()[0];

        }
        private int GetIndex()
        {
            Read();
            Console.WriteLine("Digite o id para continuar");
            return Convert.ToInt32(Console.ReadLine());
        }
        public void Update()
        {
            PasseioEntity passeio = Popular(GetById());
            string sql = "UPDATE PASSEADOR SET NOME = @NOME, LOCAL_PASSEIO = @LOCAL_PASSEIO, PRECO = @PRECO, CLIENTE_ID = @CLIENTE_ID WHERE ID = @ID";
            int linhas = this.Execute(sql, passeio);
            Console.WriteLine($"Produto atualizado - {linhas} linhas afetadas");
        }
    }
}
