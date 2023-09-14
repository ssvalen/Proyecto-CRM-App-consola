using CRM.Controllers;
using CRM.Models;
using System.Reflection;


namespace CRM
{
    public class AdminController : LogsHandler
    {

        private UserModel userModel = new UserModel();
        // Para controlar las impresiones de consola relacionadas con el modulo Admin
        private AdminView adminView = new AdminView();

        public void Run()
        {

            string currentMethod = MethodBase.GetCurrentMethod().Name;

            bool closeModule = false;

            Console.Clear();

            while(!closeModule) 
            {
                
                adminView.ShowMenu();
                string option = Console.ReadLine();

                switch (option)
                {
                    // Crear usuario
                    case "1": 

                        try
                        {
                            
                            User newUser = adminView.CreateUser();
                            bool userCreated = userModel.CreateCustomer(newUser.Username, newUser.Password);
                            Console.Clear();
                            if (userCreated)
                            {
                                Console.WriteLine("\nUsuario creado exitosamente!");
                                CreateNewLog("INFO", currentClass(this), currentMethod, $"User '{newUser.Username}' created successfully.");
                            }
                            else
                            {
                                Console.WriteLine("\nHa ocurrido un error al crear el usuario.");
                            }
                        }
                        catch (Exception ex)
                        { 
                            Console.WriteLine("\nHa ocurrido un error al crear el usuario.");
                            CreateNewLog(
                                "ERROR",
                                currentClass(this),
                                currentMethod,
                                $"Error creating user: {ex}"
                            );
                        }
                        


                        break;
                    // Modificar usuario
                    case "2":

                        try
                        {

                            Console.Clear();
                            Console.WriteLine("\nMostrando listado de clientes: ");

                            // Muestra la lista de usuarios
                            adminView.ShowUsersList(userModel.GetUsers());


                            while (true)
                            {
                                // Obtiene el número de fila del usuario a editar
                                string userRow = adminView.GetUserRow();

                                // Obtiene el objeto del usuario correspondiente al número de fila.
                                User? userToEdit = userModel.GetUserToEdit(userRow);

                                if (userToEdit != null)
                                {
                                    User? userToUpdate = adminView.UpdateUser(userToEdit);

                                    if(userToUpdate != null)
                                    {
                                        bool userUpdated = userModel.UpdateUser(
                                            userToUpdate.UserID,
                                            userToUpdate.Username,
                                            userToUpdate.Password
                                        );

                                        if(userUpdated) 
                                        {
                                            Console.Clear();
                                            Console.WriteLine($"Usuario '{userToUpdate.Username}' modificado exitosamente.");
                                            CreateNewLog("INFO", currentClass(this), currentMethod, $"User '{userToUpdate.Username}' updated successfully.");
                                            break;

                                        }
                                    } 
                                    else
                                    {
                                        break;
                                    }

                                }
                                else
                                {
                                    // Muestra un mensaje de error si no se encuentra el cliente y se repite el bucle.
                                    Console.WriteLine($"\n\nNo se ha encontrado ningún cliente con el ID: {userRow}");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // Captura cualquier error que ocurra en el bloque anterior y crea un registro en el log.
                            CreateNewLog(
                                "ERROR",
                                currentClass(this),
                                currentMethod,
                                $"Error throw while action 'Delete User': {ex}"
                             );
                        }

                        break;
                    // Eliminar usuario
                    case "3":

                        try
                        {

                            Console.Clear();
                            Console.WriteLine("\nMostrando listado de clientes: ");
                            adminView.ShowUsersList(userModel.GetUsers());

                            while (true)
                            {
                                // Obtiene el número de fila del usuario a editar
                                string userRow = adminView.GetUserRow();

                                // Obtiene el objeto del usuario correspondiente al número de fila.
                                User? userToEdit = userModel.GetUserToEdit(userRow);

                                if (userToEdit != null)
                                {
                                    // Crea una instancia de 'AppController' para gestionar la confirmación.
                                    AppController appController = new();

                                    string msg = $"\n\n¿Está seguro de eliminar el cliente: {userToEdit.Username}? No podrá revertir esta acción.\n\nPresione Y para confirmar o pulse cualquier otra tecla para cancelar.";

       
                                    if (appController.confirmAction(msg))
                                    {
                            
                                        bool userDeleted = userModel.DeleteUser(userRow: userToEdit.UserID);

                                        if (userDeleted)
                                        {
                                           
                                            Console.Clear();
                                            Console.WriteLine($"Usuario '{userToEdit.Username}' eliminado exitosamente");

                                           
                                            CreateNewLog(
                                                "INFO",
                                                currentClass(this),
                                                currentMethod,
                                                $"Delete user ID={userToEdit.UserID} AND username={userToEdit.Username} successfully."
                                            );

                                            // Sale del bucle.
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        // Limpia la consola y muestra un mensaje de cancelación.
                                        Console.Clear();
                                        Console.WriteLine("Acción 'eliminar usuario' cancelada");

                                        // Sale del bucle.
                                        break;
                                    }
                                }
                                else
                                {
                                    // Muestra un mensaje de error si no se encuentra el cliente y se repite el bucle.
                                    Console.WriteLine($"\n\nNo se ha encontrado ningún cliente con el ID: {userRow}");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // Captura cualquier error que ocurra en el bloque anterior y crea un registro en el log.
                            CreateNewLog(
                                "ERROR",
                                currentClass(this),
                                currentMethod,
                                $"Error while 'Delete User': {ex}"
                             );
                        }

                        break;
                    // Listar usuario
                    case "4":

                        Console.Clear();
                        Console.WriteLine("\nMostrando listado de clientes:");

                        adminView.ShowUsersList(userModel.GetUsers());
                        Console.WriteLine("\nPulse cualquier tecla para continuar");
                        Console.ReadKey(true);
                        Console.Clear();
                        break;
                    // Salir del modulo
                    case "5":
                        closeModule = true;
                        break;
                    default:
                        Console.WriteLine("\nOpción ingresada no válida, intente nuevamente.");
                        break;
                }

            }

        }

    }
}
