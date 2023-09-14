using CRM.Models;


namespace CRM.Controllers
{
    internal class AppController : AuthServices
    {

        public bool Run()
        {

            bool closeApp = false;

            while(!closeApp) 
            {
                AppMenu();
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":

                        AdminController adminController = new AdminController();
                        adminController.Run();

                        break;
                    case "2":

                        CustomerController customerController = new CustomerController();
                        customerController.Run();
                        break;
                    case "3":
                        closeApp = confirmAction("Deseas cerrar sesíon? Pulse Y para confirmar o presione cualquier otra tecla para cancelar.");
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Opción no valida, intente otra opción.");
                        break;
                }
            }

           



            return false;

        }
        public void AppMenu()
        {

            Console.WriteLine("Menú Principal:\n");
            Console.WriteLine("1. Módulo administración");
            Console.WriteLine("2. Módulo clientes");
            Console.WriteLine("3. Cerrar sesión");
            Console.Write("\nElija una opción: ");
        }
        /* 
           La función confirmAction muestra un mensaje al usuario y espera a que el usuario confirme
           la acción presionando la tecla 'Y'. Retorna true si se confirma la acción, o false si no.
        */
        public bool confirmAction(string msg) 
        {
            ConsoleKeyInfo key;
            Console.WriteLine(msg);
            key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Y)
            {
                return true;
            }

            return false;
        }
        /* 
           La función OnlyDigitsInput permite al usuario ingresar solo dígitos numéricos en la consola.
           Retorna una cadena que contiene los dígitos ingresados por el usuario.
        */
        public static string OnlyDigitsInput()
        {

            int pos = 0;
            string input = string.Empty;

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Enter) break;

                else if (char.IsDigit(keyInfo.KeyChar))
                {
                    Console.Write(keyInfo.KeyChar);
                    input += keyInfo.KeyChar;
                    pos++;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    

                    if(pos > 0) 
                    {
                        pos--;
                        input.Substring(0, input.Length - 1);
                        // Mover el cursor atrás y borrar el carácter en la pantalla
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    }

                    
                }

            }


            return input;

        }

    }
}
