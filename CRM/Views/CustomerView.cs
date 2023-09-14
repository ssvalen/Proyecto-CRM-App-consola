using CRM.Controllers;
using CRM.Models;

namespace CRM
{
    internal class CustomerView : AppController
    {
        public void ShowMenu()
        {
            Console.WriteLine("\nMódulo clientes:\n");
            Console.WriteLine("1. Crear cliente");
            Console.WriteLine("2. Modificar cliente");
            Console.WriteLine("3. Eliminar cliente");
            Console.WriteLine("4. Listar clientes");
            Console.WriteLine("5. Volver al Menú Principal");
            Console.Write("\nElija una opción: ");
        }
        public Customer CreateCustomer()
        {
            string customerName = string.Empty;
            string customerDPI = string.Empty;
            string birthDate = string.Empty;

            do
            {
                #region CustomerDPIValidation
                Console.Write("\nIngrese el DPI del cliente: ");
                // OnlyNumbersInput() unicamente permite que el usuario ingrese digitos
                customerDPI = OnlyDigitsInput();

                
                if (customerDPI.Length != 13)
                {
                    Console.WriteLine("\nDebe ingresar un DPI válido de 13 dígitos.");
                    continue;
                }

                CustomerModel CustomerDB = new CustomerModel();
                if(CustomerDB.CustomerExists(customerDPI))
                {
                    Console.WriteLine("\nEl DPI ingresado ya se encuentra registrado.");
                    continue;
                }
                #endregion
                #region CustomerNameValidation
                Console.Write("\n\nIngrese el nombre del cliente: ");
                customerName = Console.ReadLine();

                if(string.IsNullOrEmpty(customerName))
                {
                    Console.WriteLine("\nDebe ingresar un nombre válido.");
                    continue;
                }
                #endregion
                #region CustomerBirthDayValidation

                Console.WriteLine("\n\nIngrese la fecha de nacimiento del cliente (DD/MM/YYYY): \n");
                DateInput DateInput = new DateInput();
                birthDate = DateInput.loadInput();

                if (!DateInput.IsValidDate(birthDate))
                {
                    Console.WriteLine("\nDebe ingresar una fecha válida en formato DD/MM/YYYY.");
                    continue;
                }

                break;

                #endregion

                

            } while (true);

            #region CreateCustomer

            return new Customer
            {
                customerName = customerName,
                customerDPI = customerDPI,
                customerBirthDate = birthDate,
            };

            #endregion

        }
        public Customer? UpdateCustomer(List<Customer> customers) 
        {
            string customerName = string.Empty;
            string customerBirthDay = string.Empty;
            string customerDPI = string.Empty;

            bool UserUpdated = false;
            bool exitWithoutSaving = false;

            // Buscar cliente por DPI
            do
            {
                Console.Write("\nIngrese el DPI del cliente que desea editar: ");
                customerDPI = OnlyDigitsInput();

                if (customerDPI.Length != 13)
                {
                    Console.WriteLine("\n\nDebe ingresar un número de DPI válido.");
                    continue;
                }

                Customer customerToEdit = customers.FirstOrDefault(customer => customer.customerDPI == customerDPI);

                if (customerToEdit == null)
                {


                    string msg = "\n\nNo se ha encontrado al cliente ingresado. Desea salir de 'Modificar cliente'? \n\nPresione Y para salir o cualquier otra tecla para continuar.";
                    if(!confirmAction(msg))
                    {
                        continue;
                    }
                    else
                    {
                        return null;
                    }

                    
                }
                else
                {
                    customerName = customerToEdit.customerName;
                    customerDPI = customerToEdit.customerDPI;
                    customerBirthDay = customerToEdit.customerBirthDate;

                }

                while (!UserUpdated && !exitWithoutSaving)
                {
                    Console.WriteLine($"\n\nModificando cliente: {customerName}\n");
                    Console.WriteLine("1. Modificar nombre del cliente");
                    Console.WriteLine("2. Modificar fecha de nacimiento");
                    Console.WriteLine("3. Guardar cambios");
                    Console.WriteLine("4. Salir sin guardar");
                    Console.Write("\nSeleccione una opción: ");

                    string option = Console.ReadLine();

                    switch (option)
                    {
                        case "1":
                            Console.Write("\nIngrese el nuevo nombre del cliente: ");
                            customerName = Console.ReadLine();

                            if (string.IsNullOrEmpty(customerName)) continue;

                            break;
                        case "2":
                            Console.WriteLine("\nIngrese la fecha de nacimiento del cliente (DD/MM/YYYY): \n");
                            DateInput DateInput = new DateInput();
                            customerBirthDay = DateInput.loadInput();

                            if (DateInput.IsValidDate(customerBirthDay))
                            {
                                Console.WriteLine("\nDebe ingresar una fecha válida en formato DD/MM/YYYY.");
                                continue;
                            }
                            break;
                        case "3":
                            UserUpdated = true;
                            break;
                        case "4":
                            exitWithoutSaving = true;
                            break;
                        default:
                            Console.WriteLine("Opción seleccionada no válida, intente nuevamente");
                            break;
                    }


                }

                break;

            } while (true);

            if(UserUpdated && !exitWithoutSaving)
            {
                return new Customer
                {
                    customerName = customerName,
                    customerBirthDate = customerBirthDay,
                    customerDPI = customerDPI
                };
            }
            else
            {
                return null;
            }
            
        }
        public string DeleteCustomerView()
        {
            string customerDPI = string.Empty;

            do
            {
                Console.Write("\nIngrese el DPI del cliente que desea eliminar: ");
                customerDPI = OnlyDigitsInput();

                if (customerDPI.Length != 13 || string.IsNullOrEmpty(customerDPI))
                {
                    Console.WriteLine("\n\nDebe ingresar un número de DPI válido.");
                    continue;
                }

                
                break;


            } while (true);

           
             return customerDPI;
             
          

            

        }
        public void ShowCustomerList(List<Customer> customers)
        {
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("| Indice |      DPI       |     Nombre     | Fecha Nacimiento |");
            Console.WriteLine("---------------------------------------------------------------");
            if (customers.Count > 0)
            {
                for (int i = 0; i < customers.Count; i++)
                {
                    Console.WriteLine($"|   {i,-5} | {customers[i].customerDPI,-14} | {customers[i].customerName,-15} | {customers[i].customerBirthDate,-17} |");
                    Console.WriteLine("---------------------------------------------------");
                }
            }
            else
            {
                Console.WriteLine("\nNo se han encontrado registros de clientes.");
            }

        }
    }
}
