
using System.Reflection;

namespace CRM.Models
{
    public class LogsHandler
    {

        private string logPath = Path.Combine(GetApplicationDirectory(), "logs");
        const string errorLogFileName = "crm-errors.log";
        const string infoLogFileName = "crm-info.log";
        private string MethodName = MethodBase.GetCurrentMethod().Name;

        public LogsHandler()
        {
            // Para crear el folder de logs
            CreateLogsFolder();
            // Para crear los archivos de texto de logs: crm-info.log y crm-errors.log
            CreateLogsFiles();

        }
        public static string GetApplicationDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        private void CreateLogsFolder()
        {



            if (!Directory.Exists(logPath))
            {

                Directory.CreateDirectory(logPath);


            }


        }
        private void CreateLogsFiles()
        {

            if (Directory.Exists(logPath))
            {

                string infoLogPath = Path.Combine(logPath, infoLogFileName);
                string errorLogPath = Path.Combine(logPath, errorLogFileName);

                if (!File.Exists(infoLogPath))
                {

                    try
                    {
                        using (StreamWriter file = File.CreateText(infoLogPath))
                        {
                            file.WriteLine(logFormat("INFO", currentClass(this), MethodName, $"File '{infoLogFileName}' created successfully."));
                        }
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine($"Error al crear directorio de logs {infoLogFileName}: {ex}");

                    }


                }

                if (!File.Exists(errorLogPath))
                {

                    try
                    {
                        using (StreamWriter file = File.CreateText(errorLogPath))
                        {
                            file.WriteLine(logFormat("INFO", currentClass(this), MethodName, $"File '{errorLogFileName}' created successfully."));
                        }
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine($"Error al crear directorio de logs {errorLogFileName}: {ex}");
                    }
                }
            }

        }

        static string logFormat(string logType, string activeClass, string activeMethod, string actionInProgress)
        {
            return $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logType}] [Active class: {activeClass}] [Method: {activeMethod}] {actionInProgress}";
        }
        public void CreateNewLog(string logType, string activeClass, string activeMethod, string actionInProgress)
        {

            try
            {
                string LogText = logFormat(logType, activeClass, activeMethod, actionInProgress);
                string logFile = (logType == "INFO") ? infoLogFileName : errorLogFileName;
             
                using (StreamWriter log = File.AppendText(Path.Combine(logPath, logFile)))
                {
                    log.WriteLine(LogText);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar log: {ex}");
            }

        }
        public static string currentClass(object activeClass)
        {
            return activeClass.GetType().Name;
        }
  

    }



}
