using System.Text;


namespace CRM.Models
{   /* 
       Clase para gestionar la entrada y validación de fechas en formato "dd/MM/yyyy".
       Esta clase permite al usuario editar una fecha en la consola.
    */
    internal class DateInput
    {
        public static string loadInput()
        {
            const string dateFormat = "dd/MM/yyyy";
            // Utilizamos StringBuilder para manipular la cadena
            StringBuilder inputBuilder = new StringBuilder(dateFormat);
            // Indice del cursor inicial
            int cursorIndex = 0;

            while (true)
            {

                // Mostrar la cadena actual y posiciona el cursor

                DisplayInput(inputBuilder.ToString(), cursorIndex);

                // Leer tecla presionada
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (char.IsDigit(keyInfo.KeyChar))
                {

                    // Si se presiona un digito editar la cadena

                    EditDigits(inputBuilder, ref cursorIndex, keyInfo.KeyChar);

                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    MoveCursorLeft(ref cursorIndex);
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    MoveCursorRight(ref cursorIndex, inputBuilder.Length);
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    DeleteDigit(inputBuilder, ref cursorIndex);
                }

            }

            return inputBuilder.ToString();

        }
        private static void DisplayInput(string input, int cursorIndex)
        {
            // Muestra la cadena 'input' y posiciona el cursor
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(input);

            Console.SetCursorPosition(cursorIndex, Console.CursorTop);

        }
        private static void EditDigits(StringBuilder input, ref int cursorIndex, char digit)
        {
            if (cursorIndex >= 0 && cursorIndex < input.Length)
            {

                // Si el cursor está en una posición de separador ('/'), avanza un espacio y agrega el dígito
                if (IsSeparator(cursorIndex))
                {
                    cursorIndex++;
                }

                // Actualiza la cadena 'input' con el dígito ingresado en la posición actual

                input[cursorIndex] = digit;

                // Mueve el cursor a la siguiente posición (si está dentro de los límites)

                if (cursorIndex + 1 < input.Length)
                {
                    cursorIndex++;
                }

            }
        }
        private static void MoveCursorLeft(ref int cursorIndex)
        {
            if (cursorIndex > 0)
            {
                cursorIndex--;
            }
        }
        private static void MoveCursorRight(ref int cursorIndex, int inputLength)
        {
            if (cursorIndex < inputLength - 1)
            {
                cursorIndex++;
            }
        }
        private static void DeleteDigit(StringBuilder input, ref int cursorIndex)
        {
            // Borra el carácter en la posición actual

            if (cursorIndex > 0 && !IsSeparator(cursorIndex))
            {
                // Reemplaza el carácter en la posición actual con '0'
                input[cursorIndex - 1] = '0';
                cursorIndex--;
            }
        }
        private static bool IsSeparator(int index)
        {
            return index == 2 || index == 5;
        }
        public bool IsValidDate(string date)
        {
            DateTime parsedDate;
            if (DateTime.TryParseExact(date, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate))
                return true;
            else
                return false;
        }

    }
}
