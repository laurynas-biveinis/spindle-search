using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;

namespace Org.ManasTungare.SpindleSearch {
  /// <summary>
  /// A wrapper for the ZipOutputStream provided by ICSharpCode.SharpZipLib.
  /// #ziplib has been developed by Mike Krueger; the current maintainer of #ziplib is John Reilly. 
  /// For more information, see http://www.icsharpcode.net/OpenSource/SharpZipLib/
  /// </summary>
  public class ZipWriter {
    private FileInfo _location;
    private ZipOutputStream _zipFile;

    /// <summary>
    /// Create a new zip archive
    /// </summary>
    /// <param name="location">Location to create the archive at</param>
    public ZipWriter (FileInfo location) {
      _location = location;
      _zipFile = new ZipOutputStream(File.Create(location.FullName));
      _zipFile.SetLevel(5);
    }

    /// <summary>
    /// Exactly as it says. Add a file to the zip archive.
    /// </summary>
    /// <param name="fileToAdd">File to add</param>
    /// <param name="prependPath">Add a path inside the zip archive, if necessary.</param>
    public void AddFile(FileInfo fileToAdd, string prependPath) {
      // Create a CRC record
      Crc32 crc = new Crc32();

      // Create an Entry
      ZipEntry entry =new ZipEntry(prependPath + "\\" + fileToAdd.Name);
      entry.DateTime = fileToAdd.LastWriteTime;
      

      // Read the Contents
      FileStream ifs = File.OpenRead(fileToAdd.FullName);
      byte[] buffer = new byte[fileToAdd.Length];
      ifs.Read(buffer, 0, buffer.Length);
      entry.Size = buffer.Length;
      ifs.Close();

      // Update CRC
      crc.Reset();
      crc.Update(buffer);
      entry.Crc  = crc.Value;

      // Put everything in the Zip file
      _zipFile.PutNextEntry(entry);
      _zipFile.Write(buffer, 0, buffer.Length);
    }

    /// <summary>
    /// Add bytes from a readable stream into the zip archive.
    /// </summary>
    /// <param name="stream">Stream to read from</param>
    /// <param name="fileName">Filename to be assigned to this stream of bytes.</param>
    public void AddStream(Stream stream, string fileName) {
      // Create a CRC record
      Crc32 crc = new Crc32();

      // Create an Entry
      ZipEntry entry =new ZipEntry(fileName);
      entry.DateTime = System.DateTime.Now;

      // Read the Contents
      byte[] buffer = new byte[stream.Length];
      stream.Read(buffer, 0, buffer.Length);
      entry.Size = buffer.Length;
      stream.Close();

      // Update CRC
      crc.Reset();
      crc.Update(buffer);
      entry.Crc  = crc.Value;

      // Put everything in the Zip file
      _zipFile.PutNextEntry(entry);
      _zipFile.Write(buffer, 0, buffer.Length);
    }

    /// <summary>
    /// Must call close to tie up the Zip archive.
    /// </summary>
    public void Close() {
      _zipFile.Finish();
      _zipFile.Close();
    }
  }
}
