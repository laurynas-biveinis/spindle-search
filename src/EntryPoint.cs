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
using System.IO;
using System.Windows.Forms;
using System.Web;
using Microsoft.Win32;
using Org.ManasTungare.Google.Desktop;

namespace Org.ManasTungare.SpindleSearch
{
    /// <summary>
    /// Application Entry Point; examine command-line arguments and act accordingly.
    /// </summary>
    class EntryPoint
    {
        /// <summary>
        /// Go figure ...
        /// </summary>
        /// <param name="args">...</param>
        [STAThread]
        public static void Main(string[] args)
        {
            Application.SafeTopLevelCaptionFormat = Application.ProductName;
            Application.EnableVisualStyles();

            if (args.Length > 0)
            {
                Plugin spindlePlugin = new Plugin(Application.ProductName,
                  "Search within CDs/DVDs without having to insert them in a drive.",
                  "{4A0C1E72-9FD8-42ca-8D95-E56C3D9ADF92}", "no icon");

                switch (args[0])
                {
                    case "-register":
                        try
                        {
                            spindlePlugin.Register();
                        }
                        catch (GoogleDesktopException e)
                        {
                            Console.WriteLine(Application.ProductName + " could not be registered with Google Desktop Search.");
                            Console.WriteLine(e.Message);
                        }

                        // Create registry keys to add a protocol handler for the "spindle://" protocol.
                        // This is essential to provide a seamless experience when looking at retrieved files.
                        RegistryKey spindleProtocolKey = Registry.ClassesRoot.CreateSubKey("spindle");
                        spindleProtocolKey.SetValue("", "URL:Spindle Item");
                        spindleProtocolKey.SetValue("URL Protocol", "");
                        RegistryKey commandKey = spindleProtocolKey.CreateSubKey("shell").CreateSubKey("open").CreateSubKey("command");
                        commandKey.SetValue("", "\"" + Application.ExecutablePath + "\" \"%1\"");
                        break;

                    case "-unregister":
                        try
                        {
                            Registry.ClassesRoot.DeleteSubKeyTree("spindle");
                        }
                        catch (Exception)
                        {
                            // Unable to delete subkey because subkey does not exist.
                        }
                        try
                        {
                            spindlePlugin.Unregister();
                        }
                        catch (GoogleDesktopException e)
                        {
                            Console.WriteLine(Application.ProductName + " could not be unregistered from Google Desktop Search.");
                            Console.WriteLine(e.Message);
                        }
                        return;

                    case "-help": // Listen to all cries for help, add here if I've missed any!
                    case "--help":
                    case "/help":
                    case "/h":
                    case "/?":
                        string usage = Application.ProductName +
                          "\nCopyright 2005, Manas Tungare. http://www.manastungare.com/" +
                          "\n" +
                          "\nUsage:" +
                          "\n  -register     : register the plugin with Google Desktop Search." +
                          "\n  -unregister : unregister the plugin from Google Desktop Search." +
                          "\n  -help          : display this message and quit." +
                          "\n  <uri>         : display details about the specified spindle file uri.";

                        MessageBox.Show(usage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    default:
                        // Show a dialog which parses the Uri passed to it and tells the user
                        // where to find the file s/he wanted.
                        SpindleFileInfo spindleFileInfo = new SpindleFileInfo(String.Join(" ", args));
                        Application.Run(spindleFileInfo);
                        break;
                }
            }
            else
            {
                MainWindow mainW = new MainWindow();
                Application.Run(mainW);
            }
        }
    }
}
