/*
 *
 *	FILE		:	Listener.cs
 *	PROJECT		:	PROG2120 - Assignment #6
 *	PROGRAMMERS	:	Denys Solomonov     Ali Rohaili     Grigory Kozyrev
 *	STUDENT #s	:	6849806             6300321         6850549
 *	FIRST VER.	:	14 November 2014
 *	DESCRIPTION	:	SET Server Pages - Static script parser and processor
 * 	
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SET_Server
{
    /// <summary>
    /// Class Listener
    /// </summary>
    class Listener
    {
        private Socket handler;
        private const int kBufferSize = 1024;
        string directory;
        string defaultFile;

        /// <summary>
        /// Constructor for Listener class
        /// </summary>
        /// <param name="socket">Client socket as Socket</param>
        /// <param name="directory">Server directory as string</param>
        /// <param name="defaultFile">Default launch file as string</param>
        public Listener(Socket socket, string directory, string defaultFile)
        {
            handler = socket;
            this.directory = directory;
            this.defaultFile = defaultFile;
        }

        /// <summary>
        /// Thread launcher for ProcessRequest
        /// </summary>
        public void Run()
        {
            try
            {
                ProcessRequest(handler);
            }
            catch (Exception)
            {}
        }

        /// <summary>
        /// Method ProcessRequest
        /// Receives request and processes it, providing client with a data,
        /// or informing about error
        /// </summary>
        /// <param name="handler">handler as Socket</param>
        /// <returns>process result ad bool</returns>
        private bool ProcessRequest(Socket handler)
        {
            bool status = false;

            byte[] buffer = new byte[kBufferSize];
            int receivedBytes = handler.Receive(buffer);
            string message = Encoding.UTF8.GetString(buffer, 0, receivedBytes);

            string HTTP_Method = "";        //Used method
            string url = "";                //Requested url
            string file = "";               //Requested file taken from the url
            string extension = "";          //Requested file extension

            //Get HTTP method
            HTTP_Method = message.Substring(0, message.IndexOf(" "));   

            //Get url
            {
                int filenameStart = HTTP_Method.Length + 1;
                int filenameLength = message.IndexOf("HTTP") - HTTP_Method.Length - 2;
                url = message.Substring(filenameStart, filenameLength);
            }        

            //Get filename
            if (HTTP_Method.Equals("GET"))
            {
                file = url.Split('?')[0];
                file.Replace(@"/", @"\\");
            }
            
            //Get extension
            extension = GetExtension(file);

            //Check if default page is requested
            if (file.Equals("/"))
            {
                //Check if file exists
                if (File.Exists(directory + defaultFile))
                {
                    extension = GetExtension(defaultFile);

                    //Make sure that extension is supported
                    if (Server.extensions.ContainsKey(GetExtension(defaultFile)))
                    {
                        //Process if file is .ssp
                        if (extension.Equals(".ssp") == true)
                        {
                            string HTML = SETServerPages.SSPtoHTML(File.ReadAllText(directory + defaultFile));
                            SendRespond(Encoding.UTF8.GetBytes(HTML), Server.extensions[extension], 200);
                        }
                        else    //Or just send the file content
                        {
                            SendRespond(File.ReadAllBytes(directory + defaultFile), Server.extensions[extension], 200);
                        }
                        status = true;
                    }
                    else
                    {
                        //Server error: Extension not supported
                        SendRespond(Encoding.UTF8.GetBytes("Extension is not supported"), "text/plain", 501);
                    }
                }
                else
                {
                    //Server errorL File not found
                    SendRespond(Encoding.UTF8.GetBytes("File not found"), "text/plain", 404);
                }
            }
            else    //Specific file requested
            { 
                //Check if extension is supported
                if (Server.extensions.ContainsKey(extension))
                {
                    //Make sure that file exists
                    if (File.Exists(directory + file) == false)
                    {
                        //Server error: File not found
                        SendRespond(Encoding.UTF8.GetBytes("File not found"), "text/plain", 404);
                    }
                    else
                    {
                        //Process file, if extension is .ssp
                        if (extension.Equals(".ssp") == true)
                        {
                            string HTML = SETServerPages.SSPtoHTML(File.ReadAllText(directory + file));
                            SendRespond(Encoding.UTF8.GetBytes(HTML), Server.extensions[extension], 200);
                        }
                        else    //Otherwise just send file content
                        {
                            SendRespond(File.ReadAllBytes(directory + file), Server.extensions[extension], 200);
                        }
                        status = true;
                    }
                }
                else
                {
                    //Server error: Extension is not supported
                    SendRespond(Encoding.UTF8.GetBytes("Extension is not supported"), "text/plain", 501);
                }
            }
            return status;
        }

        /// <summary>
        /// Method GetExtension
        /// As name implies, gets an extension of the file
        /// </summary>
        /// <param name="filename">filename as string</param>
        /// <returns>file extension, as string</returns>
        private string GetExtension(string filename)
        {
            string extension = "";
            //Get index of last '.'
            int extensionStart = filename.LastIndexOf('.');

            if (extensionStart > 0)
            {
                int extenstionLength = filename.Length - extensionStart;

                //Make sure that there is ., and something after that
                if (extenstionLength > 0)
                {
                    extension = filename.Substring(extensionStart, extenstionLength);
                }
            }
            return extension;
        }

        /// <summary>
        /// Method SendRespond
        /// forms a response header for http request
        /// </summary>
        /// <param name="Content">bytestream of what is needed to be sent</param>
        /// <param name="contentType">string delineating the content type</param>
        /// <param name="code">HTTP status code</param>
        private void SendRespond(byte[] Content, string contentType, int code)
        {
            try
            {
                //Create HTTP header
                string header = "HTTP/1.1 " + code.ToString() + " " + Server.codes[code] + " " + "\r\n" +
                                    "Server: SET Web Server\r\n" +
                                    "Content-Length: " + Content.Length.ToString() + "\r\n" +
                                    "Connection: close\r\n" +
                                    "Content-Type: " + contentType + "\r\n\r\n";

                //Send header first
                handler.Send(Encoding.UTF8.GetBytes(header));
                //Send actuall content
                handler.Send(Content);
                //Close socket
                handler.Close();
            }
            catch (Exception)
            { }
        }
    }
}
