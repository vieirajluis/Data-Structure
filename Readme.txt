Rules:

1.     The solution should be done as a Windows console application written in C#.

=> Console Application (Target framework: Net Framework 4.6.1)

2.     The input should be read from a text file where the filename is given as the first and only parameter to the Windows console application.

=>  class Program
    {
        static void Main(string[] args)
        {

            ProcessBills pbs = new ProcessBills();//Input Parameters - Trips from TripData.cs
            pbs.Transations();

        }
    }

3.     The output should be written to a text file in the same directory as the input file.
..\SplittingTheBill
=> Input (expenses.txt) and additional test (expensesTest.txt)
=> Output (expenses.txt.out)


4.     The output filename should be the original input filename + “.out”.  For example, if the input file was expenses.txt then the output file would be expenses.txt.out.
=> Output (expenses.txt.out)

5.     The solution should follow coding best practices.
=> Camel case
=> OOP

6.     Unit tests should be created.
=> ProcessBillsTests.cs

7.     You must provide the full source code of your solution as a zip file.
=> ZIP file (Source Code and Classes UML - *.png)

8.     Your zip package should only include your source files and no temporary files or binaries.
=> ZIP file (Source Code and Classes UML - *.png)