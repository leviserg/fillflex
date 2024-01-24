using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FillFlex
{
    static class readconfig
    {
        public static void ReadInitSettings(){
            readxml initfile = new readxml(GVL.appPath + "/FillFlexInit/Init.xml");
            // ----- system -----
            List<string> data = initfile.GetData("Settings");
            for (int i = 0; i < data.Count; i++)
            {
                GVL.settings[i] = data[i];
            }
            // ----- indexes -----
            List<string> ind = initfile.GetData("Indexes");
            for (int i = 0; i < ind.Count; i++)
            {
                GVL.indexes[i] = int.Parse(ind[i]);
            }
        }

        public static void ReadPlcSettings()
        {
            readxml plcsettfile = new readxml(GVL.appPath + "/FillFlexInit/InitPLC.xml");
            List <string> data = plcsettfile.GetData("plcsettings");
            for (int i = 0; i < data.Count; i++)
            {
                GVL.PLCsettings[i] = data[i];
            }
        }

        public static void SavePlcSettings()
        {
            readxml plcsettfile = new readxml(GVL.appPath + "/FillFlexInit/InitPLC.xml");
            plcsettfile.SaveData("plcsettings", GVL.PLCsettings);
        }

    }
}
