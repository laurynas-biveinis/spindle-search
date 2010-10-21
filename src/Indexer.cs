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
using System.Globalization;
using System.Xml;
using System.Web;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Org.ManasTungare.Google.Desktop;
using Org.ManasTungare.Google.Desktop.Schemas;

namespace Org.ManasTungare.SpindleSearch
{
    /// <summary>
    /// Submits available catalog data to the Google Desktop Search application
    /// for indexing.
    /// </summary>
    class Indexer
    {
        const string kPlugInDescription = "Search within CDs/DVDs without having to insert them in a drive.";
        const string kComponentGuid = "{4A0C1E72-9FD8-42ca-8D95-E56C3D9ADF92}";
        const string kProtocol = "spindle";

        const string kRegistrationSuccessful = "The plugin was registered successfully with the Google Desktop Search Engine.";
        const string kUnregistrationSuccessful = "The plugin was unregistered from the Google Desktop Search Engine.";

        /// <summary>
        /// MetaInfo about a disk is the data collected at the time the disk was indexed.
        /// This stores it in a human-readable format.
        /// </summary>
        private string _metaInfoContent = "";

        /// <summary>
        /// MetaInfo about a disk is the data collected at the time the disk was indexed.
        /// This stores it in an HTTP query-like format.
        /// </summary>
        private string _metaInfoQuery = "";

        /// <summary>
        /// The background worker task that is running this indexer
        /// </summary>
        private BackgroundWorker Worker;

        // Plugin object is the .Net wrapper.
        IndexingComponent spindleIndexer;

        /// <summary>
        /// Adds each file from this <code>Catalog</code> to the Google Desktop Search Engine.
        /// </summary>
        /// <param name="BkgWorker">The BackgroundWorker task running this indexer.</param>
        /// <param name="catalog">an XML representation of the disk to be indexed.</param>
        public void IndexCatalog(BackgroundWorker BkgWorker, Catalog catalog)
        {
            Worker = BkgWorker;
            if (spindleIndexer == null)
            {
                // Create an instance of Plugin object ...
                spindleIndexer = new IndexingComponent(Application.ProductName, kPlugInDescription, kComponentGuid, "no icon");
            }

            // Top-level node
            XmlNode rootNode = catalog.XmlCatalog.SelectSingleNode("//disk");
            string rootUri = rootNode.Attributes["uri"].Value;

            // Some first-level children are meta information
            foreach (XmlNode dirNode in rootNode.ChildNodes)
            {
                if (dirNode.Name.ToLower().Equals("meta") && 0 != dirNode.Attributes["value"].Value.Length)
                {
                    _metaInfoQuery += HttpUtility.UrlEncode(dirNode.Attributes["name"].Value) + "=" + HttpUtility.UrlEncode(dirNode.Attributes["value"].Value) + "&";
                    _metaInfoContent += dirNode.Attributes["name"].Value + ": " + dirNode.Attributes["value"].Value + "\n";
                }
            }
            _metaInfoQuery = _metaInfoQuery.Remove(_metaInfoQuery.Length - 1, 1); // Strip "&"
            _metaInfoContent = _metaInfoContent.Remove(_metaInfoContent.Length - 1, 1); // Strip "\n"

            // Some first-level children are directories
            foreach (XmlNode dirNode in rootNode.ChildNodes)
            {
                if (dirNode.Name.ToLower().Equals("dir"))
                {
                    IndexDirectory(dirNode, rootUri + HttpUtility.UrlEncode(dirNode.Value));
                }
            }
        }

        /// <summary>
        /// Indexes a single directory from the entire catalog.
        /// </summary>
        /// <param name="directoryNode">node representing this directory</param>
        /// <param name="parentUri">Uri of the parent, so it can form its own Uri.</param>
        private void IndexDirectory(XmlNode directoryNode, string parentUri)
        {
            foreach (XmlNode node in directoryNode.ChildNodes)
            {
                if (node.Name.ToLower().Equals("file"))
                {
                    IndexFile(node, parentUri);
                }
                else if (node.Name.ToLower().Equals("dir"))
                {
                    IndexDirectory(node, parentUri + "/" + HttpUtility.UrlEncode(node.Attributes["name"].Value));
                }
            }
        }

        /// <summary>
        /// Indexes a single file from the entire catalog
        /// </summary>
        /// <param name="fileNode">node representing this file</param>
        /// <param name="containerDirUri">Uri of the directory containing this, so it can form its own Uri.</param>
        private void IndexFile(XmlNode fileNode, string containerDirUri)
        {
            Google.Desktop.Schemas.File fileSchema = new Google.Desktop.Schemas.File(spindleIndexer);

            // ... add specified properties
            string indexedUri = containerDirUri + "/" + HttpUtility.UrlEncode(fileNode.Attributes["name"].Value);
            fileSchema.URI = kProtocol + "://" + indexedUri + "?" + _metaInfoQuery;
            fileSchema.Content = "On Removable Device: " + _metaInfoContent + "; " + HttpUtility.UrlDecode(indexedUri);
            fileSchema.ContentType = File.ContentTypes.TextPlain;
            fileSchema.NativeSize = Convert.ToInt64(fileNode.Attributes["size"].Value);
            fileSchema.LastModified = System.DateTime.ParseExact(fileNode.Attributes["mod"].Value, Global.ISO_8601_TIME_FORMAT, DateTimeFormatInfo.InvariantInfo);
            fileSchema.Title = fileNode.Attributes["name"].Value;

            // ... and send it on its way.
            fileSchema.Send(Google.Desktop.Schemas.Indexable.EventFlags.Historical);

            Worker.ReportProgress(0, "Cataloged: " + fileSchema.URI);
        }
    }
}
