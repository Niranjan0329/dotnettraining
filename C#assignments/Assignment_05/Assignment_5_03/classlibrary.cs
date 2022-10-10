
/*3.
 I.Create a class library Project called Concession. Write a method
called CalculateConcession(int age) 
that takes age as an input and calculates concession for travel as below:
If age<=5 then “Little Champs- Free Ticket” should be displayed
If age >60 then calculate 30% concession on the totalfare(Which is a
constant Eg:500 / -) and Display “ Senior Citizen” + Calculated Fare
Else “Print Ticket Booked” + Fare*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concession
{
    public class classlibrary
    {
        public int Age;
        public void CalculateConcession(int age)
            
        {

            double concession ;
            double totalfare = 500;
      
            if(age <= 5)
            {
                Console.WriteLine("“Little Champs- Free Ticket”");
            }
      else if(age>60) 
            {
                concession = totalfare * 0.3;
                double totalfare2 = totalfare - concession;
                Console.WriteLine("senior citizen {0}", totalfare2);
            }
            else
            {
                Console.WriteLine("print ticket booked {0}" ,totalfare);
            }
        }
    }
}
