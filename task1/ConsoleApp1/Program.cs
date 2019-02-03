
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;
using NLog;

namespace testTask1
{

    using System;
    public class Program
    {
        public static Logger log;
        public static string preferenceJSON = "Preferences.json";

        public static void Main(string[] args)
        {
            Program.log = LogManager.GetCurrentClassLogger();
            Program.log.Info("Начало работы");
            Copy copy;
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Copy));
            {
                if (File.Exists(Program.preferenceJSON))
                {
                    Program.log.Info("Найден файл настроек");
                    FileStream fs = new FileStream(Program.preferenceJSON, FileMode.OpenOrCreate);
                    copy = (Copy)jsonFormatter.ReadObject(fs);
                    Program.log.Info("Сериализация объекта завершена");
                    copy.copy();
                }
                else
                {
                    Program.log.Info("Файл настроек не найден." +
                     "Будет создан файл настроек по умолчанию");
                    FileStream fs = File.Create(Program.preferenceJSON);
                    copy = new Copy();
                    jsonFormatter.WriteObject(fs, copy);
                    Program.log.Info("Файл настроек создан");
                }
            }

        }
    }
}