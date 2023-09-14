using System.Text.RegularExpressions;

namespace CRM.Models
{

    class AuthServices
    {
        /*
           El método ReadPassword permite al usuario ingresar una contraseña de forma segura en la consola.
           Muestra asteriscos (*) en lugar de los caracteres de la contraseña mientras se escribe.
           Retorna la contraseña ingresada por el usuario como una cadena.
        */
        public string ReadPassword()
        {

            string password = "";

            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter && !char.IsControl(key.KeyChar))
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                { //Para borrar un caracter mientras escribe

                    // Mueve el ¨cursor¨ atras y escribe un espacio en blanco para limpiar el asterisco
                    password = password.Substring(0, password.Length - 1);
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    Console.Write(" ");
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                }

            } while (key.Key != ConsoleKey.Enter);

            return password;
        }
        /*
           El método validatePassword valida una contraseña según ciertos criterios:
           - Debe tener entre 8 y 13 caracteres.
           - Debe contener al menos una letra mayúscula.
           - Debe contener al menos un carácter especial.
           Retorna true si la contraseña es válida según estos criterios, de lo contrario, retorna false y muestra mensajes de error en la consola.
        */
        public bool validatePassword(string password)
        {
            bool response = true;

            if (password.Length < 8 || password.Length > 13)
            {
                Console.WriteLine("\nLa contraseña debe tener entre 8 y 13 caracteres.");
                response = false;
            }

            // Validar mayúscula
            if (!Regex.IsMatch(password, "[A-Z]"))
            {
                Console.WriteLine("\nLa contraseña debe contener al menos una letra mayúscula.");
                response = false;
            }

            // Validar carácter especial
            if (!Regex.IsMatch(password, "[\\W_]"))
            {
                Console.WriteLine("\nLa contraseña debe contener al menos un carácter especial.");
                response = false;
            }


            return response;
        }

    }

}
