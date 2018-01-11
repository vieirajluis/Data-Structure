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
* @class   ProcessBillsTests (Trip unit test.)
* @author  Luis Vieira
* @date	   2018-01-09, 2018
* Information: Name and License type and version 
*(url:joaolsvieira@gmail.com)
*
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SplittingTheBill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SplittingTheBill.Tests
{
    [TestClass()]
    public class ProcessBillsTests
    {
        /// <summary>
        /// Checks the data stored in arvove n-ary and it results per unit. 
        /// </summary>
        [TestMethod()]
        public void StartBillsTransacationTest()
        {
            Trip trip = new Trip(true);
            Trip group = null;
            //Insert Group/Trip
            group = trip.newNode(3);
            decimal expected = 3;
            decimal actual = group.Data;
            Assert.AreEqual(expected, actual);

            //Insert Participant
            Trip ptc1 = trip.addComponent(group, 2);
            Trip ptc2 = trip.addComponent(group, 4);
            Trip ptc3 = trip.addComponent(group, 3);

            //Insert Bills
            Trip bill1 = trip.addComponent(ptc1, 10.00M);
            Trip bill2 = trip.addComponent(ptc1, 20.00M);

            Trip bill3 = trip.addComponent(ptc2, 15.00M);
            Trip bill4 = trip.addComponent(ptc2, 15.01M);
            Trip bill5 = trip.addComponent(ptc2, 3.00M);
            Trip bill6 = trip.addComponent(ptc2, 3.01M);

            Trip bill7 = trip.addComponent(ptc3, 5.00M);
            Trip bill8 = trip.addComponent(ptc3, 9.00M);
            Trip bill9 = trip.addComponent(ptc3, 4.00M);

            //Trip 1
            decimal expectedTotal = 84.02M;

            Trip.sumBills(group);
            decimal actualTotal = Trip.TotalPaidParticipant.Sum();
            Assert.AreEqual(expectedTotal, actualTotal);

            Trip.TotalPaidParticipant.Clear();
            Trip.ParticipantExpense.Clear();

            Trip group2 = null;
            //Insert Group/Trip
            group2 = trip.newNode(2);
            decimal expected2 = 2;
            decimal actual2 = group2.Data;
            Assert.AreEqual(expected2, actual2);

            //Insert Participant
            Trip ptc12 = trip.addComponent(group2, 2);
            Trip ptc22 = trip.addComponent(group2, 2);


            //Insert Bills
            Trip bill12 = trip.addComponent(ptc12, 8.00M);
            Trip bill22 = trip.addComponent(ptc12, 6.00M);

            Trip bill32 = trip.addComponent(ptc22, 9.20M);
            Trip bill42 = trip.addComponent(ptc22, 6.75M);



            //Trip 2
            decimal expectedTotal2 = 29.95M;

            Trip.sumBills(group2);
            decimal actualTotal2 = Trip.TotalPaidParticipant.Sum();
            Assert.AreEqual(expectedTotal2, actualTotal2);

            Trip.TotalPaidParticipant.Clear();
            Trip.ParticipantExpense.Clear();
        }

       
    }
}