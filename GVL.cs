using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FillFlex
{
    static class GVL
    {
        public static int LogRecords = 0;
        public static int DBRecords = 0;
        public static int shLogRecords = 0;
        public static string reservedStartCounterValue = "";
        // trend data
        public static double sectime = 0.0;
        public static double tempValue = 0.0;
        // trend data
        //public static double[] temp = new double[100];
        public static int trendWidth = 0;
        public static int TankNum = 0;
        public static bool FirstCycle = false;
        public static int[] registers = new int[6000];
        public static string[] settings = new string[22];
        public static int[] indexes = new int[22];
        public static string[] PLCsettings = new string[8];
        public static bool[] FixEvent = new bool[30];
        public static int RetryAttempt = 0;
        public static int PLCTick = 0;
        public static bool readexc = false;
        public static bool dbrec = false;
        public static bool FixConn = false;
        public static bool LoginFormShow = false;
        public static bool isAdmin = false;
        public static bool isRefill = false;
        public static bool fixLogin = false;
        public static int SysMode = 5002;
        public static string MyLogin;
        public static string MyPwd;
        public static int FillTime = 0;
        public static int FillMaxLimit = 0;
        public static bool DataWritten = false;
        public static int useTrends = 0;

        public static string appPath = Path.GetDirectoryName(Application.ExecutablePath).Replace("\\","/");

    }
}
