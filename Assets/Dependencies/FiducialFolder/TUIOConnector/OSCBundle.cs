using System.Collections;

namespace OSC.NET
{
    /// <summary>
    /// OSCBundle
    /// </summary>
    public class OSCBundle : OSCPacket
    {
        protected const string BUNDLE = "#bundle";
        private long timestamp = 0;

        public OSCBundle(long ts)
        {
            address = BUNDLE;
            timestamp = ts;
        }

        public OSCBundle()
        {
            address = BUNDLE;
            timestamp = 0;
        }

        override protected void Pack()
        {
            ArrayList data = new ArrayList();

            AddBytes(data, PackString(Address));
            PadNull(data);
            AddBytes(data, PackLong(0)); // TODO

            foreach (object value in Values)
            {
                if (value is OSCPacket)
                {
                    byte[] bs = ((OSCPacket)value).BinaryData;
                    AddBytes(data, packInt(bs.Length));
                    AddBytes(data, bs);
                }
                else
                {
                    // TODO
                }
            }

            binaryData = (byte[])data.ToArray(typeof(byte));
        }

        public static new OSCBundle Unpack(byte[] bytes, ref int start, int end)
        {
            string address = UnpackString(bytes, ref start);
            //Console.WriteLine("bundle: " + address);
            if (!address.Equals(BUNDLE)) return null; // TODO

            long timestamp = UnpackLong(bytes, ref start);
            OSCBundle bundle = new OSCBundle(timestamp);

            while (start < end)
            {
                int length = UnpackInt(bytes, ref start);
                int sub_end = start + length;
                //Console.WriteLine(bytes.Length +" "+ start+" "+length+" "+sub_end);
                bundle.Append(OSCPacket.Unpack(bytes, ref start, sub_end));

            }

            return bundle;
        }

        public long GetTimeStamp() => timestamp;

        override public void Append(object value)
        {
            if (value is OSCPacket)
            {
                values.Add(value);
            }
            else
            {
                // TODO: exception
            }
        }

        override public bool IsBundle() => true;
    }
}