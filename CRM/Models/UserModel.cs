

namespace CRM.Models
{
    internal class UserModel
    {
        private static List<User> users = new();

        public bool CreateCustomer(string username, string password)
        {

            User user = new() 
            { 
                UserID = $"user_{GetUsers().Count + 1}",
                Username = username,
                Password = password,
                CreatedAt = DateTime.Now.ToString(),
                LastUpdated = DateTime.Now.ToString()
            };
            

            users.Add(user);
            // Verificar si el cliente se encuentra en la lista después de agregarlo
            return users.Contains(user);
        }
        public bool UpdateUser(string userID, string username, string password)
        {
            User userToEdit = this.GetUserByID(userID);

            if (userToEdit == null)
            {
                return false;
            }

            userToEdit.Username = username;
            userToEdit.Password = password;
            userToEdit.LastUpdated = DateTime.Now.ToString();

            return true;

        }

        public User? GetUserByID(string userID)
        {
            return users.FirstOrDefault(user => user.UserID == userID);
        }
        public static bool UserExists(string username)
        {
            return users.Any(user => user.Username == username);
        }
        public List<User> GetUsers()
        {
            return users;
        }

        public bool Authenticate(string username, string password)
        {
            return users.Exists(user => user.Username == username && user.Password == password);   
        }
        public bool DeleteUser(string userRow)
        {

            User? userToDelete = GetUserByID(userRow);

            if (userToDelete != null)
            {
                users.Remove(userToDelete);
                return true;
            }
            else
            {
                return false;
            }


        }
        /* 
           Método para obtener un usuario de la lista 'users' basado en el número de fila proporcionado.
           Si la fila es válida y se encuentra un usuario en esa fila, se devuelve un objeto 'User' con los detalles del usuario.
           Si no se encuentra ningún usuario o la fila es inválida, se devuelve 'null'.
        */
        public User? GetUserToEdit(string userRow)
        {
            string? username = string.Empty;
            string? password = string.Empty;
            string? userID = string.Empty;

            // Comprobar si 'userRow' se puede convertir a un entero y si está dentro del rango válido de la lista List<Users>.
            if (int.TryParse(userRow, out int row) && row >= 0 && row < users.Count)
            {
                // Recuperar el usuario en el índice de fila especificado.
                User userToEdit = users[row];

                // Comprobar si el usuario recuperado no es nulo.
                if (userToEdit != null)
                {
                    // Extraer los detalles del usuario.
                    userID = userToEdit.UserID;
                    username = userToEdit.Username;
                    password = userToEdit.Password;
                }
            }

            // Comprobar si alguno de los detalles del usuario no está vacío y devolver un objeto User o nulo en consecuencia.
            if (!string.IsNullOrEmpty(username) || !string.IsNullOrEmpty(password) || !string.IsNullOrEmpty(userID))
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
                // Si ninguno de los detalles del usuario está disponible, devolver nulo.
                return null;
            }
        }

    }
}
