using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;

namespace UStream
{
    public unsafe class UnmanagedStream:IDisposable
    {
        Stream _stream = null;
        int _length;
        byte* _startPointer;
        byte* _currentPointer;
        public byte* StartPointer => _startPointer;
        public byte* CurrentPointer => _currentPointer;
        public long Length => _stream == null ? _length : _stream.Length;
        public int Pos
        {
            get => _stream == null ? (int)(_currentPointer - _startPointer) : (int)_stream.Position; 
            set => this.Seek(SeekOrigin.Begin, value); 
        }

        public UnmanagedStream(IntPtr ptr, int length, int offset = 0) => this.InternalParsePoint((byte*)ptr, length, offset);
        public UnmanagedStream(UIntPtr ptr, int length, int offset = 0) => this.InternalParsePoint((byte*)ptr, length, offset);
        public UnmanagedStream(byte[] buffer, int offset = 0) => this.InternalParseArray(buffer, offset);
        public UnmanagedStream(byte* pointer, int length, int offset = 0) => this.InternalParsePoint(pointer, length, offset);
        public UnmanagedStream(Stream stream, int offset = 0) => this.InternalParseStream(stream, offset);
        
        internal void InternalParseArray(byte[] buffer,int offset)
        {
            _length = buffer.Length;
            fixed (byte* ptr = buffer)
            {
                _startPointer = ptr;
                _currentPointer = ptr;
            }
        }
        internal void InternalParsePoint(byte* pointer, int length, int offset)
        {
            _length = length;
            _startPointer = pointer + offset;
            _currentPointer = pointer + offset;
        }
        internal void InternalParseStream(Stream stream, int offset)
        {
            _stream = stream;
            this.Seek(SeekOrigin.Current, offset);
        }

        public U ReadValue<U>() where U : unmanaged
        {
            U val;
            var size = sizeof(U);
            if (_stream != null)
            {
                byte[] buf = new byte[size];
                if (_stream.Read(buf, 0, size) != size) throw new IndexOutOfRangeException("EOF");
                fixed (byte* a = buf) val = *(U*)a;
            }
            else
            {
                if (Pos + size > _length) throw new IndexOutOfRangeException("EOF");
                val = *(U*)_currentPointer;
                _currentPointer += size;
            }
            return val;
        }
        public U[] ReadValues<U>(int count) where U : unmanaged
        {
            U[] vals = new U[count];
            for (int i = 0; i < count; i++)
                vals[i] = ReadValue<U>();
            return vals;
        }
        public void WriteValue<U>(U value) where U : unmanaged
        {
            var size = sizeof(U);
            if (_stream != null)
            {
                byte[] buf = new byte[size];
                fixed (byte* a = buf) *(U*)a = value;
                _stream.Write(buf, 0, size);
            }
            else
            {
                if(Pos+size>_length) throw new IndexOutOfRangeException("EOF");
                *(U*)_currentPointer = value;
                _currentPointer += size;
            }
        }
        public void WriteValues<U>(U[] values) where U : unmanaged
        {
            for (int i = 0; i < values.Length; i++)
                WriteValue(values[i]);
        }
        public void Seek(SeekOrigin where, int offset)
        {
            if (_stream != null)
                _stream.Seek(offset, where);
            else
                switch (where)
                {
                    case SeekOrigin.Begin: _currentPointer = _startPointer + offset; break;
                    case SeekOrigin.Current: _currentPointer += offset; break;
                    case SeekOrigin.End: _currentPointer = _startPointer + _length - 1 + offset; break;
                    default: throw new ArgumentException("SeekOrigin unk type");
                }
        }
        public int CopyTo<U>(UnmanagedStream stream)
        {
            this.Seek(SeekOrigin.Begin, 0);
            stream.WriteValues(this.ReadValues<byte>(_length));
            return _length;
        }
        public int CopyTo(Stream stream)
        {
            this.Seek(SeekOrigin.Begin, 0);
            stream.Write(this.ReadValues<byte>(_length),0,_length);
            return _length;
        }
        public void Dispose()
        {
            if (_stream != null)
            {
                _stream.Close();
                _stream.Dispose();
                _stream = null;
            }
            _startPointer = null;
            _currentPointer = null;
            _length = 0;
            GC.SuppressFinalize(this);
        }
    }
}
