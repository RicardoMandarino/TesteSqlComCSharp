using testeBancoComC_.Model;

namespace testeBancoComC_
{
    internal class Program
    {
        static void Main(string[] args)
        {
           ClienteModel cliente = new ClienteModel();
            cliente.Read();
            cliente.Create();
            cliente.Read();
            
        }
    }
}