using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace FillFlex
{
    class readxml
    {
        private string sFileName;
        // ---------------
        public readxml(string sFileName)
        {
            this.sFileName = (sFileName);
        }
        // ---------------
        public List<string> getData(string sNode)
        {
            List<string> Res = new List<string>();
            string sPath = this.sFileName;
            if (this.checkFile())
            {
                FileStream fs = new FileStream(sPath, FileMode.Open, FileAccess.Read);
                System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
                xmldoc.Load(fs);
                XmlNodeList xmlnode = xmldoc.GetElementsByTagName(sNode);
                int iLen = xmlnode.Item(0).ChildNodes.Count;
                for (int i = 0; i < iLen; i++)
                {
                    Res.Add(xmlnode.Item(0).ChildNodes.Item(i).InnerText.Trim());
                }
            }
            return Res;
        }
        // ---------------
        public void saveData(string sNode, string[] sData)
        {
            string sPath = this.sFileName;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(sPath);
            XmlNodeList xmlnode = xmldoc.GetElementsByTagName(sNode);
            for (int i = 0; i < sData.Length; i++)
            {
                xmlnode.Item(0).ChildNodes.Item(i).InnerText = sData[i];
            }
            xmldoc.Save(sPath);
        }
        // ---------------
        public bool checkFile()
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
        // ---------------
    }
}
