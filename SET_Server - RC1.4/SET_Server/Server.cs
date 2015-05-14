/*
 *
 *	FILE		:	Server.cs
 *	PROJECT		:	PROG2120 - Assignment #6
 *	PROGRAMMERS	:	Denys Solomonov     Ali Rohaili     Grigory Kozyrev
 *	STUDENT #s	:	6849806             6300321         6850549
 *	FIRST VER.	:	14 November 2014
 *	DESCRIPTION	:	SET Server Pages - Main server
 * 	
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections;

namespace SET_Server
{
    class Server
    {
        private bool running;
        private Socket server;
        //Default server file path
        public string Path { get; set; }
        //Default server file
        public string DefaultFile { get; set; }
        //Server communication timeout
        public int Timeout { get; set; }

        //Dictionary with all supported extensions and http method assigned to them
        public static Dictionary<string, string> extensions = new Dictionary<string, string>()
        { 
            { ".htm", "text/html" },
            { ".html", "text/html" },
            { ".htmls", "text/html"},
            { ".ssp", "text/html"},
            { ".htx", "text/html"},
            { ".txt", "text/plain" },
            { ".jpg", "image/jpg" },
            { ".jpeg", "image/jpeg" },
            { ".png", "image/png" },
            { ".gif", "image/gif" },
            { ".bm", "image/bmp"},
            { ".bmp", "image/bmp"},
            { ".ico", "image/x-icon"},
            { ".c", "text/plain"},
            { ".cc", "text/plain"},
            { ".conf", "text/plain"},
            { ".zip", "application/zip"},
        };

        //Dictionary with http codes and message assigned to them
        public static Dictionary<int, string> codes = new Dictionary<int, string>()
        {
            {200, "OK"},
            {404, "Not Found"},
            {500, "Internal Server Error"},
            {501, "Not Implemented"},
        };

        /// <summary>
        /// Constructor for Server class
        /// </summary>
        /// <param name="path">Server directory as string</param>
        /// <param name="defaultFile">Default launch file as string</param>
        public Server(string path, string defaultFile)
        {
            Path = path;

            //Make sure that directory ends with \ (For windows navigation)
            if (path[path.Length - 1] != '\\')
            {
                path += '\\';
            }

            //Make sure that filename starts with \ (for windows navigation)
            if (defaultFile[0] != '\\')
            {
                defaultFile = '\\' + defaultFile;
            }

            DefaultFile = defaultFile;

            running = false;
        }

        /// <summary>
        /// Method Start
        /// Starts a server. Creates a server socket, and starts Client Accept thread
        /// </summary>
        /// <param name="address">server address as IPAddress</param>
        /// <param name="port">server port as int</param>
        /// <param name="maxConnections">number of maximum connections as int</param>
        /// <returns>start result as bool</returns>
        public bool Start(IPAddress address, int port, int maxConnections)
        {
            bool status = false;

            //Make sure that server is not running yet
            if (running == false)
            {
                try
                {
                    //Create server socket
                    server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    //Bind socket to IP address on specific port
                    server.Bind(new IPEndPoint(address, port));
                    //Start listening with maximum number of clients specified
                    server.Listen(maxConnections);
                    //Set connection timeout
                    server.ReceiveTimeout = Timeout;
                    server.SendTimeout = Timeout;

                    //Create thread for client acceptance
                    Thread t = new Thread(new ThreadStart(this.AcceptClients));
                    //Launch thread
                    t.Start();

                    //Change running status and return value
                    running = true;
                    status = true;
                }
                catch (Exception)
                { }
            }

            return status;
        }

        /// <summary>
        /// Method AccepClients
        /// Accepts client connections and starts listener thread for each one of them
        /// </summary>
        private void AcceptClients()
        {
            //Run while server is running
            while (running == true)
            {
                //Create client socket
                Socket clientSocket;
                try
                {
                    //Get client
                    clientSocket = server.Accept();
                    //Set connection timeout
                    clientSocket.ReceiveTimeout = Timeout;
                    clientSocket.SendTimeout = Timeout;

                    Thread t = new Thread(new ThreadStart(new Listener(clientSocket, Path, DefaultFile).Run));
                    t.Start();
                }
                catch (Exception)
                { }
            }
        }

        /// <summary>
        /// Method AccepClients
        /// Stops a server.
        /// </summary>
        /// /// <returns>stop result as bool</returns>
        public bool Stop()
        {
            bool status = false;

            if (running == true)
            {
                try
                {
                    //Close a server
                    server.Close();
                    //Reset a socket
                    server = null;
                    //Set flags
                    running = false;
                    status = true;
                }
                catch (Exception)
                { }
            }

            return status;
        }
    }
}
