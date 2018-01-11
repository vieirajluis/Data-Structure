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
* @class   Trip (Data Structure - N-Ary Tree of Trips)
* @author  Luis Vieira
* @date	   2018-01-09, 2018
* Information: Name and License type and version 
*(url:joaolsvieira@gmail.com)
*
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplittingTheBill
{

    public class Trip
    {
        /// <summary>
        /// Trip/Group identifier.
        /// </summary>
        private static int parent=0;

        /// <summary>
        /// Total paid per participant.
        /// </summary>
        public static List<decimal> TotalPaidParticipant { get; private set; }

        /// <summary>
        /// Total paid per trip.
        /// </summary>
        public static Dictionary<int,decimal> ParticipantExpense { get; private set;}

       
        /// <summary>
        /// Value of the component: int or currency.
        /// </summary>
        public decimal Data { get; set; }
                        
        /// <summary>
        /// Next component
        /// </summary>
        internal Trip Next { get; set; }
        
        /// <summary>
        /// Component: person or bill/charges in the Trip. Component from a group.
        /// </summary>
        internal Trip Component { get; set; }

        public Trip() {}

        public Trip(bool splitting)
        {
            TotalPaidParticipant = new List<decimal>();
            ParticipantExpense = new Dictionary<int, decimal>();
        }

        /// <summary>
        /// Creating new Node=>Group/Component: person or bill/charges
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Trip newNode(decimal data)
        {
            Trip newNode = new Trip();
            newNode.Next = newNode.Component = null;
            newNode.Data = data;
            return newNode;
        }

        /// <summary>
        /// Adds components: person or bill/charges to a list with starting with n
        /// </summary>
        /// <param name="n"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Trip addComponents(Trip n, decimal data)
        {
            if (n == null)
                return null;

            // Check if next in the list is not empty.
            while (n.Next != null)
                n = n.Next;

            return (n.Next = newNode(data));
        }

        /// <summary>
        /// Add components: person or bill/charges - Node to a Node
        /// </summary>
        /// <param name="n"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Trip addComponent(Trip n, decimal data)
        {
            if (n == null)
                return null;

            // Check if component list is not empty.
            if (n.Component != null)
                return addComponents(n.Component, data);
            else
                return (n.Component = newNode(data));
        }

        /// <summary>
        /// Traverses tree in level order
        /// </summary>
        /// <param name="root"></param>
        public static void printTrip(Trip root)
        {
            if (root == null)
                return;

            while (root != null)
            {
                Console.WriteLine(root.Data);
                if (root.Component != null)
                {
                    printTrip(root.Component);
                }
                root = root.Next;
            }
        }

        /// <summary>
        /// Total of paid bills/charges per participant.
        /// </summary>
        /// <param name="root"></param>
        public static void sumBills(Trip root)
        {
            
            decimal sum =0;
            
            if (root == null)
                return;

            while (root != null)
            {
             
                Console.WriteLine(root.Data);
                if (isLeaf(root))
                {
                    sum += root.Data;
                    
                }
                else
                {
                    
                    sumBills(root.Component);
                }
                root = root.Next;
            }

            if (sum > 0)
            {
                ParticipantExpense.Add(++parent, sum);
                TotalPaidParticipant.Add(sum);
                Console.WriteLine("Parent: {0}", parent);
                Console.WriteLine("Sum: {0}", sum);
            }

            
            
        }

        /// <summary>
        /// A utility function to check if a given node is leaf or not
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static bool isLeaf(Trip node)
        {
            if (node == null)
                return false;
            
            if (!(node.Next == null) 
                && (node.Component==null))
            {
                
                return true;
            }
            else if ((node.Next == null) && (node.Component == null))
            {
                
                return true;
            }
            return false;
        }

    }
}
