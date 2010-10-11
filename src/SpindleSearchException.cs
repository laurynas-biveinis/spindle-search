//-------------------------------------------------------------------\\
// CD / DVD Spindle Search Plugin for Google Desktop Search          \\
// Copyright (c) 2010 spindle-search developers.                     \\
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
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Org.ManasTungare.Google.Desktop;

namespace Org.ManasTungare.SpindleSearch
{
    /// <summary>
    /// An application-specific exception class for Spindle Search.
    /// </summary>
    [Serializable]
    class SpindleSearchException : Exception
    {
        /// <summary>
        /// Initializes a new instance of SpindleSearchException.
        /// </summary>
        public SpindleSearchException() 
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of SpindleSearchException with a specified error message.
        /// </summary>
        /// <param name="message">error message</param>
        public SpindleSearchException(String message) 
            : base(message)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of SpindleSearchException with a specified error message and an inner exception that is the
        /// cause of this exception.
        /// </summary>
        /// <param name="message">error message</param>
        /// <param name="innerException">inner exception</param>
        public SpindleSearchException(String message, Exception innerException) 
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of SpindleSearchException from serialized data
        /// </summary>
        /// <param name="info">serialized data</param>
        /// <param name="context">serialization context</param>
        protected SpindleSearchException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
