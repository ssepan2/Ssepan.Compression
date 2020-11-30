using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Ssepan.Utility;
using System.Diagnostics;
using System.Reflection;

namespace Ssepan.Compression
{
    /// <summary>
    /// Compress and decompress items.
    /// </summary>
    public static class Deflate
    {
        /// <summary>
        /// Deflate Byte[].
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Byte[] Compress(Byte[] bytes)
        {
            Byte[] returnValue = default(Byte[]);
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress);
                deflateStream.Write(bytes, 0, bytes.Length);
                deflateStream.Flush();
                deflateStream.Close();

                returnValue = memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                        
                throw;
            }
            return returnValue;
        }

        /// <summary>
        /// Inflate Byte[].
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Byte[] Decompress(Byte[] bytes)
        {
            Byte[] returnValue = default(Byte[]);
            try
            {
                const Int32 BUFFER_SIZE = 256;
                Byte[] buffer = new Byte[BUFFER_SIZE];
                List<Byte[]> listOfByteArray = new List<Byte[]>();
                Int32 count = 0;
                Int32 length = 0;

                MemoryStream memoryStream = new MemoryStream(bytes);
                DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress);

                while ((count = deflateStream.Read(buffer, 0, BUFFER_SIZE)) > 0)
                {
                    if (count == BUFFER_SIZE)
                    {
                        listOfByteArray.Add(buffer);
                        buffer = new Byte[BUFFER_SIZE];
                    }
                    else
                    {
                        Byte[] temp = new Byte[count];
                        Array.Copy(buffer, 0, temp, 0, count);
                        listOfByteArray.Add(temp);
                    }
                    length += count;
                }

                returnValue = new Byte[length];

                count = 0;
                foreach (Byte[] bufferListItem in listOfByteArray)
                {
                    Array.Copy(bufferListItem, 0, returnValue, count, bufferListItem.Length);
                    count += bufferListItem.Length;
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                        
                throw;
            }
            return returnValue;
        }
    }
}
