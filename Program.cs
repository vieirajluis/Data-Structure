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
* @class   Program
* @author  Luis Vieira
* @date	   2018-01-09, 2018
* Information: Name and License type and version 
*(url:joaolsvieira@gmail.com)
*
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SplittingTheBill
{
    /// <summary>
    /// Init
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //Start to process bills.
            ProcessBills pbs = new ProcessBills();
            pbs.Transations();

        }
    }
}
