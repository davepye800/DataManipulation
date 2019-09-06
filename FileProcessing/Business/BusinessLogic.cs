using System.IO;
using FileProcessing.File;
using FileProcessing.Interface;

namespace FileProcessing.Business
{
    public class BusinessLogic
    {
        public bool ProcessClaims(string pathIn
                                  , string pathOut
                                  , IFile file)
        {
            using (StreamReader sr = new StreamReader(pathIn))
            {
                using (StreamWriter sw = new StreamWriter(pathOut))
                {
                    sw.Write(file.Process(sr.ReadToEnd()));
                    sw.Flush();
                    return true;
                }
            }
        }
    }
}
