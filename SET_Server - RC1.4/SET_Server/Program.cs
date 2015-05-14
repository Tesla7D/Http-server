﻿/*
 *
 *	FILE		:	Program.cs
 *	PROJECT		:	PROG2120 - Assignment #6
 *	PROGRAMMERS	:	Denys Solomonov     Ali Rohaili     Grigory Kozyrev
 *	STUDENT #s	:	6849806             6300321         6850549
 *	FIRST VER.	:	14 November 2014
 *	DESCRIPTION	:	SET Server Pages - Autogenerated program entry file
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SET_Server
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}