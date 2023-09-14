
using CRM.Controllers;
using CRM.Models;

namespace CRM
{
    internal class AdminView : AppController
    {
        AuthServices auth = new AuthServices();
        public void ShowMenu()
        {
            Console.WriteLine("\nMódulo de Usuarios:");
            Console.WriteLine("\n1. Crear Usuario");
            Console.WriteLine("2. Modificar Usuario");
            Console.WriteLine("3. Eliminar Usuario");
            Console.WriteLine("4. Listar Usuarios"); ;
            Console.WriteLine("5. Volver al Menú Principal");
            Console.Write("\nElija una opción: ");
        }
        public User CreateUser()
        {
            string username = string.Empty;
            string password = string.Empty;

            UserModel UserDB = new UserModel();
            #region CreateUserValidation
            do
            {
                
                Console.Write("\nIngrese el nombre de usuario: ");
                username = Console.ReadLine();

                if (string.IsNullOrEmpty(username))
                {
                    Console.Write("\nDebe ingresar un usuario válido.");
                    continue;
                }
                if (UserModel.UserExists(username))
                {
                    Console.Write("\nEl usuario que intenta registrar ya existe.\n");
                    continue;
                }

                do
                {
                    Console.Write("\nIngrese la contraseña: ");
                    password = auth.ReadPassword();

                } while (!auth.validatePassword(password));

                break;

            } while (true);
            #endregion
            #region CreateUser

            return new User
            {
                Username = username,
                Password = password
            };

            #endregion

        }
        public User? UpdateUser(User userToEdit) 
        {
            string username = userToEdit.Username;
            string password = userToEdit.Password;
            string userID = userToEdit.UserID;

            bool UserUpdated = false;
            bool exitWithoutSaving = false;

            
            while (!UserUpdated && !exitWithoutSaving)
            {
                Console.WriteLine($"\n\nModificando usuario: {username}\n");
                Console.WriteLine("1. Modificar nombre de usuario");
                Console.WriteLine("2. Modificar contraseña de usuario");
                Console.WriteLine("3. Guardar cambios");
                Console.WriteLine("4. Salir sin guardar");
                Console.Write("\nSeleccione una opción: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Write("\nIngrese el nuevo nombre del cliente: ");
                        username = Console.ReadLine();

                        if (string.IsNullOrEmpty(username)) continue;

                        break;
                    case "2":

                        bool passwordIsValid = false;

                        do
                        {
                            Console.Write("\nIngrese la nueva contraseña: ");
                            password = auth.ReadPassword();

                            passwordIsValid = auth.validatePassword(password);


                        } while (!passwordIsValid);


                        break;
                    case "3":
                        UserUpdated = true;
                        break;
                    case "4":

                        if (confirmAction("\n¿Deseas salir sin guardar los cambios? Presione Y para salir o presione cualquier tecla para continuar."))
                        {
                            exitWithoutSaving = true;
                            break;
                        }

                        break;
                    default:
                        Console.WriteLine("Opción seleccionada no válida, intente nuevamente");
                        break;
                }

            }




            if (UserUpdated)
            {

                return new User
                { 
                    UserID = userID,
                    Username = username,
                    Password = password
                };
            }
            else
            {
                return null;
            }
            
        }
        public  string GetUserRow()
        {
    
            string userRow;

            do
            {
                Console.Write("\nIngrese el ID del usuario que desea seleccionar: ");
                userRow = OnlyDigitsInput();
                if(string.IsNullOrEmpty(userRow))
                {
                    Console.WriteLine("\n\nID de usuario no válido. Intente nuevamente.");
                    continue;
                }

                break;

            } while (true);


            return userRow; 

        }
        public void ShowUsersList(List<User> users)
        {

            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine($"| ID |     Usuario     |     Fecha creación     |     Última vez modificado     |");
            Console.WriteLine("-------------------------------------------------------------------------------------");
            if (users.Count > 0)
            {
                for (int i = 0; i < users.Count; i++)
                {
                    Console.WriteLine($"|  {i, -2}| {users[i].Username,-14}  |  {users[i].CreatedAt,-15}   | {users[i].LastUpdated,-15}           |");
                    Console.WriteLine("-------------------------------------------------------------------------------------");
                }
            }
            else
            {
                Console.WriteLine("\nNo se han usuarios registrados.");
            }

        }
    }
}
