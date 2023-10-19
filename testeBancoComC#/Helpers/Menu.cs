using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testeBancoComC_.Model;

namespace testeBancoComC_.Helpers
{
    public class Menu
    {
        ClienteModel _clienteModel = new ClienteModel();
        PasseioModel _passeioModel = new PasseioModel();
        public void MostrarMenuPrincipal()
        {
            switch (MenuPrincipal())
            {
                case 1:
                    MostrarMenuCrud(_clienteModel);
                    break;
                case 2:
                    MostrarMenuCrud(_passeioModel);
                    break;
                default:
                    Console.WriteLine("Opção invalida");
                    Console.ReadLine();
                    break;
            }
        }

        public void MostrarMenuCrud(ICrud crud)
        {
            switch (MenuCrud())
            {
                case 1:
                    crud.Read();
                    break;
                case 2:
                    crud.Create();
                    break;
                case 3:
                    crud.Update();
                    break;
                case 4:
                    crud.Delete();
                    break;
                case 0:
                    MostrarMenuPrincipal();
                    break;
                default:
                    Console.WriteLine("Opção Invalida,Pressione uma tecla pra retonar");
                    Console.ReadLine();
                    MostrarMenuCrud(crud);
                    break;
            }
            Console.WriteLine("Pressione uma tecla para retonar ao menu");
            Console.ReadLine();
            MostrarMenuCrud(crud);
        }

        public int MenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("1 - Cliente");
            Console.WriteLine("2 - Passeio");
            Console.WriteLine("3 - Banho e Tosa");
            return Convert.ToInt32(Console.ReadLine());
        }
        public int MenuCrud()
        {
            Console.Clear();
            Console.WriteLine("1 - Visualizar");
            Console.WriteLine("2 - Cadastrar");
            Console.WriteLine("3 - Editar");
            Console.WriteLine("4 - Excluir");
            Console.WriteLine("0 - Retornar");
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
