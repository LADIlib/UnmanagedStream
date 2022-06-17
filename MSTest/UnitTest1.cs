using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using UStream;

namespace MSTest
{
    [TestClass]
    public unsafe class UnitTest1
    {
        Random r = new Random();
        [TestMethod]
        public void _1__UMS__256bytes()
        {
            byte[] data = new byte[0xFF];
            UnmanagedMemoryStream str;
            fixed (byte* a = data)
                str = new UnmanagedMemoryStream(a, data.Length, data.Length, FileAccess.Write);
            for (int i = 0; i < data.Length; i++)
            {
                str.WriteByte((byte)r.Next(0xff));
            }
        }
        [TestMethod]
        public void _2__UMS__256bytesRandom()
        {
            byte[] data = new byte[0xFF];
            UnmanagedMemoryStream str;
            fixed (byte* a = data)
                str = new UnmanagedMemoryStream(a, data.Length, data.Length, FileAccess.Write);
            for (int i = 0; i < data.Length; i++)
            {
                str.WriteByte((byte)r.Next(0xff));
            }
        }
        [TestMethod]
        public void _3__US__256bytes()
        {
            byte[] data = new byte[0xFF];
            var str = new UnmanagedStream(data);
            for (int i = 0; i < data.Length; i++)
            {
                str.WriteValue((byte)0xff);
            }
        }
        [TestMethod]
        public void _4__US__256bytesRandom()
        {
            byte[] data = new byte[0xFF];
            var str = new UnmanagedStream(data);
            for (int i = 0; i < data.Length; i++)
            {
                str.WriteValue((byte)r.Next(0xff));
            }
        }
        [TestMethod]
        public void _5__UMS__256megabytes()
        {
            byte[] data = new byte[0xFF * 0x100000];
            UnmanagedMemoryStream str;
            fixed (byte* a = data)
                str = new UnmanagedMemoryStream(a, data.Length, data.Length, FileAccess.Write);
            for (int i = 0; i < data.Length; i++)
            {
                str.WriteByte((byte)r.Next(0xff));
            }
        }
        [TestMethod]
        public void _6__UMS__256megabytesRandom()
        {
            byte[] data = new byte[0xFF * 0x100000];
            UnmanagedMemoryStream str;
            fixed (byte* a = data)
                str = new UnmanagedMemoryStream(a, data.Length, data.Length, FileAccess.Write);
            for (int i = 0; i < data.Length; i++)
            {
                str.WriteByte((byte)r.Next(0xff));
            }
        }
        [TestMethod]
        public void _7__US__256megabytes()
        {
            byte[] data = new byte[0xFF * 0x100000];
            var str = new UnmanagedStream(data);
            for (int i = 0; i < data.Length; i++)
            {
                str.WriteValue((byte)0xff);
            }
        }
        [TestMethod]
        public void _8__US__256megabytesRandom()
        {
            byte[] data = new byte[0xFF * 0x100000];
            var str = new UnmanagedStream(data);
            for (int i = 0; i < data.Length; i++)
            {
                str.WriteValue((byte)r.Next(0xff));
            }
        }
        [TestMethod]
        public void _9__UMS__1gigabyte()
        {
            byte[] data = new byte[0x40000000];
            UnmanagedMemoryStream str;
            fixed (byte* a = data)
                str = new UnmanagedMemoryStream(a, data.Length, data.Length, FileAccess.Write);
            for (int i = 0; i < data.Length; i++)
            {
                str.WriteByte((byte)r.Next(0xff));
            }
        }
        [TestMethod]

        public void _A__UMS__1gigabyteRandom()
        {
            byte[] data = new byte[0x40000000];
            UnmanagedMemoryStream str;
            fixed (byte* a = data)
                str = new UnmanagedMemoryStream(a, data.Length, data.Length, FileAccess.Write);
            for (int i = 0; i < data.Length; i++)
            {
                str.WriteByte((byte)r.Next(0xff));
            }
        }
        [TestMethod]
        public void _B__US__1gigabyte()
        {
            byte[] data = new byte[0x40000000];
            var str = new UnmanagedStream(data);
            for (int i = 0; i < data.Length; i++)
            {
                str.WriteValue((byte)0xff);
            }
        }
        [TestMethod]
        public void _C__US__1gigabyteRandom()
        {
            byte[] data = new byte[0x40000000];
            var str = new UnmanagedStream(data);
            for (int i = 0; i < data.Length; i++)
            {
                str.WriteValue((byte)r.Next(0xff));
            }
        }

    }
}