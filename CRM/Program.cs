using CRM.Controllers;
using CRM.Models;

namespace CRM
{
    class Program
    {
        static void Main()
        {
            // Se instancia para controlar el inicio de sesion
            AuthController authController = new AuthController();
            AppController appController = new AppController();
            //Se instancia el model UserModel para insertar el usuario admin en la base de datos (List<User>)
            UserModel userModel = new UserModel();
            userModel.CreateCustomer("admin", "1");

            //Se instancia el model UserModel para insertar clientes en la base de datos (List<Customer>)
            CustomerModel customerModel = new CustomerModel();

            customerModel.CreateCustomer("Valentin Solorzano Sandoval", "2988685120101", "19/09/2003");
            customerModel.CreateCustomer("Cliente 2", "4578124690876", "02/02/1995");
            customerModel.CreateCustomer("Cliente 3", "5178125670983", "03/03/2000");

            while (true) 
            {
                bool isLogged = authController.Auth();

                while (isLogged)
                {
                    isLogged = appController.Run();

                }
            }

        }

    }

}