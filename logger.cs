using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FillFlex
{
    public class logger
    {
        private string sFileName;

        public logger(string sFileName)
        {
            this.sFileName = (sFileName);
        }

        public void writeData(string LogMessage, int Category)
        {
            string sWrStr = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + " ; " + DateTime.Now.ToString("dd.MM.yyyy") + "; " + DateTime.Now.ToString("HH:mm:ss") + " ; " + LogMessage + " ; " + Category.ToString() + " ; ";
            if (checkFile())
            {
                StreamWriter sw = new StreamWriter(this.sFileName, true, System.Text.Encoding.Default);
                sw.WriteLine(sWrStr);
                sw.Close();
                sw.Dispose();
            }
            else
            {
                FileStream fstream = new FileStream(this.sFileName, FileMode.Create);
                fstream.Close();
                fstream.Dispose();
                StreamWriter sw = new StreamWriter(this.sFileName, false, System.Text.Encoding.Default);
                sw.WriteLine(sWrStr);
                sw.Close();
                sw.Dispose();
            }
        }

        private bool checkFile()
        {
            System.IO.FileInfo ConfigFile = new System.IO.FileInfo(this.sFileName);
            if (ConfigFile.Exists)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
