using CRM.Models;


namespace CRM.Controllers
{

    internal class AuthController
    {
        
        private AuthServices authServices = new();
        private UserModel userModel = new();
        
        public bool Auth()
        {
            int loginAttempts = 3;
            string username = string.Empty;
            string password = string.Empty;
            bool isLogged = false;

            

            while (loginAttempts > 0 && !isLogged)
            {
               
                

                Console.Write("Usuario: ");
                username = Console.ReadLine();
                Console.Write("Contaseña: ");
                password = authServices.ReadPassword();
                
                if (userModel.Authenticate(username, password))
                {

                    isLogged = true;

                    Console.Clear();
                    Console.WriteLine($"Bienvenido al sistema Upana CRM: {username}\n");
                    
                } 
                else
                {
        
                    loginAttempts--;
                    Console.Clear();
                    Console.WriteLine($"Acceso denegado. Intentos restantes: {loginAttempts}\n");
                }

            }

            return isLogged;

        }
    }
}
