using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UStream
{
    public static class UnmanagedStreamExtentions
    {
        public unsafe static UnmanagedStream GetGenericUStream<U>(U[] buffer, int offset = 0) where U : unmanaged
        {
            fixed(U*s = buffer)
            {
                return new UnmanagedStream((byte*)s, buffer.Length * sizeof(U), offset * sizeof(U));
            }
        }
        public unsafe static UnmanagedStream GetGenericUStream<U>(U* pointer, int length, int offset = 0) where U : unmanaged
        {
            return new UnmanagedStream((byte*)pointer, length * sizeof(U), offset * sizeof(U));
        }

        public unsafe static T ReadUVarInt<T>(this UnmanagedStream stream) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
        {
            ulong result = 0;
            int shift = 0;

            while (true)
            {
                var value = stream.ReadValue<byte>();
                result |= (ulong)(value & 0x7F) << shift;
                shift += 7;
                if ((value & 0x80) == 0) break;
            }
            return (T)Convert.ChangeType(result, typeof(T));
        }
    }
}
