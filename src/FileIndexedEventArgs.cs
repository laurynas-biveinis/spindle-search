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

namespace Org.ManasTungare.SpindleSearch {
  /// <summary>
  /// Event raised when a file is indexed.
  /// </summary>
  public class FileIndexedEventArgs : EventArgs {

    /// <summary>
    /// Stores the filename
    /// </summary>
    private string _file;

    /// <summary>
    /// Create new event, given a filename
    /// </summary>
    /// <param name="file">the file that was indexed</param>
    public FileIndexedEventArgs (string file) : base() {
      _file = file;
    }

    /// <summary>
    /// Gets the name of the file that was cataloged.
    /// </summary>
    public string File {
      get {
        return _file;
      }
    }
  }
}
