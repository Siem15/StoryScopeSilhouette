using System;
using System.Collections;
using System.Text;

namespace OSC.NET
{
    /// <summary>
    /// OSCPacket
    /// </summary>
    abstract public class OSCPacket
    {
        public OSCPacket()
        {
            values = new ArrayList();
        }

        protected static void AddBytes(ArrayList data, byte[] bytes)
        {
            foreach (byte b in bytes)
            {
                data.Add(b);
            }
        }

        protected static void PadNull(ArrayList data)
        {
            byte zero = 0;
            int pad = 4 - (data.Count % 4);

            for (int i = 0; i < pad; i++)
            {
                data.Add(zero);
            }
        }

        protected static byte[] swapEndian(byte[] data)
        {
            byte[] swapped = new byte[data.Length];

            for (int i = data.Length - 1, j = 0; i >= 0; i--, j++)
            {
                swapped[j] = data[i];
            }

            return swapped;
        }

        protected static byte[] packInt(int value)
        {
            byte[] data = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                data = swapEndian(data);
            }

            return data;
        }

        protected static byte[] PackLong(long value)
        {
            byte[] data = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                data = swapEndian(data);
            }

            return data;
        }

        protected static byte[] PackFloat(float value)
        {
            byte[] data = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                data = swapEndian(data);
            }

            return data;
        }

        protected static byte[] PackDouble(double value)
        {
            byte[] data = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                data = swapEndian(data);
            }

            return data;
        }

        protected static byte[] PackString(string value) => Encoding.ASCII.GetBytes(value);

        abstract protected void Pack();

        protected byte[] binaryData;

        public byte[] BinaryData
        {
            get
            {
                Pack();
                return binaryData;
            }
        }

        protected static int UnpackInt(byte[] bytes, ref int start)
        {
            byte[] data = new byte[4];

            for (int i = 0; i < 4; i++, start++)
            {
                data[i] = bytes[start];
            }

            if (BitConverter.IsLittleEndian)
            {
                data = swapEndian(data);
            }

            return BitConverter.ToInt32(data, 0);
        }

        protected static long UnpackLong(byte[] bytes, ref int start)
        {
            byte[] data = new byte[8];

            for (int i = 0; i < 8; i++, start++)
            {
                data[i] = bytes[start];
            }

            if (BitConverter.IsLittleEndian)
            {
                data = swapEndian(data);
            }

            return BitConverter.ToInt64(data, 0);
        }

        protected static float UnpackFloat(byte[] bytes, ref int start)
        {
            byte[] data = new byte[4];

            for (int i = 0; i < 4; i++, start++)
            {
                data[i] = bytes[start];
            }

            if (BitConverter.IsLittleEndian)
            {
                data = swapEndian(data);
            }

            return BitConverter.ToSingle(data, 0);
        }

        protected static double unpackDouble(byte[] bytes, ref int start)
        {
            byte[] data = new byte[8];

            for (int i = 0; i < 8; i++, start++)
            {
                data[i] = bytes[start];
            }

            if (BitConverter.IsLittleEndian)
            {
                data = swapEndian(data);
            }

            return BitConverter.ToDouble(data, 0);
        }

        protected static string UnpackString(byte[] bytes, ref int start)
        {
            int count = 0;
            //for (int index = start; bytes[index] != 0; index++, count++) {}
            string s = Encoding.ASCII.GetString(bytes, start, count);
            start += count + 1;
            start = (start + 3) / 4 * 4;
            return s;
        }

        public static OSCPacket Unpack(byte[] bytes)
        {
            int start = 0;
            return Unpack(bytes, ref start, bytes.Length);
        }

        public static OSCPacket Unpack(byte[] bytes, ref int start, int end)
        {
            if (bytes[start] == '#')
            {
                return OSCBundle.Unpack(bytes, ref start, end);
            }
            else
            {
                return OSCMessage.Unpack(bytes, ref start);
            }
        }

        protected string address;

        public string Address
        {
            get => address;
            set => address = value; // TODO: validate
        }

        protected ArrayList values;

        public ArrayList Values => (ArrayList)values.Clone();

        abstract public void Append(object value);

        abstract public bool IsBundle();
    }
}