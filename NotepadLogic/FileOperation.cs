using System;
using System.IO;

namespace NotepadLogic
{
    public class FileOperation
    {
        private string filename;
        private bool isFileSaved;
        private string fileLocation;

        public string Filename { get => filename; set => filename = value; }
        public bool IsFileSaved { get => isFileSaved; set => isFileSaved = value; }
        public string FileLocation { get => fileLocation; set => fileLocation = value; }

        public FileOperation()
        {
            this.filename = "ayush_test.txt";
            this.isFileSaved = true;
        }

        public string OpenFile(string fileLocation)
        {
            string content;
            this.FileLocation = fileLocation;
            Stream stream = File.Open(fileLocation, FileMode.Open, FileAccess.ReadWrite);
            using (StreamReader streamReader = new StreamReader(stream))
            {
                content = streamReader.ReadToEnd();
            }
            UpdateFileStatus();
            return content;
            //throw new NotImplementedException();
        }

        private void UpdateFileStatus()
        {
            string filename = Filename.Substring(FileLocation.LastIndexOf("\\") + 1);
            this.Filename = filename;
            this.isFileSaved = true;
        }

        public void Savefile(string fileLocation, string[] lines)
        {
            this.FileLocation = fileLocation;
            Stream stream = File.Open(FileLocation, FileMode.OpenOrCreate, FileAccess.Write);
            using (StreamWriter streamwriter = new StreamWriter(stream))
            {
                foreach(string line in lines)
                {
                    streamwriter.WriteLine(line);
                }
            }
            UpdateFileStatus();
            //throw new NotImplementedException();
        }
    } 

}
