using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;

namespace FillFlex
{
    public partial class MainForm : Form
    {
        private static logger Logger = new logger("C:/FillFlex/Logs.csv");

        private static database AppDb = new database();

        public static database dbInstance
        {
            get { return AppDb; }
        }

        public static logger loggerInstance {
            get { return Logger; }
        }

        System.Windows.Forms.DataVisualization.Charting.Series tempseries = new System.Windows.Forms.DataVisualization.Charting.Series
        {
            Name = "Температура, °С",
            BorderWidth = 3,
            Color = System.Drawing.Color.Red,
            IsVisibleInLegend = true,
            IsXValueIndexed = true,
            ChartType = SeriesChartType.Line,
            YValuesPerPoint = 1,
            XValueType = ChartValueType.String,
            YValueType = ChartValueType.Double,
        };

        public MainForm()
        {
            InitializeComponent();
            StartUp();
        }

//  ===================================  1 sec TIMER =================================== 

        private void TimeTick_Tick(object sender, EventArgs e)
        {
            DateLabel.Text = DateTime.Now.ToString("dd.MM.yyyy");
            TimeLabel.Text = DateTime.Now.ToString("HH:mm:ss");
            if (GVL.dbrec) {
                ShowDataGridView();
                GVL.dbrec = false;
            }
            if (DateTime.Now.Hour == 10 && DateTime.Now.Minute == 0 && DateTime.Now.Second == 0) {
                PLCSendDate();
            }
            Anim();
            checkEvents();
            showProgress();
            if (GVL.isAdmin)
            {
                CurUser.Text = GVL.MyLogin;
            }
            else {
                CurUser.Text = "";
            }
            if (GVL.isAdmin && !GVL.fixLogin) {
                LogMessage("Виконано вхід в систему", 2);
                GVL.fixLogin = true;
            }
            if (!GVL.isAdmin && GVL.fixLogin)
            {
                LogMessage("Виконано вихід з системи", 2);
                GVL.fixLogin = false;
            }
            showTrend();
        }

//  ===================================  START UP  =================================== 

        private void StartUp() {

                OdbcManager odbc = new OdbcManager();
                string dsnName = "FillFlex";//Name of the DSN connection here

                if (odbc.CheckForDSN(dsnName) > 0)
                {
                    Console.WriteLine("\n\nODBC Connection " + dsnName + " already exists on the system");
                }
                else
                {
                    DialogResult res = MessageBox.Show("Не знайдено джерела даних DSN", "Вихід", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    this.Dispose();
                    Application.Exit();
                }

            this.WindowState = FormWindowState.Maximized;
            readconfig.readInitSettings();
            readconfig.readPlcSettings();
            CurUser.Text = "";
            sett0.Text = GVL.settings[0]; // IP
            sett1.Text = GVL.settings[1]; // port
            sett2.Text = Math.Round(double.Parse(GVL.settings[2]) / 1000, 2).ToString(); // retry timeout
            sett3.Text = GVL.settings[3];
            GVL.LogRecords = int.Parse(GVL.settings[4]);
            GVL.DBRecords = int.Parse(GVL.settings[5]);
            sett4.Text = GVL.settings[4];
            sett5.Text = GVL.settings[5];
            GVL.MyLogin = GVL.settings[6];
            GVL.MyPwd = GVL.settings[7];
            //---- plc connection timeout & retry ----
            sett8.Text = GVL.settings[8];
            sett9.Text = GVL.settings[9];
            modbustcp.plcInstance.ConnectionTimeout = int.Parse(GVL.settings[8]);
            modbustcp.plcInstance.NumberOfRetries = int.Parse(GVL.settings[9]);
            sett10.Text = GVL.settings[10];
            sett11.Text = GVL.settings[11];
            GVL.trendWidth = int.Parse(GVL.settings[11]);
            GVL.shLogRecords = int.Parse(GVL.settings[10]);
            sett12.Text = GVL.settings[12];
            GVL.TankNum = int.Parse(GVL.settings[12]);

            SourceTanSel.Items.Add("всі");
            for (int i = 1; i <= GVL.TankNum; i++)
            {
                SourceTanSel.Items.Add(i.ToString());
            }
            SourceTanSel.SelectedIndex = 0;

 //           modbustcp.plcInstance.LogFileFilename = "C:/FillFlex/modbuslogs.txt";
            // --- meh indexes ---
            ind0.Text = GVL.indexes[0].ToString();
            ind1.Text = GVL.indexes[1].ToString();
            ind2.Text = GVL.indexes[2].ToString();
            ind3.Text = GVL.indexes[3].ToString();
            ind4.Text = GVL.indexes[4].ToString();
            ind5.Text = GVL.indexes[5].ToString();
            // --- meh indexes ---
            plcsett0.Text = GVL.PLCsettings[0];
            plcsett1.Text = GVL.PLCsettings[1];
            plcsett2.Text = GVL.PLCsettings[2];
            //plcsett3.Text = GVL.PLCsettings[3];
            plcsettnum3.Value = int.Parse(GVL.PLCsettings[3]);
            plcsett4.Text = GVL.PLCsettings[4];
            plcsett5.Text = GVL.PLCsettings[5];
            //plcsett6.Text = GVL.PLCsettings[6];
            plcsettnum6.Value = int.Parse(GVL.PLCsettings[6]);
            plcsett7.Text = GVL.PLCsettings[7];

            statusSel.SelectedIndex = 0; // for dropdown list in reports tab
            RetryTimer.Interval = int.Parse(GVL.settings[2]);
            PLCtimer.Interval = int.Parse(GVL.settings[3]);
            LogMessage("Додаток запущено", 2);
            LoadEvents();
            AppDb.OpenDb();
            ShowDataGridView();
            modbustcp.openConn();
            PLCtimer.Enabled = true;
            TempChart.Series.Clear();
            TempChart.ChartAreas[0].AxisY.Maximum = 100;
            TempChart.ChartAreas[0].AxisY.Minimum = -20;
            this.TempChart.Series.Add(tempseries);
            Console.WriteLine("Application started");
        }

//  ===================================  SHUTDOWN  =================================== 

        private void FillFormShutdown(object sender, FormClosingEventArgs e)
        {
            DialogResult res = MessageBox.Show("Ви бажаєте завершити роботу?", "Вихід", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                AppDb.CloseDb();
                PLCtimer.Enabled = false;
                modbustcp.closeConn();
                LogMessage("Додаток закрито", 2);
                Console.WriteLine("Application stopped");
            }
            else {
                e.Cancel = true;
            }
        }

//  ===================================  LOGGER  ===================================

        public void LogMessage(string sMessage, int Category) {
            Logger.writeData(sMessage, Category);
            EventsList.Items.Insert(0, DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + "    " + sMessage + " |   " + Category.ToString());
            EventsListSh.Items.Insert(0, DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + "    " + sMessage + " | " + Category.ToString());
            if (this.EventsListSh.Items.Count > 5)
            {
                this.EventsListSh.Items.RemoveAt(5);
            }
        }

//  ===================================  EVENTS LISTBOX  ===================================

        private void EventsList_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            e.DrawBackground();
            string sText = EventsList.Items[e.Index].ToString();
            int Categ = Convert.ToInt16(sText[sText.Length - 1]);
            if (Categ == 49)// ascii - 1
            {
                e.Graphics.FillRectangle(Brushes.Red, e.Bounds);
            }
            e.Graphics.DrawString(EventsList.Items[e.Index].ToString(), e.Font, Brushes.Black, new System.Drawing.PointF(e.Bounds.X, e.Bounds.Y));
            e.DrawFocusRectangle();
        }

        private void EventsListSh_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            e.DrawBackground();
            string sText = EventsListSh.Items[e.Index].ToString();
            int Categ = Convert.ToInt16(sText[sText.Length - 1]);
            if (Categ == 49)
            {
                e.Graphics.FillRectangle(Brushes.Red, e.Bounds);
            }
            e.Graphics.DrawString(EventsListSh.Items[e.Index].ToString(), e.Font, Brushes.Black, new System.Drawing.PointF(e.Bounds.X, e.Bounds.Y));
            e.DrawFocusRectangle();
        }

        private void LoadEvents() {
            EventsList.MultiColumn = false;
            EventsList.Sorted = false;
            EventsList.ScrollAlwaysVisible = true;
            using (StreamReader reader = new StreamReader("C:/FillFlex/Logs.csv", System.Text.Encoding.Default))
            {
                while (true) {
                    string sLine = reader.ReadLine();
                    if (sLine == null)
                    {
                        break;
                    }
                    else {
                        if(sLine.Length>0){
                            string sMessage = sLine.Substring(45, sLine.Length - 49).Replace(";", "|");
                            EventsList.Items.Insert(0, sLine.Substring(0, 20) + "   " + sMessage + "    " + sLine.Substring(sLine.Length - 4, 1));
                            EventsListSh.Items.Insert(0, sLine.Substring(0, 20) + "   " + sMessage + "    " + sLine.Substring(sLine.Length - 4, 1));
                        }
                    }
                    if (this.EventsList.Items.Count > GVL.LogRecords)
                    {
                        this.EventsList.Items.RemoveAt(GVL.LogRecords);
                    }
                    if (this.EventsListSh.Items.Count > GVL.shLogRecords)
                    {
                        this.EventsListSh.Items.RemoveAt(GVL.shLogRecords);
                    }
                }
            };
        }

        private void checkAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAlarm.Checked) {
                LogMessage("Тест запису аварії", 1);
            }
        }

        private void checkEvent_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEvent.Checked)
            {
                LogMessage("Тест запису події", 2);
            }
        }

        private void showMessage(string sText)
        {
            MessageBox.Show(sText, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
//  ===================================  DATAGRIDVIEW  ===================================

        public void ShowDataGridView() {
            dataTableView.AutoGenerateColumns = true;
            dataTableView.ColumnHeadersDefaultCellStyle.Font = new Font(dataTableView.DefaultCellStyle.Font, FontStyle.Bold);
            maxId.Text = AppDb.RecCount().ToString();
            lastDate.Text = AppDb.lastDate();
            dataTableView.DataSource = AppDb.dataTable();
            dataTableView.Refresh();
            showLastRec();
        }

        private void RowSelectedEvent(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selRow = e.RowIndex + 1;
            int selCell = int.Parse(dataTableView.SelectedCells[0].Value.ToString());
            List<string> selData = AppDb.selectTask(selCell);
            SingleRecord singleRecWindow = new SingleRecord();
            singleRecWindow.Show();
            singleRecWindow.setId(selData[0]);
            singleRecWindow.setPlcId(selData[1]);
            singleRecWindow.setDtc(selData[2]);
            singleRecWindow.setDts(selData[3]);
            singleRecWindow.setDtf(selData[4]);
            singleRecWindow.setSetp(selData[5]);
            singleRecWindow.setMass(selData[6]);
            singleRecWindow.setVol(selData[7]);
            singleRecWindow.setCns(selData[10]);
            singleRecWindow.setCnf(selData[11]);
            singleRecWindow.setTemp(selData[9]);
            singleRecWindow.setDens(selData[8]);
            singleRecWindow.setSts(selData[12]);
            singleRecWindow.setTankId(selData[13]);
            singleRecWindow.setTransp(selData[14]);
            singleRecWindow.setDriver(selData[15]);
            singleRecWindow.setCustomer(selData[16]);
            singleRecWindow.setSourceTank(selData[17]);
            double ddiff = double.Parse(selData[5]) - double.Parse(selData[6]);
            double pDiff = Math.Abs(Math.Round(ddiff * 100 / double.Parse(selData[5]), 3));
            string sDiff = Math.Round(ddiff, 2).ToString() + " (" + pDiff.ToString() + "%)";
            singleRecWindow.setSelDiff(sDiff);
        }

        private void showLastRec() {
            List<string> lastData = AppDb.getLastRec();
            lastId.Text = lastData[0];// 0 - dbID, 1 - plcId
            lastPlcId.Text = lastData[1];
            lastDtc.Text = lastData[2];
            lastDts.Text = lastData[3];
            lastDtf.Text = lastData[4];
            lastSts.Text = lastData[12];
            lastSetp.Text = lastData[5];
            lastMass.Text = lastData[6];
            TankId.Text = "Код: " + lastData[13];
            switch (lastSts.Text)
            {
                case "створено":
                    Upload.Enabled = true;
                    FinBtn.Enabled = false;
                    StartBtn.Enabled = false;
                    rstBtn.Enabled = true;
                    newTask.Enabled = false;
                    PauseBtn.Enabled = false;
                    break;
                case "завантажено":
                    Upload.Enabled = false;
                    if (GVL.registers[5796] >= 1) {
                        StartBtn.Enabled = true;
                    }
                    rstBtn.Enabled = true;
                    FinBtn.Enabled = false;
                    newTask.Enabled = false;
                    PauseBtn.Enabled = false;
                    break;
                case "запущено":
                    Upload.Enabled = false;
                    StartBtn.Enabled = false;
                    FinBtn.Enabled = true;
                    rstBtn.Enabled = true;
                    newTask.Enabled = false;
                    PauseBtn.Enabled = true;
                    break;
                case "зупинено":
                    Upload.Enabled = false;
                    if (GVL.registers[5796] >= 1)
                    {
                        StartBtn.Enabled = true;
                    }
                    FinBtn.Enabled = false;
                    rstBtn.Enabled = true;
                    newTask.Enabled = false;
                    PauseBtn.Enabled = false;
                    break;
                default:
                    FinBtn.Enabled = false;
                    Upload.Enabled = false;
                    StartBtn.Enabled = true; // false
                    rstBtn.Enabled = false;
                    newTask.Enabled = true;
                    PauseBtn.Enabled = false;
                    break;
            }
        }

        private void CrtRepBtn_Click(object sender, EventArgs e)
        {
            AppDb.printReport(datePickerStart.Value, datePickerEnd.Value, statusSel.SelectedItem.ToString(), SourceTanSel.SelectedIndex);
        }

//  ===================================  TASKS CONTROL  ===================================

        private void newTask_Click(object sender, EventArgs e)
        {
            NewTaskForm newTaskFrm = new NewTaskForm();
            newTaskFrm.Show();
        }

        private void Upload_Click(object sender, EventArgs e)
        {
            Single setpoint = Single.Parse(lastSetp.Text);
            int[] newtaskdata = new int[4];
            newtaskdata[0] = int.Parse(lastId.Text); // 5004 task local id
            newtaskdata[1] = 0; // 5005 (route number)
            newtaskdata[3] = EasyModbus.ModbusClient.ConvertFloatToRegisters(setpoint)[1]; // 5006 real (new task setpoint)
            newtaskdata[2] = EasyModbus.ModbusClient.ConvertFloatToRegisters(setpoint)[0];
            PLCWriteRegisters(5004, newtaskdata);
            Thread.Sleep(20);
            AppDb.updateOnUpload();
            GVL.reservedStartCounterValue = curcnt2.Text.Replace(",", ".");
            GVL.dbrec = true;
            PLCSendSingleRegister(GVL.SysMode, 1);
            LogMessage("Завдання " + lastId.Text + " : " + lastPlcId.Text + " завантажено.", 2);
            showMessage("Поточне завдання завантажено до ПЛК");
            modbustcp.sendSingleRegister(5797, 0); // reset refill mode
        }

        private void rstBtn_Click(object sender, EventArgs e)
        {
            if (!lastSts.Text.Equals("виконано")) {
                DialogResult res = MessageBox.Show("Відхилити поточне завдання?", "Відхилення", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    modbustcp.sendSingleRegister(5797, 0); // reset refill mode
                    List<string> data = new List<string>();
                    data.Add(currescnt2.Text.Replace(",", "."));    // mass
                    data.Add(currescnt1.Text.Replace(",", "."));     // vol
                    data.Add(GVL.reservedStartCounterValue);
                    data.Add(curcnt2.Text.Replace(",", ".")); 
                    AppDb.updateOnReset(data);
                    GVL.dbrec = true;
                    MessageBox.Show("Відхилено завдання №" + lastId.Text, "Відхилення", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogMessage("Завдання " + lastId.Text + " : " + lastPlcId.Text + " відхилено", 2);
                    PLCSendSingleRegister(GVL.SysMode, 8);
                }
            }
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            if (int.Parse(cursetpoint.Text)<=0)
            {
                showMessage("Відсутнє завантажене завдання.");
            }
            else if (modbustcp.getBit(23, 14) && GVL.registers[GVL.SysMode] != 3 && GVL.registers[GVL.SysMode] != 5 && GVL.registers[GVL.SysMode] != 7 && GVL.registers[GVL.SysMode] != 8 && GVL.registers[GVL.SysMode] != 9)
            {
                PLCSendSingleRegister(GVL.SysMode, 2);
            }
            else {
                showMessage("Система не підготовлена до пуску.");
            }
        }

        private void PauseBtn_Click(object sender, EventArgs e)
        {
            PLCSendSingleRegister(GVL.SysMode, 4);
        }

        private void FinBtn_Click(object sender, EventArgs e)
        {
            if (GVL.registers[GVL.SysMode] == 3)
            {
                PLCSendSingleRegister(GVL.SysMode, 7);
            }
            else {
                showMessage("Завдання не може бути завершено до виконання.");
            }
        }

//  ===================================  PLC CONTROL  ===================================

        private void PLCtimer_Tick(object sender, EventArgs e)
        {
            if (!GVL.readexc)
            {
                switch (GVL.PLCTick) {
                    case 1:
                        modbustcp.ReadRegisters(5000, 50);
                        break;
                    case 2:
                        modbustcp.ReadRegisters(5612, 2);
                        break;
                    case 3:
                        modbustcp.ReadRegisters(5700, 100);
                        break;
                    case 4:
                        modbustcp.ReadRegisters(0, 90);
                        GVL.PLCTick = 0;
                        break;
                    default:
                        break;
                }
                if (!GVL.FixConn && !GVL.readexc && modbustcp.Connected())
                {
                    GVL.FixConn = true;
                    LogMessage("Встановлено зв'язок з ПЛК", 2);
                    GVL.RetryAttempt = 0;
                }
                GVL.PLCTick++;
            }
            else {
                CommFail();
            }
        }

        private void RetryTimer_Tick(object sender, EventArgs e)
        {
            CommRestore();
        }

        private void CommFail() {
            GVL.FixConn = false;
            Anim();
            PLCtimer.Enabled = false;
            modbustcp.closeConn();
            GVL.PLCTick = 0;
            GVL.RetryAttempt++;
            LogMessage("Відсутній зв'язок з ПЛК", 1);
            ConnInd.Image = Image.FromFile("C:/FillFlex/Images/RedConn.png");
            ResetComm.Visible = true;
            RetrTickLabel.Visible = true;
            RetrTickLabel.Text = "спроб " + GVL.RetryAttempt.ToString();
            CheckCommCount.Text = GVL.RetryAttempt.ToString();
            GVL.FirstCycle = false;
            if (!DisChckTout.Checked)
            {
                RetryTimer.Enabled = true;
            }
        }

        private void CommRestore()
        {
            GVL.readexc = false;
            if (!modbustcp.Connected())
            {
                modbustcp.openConn();
            }
            RetryTimer.Enabled = false;
            PLCtimer.Enabled = true;
            ResetComm.Visible = false;
            RetrTickLabel.Visible = false;
            ConnInd.Image = Image.FromFile("C:/FillFlex/Images/GreenConn.png");
            Anim();
        }

        private void ResetComm_Click(object sender, EventArgs e)
        {
            CommRestore();
        }

        private void PLCSendDate() {
            PLCtimer.Enabled = false;
            Thread.Sleep(10);
            if (!GVL.readexc && modbustcp.Connected())
            {
                modbustcp.sendDate();
            }
            Thread.Sleep(10);
            PLCtimer.Enabled = true;
        }

        private void PLCSendLocCommand(int start, int index, int command)
        {
            PLCtimer.Enabled = false;
            Thread.Sleep(10);
            if (!GVL.readexc && modbustcp.Connected())
            {
                modbustcp.sendLocCmd(start, index, command);
            }
            Thread.Sleep(10);
            PLCtimer.Enabled = true;
        }

        private void PLCSendSingleRegister(int register, int value)
        {
            PLCtimer.Enabled = false;
            Thread.Sleep(10);
            if (!GVL.readexc && modbustcp.Connected())
            {
                modbustcp.sendSingleRegister(register, value);
            }
            Thread.Sleep(10);
            PLCtimer.Enabled = true;
        }

        private void PLCWriteRegisters(int start, int[] data)
        {
            PLCtimer.Enabled = false;
            Thread.Sleep(10);
            if (!GVL.readexc && modbustcp.Connected())
            {
                modbustcp.writeRegisters(start, data);
            }
            Thread.Sleep(10);
            PLCtimer.Enabled = true;
        }

        private void PLCSendSettings()
        {
            GVL.PLCsettings[0] = plcsett0.Text;
            GVL.PLCsettings[1] = plcsett1.Text;
            GVL.PLCsettings[2] = plcsett2.Text;
            GVL.PLCsettings[3] = plcsettnum3.Value.ToString();
            GVL.PLCsettings[4] = plcsett4.Text;
            GVL.PLCsettings[5] = plcsett5.Text;
            GVL.PLCsettings[6] = plcsettnum6.Value.ToString();
            GVL.PLCsettings[7] = plcsett7.Text;

            readconfig.savePlcSettings();
            PLCtimer.Enabled = false;
            Thread.Sleep(10);
            if (!GVL.readexc && modbustcp.Connected())
            {
                Single[] wrSettings = new Single[GVL.PLCsettings.Length-2];
                for(int i = 0; i < wrSettings.Length; i++){
                    wrSettings[i] = Single.Parse(GVL.PLCsettings[i]);
                }
                modbustcp.saveSettings(5008, wrSettings);
                Thread.Sleep(10);
                Single[] wrRefSettings = new Single[2];
                wrRefSettings[0] = Single.Parse(GVL.PLCsettings[6]);
                wrRefSettings[1] = Single.Parse(GVL.PLCsettings[7]);
                modbustcp.saveSettings(5028, wrRefSettings);
            }
            Thread.Sleep(10);
            PLCtimer.Enabled = true;
        }

        private void SaveSett_Click(object sender, EventArgs e)
        {
            PLCSendSettings();
        }

        private void ResetValve_Click(object sender, EventArgs e)
        {
            PLCSendLocCommand(307, GVL.indexes[1], 5);
            LogMessage("Скинуто аварії клапана", 2);
        }

        private void ResetPump_Click(object sender, EventArgs e)
        {
            PLCSendLocCommand(307, GVL.indexes[0], 5);
            LogMessage("Скинуто аварії насоса", 2);
        }

        private void ResetAlm_Click(object sender, EventArgs e)
        {
            int register = 25;
            int value = Convert.ToUInt16(Math.Pow(2, 4)) & GVL.registers[register] | Convert.ToUInt16(Math.Pow(2, 15)); // bit 4 & 15 = 1  - unsigned
            //showMessage(value.ToString());
            PLCSendSingleRegister(register, value);
            if (GVL.registers[GVL.SysMode] == 6) {
                PLCSendSingleRegister(GVL.SysMode, 0);
            }
            LogMessage("Скинуто аварії системи", 2);
        }

        private void ResetCounters12_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Скинути лічильники?", "Скид", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                PLCSendSingleRegister(81, 1);
                LogMessage("Скинуто лічильники маси та об`єму.", 2);
            }
        }

        private void ResetCounters3_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Скинути загальний лічильник?", "Скид", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                PLCSendSingleRegister(82, 1);
                LogMessage("Скинуто загальний лічильник.", 2);
            }
        }

        private void SendPLCTime_Click(object sender, EventArgs e)
        {
            PLCSendDate();
        }

//  ===================================  ANIMATION  ===================================

        private void Anim()
        {
            //PLCTickLabel.Text = (GVL.PLCTick % 10).ToString();
            plcsec.Text = GVL.registers[83].ToString();
// ------------- flowmeter -------------
            curspd.Text = modbustcp.getReal(30).ToString() + " м/с";
            curvolflow.Text = Math.Round(modbustcp.getReal(32)/1000,3).ToString() + " м3/г";
            curmassflow.Text = Math.Round(modbustcp.getReal(34) / 1000, 3).ToString() + " т/г";
            curtemp.Text = modbustcp.getReal(36).ToString() + " °С";
            curdens.Text = modbustcp.getAccReal(38).ToString() + " кг/л";
            curcnt1.Text = modbustcp.getReal(5022).ToString();
            curcnt2.Text = modbustcp.getReal(5020).ToString();
            cnt1sett.Text = modbustcp.getReal(5022).ToString();
            cnt2sett.Text = modbustcp.getReal(5020).ToString();
            cnt3sett.Text = Math.Round(modbustcp.getLongReal(5024),2).ToString();

            cursetpoint.Text = modbustcp.getReal(5006).ToString();
            currescnt1.Text = modbustcp.getReal(5794).ToString();
            currescnt2.Text = modbustcp.getReal(5792).ToString();
            // 5792 - real mass counter resettable
            // 5794 - real volume counter resettable

// ------------- task -------------
            rdstp.Text = modbustcp.getReal(5704).ToString();
            rdmass.Text = modbustcp.getReal(5742).ToString();
            double diff = Math.Round(modbustcp.getReal(5704) - modbustcp.getReal(5742),2);
            //double pdiff = Math.Round(Math.Abs(modbustcp.getReal(5704) - modbustcp.getReal(5742))*100 / modbustcp.getReal(5704), 3);
            double pdiff = Math.Round(Math.Abs(modbustcp.getReal(5704) - modbustcp.getReal(5742)) * 100 / Double.Parse(lastSetp.Text), 3);
            rddiff.Text = diff.ToString() + " кг ("+ pdiff.ToString() +"%)";
            rdvol.Text = modbustcp.getReal(5706).ToString();
            rddens.Text = modbustcp.getAccReal(5730).ToString();
            rdtemp.Text = modbustcp.getReal(5718).ToString();
           // rdcns.Text = modbustcp.getLongReal(5744).ToString();
           // rdcnf.Text = modbustcp.getLongReal(5750).ToString();
            rdcns.Text = modbustcp.getReal(5744).ToString();
            rdcnf.Text = modbustcp.getReal(5750).ToString();

            // 5796 = 1 automatic mode / 0 - manual mode (block button "start")


// ------------- settings -------------
            plcsett00.Text = modbustcp.getReal(5008).ToString();
            plcsett11.Text = modbustcp.getReal(5010).ToString();
            plcsett22.Text = modbustcp.getReal(5012).ToString();
            plcsett33.Text = modbustcp.getReal(5014).ToString();
            plcsett44.Text = modbustcp.getReal(5016).ToString();
            plcsett55.Text = modbustcp.getReal(5018).ToString();
            plcsett66.Text = modbustcp.getReal(5028).ToString();
            plcsett77.Text = modbustcp.getReal(5030).ToString();
// ------------- valve ------------- 
            if (modbustcp.getBit(12, 2) || modbustcp.getBit(12, 3) || modbustcp.getBit(12, 8))
            {
                V1pictbox.Image = Image.FromFile("C:/FillFlex/Images/VH_red.png");
            }
            else if (!modbustcp.getBit(12, 0) && !modbustcp.getBit(12, 1)) {
                V1pictbox.Image = Image.FromFile("C:/FillFlex/Images/VH_yellow.png");
            }
            else if (modbustcp.getBit(12, 0))
            {
                V1pictbox.Image = Image.FromFile("C:/FillFlex/Images/VH_green.png");
            }
            else {
                V1pictbox.Image = Image.FromFile("C:/FillFlex/Images/VH_grey.png");
            }
// ------------- pump -------------
            if (modbustcp.getBit(0, 2) || modbustcp.getBit(0, 3) || modbustcp.getBit(25, 2))
            {
                P1pictbox.Image = Image.FromFile("C:/FillFlex/Images/PH_red.png");
            }
            else if (modbustcp.getBit(0, 1) && !modbustcp.getBit(0, 0))
            {
                P1pictbox.Image = Image.FromFile("C:/FillFlex/Images/PH_yellow.png");
            }
            else if (modbustcp.getBit(0, 0))
            {
                P1pictbox.Image = Image.FromFile("C:/FillFlex/Images/PH_green.png");
            }
            else
            {
                P1pictbox.Image = Image.FromFile("C:/FillFlex/Images/PH_grey.png");
            }
// ------------- task indicators -------------

            syscmdval.Text = GVL.registers[GVL.SysMode].ToString();
            syscmd1.Image = setInd(23, 14);
            syscmd2.Image = setIntInd(GVL.SysMode, 2);
            syscmd3.Image = setIntInd(GVL.SysMode, 3);
            syscmd4.Image = setIntInd(GVL.SysMode, 4);
            syscmd5.Image = setIntInd(GVL.SysMode, 5);
            syscmd6.Image = setIntAlm(GVL.SysMode, 6);
            syscmd7.Image = setIntInd(GVL.SysMode, 7);
            syscmd8.Image = setIntInd(GVL.SysMode, 8);
            syscmd9.Image = setIntInd(GVL.SysMode, 9);
            refInd.Image = setIntWarn(5797, 1);
// ------------- indicators ------------- 
            // valve
                vstb0.Image = setInd(12,0);
                vstb1.Image = setInd(12, 1);
                vstb2.Image = setAlm(12, 2);
                vstb3.Image = setAlm(12, 3);
                vstb8.Image = setAlm(12, 8);
                vstb9.Image = setWarn(12, 9);
                vstb10.Image = setWarn(12, 10);
            // sys status D23
                D23b0.Image = setInd(23, 0);
                D23b1.Image = setInd(23, 1);
                D23b2.Image = setInd(23, 2);
                D23b3.Image = setNCAlm(23, 3);
                D23b4.Image = setWarn(23, 4);
                D23b5.Image = setInd(23, 5);
                D23b15.Image = setAlm(23, 15);
            // flowmeter
                if (modbustcp.getBit(23, 15))
                {
                    FlowMetInd.Image = Image.FromFile("C:/FillFlex/Images/FlowMeterRed.png");
                }
                else
                {
                    FlowMetInd.Image = Image.FromFile("C:/FillFlex/Images/FlowMeterGreen.png");
                }
            // sys triggers D25
                D25b2.Image = setAlm(25, 2);
                D25b3.Image = setAlm(25, 3);
                D25b4.Image = setInd(25, 4);
                D25b5.Image = setAlm(25, 5);
                D25b7.Image = setAlm(25, 7);
                D25b8.Image = setAlm(25, 8);
            // wortktime
            // pump
                pstb0.Image = setInd(0, 0);
                pstb1.Image = setInd(0, 1);
                pstb2.Image = setAlm(0, 2);
                pstb19.Image = setInd(0, 19);
                pstb20.Image = setInd(0, 20);
                pstb3.Image = setAlm(0, 3);
                pdryrun.Image = setAlm(25, 2);

                if (GVL.registers[GVL.SysMode] == 2) {
                    GVL.FillTime++;
                }
                filltimelabel.Text = "Тривалість: " + TimeFmt(GVL.FillTime);

                if (GVL.registers[GVL.SysMode] == 6 && manBtn.Checked)
                {
                    manBtn.ForeColor = Color.Red;
                }
                else {
                    manBtn.ForeColor = Color.Black;
                }

            /*
                if (GVL.registers[GVL.SysMode] == 0)
                {
                    manBtn.Enabled = true;
                    autBtn.Enabled = true;
                }
                else {
                    manBtn.Enabled = false;
                    autBtn.Enabled = false;               
                }
            */
                if (!GVL.FirstCycle && modbustcp.Connected())
                {
                    if (GVL.registers[5796] >= 1)
                    {
                        autBtn.Checked = true;
                        manBtn.Checked = false;
                    }
                    else
                    {
                        autBtn.Checked = false;
                        manBtn.Checked = true;
                    }
                    GVL.FirstCycle = true;
                    GVL.reservedStartCounterValue = curcnt2.Text.Replace(",", ".");
                }
        }

//  ===================================  FUNCTIONS  ===================================

        private Image setInd(int register, int bit) {
            if (bit > 15)
            {
                if (modbustcp.getBit(register+1, bit-16))
                {
                    return Image.FromFile("C:/FillFlex/Images/Ind_green.png");
                }
                else
                {
                    return Image.FromFile("C:/FillFlex/Images/Ind_grey.png");
                }
            }
            else {
                if (modbustcp.getBit(register, bit))
                {
                    return Image.FromFile("C:/FillFlex/Images/Ind_green.png");
                }
                else
                {
                    return Image.FromFile("C:/FillFlex/Images/Ind_grey.png");
                }
            }
        }

        private Image setAlm(int register, int bit)
        {
            if (bit > 15)
            {
                if (modbustcp.getBit(register + 1, bit - 16))
                {
                    return Image.FromFile("C:/FillFlex/Images/Ind_red.png");
                }
                else
                {
                    return Image.FromFile("C:/FillFlex/Images/Ind_grey.png");
                }
            }
            else
            {
                if (modbustcp.getBit(register, bit))
                {
                    return Image.FromFile("C:/FillFlex/Images/Ind_red.png");
                }
                else
                {
                    return Image.FromFile("C:/FillFlex/Images/Ind_grey.png");
                }
            }
        }

        private Image setNCAlm(int register, int bit)
        {
            if (bit > 15)
            {
                if (modbustcp.getBit(register + 1, bit - 16))
                {
                    return Image.FromFile("C:/FillFlex/Images/Ind_grey.png");
                }
                else
                {
                    return Image.FromFile("C:/FillFlex/Images/Ind_red.png");
                }
            }
            else
            {
                if (modbustcp.getBit(register, bit))
                {
                    return Image.FromFile("C:/FillFlex/Images/Ind_grey.png");
                }
                else
                {
                    return Image.FromFile("C:/FillFlex/Images/Ind_red.png");
                }
            }
        }

        private Image setWarn(int register, int bit)
        {
            if (bit > 15)
            {
                if (modbustcp.getBit(register + 1, bit - 16))
                {
                    return Image.FromFile("C:/FillFlex/Images/Ind_yellow.png");
                }
                else
                {
                    return Image.FromFile("C:/FillFlex/Images/Ind_grey.png");
                }
            }
            else
            {
                if (modbustcp.getBit(register, bit))
                {
                    return Image.FromFile("C:/FillFlex/Images/Ind_yellow.png");
                }
                else
                {
                    return Image.FromFile("C:/FillFlex/Images/Ind_grey.png");
                }
            }
        }

        private string WorkTimeFmt(int seconds) {
            int s = seconds % 60;
            int m = (seconds % 3600) / 60;
            int h = seconds / 3600;
            return h.ToString() + " год. " + m.ToString("d2") + " хв. " + s.ToString("d2") + " с";
        }

        private string TimeFmt(int seconds)
        {
            int s = seconds % 60;
            int m = (seconds % 3600) / 60;
            return m.ToString("d2") + " хв. " + s.ToString("d2") + " с";
        }

        private Image setIntInd(int register, int value)
        {
            if (GVL.registers[register] == value)
            {
                return Image.FromFile("C:/FillFlex/Images/Ind_green.png");
            }
            else
            {
                return Image.FromFile("C:/FillFlex/Images/Ind_grey.png");
            }
        }

        private Image setIntAlm(int register, int value)
        {
            if (GVL.registers[register] == value)
            {
                return Image.FromFile("C:/FillFlex/Images/Ind_red.png");
            }
            else
            {
                return Image.FromFile("C:/FillFlex/Images/Ind_grey.png");
            }
        }

        private Image setIntWarn(int register, int value)
        {
            if (GVL.registers[register] == value)
            {
                return Image.FromFile("C:/FillFlex/Images/Ind_yellow.png");
            }
            else
            {
                return Image.FromFile("C:/FillFlex/Images/Ind_grey.png");
            }
        }

        private void BitEventLogOn(int register, int bit, ref bool fix, string sText, int category) {
            if (modbustcp.getBit(register, bit) && !fix) {
                LogMessage(sText, category);
                fix = true;
            }
            if (!modbustcp.getBit(register, bit) && fix) {
                fix = false;
            }
        }

        private void BitEventLogOff(int register, int bit, ref bool fix, string sText, int category)
        {
            if (!modbustcp.getBit(register, bit) && !fix)
            {
                LogMessage(sText, category);
                fix = true;
            }
            if (modbustcp.getBit(register, bit) && fix)
            {
                fix = false;
            }
        }

        private void IntEventLogOn(int register, int value, ref bool fix, string sText, int category)
        {
            if (GVL.registers[register] == value && !fix)
            {
                LogMessage(sText, category);
                fix = true;
            }
            if (GVL.registers[register] != value && fix)
            {
                fix = false;
            }
        }

        private void showWin(string sTitle, int Index){
            mehForm MehWindow = new mehForm(sTitle, Index);
            MehWindow.Show();
            System.Drawing.Point WinLocation = new System.Drawing.Point(MousePosition.X - (MehWindow.Width / 2), MousePosition.Y - (MehWindow.Height / 2));
            MehWindow.Location = WinLocation;
        }

// =================================== SCRIPTS ===================================

        private void checkEvents() {
            BitEventLogOn(12, 0, ref GVL.FixEvent[0], "Клапан відкрито.", 2);
            BitEventLogOn(12, 1, ref GVL.FixEvent[1], "Клапан закрито.", 2);
            BitEventLogOn(12, 2, ref GVL.FixEvent[2], "Аварія. Клапан не відкрився.", 1);
            BitEventLogOn(12, 3, ref GVL.FixEvent[3], "Аварія. Клапан не закрився.", 1);
            BitEventLogOn(12, 8, ref GVL.FixEvent[4], "Аварія. Зміщення позиції клапана відкр + закр.", 1);
            BitEventLogOn(0, 0, ref GVL.FixEvent[5], "Насос включено.", 2);
            BitEventLogOff(0, 0, ref GVL.FixEvent[6], "Насос виключено.", 2);
            BitEventLogOn(0, 2, ref GVL.FixEvent[7], "Аварія. Насос не включився.", 1);
            BitEventLogOn(25, 2, ref GVL.FixEvent[8], "Аварія. Робота насосу з мін. витратою.", 1);
            BitEventLogOn(25, 5, ref GVL.FixEvent[9], "Аварійна зупинка. Натиснена кнопка Стоп.", 1);
            BitEventLogOn(25, 7, ref GVL.FixEvent[10], "Не можу скинути лічильник або самохід", 1);
            BitEventLogOn(25, 3, ref GVL.FixEvent[11], "Аварія. Відсутній зв'язок з витратоміром.", 1); // event fixed
            IntEventLogOn(GVL.SysMode, 1, ref GVL.FixEvent[12], "Система готова до наливу.", 2);
            IntEventLogOn(GVL.SysMode, 6, ref GVL.FixEvent[13], "Аварія в процесі наливу.", 1);

            // START  == 2
            if (GVL.registers[GVL.SysMode] == 2 && !GVL.FixEvent[14])
            {
                AppDb.updateOnStart();
                LogMessage("Завдання " + lastId.Text + " : " + lastPlcId.Text + " запущено", 2);
                GVL.dbrec = true;
                GVL.FixEvent[14] = true;
            }
            if (GVL.registers[GVL.SysMode] != 2 && GVL.FixEvent[14])
            {
                GVL.FixEvent[14] = false;
            }

            // PAUSE  == 4
            if (GVL.registers[GVL.SysMode] == 4 && !GVL.FixEvent[15])
            {
                AppDb.updateOnPause();
                LogMessage("Завдання " + lastId.Text + " : " + lastPlcId.Text + " зупинено", 2);
                GVL.dbrec = true;
                GVL.FixEvent[15] = true;
            }
            if (GVL.registers[GVL.SysMode] != 4 && GVL.FixEvent[15])
            {
                GVL.FixEvent[15] = false;
            }
            // FINISH       
            if (GVL.registers[GVL.SysMode] == 3 && !GVL.FixEvent[16])
            {
                LogMessage("Завдання " + lastId.Text + " : " + lastPlcId.Text + " завершено.", 2);
                FinBtn.Enabled = true;
                FinBtn.Focus();
                GVL.FixEvent[16] = true;
                modbustcp.sendSingleRegister(5797, 0); // reset refill mode
                showMessage("Завдання завершено." + Environment.NewLine + "Для розрахунку даних наливу натисність кнопку «Завершити».");
                GVL.FillTime = 0;
            }
            if (GVL.registers[GVL.SysMode] != 3 && GVL.FixEvent[16])
            {
                GVL.FixEvent[16] = false;
            }

            /*
            if (GVL.registers[GVL.SysMode] == 5 && !GVL.FixEvent[15])
            {
                LogMessage("Завдання " + lastId.Text + " : " + lastPlcId.Text + " завершено.", 2);
                GVL.FixEvent[15] = true;
                FinBtn.Enabled = true;
                FinBtn.Focus();
                showMessage("Завдання завершено." + Environment.NewLine + "Для розрахунку даних наливу натисність кнопку «Завершити».");
            }
            if (GVL.registers[GVL.SysMode] != 5 && GVL.FixEvent[15])
            {
                GVL.FixEvent[15] = false;
            }
            */

            if (GVL.registers[GVL.SysMode] == 9 && !GVL.FixEvent[17])
            {
                LogMessage("Розрахунок по наливу " + lastId.Text + " : " + lastPlcId.Text + " завершено.", 2);
                SendFillDataToDB();
                Thread.Sleep(500);
                GVL.FixEvent[17] = true;
                DialogResult res = MessageBox.Show("Розрахунок по наливу завершено.", "Налив", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (res == DialogResult.OK && double.Parse(rdmass.Text) != 0 && double.Parse(rdvol.Text) != 0 && GVL.DataWritten)
                {
                    PLCSendSingleRegister(GVL.SysMode, 8);
                    GVL.DataWritten = false;
                }
            }
            if (GVL.registers[GVL.SysMode] == 0 && GVL.FixEvent[17])
            {
                GVL.FixEvent[17] = false;
            }

            BitEventLogOn(23, 15, ref GVL.FixEvent[18], "Аварія. Відсутній зв'язок з витратоміром.", 1); // real state
            BitEventLogOff(23, 15, ref GVL.FixEvent[19], "Зв'язок з витратоміром встановлено.", 2); // real state
            BitEventLogOn(25, 8, ref GVL.FixEvent[20], "Аварія. Лічильники вимкнено.", 1);
            IntEventLogOn(5797, 1, ref GVL.FixEvent[21], "Включено режим доливу.", 2);

            if (modbustcp.getBit(25, 10) && !GVL.FixEvent[22])
            {
                GVL.FixEvent[22] = true;
                LogMessage("Налив вимкнувся без досягнення завдання. Потрібен долив", 1);
                string sMessage = "Налив вимкнувся без досягнення завдання." + Environment.NewLine;
                sMessage += "Для завершення завдання виконайте наступні дії:" + Environment.NewLine;
                sMessage += "  1. Включіть режим Доливу" + Environment.NewLine;
                sMessage += "  2. Натисність кнопку Пуск";
                DialogResult res = MessageBox.Show(sMessage, "Налив", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (res == DialogResult.OK)
                {
                    int register = 25;
                    int value = Convert.ToUInt16(Math.Pow(2, 4)) & GVL.registers[register] | Convert.ToUInt16(Math.Pow(2, 15)); // bit 4 & 15 = 1  - unsigned
                    PLCSendSingleRegister(register, value);
                }
            }

            if (!modbustcp.getBit(25, 10) && GVL.FixEvent[22])
            {
                GVL.FixEvent[22] = false;
            }


        }

        private void SendFillDataToDB(){

            List<string> data = new List<string>();
            data.Add(rdmass.Text.Replace(",", "."));    // mass
            data.Add(rdvol.Text.Replace(",", "."));     // vol
            data.Add(rddens.Text.Replace(",", "."));    // dens
            data.Add(rdtemp.Text.Replace(",", "."));    // temp
            data.Add(rdcns.Text.Replace(",", "."));   // cntst
            data.Add(rdcnf.Text.Replace(",", "."));  // cntfin

            AppDb.updateOnDone(data);
            LogMessage("Завдання " + lastId.Text + " : " + lastPlcId.Text + " успішно внесено до БД.", 2);
            GVL.dbrec = true;        
        }

        private void showProgress() {
            double Val = Math.Round((modbustcp.getReal(5792) / Single.Parse(lastSetp.Text))*100,2);
            pgVal.Text = Val.ToString() + "%";
            if (Val > 100.0) {
                Val = 100.0;
            }
            else if (Val < 0.0) {
                Val = 0.0;
            }
            progressBar1.Value = Convert.ToInt16(Val);
        }
       
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (GVL.isAdmin)
            {
                GVL.isAdmin = false;
                showMessage("Ви вийшли з системи");
            }
            else {
                if (!GVL.LoginFormShow) {
                    LoginForm LoginFrm = new LoginForm();
                    LoginFrm.Show();
                    GVL.LoginFormShow = true;
                }
            }
        }

        private void SettControl(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 1) {
                if (GVL.isAdmin)
                {
                    this.tabPage2.Show();
                }
                else
                {
                    this.tabPage2.Hide();
                }
            }
        }

        private void linkWindow1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            showWin("Насос 1", GVL.indexes[3]);
        }

        private void linkWindow2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            showWin("Насос 2", GVL.indexes[4]);
        }


        private void showTrend() {
            GVL.tempValue = modbustcp.getReal(36);
            tempVal.Text = GVL.tempValue.ToString() + "°С";
            curTrendPoints.Text = tempseries.Points.Count.ToString();
            string sXLabel = TimeLabel.Text + Environment.NewLine + DateLabel.Text;
            tempseries.Points.AddXY(sXLabel, GVL.tempValue);
            //tempseries.Points.AddXY(DateTime.Now.Second, GVL.tempValue);
            if (tempseries.Points.Count > GVL.trendWidth)
            {
                tempseries.Points.RemoveAt(0);
            }
            TempChart.Invalidate();
        }

        private void GetToolTipText(object sender, ToolTipEventArgs e)
        {
            switch (e.HitTestResult.ChartElementType)
            {
                case ChartElementType.DataPoint:
                    var dataPoint = e.HitTestResult.Series.Points[e.HitTestResult.PointIndex];
                    //e.Text = string.Format("X:\t{0}\nY:\t{1}", dataPoint.XValue, dataPoint.YValues[0]);
                    e.Text = dataPoint.YValues[0].ToString();
                    break;
            }
        }

        private void manBtn_Click(object sender, EventArgs e)
        {
            if (!manBtn.Checked) {
                autBtn.Checked = false;
                manBtn.Checked = true;
            }
            if (GVL.registers[5796] != 0) {
                modbustcp.sendSingleRegister(5796, 0);
            }
        }

        private void autBtn_Click(object sender, EventArgs e)
        {
            if (!autBtn.Checked)
            {
                autBtn.Checked = true;
                manBtn.Checked = false;
            }
            if (GVL.registers[5796] != 1)
            {
                modbustcp.sendSingleRegister(5796, 1);
            }
        }

        private void showRefill_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!GVL.isRefill) {
                RefillWind refillform = new RefillWind();
                refillform.Show();
                System.Drawing.Point WinLocation = new System.Drawing.Point(MousePosition.X - (refillform.Width / 2), MousePosition.Y - (refillform.Height / 2));
                refillform.Location = WinLocation;
            }
        }

        private void HoverRefillMode(object sender, EventArgs e)
        {
            showRefill.ForeColor = Color.Blue;
        }


    }
}
