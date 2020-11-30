using System;
using System.Collections.Generic;
using ICSharpCode.SharpZipLib.Zip;
using Ssepan.Utility;
using System.Diagnostics;
using System.Reflection;

namespace Ssepan.Compression
{
    /// <summary>
    /// Compress and decompress files with Zip.
    /// </summary>
    public static class Zip
    {
        /// <summary>
        /// Zip file(s).
        /// </summary>
        /// <param name="zipFileName"></param>
        /// <param name="sourceDirectory"></param>
        /// <param name="recurse"></param>
        /// <param name="fileFilter"></param>
        /// <param name="directoryFilter"></param>
        public static Boolean Compress
        (
            String zipFileName, 
            String sourceDirectory, 
            Boolean recurse, 
            String fileFilter, 
            String directoryFilter,
            ref String errorMessage
        )
        {
            Boolean returnValue = default(Boolean);
            FastZip fastZip;
            try
            {
		        fastZip = new FastZip();
                fastZip.CreateZip(zipFileName, sourceDirectory, recurse, fileFilter, directoryFilter);
                fastZip = null;

                returnValue = true;
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                        
                //throw;
                errorMessage = ex.Message;
            }
            return returnValue;
        }

        /// <summary>
        /// UnZip file(s).
        /// </summary>
        /// <param name="zipFileName"></param>
        /// <param name="targetDirectory"></param>
        /// <param name="fileFilter"></param>
        public static Boolean Decompress
        (
            String zipFileName, 
            String targetDirectory, 
            String fileFilter,
            ref String errorMessage
        )
        {
            Boolean returnValue = default(Boolean);
            FastZip fastZip;
            try
            {
		        fastZip = new FastZip();
		        fastZip.ExtractZip(zipFileName, targetDirectory, fileFilter);
                fastZip = null;

                returnValue = true;
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                        
                //throw;
                errorMessage = ex.Message;
            }
            return returnValue;
        }
    }
}
