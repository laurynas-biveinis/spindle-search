//-------------------------------------------------------------------\\
// CD / DVD Spindle Search Plugin for Google Desktop Search          \\
// Copyright (c) 2005, Manas Tungare. http://www.manastungare.com/   \\
// Copyright (c) 2009, 2010 spindle-search developers.               \\
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
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Web;

namespace Org.ManasTungare.SpindleSearch
{
    /// <summary>
    /// Performs an indexing operation on items stored offline.
    /// </summary>
    class Catalog
    {

        /// <summary>
        /// Root of the XML document that represents the directory tree.
        /// </summary>
        XmlDocument _xmlDoc;

        /// <summary>
        /// The background worker task that is running this indexer
        /// </summary>
        private BackgroundWorker Worker;

        /// <summary>
        /// Creates a new catalog.
        /// </summary>
        /// <param name="BkgWorker">The executing BackgroundWorker task of this catalog</param>
        public Catalog(BackgroundWorker BkgWorker)
        {
            Worker = BkgWorker;
        }

        internal XmlDocument XmlCatalog
        {
            get
            {
                return _xmlDoc;
            }
        }

        /// <summary>
        /// Recursively add the file to catalog.
        /// </summary>
        /// <param name="directory">Directory to be indexed</param>
        public void CreateCatalogFrom(DirectoryInfo directory, string rootUri)
        {
            // TODO: probably must be a constructor or a factory method
            _xmlDoc = new XmlDocument();
            _xmlDoc.LoadXml("<disk></disk>");
            _xmlDoc.SelectSingleNode("//disk").AppendChild(GetNodeFromDirectory(directory));
            ((XmlElement)_xmlDoc.SelectSingleNode("//disk")).SetAttribute("uri", rootUri);
        }

        /// <summary>
        /// Loads the Catalog from a previously-saved XML file
        /// </summary>
        /// <param name="xmlDocument">Previously-saved XML file</param>
        public void LoadCatalogFrom(FileInfo xmlDocument)
        {
            // TODO: probably must be a constructor or a factory method
            _xmlDoc = new XmlDocument();
            _xmlDoc.Load(xmlDocument.FullName);
        }

        /// <summary>
        /// Loads the Catalog from any readable stream.
        /// </summary>
        /// <param name="xmlStream">Any readable stream</param>
        public void LoadCatalogFrom(Stream xmlStream)
        {
            // TODO: probably must be a constructor or a factory method
            _xmlDoc = new XmlDocument();
            _xmlDoc.Load(xmlStream);
        }

        /// <summary>
        /// Add specified items of meta information to the disk being catalogued. 
        /// This can include details about physical storage location, date of the last backup,
        /// etc. This text is not parsed, it is simply included as part of the catalog
        /// and available for indexing/searching.
        /// </summary>
        /// <param name="name">Identifier for type of meta information: 
        /// e.g. "Artist", "Album", "Label", "Medium", "Manufacturer", etc.</param>
        /// <param name="val">Value of the specified parameter.</param>
        public void AddMetaInformation(string name, string val)
        {
            XmlElement metaInfoNode = _xmlDoc.CreateElement("meta");
            metaInfoNode.SetAttribute("name", name);
            metaInfoNode.SetAttribute("value", val);
            _xmlDoc.SelectSingleNode("//disk").AppendChild(metaInfoNode);
        }

        /// <summary>
        /// Exports the internal representation of the catalog to a writeable stream.
        /// </summary>
        /// <param name="writeableStream">A stream that has been opened for writing, it could be a FileStream or a NetworkStream or a MemoryStream.</param>
        public void ExportTo(Stream writeableStream)
        {
            _xmlDoc.Save(writeableStream);
        }

        /// <summary>
        /// Returns an XML node for the given directory; calls itself recursively.
        /// </summary>
        /// <param name="dir">Directory to create a node for.</param>
        /// <returns>An xml representation of the node; ready to be added into a document.</returns>
        private XmlElement GetNodeFromDirectory(DirectoryInfo dir)
        {
            // Create a node for yourself
            XmlElement dirNode = _xmlDoc.CreateElement("dir");
            dirNode.SetAttribute("name", dir.Name);

            // Then add files contained in you
            FileInfo[] files;
            try
            {
                files = dir.GetFiles();
            }
            catch (UnauthorizedAccessException)
            {
                //TODO: Log Error
                files = new FileInfo[0];
            }

            foreach (FileInfo fileInfo in files)
            {
                XmlElement fileNode = _xmlDoc.CreateElement("file");
                fileNode.SetAttribute("name", fileInfo.Name);
                fileNode.SetAttribute("size", fileInfo.Length.ToString());
                fileNode.SetAttribute("mod", fileInfo.LastWriteTimeUtc.ToString(Global.ISO_8601_TIME_FORMAT));

                dirNode.AppendChild(fileNode);

                Worker.ReportProgress(0, "Cataloged: " + fileInfo.FullName);
            }

            // Then recurse for all subdirectories
            DirectoryInfo[] subDirectories;
            try
            {
                subDirectories = dir.GetDirectories();
            }
            catch (UnauthorizedAccessException)
            {
                subDirectories = new DirectoryInfo[0];
            }

            foreach (DirectoryInfo subDirectory in subDirectories)
            {
                dirNode.AppendChild(GetNodeFromDirectory(subDirectory));
            }

            // Return what you just created
            return dirNode;
        }
    }
}