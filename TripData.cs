/************************************************************************/
/*Program:   Splitting The Bill
* Dissemination of this information or reproduction of this material is strictly
* forbidden unless prior written permission is obtained from Luis Vieira
*
* This software is distributed WITHOUT ANY WARRANTY; without even
* the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
*/
/************************************************************************/
/*
* @class   TripData (Import the data file of trips)
* @author  Luis Vieira
* @date	   2018-01-09, 2018
* Information: Name and License type and version 
*(url:joaolsvieira@gmail.com)
*
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplittingTheBill
{
    public class TripData
    {
       

        /// <summary>
        /// Import expenses file.
        /// </summary>
        /// <param name="fileName"></param>
        public static StreamReader ImportFile(string fileName)
        {

            StreamReader strFile=null;
            try {

                // Read the file as one string.
                strFile = File.OpenText(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\" + fileName);
                // Display the file contents to the console. Variable line is a string.
           
            }
            catch (FileNotFoundException exf)
            {

                Console.WriteLine("Import Data: " + exf.Message);
                throw;
            }
            catch (IOException exi)
            {

                Console.WriteLine("Import Data: " + exi.Message);
                throw;
            }
            
            catch (Exception ex)
            {
                Console.WriteLine("Import Data: " + ex.Message);
                throw;
            }

            return strFile;
        }

        
        
    }
}
