/*
 *
 *	FILE		:	Processor.cs
 *	PROJECT		:	PROG2120 - Assignment #6
 *	PROGRAMMERS	:	Denys Solomonov     Ali Rohaili     Grigory Kozyrev
 *	STUDENT #s	:	6849806             6300321         6850549
 *	FIRST VER.	:	14 November 2014
 *	DESCRIPTION	:	SET Server Pages - Static script parser and processor
 * 	
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace SET_Server
{
    /// <summary>
    /// Class   :   SETServerPages
    ///     attributes  :   public, static
    ///     2 methods   :
    ///         SSPtoHTML   :   Takes .ssp file's content, runs script contained, and generates a simple webpage with results
    ///         
    ///         SETScript   :   This method attempts to parse the string passed and perform operations, 
    ///                         returning a <div> string</div> in case of well-formed command, or error string
    ///                         otherwise
    /// </summary>
    public static class SETServerPages
    {
        /// <summary>
        /// SSPtoHTML processes the SSP script to HTML. 
        /// This method can be modified to input and/or output strings instead is desired
        /// </summary>
        /// <param name="script">string as contents of the ssp script file.</param>
        /// <returns>A HTML-formed string object to be able to output the result</returns>
        public static string SSPtoHTML(string input)
        {
            string content = "";
            string script = "";

            int posStart = 2;
            int posStop = 0;

            while ((posStart - 2 > -1) && (posStop > -1))
            {
                posStart = input.IndexOf("<%") + 2;
                posStop = input.IndexOf("%>");

                if (posStart - 2 > -1)
                {
                    content += input.Substring(0, posStart - 2);
                }
                else
                {
                    content += input;
                }

                if ((posStart - 2 > -1) && (posStop > -1))
                {
                    script = input.Substring(posStart, (posStop - posStart));

                    input = input.Substring(posStop + 2, input.Length - posStop - 2);

                    string[] strings = script.Split('\n');

                    foreach (string str in strings)
                    {
                        if (str != "" && str != "\n" && str != "\r\n" && str != "\r")
                        {
                            string line = "";
                            //Shorten the line by 1 if '\r' is encountered
                            if (str.EndsWith("\r"))
                            {
                                line = str.Substring(0, str.Length - 1);
                            }
                            else
                            {
                                line = str;
                            }
                            //Look for terminator
                            int endIndex = line.IndexOf(";");
                            //No terminator encountered in line - print error message
                            if (endIndex == -1)
                            {
                                content = content + "Error: expression missing terminator \";\"";
                            }
                            //Normal line encountered - one statement per line
                            else if (endIndex == line.Length - 1)
                            {
                                content = content + SETScript(line);
                            }
                            //Encountered terminator not in the last place   
                            else
                            {
                                //In this case, split the line along the terminators
                                string[] multLines = line.Split(';');
                                foreach (string nline in multLines)
                                {
                                    if (nline != "")
                                    {
                                        //Add terminators back into the lines
                                        string temp = nline + ";";
                                        //Process them normally
                                        content = content + SETScript(temp);
                                    }
                                }
                            }
                        }
                    }
                }

            }

            return content;
        }

        /// <summary>
        /// This function attempts to recursively parse a string and perform operations
        /// </summary>
        /// <param name="str">A string of </param>
        /// <returns></returns>
        public static string SETScript(string str)
        {
            string retVal = "";
            //remove trailing spaces
            while (str.EndsWith(" "))
            {
                str = str.Substring(0, str.Length - 1);
            }
            //Check if the string is colon-terminated
            if (!str.EndsWith(";"))
            {
                retVal = "Error: expression missing \";\" at the end";
            }
            else
            {
                //remove leading spaces if any
                while (str.StartsWith(" "))
                {
                    str = str.Substring(1);
                }
                //Forming response as div - line must start with pr for echo-like command
                if (str.StartsWith("pr "))
                {
                    string tempStr = str.Substring(3);
                    retVal = SETScript(tempStr);
                }
                //Check for date request
                else if (str.StartsWith("date()"))
                {
                    //Get datetime and output
                    DateTime myTime = DateTime.Now;
                    retVal = myTime.Date.ToString("MM/dd/yyyy");
                    //Look for other strings
                    int posAmp = str.IndexOf('&');
                    if (posAmp != -1)
                    {
                        //& found - add 1 to account for a space character
                        posAmp += 1;
                        string otherStr = str.Substring(posAmp);
                        retVal = retVal + SETScript(otherStr);
                    }
                }
                //check for time request
                else if (str.StartsWith("time()"))
                {
                    DateTime myTime = DateTime.Now;
                    retVal = myTime.ToString("T");
                    //Look for other strings
                    int posAmp = str.IndexOf('&');
                    if (posAmp != -1)
                    {
                        //& found - add 1 to account for a space character
                        posAmp += 1;
                        string otherStr = str.Substring(posAmp, str.Length - posAmp);
                        retVal = retVal + SETScript(otherStr);
                    }
                }
                //Search for " 
                //If found, search for next one
                else if (str.StartsWith("\""))
                {
                    //If found, search for next one
                    int endStr = str.IndexOf("\"", 1);
                    //Check if there is more stuff left
                    if (endStr != -1)
                    {
                        if (str.Substring(endStr - 1, 1) == "=")
                        {
                            endStr = str.IndexOf('\"', endStr + 1);
                            endStr = str.IndexOf('\"', endStr + 1);
                        }
                        retVal = str.Substring(1, endStr - 1);
                        //Look for other strings
                        int posAmp = str.IndexOf('&');
                        if (posAmp != -1)
                        {
                            //& found - add 1 to account for a space character
                            posAmp += 1;
                            string otherStr = str.Substring(posAmp);
                            retVal = retVal + SETScript(otherStr);
                        }
                    }
                    //ending " not found - print error
                    else
                    {
                        retVal = "Error: Incorrectly terminated string encountered!";
                    }
                }
                    //None of the explicit directives found
                    //Looking for implicit directives
                else
                {
                    int result = 0;
                    //Look for spaces
                    int strSpace = str.IndexOf(" ");
                    //If no spaces found, look for terminator ;
                    if (strSpace == -1)
                    {
                        strSpace = str.IndexOf(";");
                    }
                    //These 3 are vey similar paths
                    //Check for operator - Get values
                    //process accordingly
                    if (str.Contains('+'))
                    {
                        string addStr = str.Substring(0, strSpace);
                        var myInts = addStr.Split('+');
                        int a = Convert.ToInt32(myInts[0]);
                        int b = Convert.ToInt32(myInts[1]);
                        result = a + b;
                    }
                    else if (str.Contains('*'))
                    {
                        string addStr = str.Substring(0, strSpace);
                        var myInts = addStr.Split('*');
                        int a = Convert.ToInt32(myInts[0]);
                        int b = Convert.ToInt32(myInts[1]);
                        result = a * b;
                    }
                    else if (str.Contains('-'))
                    {
                        string addStr = str.Substring(0, strSpace);
                        var myInts = addStr.Split('-');
                        int a = Convert.ToInt32(myInts[0]);
                        int b = Convert.ToInt32(myInts[1]);
                        result = a - b;
                    }
                    else
                    {
                        retVal = "Error: Invalid script element encountered";
                    }
                    //Look for more processable code in the string 
                    //And recursively send it to itself.
                    retVal = result.ToString();
                    int posAmp = str.IndexOf('&');
                    if (posAmp != -1)
                    {
                        posAmp += 1;
                        string tempStr = str.Substring(posAmp);
                        retVal = retVal + SETScript(tempStr);
                    }
                }
            }
            return retVal;
        }
    }
}
