
namespace CRM.Models
{
    public class CustomerModel
    {
        private static List<Customer> customers = new List<Customer>();


        public bool CreateCustomer(string name, string dpi, string birthDate)
        {
            Customer customer = new Customer
            {

                customerName = name,
                customerBirthDate = birthDate,
                customerDPI = dpi

            };

            customers.Add(customer);
            // Verificar si el cliente se encuentra en la lista después de agregarlo
            return customers.Contains(customer);
        }
        public bool UpdateCustomer(string name, string dpi, string birthDate) 
        {
            Customer? customerToEdit = GetCustomerByDPI(dpi);
            
            if(customerToEdit == null)
            {
                return false;
            }

            customerToEdit.customerName = name;
            customerToEdit.customerDPI = dpi;
            customerToEdit.customerBirthDate = birthDate;

            return true;

        }
        public bool DeleteCustomer(string dpi)
        {

            Customer? customerToDelete = GetCustomerByDPI(dpi);

            if (customerToDelete != null)
            {
                customers.Remove(customerToDelete);
                return true;
            } 
            else
            {
                return false;
            }

            
        }
        public Customer? GetCustomerByDPI(string dpi)
        {
            return customers.FirstOrDefault(customer => customer.customerDPI == dpi);
        }
        public List<Customer> GetCustomers()
        {
            return customers;
        }
        public bool CustomerExists(string dpi)
        {
            return customers.Any(customer => customer.customerDPI == dpi);
        }


    }
}
