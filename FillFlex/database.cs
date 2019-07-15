  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Spire;
using Spire.License;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Pdf.Tables;

  namespace FillFlex
{
    public class database
    {
        string sDSNname = "FillFlex";
        OdbcConnection connection = new OdbcConnection("dsn=FillFlex;uid=leviserg;pwd=pohtefdg");
        OdbcCommand command = new OdbcCommand();
        DBNull dbnull;

        public void OpenDb() {
            try
            {
                command.Connection = connection;
                connection.Open();
            }
            catch (OdbcException e)
            {
                Console.WriteLine(e.Message + ". Data: " + e.Data);
            }
        }

        public void CloseDb()
        {
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public void InsRecord(double value, string sPlcId, string sTankId, string sTransp, string sDriver, string sCustomer, int SourceTank) { 
            /*
            string sPlcId = DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss");
            sPlcId = sPlcId.Replace(".","");
            sPlcId = sPlcId.Replace(" ", "");
            */
            string sValue = value.ToString().Replace(",",".");
            string sqlQuery = "insert into datatable (`plcid`, `datecreate`, `setp`, `status`, `tankId`,`transp`,`driver`,`customer`,`sourcetank`) values ";
            sqlQuery += "('" + sPlcId + "', now(), '" + sValue + "','створено', '" + sTankId + "', '" + sTransp + "', '" + sDriver + "', '" + sCustomer + "', '" + SourceTank.ToString() + "')";
            command.CommandText = sqlQuery;
            command.ExecuteNonQuery();
            GVL.DataWritten = false;
        }

        public void updateOnReset(List<string> data)
        {
            string sqlQuery = "update `datatable` set `datefin` = now(), `status` = 'відхилено', `mass` = '" + data[0];
            sqlQuery += "', `vol` = '" + data[1] + "', `cntst` = '" + data[2] + "', `cntfin` = '" + data[3] + "' where id='" + maxId() + "'";
            command.CommandText = sqlQuery;
            command.ExecuteNonQuery();
        }

        public void updateOnDone(List<string> data)
        {
            string sqlQuery = "update `datatable` set `datefin` = now(), `status` = 'виконано', ";
            sqlQuery += "`mass` = '" + data[0] + "', `vol` = '" + data[1] + "', `dens` = '" + data[2] + "', ";
            sqlQuery += "`temp` = '" + data[3] + "', `cntst` = '" + data[4] + "', `cntfin` = '" + data[5] + "' where id='" + maxId() + "'";
            command.CommandText = sqlQuery;
            command.ExecuteNonQuery();
            GVL.DataWritten = true;
        }

        public void updateOnStart()
        {
            string sqlQuery = "update `datatable` set `datestart` = now(), `status` = 'запущено' where id='" + maxId() + "'";
            command.CommandText = sqlQuery;
            command.ExecuteNonQuery();
        }

        public void updateOnUpload()
        {
            string sqlQuery = "update `datatable` set `status` = 'завантажено' where id='" + maxId() + "'";
            command.CommandText = sqlQuery;
            command.ExecuteNonQuery();
        }

        public void updateOnPause()
        {
            string sqlQuery = "update `datatable` set `status` = 'зупинено' where id='" + maxId() + "'";
            command.CommandText = sqlQuery;
            command.ExecuteNonQuery();
        }

        public int maxId() {
            int ret = 0;
            string sqlQuery = "select max(id) from datatable";
            command.CommandText = sqlQuery;
            string sRes = command.ExecuteScalar().ToString();
            ret = int.Parse(sRes);
            return ret;
        }

        public int RecCount()
        {
            int ret = 0;
            string sqlQuery = "select count(id) from datatable";
            command.CommandText = sqlQuery;
            string sRes = command.ExecuteScalar().ToString();
            ret = int.Parse(sRes);
            return ret;
        }

        public string lastDate() {
            string sqlQuery = "select max(datefin) from datatable";
            command.CommandText = sqlQuery;
            string sRes = command.ExecuteScalar().ToString();
            return sRes;
        }

        public DataTable dataTable() {
            DataTable dt = new DataTable();
            //string sqlQuery = "select id as ID, datecreate as Створено, datestart as Запущено, datefin as Завершено, ";
            string sqlQuery = "select id as ID, datecreate as Створено, datefin as Завершено, ";
            sqlQuery += "setp as Завдання, mass as Маса, vol as Об´єм, dens as Густина, temp as t°С, ";
            sqlQuery += "cntst as Початкове, cntfin as Кінцеве, status as Статус, sourcetank as Бак from datatable order by id desc limit " + GVL.DBRecords.ToString();
            command.CommandText = sqlQuery;
            OdbcDataReader dataReader = command.ExecuteReader();
            dt.Load(dataReader);
            dataReader.Close();
            return dt;
        }

        public List<string> selectTask(int id) 
        {
           var retList = new List<string>();
            string sqlQuery = "select * from datatable where id='" + id.ToString() + "'";
            command.CommandText = sqlQuery;
            OdbcDataReader dataReader = command.ExecuteReader();
            dataReader.Read();
            for(int i = 0; i < dataReader.FieldCount; i++)
            {
                string StringRes = dataReader.GetValue(i).ToString();
                retList.Add(StringRes);
            }
            dataReader.Close();
           return retList;
        }

        public List<string> getLastRec()
        {
            var retList = new List<string>();
            string sqlQuery = "select * from datatable where id=(select max(id) from datatable)";
            command.CommandText = sqlQuery;
            OdbcDataReader dataReader = command.ExecuteReader();
            dataReader.Read();
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                string StringRes = dataReader.GetValue(i).ToString();
                retList.Add(StringRes);
            }
            dataReader.Close();
            return retList;
        }

        public void printReport(DateTime sDateStart, DateTime sDateEnd, string sStatus, int Tank)
        {
            string sFileName = DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss");
            sFileName = sFileName.Replace(".", "");
            sFileName = "Report_" + sFileName.Replace(" ", "");
            string sStartDate = sDateStart.Year + "-" + sDateStart.Month + "-" + sDateStart.Day;
            string sFinDate = sDateEnd.Year + "-" + sDateEnd.Month + "-" + sDateEnd.Day;

            string sqlQuery = "select id, datefin, tankId, setp, mass, vol, cntst, cntfin, status, sourcetank from datatable";
            sqlQuery += " where (datecreate between '" + sStartDate + " 00:00:00' and '" + sFinDate + " 23:59:59')";
            if (!sStatus.Equals("всі"))
            {
                sqlQuery += " and (status = '" + sStatus + "')";
            }
            if (Tank > 0) {
                sqlQuery += " and (sourcetank = '" + Tank.ToString() + "')";
            }
            sqlQuery += " union select '', 'Разом', '', '', sum(mass), sum(vol), '', '', '', '' from datatable";
            sqlQuery += " where (datecreate between '" + sStartDate + " 00:00:00' and '" + sFinDate + " 23:59:59')";
            command.CommandText = sqlQuery;
            OdbcDataReader dataReader = command.ExecuteReader();

            List<List<string>> data = new List<List<string>>();
            List<string> dataHeader = new List<string>();
                dataHeader.Add("ID");
                dataHeader.Add("Дата");
                dataHeader.Add("Флексітанк");
                dataHeader.Add("Завдання,кг");
                dataHeader.Add("Маса,кг");
                dataHeader.Add("Об'єм,л");
                dataHeader.Add("Поч.покази, кг");
                dataHeader.Add("Кін.покази, кг");
                dataHeader.Add("Статус");
                dataHeader.Add("Бак");
                data.Add(dataHeader);
            int records = 0;
            while(dataReader.Read()){
                List<string> dataRow = new List<string>();
                for (int i = 0; i < dataHeader.Count; i++) {
                    if (!dataReader.GetValue(i).Equals(dbnull))
                    {
                        dataRow.Add(dataReader.GetValue(i).ToString());
                    }
                    else {
                        dataRow.Add("");
                    }
                }
                data.Add(dataRow);
                records++;
            }
            dataReader.Close();
            if (records <= 1) {
                MessageBox.Show("Записів за вибраними критеріями не знайдено", "Звіт", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string[,] dataArray = new string[data.Count, data[0].Count];
            for (int i = 0; i < data.Count; i++) {
                for (int j = 0; j < data[0].Count; j++) {
                    dataArray[i, j] = data[i][j];
                }
            }

            PdfDocument doc = new PdfDocument();
            PdfUnitConvertor unitCvtr = new PdfUnitConvertor();
            PdfMargins margin = new PdfMargins();
                margin.Top = unitCvtr.ConvertUnits(1.5F, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
                margin.Bottom = margin.Top;
                margin.Left = unitCvtr.ConvertUnits(1.7F, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
                margin.Right = unitCvtr.ConvertUnits(0.6F, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
            PdfPageBase page = doc.Pages.Add(PdfPageSize.A4, margin);
            PdfStringFormat alignCenter = new PdfStringFormat(PdfTextAlignment.Center);
            PdfStringFormat alignLeft = new PdfStringFormat(PdfTextAlignment.Left);
            PdfStringFormat alignJustify = new PdfStringFormat(PdfTextAlignment.Justify);
            PdfStringFormat alignRight = new PdfStringFormat(PdfTextAlignment.Right);
            Single y = 4;
            PdfBrush brush1 = PdfBrushes.Black;
            PdfTrueTypeFont font1 = new PdfTrueTypeFont(new Font("Times", 16, FontStyle.Bold), true);
            PdfBrush brush2 = PdfBrushes.DarkGray;
            PdfTrueTypeFont font2 = new PdfTrueTypeFont(new Font("Arial", 8, FontStyle.Regular), true);
            PdfTrueTypeFont fontTable = new PdfTrueTypeFont(new Font("Times", 10, FontStyle.Regular), true);
            PdfTrueTypeFont fontHeader = new PdfTrueTypeFont(new Font("Arial", 8, FontStyle.Bold), true);
            PdfStringFormat format1 = alignCenter;
            string sTitle = "Дані наливу з " + sDateStart.Day.ToString("d2") + "." + sDateStart.Month.ToString("d2") + "." + sDateStart.Year + " по ";
            sTitle += sDateEnd.Day.ToString("d2") + "." + sDateEnd.Month.ToString("d2") + "." + sDateEnd.Year + " (включно)";
            page.Canvas.DrawString(sTitle, font1, brush1, page.Canvas.ClientSize.Width / 2, y + 20, format1);
            y += 30 + font1.MeasureString(sTitle, format1).Height;
            PdfTable table = new PdfTable();
            table.Style.BorderPen = new PdfPen(brush1, 0.001f);
            table.Style.DefaultStyle.Font = fontTable;
            table.Style.DefaultStyle.BorderPen = new PdfPen(brush1, 0.001f);
            table.Style.CellPadding = 1;
            table.Style.HeaderSource = PdfHeaderSource.Rows;
            table.Style.HeaderRowCount = 1;
            table.Style.ShowHeader = true;
            table.Style.HeaderStyle.Font = fontHeader;
            table.Style.RepeatHeader = true;
            table.Style.HeaderStyle.BackgroundBrush = PdfBrushes.LightGray;
            table.Style.HeaderStyle.StringFormat = alignCenter;
            table.Style.HeaderStyle.StringFormat.LineAlignment = PdfVerticalAlignment.Middle;
            table.Style.DefaultStyle.StringFormat = alignCenter;
            table.Style.HeaderStyle.StringFormat.LineAlignment = PdfVerticalAlignment.Middle;

            table.DataSource = dataArray;

            Single tableWidth = page.Canvas.ClientSize.Width - (table.Columns.Count + 1) * table.Style.BorderPen.Width;

            // id, datefin, tankId, setp, mass, vol, cntst, cntfin, status, sourcetank
            table.Columns[0].Width = 0.05f * tableWidth; // ID
            table.Columns[1].Width = 0.2f * tableWidth;  // Date
            table.Columns[2].Width = 0.2f * tableWidth;  // tankId 0.4
            table.Columns[3].Width = 0.075f * tableWidth;  // setp
            table.Columns[4].Width = 0.075f * tableWidth;  // mass 0.55
            table.Columns[5].Width = 0.075f * tableWidth;  // vol  0.625
            table.Columns[6].Width = 0.1f * tableWidth;  // count start
            table.Columns[7].Width = 0.1f * tableWidth;  // count fin
            table.Columns[8].Width = 0.1f * tableWidth; // status
            table.Columns[9].Width = 0.025f * tableWidth; // sourceTank

            PdfLayoutResult result = table.Draw(page, new PointF(0, y));
            y += result.Bounds.Height + 10;
            page.Canvas.DrawString(String.Format("* {0} записів знайдено ", records - 1), font2, brush2, 5, 0);
            page.Canvas.DrawString("Створено " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"), font2, brush2, 200, 0);
            doc.SaveToFile("C:/FillFlex/Reports/" + sFileName + ".pdf");
            doc.Close();

            DialogResult res =  MessageBox.Show("C:/FillFlex/Reports/" + sFileName + ".pdf успішно створено. Відкрити?", "Звіт", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("C:/FillFlex/Reports/" + sFileName + ".pdf");
            }
        }
        // *********** end print report *********** 


    }
}
