using System.Reflection;
using CRM.Controllers;
using CRM.Models;

namespace CRM
{
    public class CustomerController : LogsHandler
    {

        private CustomerModel customerDB = new CustomerModel();
        private CustomerView customerView = new CustomerView();

        public void Run()
        {

            string currentMethod = MethodBase.GetCurrentMethod().Name;

            bool closeModule = false;

            Console.Clear();

            while(!closeModule) 
            {

               
                customerView.ShowMenu();
                string option = Console.ReadLine();


                switch (option)
                {
                    // Crear cliente
                    case "1":

                        

                        try
                        {

                            Console.Clear();

                            Console.WriteLine("Creando nuevo cliente:");

                            Customer newCustomer = customerView.CreateCustomer();
                            bool customerCreated = customerDB.CreateCustomer(newCustomer.customerName, newCustomer.customerDPI, newCustomer.customerBirthDate);

                            if(customerCreated) 
                            {
                                CreateNewLog(
                                    "INFO", 
                                    currentClass(this),
                                    currentMethod,
                                    $"Customer DPI {newCustomer.customerDPI} created successfully."
                                );

                                Console.Clear();
                                Console.WriteLine("Cliente creado exitosamente!");

                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ha ocurrido un error al crear el cliente.");
                            CreateNewLog(
                                "ERROR", 
                                currentClass(this),
                                currentMethod, 
                                $"Error creating customer: {ex}"
                            );
                        }

                        
                        break;
                    // Modificar cliente
                    case "2":


                        try
                        {
                            Console.Clear();
                            Console.WriteLine("\nMostrando listado de clientes: ");
                            customerView.ShowCustomerList(customerDB.GetCustomers());
                            // Buscar cliente por 
                            Customer updateCustomerView = customerView.UpdateCustomer(customerDB.GetCustomers());

                            if(updateCustomerView != null)
                            {
                                bool updateCustomer = customerDB.UpdateCustomer(

                                    updateCustomerView.customerName,
                                    updateCustomerView.customerDPI,
                                    updateCustomerView.customerBirthDate

                                );

                                if (updateCustomer)
                                {
                                    Console.Clear();
                                    Console.WriteLine("\nCambios guardados exitosamente!");
                                    CreateNewLog(
                                        "INFO",
                                        currentClass(this),
                                        currentMethod,
                                        $"Customer DPI {updateCustomerView.customerDPI} updated successfully."
                                    );
                                }
                                else
                                {
                                    Console.WriteLine($"No se guardaron los cambios debido a que no se encontró al cliente: {updateCustomerView.customerDPI}");
                                }
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Acción 'modificar cliente' cancelada.");
                            }

                            

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ha ocurrido un error al modificar el cliente.");
                            CreateNewLog(
                                "ERROR", 
                                currentClass(this), 
                                currentMethod, 
                                $"Error updating customer: {ex}"
                            );
                        }

                        



                        break;
                    // Eliminar cliente
                    case "3":

                        Console.Clear();
                        Console.WriteLine("\nMostrando listado de clientes: ");
                        customerView.ShowCustomerList(customerDB.GetCustomers());

                        string customerDPI = customerView.DeleteCustomerView();

              
                        
                            try
                            {
                                if (customerDB.GetCustomerByDPI(customerDPI) != null)
                                {

                                    AppController appController = new AppController();
                                    
                                    if(appController.confirmAction($"\n\n¿Está seguro de eliminar el cliente: {customerDPI}? No podrá revertir los cambios.\n\nPresione Y para confirmar, para cancelar pulse cualquier otra tecla."))
                                    {
                                        bool customerDeleted = customerDB.DeleteCustomer(customerDPI);

                                        if (customerDeleted)
                                        {
                                            Console.Clear();
                                            Console.WriteLine("\nCliente eliminado exitosamente!");
                                            CreateNewLog(
                                                "INFO",
                                                currentClass(this),
                                                currentMethod,
                                                $"Cliente DPI {customerDPI} deleted successfully."
                                            );
                                        }
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine($"Acción eliminar cliente '{customerDPI} cancelada.'");
                                    }

                                }
                                else
                                {
                                    Console.Clear();    
                                    Console.WriteLine($"No se ha encontrado el cliente con el número de DPI: {customerDPI}");
                                }
                            }
                            catch (Exception ex)
                            {

                                Console.WriteLine("Ha ocurrido un error al eliminar el cliente.");
                                CreateNewLog(
                                    "ERROR", 
                                    currentClass(this),
                                    currentMethod, 
                                    $"Error deleting customer: {ex}"
                                );
                            }

                        break;
                    // Listar clientes
                    case "4":
                        Console.Clear();
                        Console.WriteLine("\nMostrando listado de clientes:");
                        customerView.ShowCustomerList(customerDB.GetCustomers());
                        Console.WriteLine("\nPulse cualquier tecla para continuar");
                        Console.ReadKey(true);
                        Console.Clear();
                        break;
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
