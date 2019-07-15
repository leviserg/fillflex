using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FillFlex
{
    static class readconfig
    {
        public static void readInitSettings(){
            readxml initfile = new readxml("C:/FillFlex/Init.xml");
            // ----- system -----
            List<string> data = initfile.getData("Settings");
            for (int i = 0; i < data.Count; i++)
            {
                GVL.settings[i] = data[i];
            }
            // ----- indexes -----
            List<string> ind = initfile.getData("Indexes");
            for (int i = 0; i < ind.Count; i++)
            {
                GVL.indexes[i] = int.Parse(ind[i]);
            }
        }

        public static void readPlcSettings()
        {
            readxml plcsettfile = new readxml("C:/FillFlex/InitPLC.xml");
            List <string> data = plcsettfile.getData("plcsettings");
            for (int i = 0; i < data.Count; i++)
            {
                GVL.PLCsettings[i] = data[i];
            }
        }

        public static void savePlcSettings()
        {
            readxml plcsettfile = new readxml("C:/FillFlex/InitPLC.xml");
            plcsettfile.saveData("plcsettings", GVL.PLCsettings);
        }

    }
}
