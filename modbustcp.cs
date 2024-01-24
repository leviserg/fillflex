using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FillFlex
{
    static class modbustcp
    {

        private static EasyModbus.ModbusClient PLC = new EasyModbus.ModbusClient();

        public static EasyModbus.ModbusClient plcInstance {
            get { return PLC; }
        }

        public static bool OpenConn() {
            string IP = GVL.settings[0];
            int Port = int.Parse(GVL.settings[1]);
            try {
                if (!PLC.Connected) {
                    PLC.Connect(IP, Port);
                }
                return true;
            }
            catch (EasyModbus.Exceptions.ConnectionException e) {
                PLC.Disconnect();
                GVL.readexc = true;
                return false;
            }
            catch(System.Net.Sockets.SocketException e){
                PLC.Disconnect();
                GVL.readexc = true;
                return false;
            }
        }

        public static void CloseConn()
        {
            if (PLC.Connected) {
                PLC.Disconnect();
            }
            GVL.readexc = false;
        }

        public static void ReadRegisters(int start, int length) {
            if (PLC.Connected) {
                int[] data = new int[length];
                GVL.readexc = false;
                try
                {
                    data = PLC.ReadHoldingRegisters(start, length);
                    for (int i = 0; i < length; i++)
                    {
                        GVL.registers[i + start] = data[i];
                    }
                }
                catch (EasyModbus.Exceptions.ModbusException e)
                {
                    GVL.readexc = true;
                    for (int i = 0; i < length; i++)
                    {
                        GVL.registers[i + start] = -200;
                    }
                    MessageBox.Show("Error Read Registers : " + e.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (System.Net.Sockets.SocketException e)
                {
                    GVL.readexc = true;
                    for (int i = 0; i < length; i++)
                    {
                        GVL.registers[i + start] = -500;
                    }
                    MessageBox.Show("Error Socket Registers : " + e.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (System.IO.IOException e)
                {
                    GVL.readexc = true;
                    for (int i = 0; i < length; i++)
                    {
                        GVL.registers[i + start] = -501;
                    }
                    //MessageBox.Show("Error PLC connection : " + e.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Error PLC connection.", "Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static bool Connected()
        {
            return PLC.Connected;
        }
        
        public static double GetReal(int regNum){
            int[] Val = new int[2];
            Val[0] = GVL.registers[regNum];//0
            Val[1] = GVL.registers[regNum + 1];//+1
            return Math.Round(EasyModbus.ModbusClient.ConvertRegistersToFloat(Val),2);
        }

        public static double GetAccReal(int regNum)
        {
            int[] Val = new int[2];
            Val[0] = GVL.registers[regNum];//0
            Val[1] = GVL.registers[regNum + 1];//+1
            return Math.Round(EasyModbus.ModbusClient.ConvertRegistersToFloat(Val), 4);
        }

        public static int GetDint(int regNum) {
            int[] Val = new int[2];
            Val[0] = GVL.registers[regNum];
            Val[1] = GVL.registers[regNum + 1];
            return EasyModbus.ModbusClient.ConvertRegistersToInt(Val);
        }

        public static double GetLongReal(int regNum)
        {
            byte[] bytes = new byte[8];
            bytes = BitConverter.GetBytes(GVL.registers[regNum] + 65536 * GVL.registers[regNum + 1] + 4294967296 * (GVL.registers[regNum+2] + 65536 * GVL.registers[regNum+3]));
            return BitConverter.ToDouble(bytes, 0);
            //string res = "0x" + Convert.ToInt16(GVL.registers[regNum + 3]).ToString("X") + Convert.ToInt16(GVL.registers[regNum + 2]).ToString("X") + Convert.ToInt16(GVL.registers[regNum + 1]).ToString("X") + Convert.ToInt16(GVL.registers[regNum]).ToString("X");
            //return Math.Round(BitConverter.Int64BitsToDouble(Convert.ToInt64(res, 16)), 2);
        }

        public static void SendDate() { 
            int[] data = new int[7];
            data[0] = 1;
            data[1] = DateTime.Now.Year;
            data[2] = DateTime.Now.Month;
            data[3] = DateTime.Now.Day;
            data[4] = DateTime.Now.Hour;
            data[5] = DateTime.Now.Minute;
            data[6] = DateTime.Now.Second;
            try
            {
                PLC.WriteMultipleRegisters(84, data);
            }
            catch (EasyModbus.Exceptions.ModbusException e) {
                GVL.readexc = true;
                MessageBox.Show("Error Write Date : " + e.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void SendLocCmd(int start, int index, int command)
        {
            int[] data = new int[2];
            data[0] = command;
            data[1] = index;
            try
            {
                PLC.WriteMultipleRegisters(start, data);
            }
            catch (EasyModbus.Exceptions.ModbusException e)
            {
                GVL.readexc = true;
                MessageBox.Show("Error Write Command : " + e.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void WriteRegisters(int start, int[] data)
        {
            try
            {
                PLC.WriteMultipleRegisters(start, data);
            }
            catch (EasyModbus.Exceptions.ModbusException e)
            {
                GVL.readexc = true;
                MessageBox.Show("Error Write Command : " + e.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void SendSingleRegister(int reg, int value)
        {
            try
            {
                PLC.WriteSingleRegister(reg, value);
            }
            catch (EasyModbus.Exceptions.ModbusException e)
            {
                GVL.readexc = true;
                MessageBox.Show("Error Write Command : " + e.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.NullReferenceException e)
            {
                GVL.readexc = true;
                MessageBox.Show("Error Write Command : " + e.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void SaveSettings(int start, Single[] data) {
            int[] reg = new int[data.Length * 2];
            for(int i = 0; i < data.Length; i++){
                int[] loc = new int[2];
                loc = EasyModbus.ModbusClient.ConvertFloatToRegisters(data[i]);
                reg[i * 2] = loc[0];
                reg[i * 2 + 1] = loc[1];
            }
            try
            {
                PLC.WriteMultipleRegisters(start, reg);
            }
            catch (EasyModbus.Exceptions.ModbusException e)
            {
                GVL.readexc = true;
                MessageBox.Show("Error Write Settings : " + e.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static bool GetBit(int register, int bit) {
            return (GVL.registers[register] & Convert.ToInt32(Math.Pow(2, bit))) > 0;
        }

    }
}
