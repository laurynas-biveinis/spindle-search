//-------------------------------------------------------------------\\
// CD / DVD Spindle Search Plugin for Google Desktop Search          \\
// Copyright (c) 2005, Manas Tungare. http://www.manastungare.com/   \\
// Copyright (c) 2010, spindle-search developers.                    \\
// http://code.google.com/p/spindle-search/                          \\
//-------------------------------------------------------------------\\
// This program is free software; you can redistribute it and/or     \\
// modify it under the terms of the GNU General Public License       \\
// as published by the Free Software Foundation; either version 2    \\
// of the License, or (at your option) any later version.            \\
//                                                                   \\
// This program is distributed in the hope that it will be useful,   \\
// but WITHOUT ANY WARRANTY; without even the implied warranty of    \\
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the     \\
// GNU General Public License for more details.                      \\
//                                                                   \\
// GNU GPL: http://www.gnu.org/licenses/gpl.html                     \\
//-------------------------------------------------------------------\\
using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;

namespace Org.ManasTungare.SpindleSearch
{
    /// <summary>
    /// A wrapper for the ZipOutputStream provided by ICSharpCode.SharpZipLib.
    /// #ziplib has been developed by Mike Krueger; the current maintainer of #ziplib is John Reilly. 
    /// For more information, see http://www.icsharpcode.net/OpenSource/SharpZipLib/
    /// </summary>
    class ZipWriter
    {
        private ZipOutputStream _zipFile;

        /// <summary>
        /// Create a new zip archive
        /// </summary>
        /// <param name="location">Location to create the archive at</param>
        public ZipWriter(FileInfo location)
        {
            _zipFile = new ZipOutputStream(File.Create(location.FullName));
            _zipFile.SetLevel(5);
        }

        /// <summary>
        /// Add bytes from a readable stream into the zip archive.
        /// </summary>
        /// <param name="stream">Stream to read from</param>
        /// <param name="fileName">Filename to be assigned to this stream of bytes.</param>
        public void AddStream(Stream stream, string fileName)
        {
            // Create a CRC record
            Crc32 crc = new Crc32();

            // Create an Entry
            ZipEntry entry = new ZipEntry(fileName);
            entry.DateTime = System.DateTime.Now;

            // Read the Contents
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            entry.Size = buffer.Length;
            stream.Close();

            // Update CRC
            crc.Reset();
            crc.Update(buffer);
            entry.Crc = crc.Value;

            // Put everything in the Zip file
            _zipFile.PutNextEntry(entry);
            _zipFile.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Must call close to tie up the Zip archive.
        /// </summary>
        public void Close()
        {
            _zipFile.Finish();
            _zipFile.Close();
        }
    }
}
