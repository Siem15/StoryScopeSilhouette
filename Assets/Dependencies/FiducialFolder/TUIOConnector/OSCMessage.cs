using System;
using System.Collections;

namespace OSC.NET
{
    /// <summary>
    /// OSCMessage
    /// </summary>
    public class OSCMessage : OSCPacket
    {
        protected const char INTEGER = 'i';
        protected const char FLOAT = 'f';
        protected const char LONG = 'h';
        protected const char DOUBLE = 'd';
        protected const char STRING = 's';
        protected const char SYMBOL = 'S';
        //protected const char BLOB	  = 'b';
        //protected const char ALL     = '*';

        public OSCMessage(string address)
        {
            typeTag = ",";
            Address = address;
        }

        public OSCMessage(string address, object value)
        {
            typeTag = ",";
            Address = address;
            Append(value);
        }

        override protected void Pack()
        {
            ArrayList data = new ArrayList();

            AddBytes(data, PackString(address));
            PadNull(data);
            AddBytes(data, PackString(typeTag));
            PadNull(data);

            foreach (object v in Values)
            {
                switch (v)
                {
                    case int value:
                        AddBytes(data, packInt(value));
                        break;
                    case long value:
                        AddBytes(data, PackLong(value));
                        break;
                    case float value:
                        AddBytes(data, PackFloat(value));
                        break;
                    case double value:
                        AddBytes(data, PackDouble(value));
                        break;
                    case string value:
                        AddBytes(data, PackString(value));
                        PadNull(data);
                        break;
                    default:
                        // TODO
                        break;
                }
            }

            binaryData = (byte[])data.ToArray(typeof(byte));
        }

        public static OSCMessage Unpack(byte[] bytes, ref int start)
        {
            string address = UnpackString(bytes, ref start);
            //Console.WriteLine("address: " + address);
            OSCMessage msg = new OSCMessage(address);

            char[] tags = UnpackString(bytes, ref start).ToCharArray();
            //Console.WriteLine("tags: " + new string(tags));
            foreach (char tag in tags)
            {
                //Console.WriteLine("tag: " + tag + " @ "+start);
                switch (tag)
                {
                    case ',':
                        continue;
                    case INTEGER:
                        msg.Append(UnpackInt(bytes, ref start));
                        break;
                    case LONG:
                        msg.Append(UnpackLong(bytes, ref start));
                        break;
                    case DOUBLE:
                        msg.Append(unpackDouble(bytes, ref start));
                        break;
                    case FLOAT:
                        msg.Append(UnpackFloat(bytes, ref start));
                        break;
                    case STRING:
                    case SYMBOL:
                        msg.Append(UnpackString(bytes, ref start));
                        break;
                    default:
                        Console.WriteLine($"unknown tag: {tag}");
                        break;
                }
            }

            return msg;
        }

        override public void Append(object v)
        {
            switch (v)
            {
                case int value:
                    AppendTag(INTEGER);
                    break;
                case long value:
                    AppendTag(LONG);
                    break;
                case float value:
                    AppendTag(FLOAT);
                    break;
                case double value:
                    AppendTag(DOUBLE);
                    break;
                case string value:
                    AppendTag(STRING);
                    break;
                default:
                    // TODO: exception
                    break;
            }

            values.Add(v);
        }

        protected string typeTag;

        protected void AppendTag(char type) => typeTag += type;

        override public bool IsBundle() => false;
    }
}