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
* @class   ProcessBill (Execute the transactions after import the data file of trips)
* @author  Luis Vieira
* @date	   2018-01-09, 2018
* Information: Name and License type and version 
*(url:joaolsvieira@gmail.com)
*
*/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplittingTheBill
{

    public class ProcessBills
    {
        private List<Trip> tripList;
        
        /// <summary>
        /// Start splitting bill transactions
        /// </summary>
        public void Transations()
        {

            StreamReader strFile = null;

            try {

                strFile = TripData.ImportFile("expenses.txt");//Input Parameters - Trips

                Trip campping = new Trip(true);

                //Make the Data Structure of the Trip.
                StartBillsTransacation(ref strFile, ref campping);

                foreach (var tr in this.tripList)
                {
                    Trip.printTrip(tr);
                    Console.WriteLine();
                }

                //Splitting the Bills/Charges per Participant.
                SplitBillsTransaction(ref campping);

               
            }
            catch (FileNotFoundException exf)
            {
                Console.WriteLine("Processing Bills - Transactions: " + exf.Message);
               
            }
            catch (IOException exi)
            {
                Console.WriteLine("Processing Bills - Transactions: " + exi.Message);
                throw;
            }
            catch (DivideByZeroException dvo)
            {
                Console.WriteLine("Processing Bills - Transactions: " + dvo.Message);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Processing Bills - Transactions: " + ex.Message);
            }
            finally
            {
                if (strFile != null)
                {
                    strFile.Close();
                }
            }
            
        }

        /// <summary>
        /// Split the Bills/Charges per Participant and output the results.
        /// </summary>
        /// <param name="campping"></param>
        private void SplitBillsTransaction(ref Trip campping)
        {
            StreamWriter swFile=null;
            FileStream fs = null;
            string fileName = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\" + "expenses.txt.out";
            try
            {
                Trip.printTrip(campping);
                foreach (var tr in this.tripList)
                {
                    Trip.sumBills(tr);
                    Console.WriteLine("TotalPaidTrip: {0}", Trip.TotalPaidParticipant.Sum());

                    int qtdParticipants = (Int32)(tr.Data);
                    decimal totalIndividualTrip = Trip.TotalPaidParticipant.Sum()/qtdParticipants;
                    
                    string printCollect = "should collect";
                    string printOwes = "owes money";
                    using (fs = new FileStream(fileName, FileMode.Append, FileAccess.Write))
                    using (swFile = new StreamWriter(fs))
                    {
                        
                        swFile.WriteLine("Splitting Trip Group of {0}: ", qtdParticipants);
                        swFile.WriteLine("-------------------------------------");
                        foreach (var vl in Trip.ParticipantExpense)
                        {
                            decimal value = totalIndividualTrip - vl.Value;
                            if (Math.Sign(value) < 0)
                            {
                                swFile.WriteLine("Participant that paid {1} " + printCollect + " {2} ", vl.Key, String.Format("{0:c}",vl.Value) , FormatCurrency(totalIndividualTrip - vl.Value));

                            }
                            if(Math.Sign(value) > 0)
                            {
                                swFile.WriteLine("Participant that paid {1} " + printOwes + " {2} ", vl.Key, String.Format("{0:c}", vl.Value), FormatCurrency(totalIndividualTrip - vl.Value));
                            }

                            if (Math.Sign(value) == 0)
                            {
                                swFile.WriteLine("Participant that paid {1} " + printCollect + " {2} ", vl.Key, String.Format("{0:c}", vl.Value), FormatCurrency(totalIndividualTrip - vl.Value));
                                swFile.WriteLine("Participant that paid {1} " + printOwes + " {2} ", vl.Key, String.Format("{0:c}", vl.Value), FormatCurrency(totalIndividualTrip - vl.Value));
                            }
                                                              
                                
                        }

                        swFile.WriteLine("Total in expenses: {0}", String.Format("{0:c}", Trip.TotalPaidParticipant.Sum()));
                        swFile.WriteLine("-------------------------------------");
                    }
                   
                    Trip.TotalPaidParticipant.Clear();
                    Trip.ParticipantExpense.Clear();
                    Console.WriteLine();
                }

                               
            }
            catch (DivideByZeroException)
            {
                throw;

            }
            catch (IOException)
            {
                throw;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (swFile!=null)
                {
                    swFile.Close();
                }

                if (fs!=null)
                {
                    fs.Close();
                }
            }
        }

        
        /// <summary>
        /// Helper to format currency.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatCurrency(decimal value)
        {
            // Gets a NumberFormatInfo associated with the en-US culture.
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            return value.ToString("C", nfi);
        }

        /// <summary>
        /// Make the Data Structure of the Trip.
        /// </summary>
        /// <param name="strFile"></param>
        /// <param name="camping"></param>
        public void StartBillsTransacation(ref StreamReader strFile, ref Trip camping)
        {
            
            int valueIntGroup;
            decimal valueBill;

            try
            {

                var listTrip = new List<string>();
                using (var sr = strFile)
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        listTrip.Add(line);
                    }
                }

                
                Trip group = null;
                Trip ptc = null;
                tripList = new List<Trip>();
                int countLines = 0;
                int pos = 0;
                foreach (string lines in listTrip)
                {
                    pos = countLines;

                    if (Int32.TryParse(listTrip.ElementAt(pos), out valueIntGroup))
                    {
                        //Check end of the file and save the last trip.
                        if ((valueIntGroup == 0) && (group != null)) {
                            tripList.Add(group);
                            break;
                        }
                        if (Int32.TryParse(listTrip.ElementAt(pos+1), out valueIntGroup))
                        {
                            
                            if (group != null)
                            {
                                tripList.Add(group);//Save a trip.
                                group = null;//Start new Trip

                            }
                        }
                    }


                    if (Int32.TryParse(listTrip.ElementAt(pos), out valueIntGroup))
                    {
                        //Check if is first Group or Participant
                        if (pos == 0)
                        {
                            //Insert Group
                            group = camping.newNode(valueIntGroup);
                        }
                        else
                        {
                            if (group == null)
                            {
                                //Insert new Group from the new Trip.
                                group = camping.newNode(valueIntGroup);
                            }
                            else
                            {
                                //Insert Participant
                                ptc = camping.addComponent(group, valueIntGroup);
                            }
                        }
                    }

                    //Check if is a Participant or Bill/Charges
                    if (!Int32.TryParse(listTrip.ElementAt(pos), out valueIntGroup))
                    {
                        //If participant exists insert bill/charge
                        if (group != null)
                        {
                            if (Decimal.TryParse(listTrip.ElementAt(pos), out valueBill))
                            {
                                Trip bill = camping.addComponent(ptc, valueBill);
                            }
                        }
                    }

                    countLines++;//Position line of the file.
                }
                
                camping=group;//publishing the trip.
                
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (strFile!=null)
                {
                    strFile.Close();
                }
            }


        }
    }
}
