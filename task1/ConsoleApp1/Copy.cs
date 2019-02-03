using System.IO;
using System.Security;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace testTask1
{
    [DataContract]
    public class Copy
    {

        [DataMember]
        private List<string> _sources = new List<string>();
        public List<string> sources
        {
            get
            {
                return new List<string>(this._sources);
            }
            private set
            {
                this.sources = new List<string>(value);
            }

        }
        public void addDirectory(string path)
        {
            this._sources.Add(path);
        }
        //public string source{get; set;}

        [DataMember]
        public string target { get; set; }
        public void copy()
        {
            if (this.target == null) return;
            this.target += "/temp";
            try
            {
                Directory.CreateDirectory(this.target);
            }
            catch (ArgumentException)
            {
                Program.log.Error("Неверно указана папка с исходниками: "
                + this.target);
                return;
            }
            Program.log.Info("Создана целевая папка " + this.target);
            Program.log.Debug("Копируем из " + this.sources.Count);
            foreach (string path in this._sources)
            {
                string[] directoryFullName = path.Split('/');
                string directoryShortName = directoryFullName[directoryFullName.Length - 1];
                string currentDirectory = this.target + "/" + directoryShortName;
                try
                {
                    Directory.CreateDirectory(currentDirectory);
                }
                catch (ArgumentException)
                {
                    Program.log.Error("Неверно указана папка с исходниками: "
                    + currentDirectory);
                    continue;
                }
                Program.log.Info("Создана целевая папка " + currentDirectory);
                string[] files = Directory.GetFiles(path);
                foreach (string fileName in files)
                {
                    Program.log.Info("Копируем файл " + fileName);
                    string[] splitedName = fileName.Split('/');
                    FileInfo fn = new FileInfo(fileName);
                    string targetName = currentDirectory + "/" +
                     splitedName[splitedName.Length - 1];
                    try
                    {
                        fn.CopyTo(targetName, true);
                        Program.log.Info("Файл " + fileName + " скопирован успешно");
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Program.log.Debug("Program doesn't have permissions to copy file "
                        + fileName);
                    }
                    catch (SecurityException)
                    {
                        Program.log.Debug("Program doesn't have permissions");
                    }
                }
            }
        }
    }
}