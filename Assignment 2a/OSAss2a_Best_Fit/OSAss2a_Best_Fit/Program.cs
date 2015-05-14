using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSAss2a_Best_Fit
{
    class Program
    {
        //GENERATION SETTINGS
        const int jobNo = 10;       //Number of jobs to be generated    
        const int jobMin = 8;        //Minimum size of generated jobs
        const int jobMax = 1024;      //Maximum size of generated jobs

        const int blockNo = 10;     //Number of blocks to be generated
        const int blockMin = 8;      //Minimum size of generated jobs
        const int blockMax = 1024;    //Maximum size of generated jobs

        int[] inputJob = new int[] { 573, 246, 346, 98, 756};
        int[,] inputBlock = new int[,] { { 218, -1 }, { 184, -1 }, { 975, -1 }, { 102, -1 }, { 248, -1 } };

        static void Main(string[] args)
        {
            var r = new Random(); //making r to use when generating random int

            //Generating random jobs from constraints
            int[] job = new int[jobNo];
            for (int i = 0; i != jobNo; i++)
            {
                job[i] = r.Next(jobMin, jobMax);
            }
            //Generating random blocks from constraints
            int[,] block = new int[blockNo, 2];
            for (int i = 0; i != blockNo; i++)
            {
                block[i, 0] = r.Next(jobMin, jobMax);
                block[i, 1] = -1;
            }
            Console.Write("Best Fit Sorting\n\nJob Sizes: ");
            foreach (int i in job) { Console.Write(i + " "); } //Lists out the job sizes
            Console.WriteLine("\n\nSorting....");

            //THE SORTING
            var timer = System.Diagnostics.Stopwatch.StartNew(); //Making a stopwatch to time how long it takes to assign jobs
            int loopCount = 0; //Counts loops, throws out big number at the end.
            for (int x = 0; x < jobNo; x++ ) //Loops for number of jobs
            {
                loopCount++;
                bool jobAssigned = false; //This is used to stop it from assigning a job to more than one block
                if (job[x] > blockMax) { /*Console.WriteLine("JOB TOO BIG");*/ }
                else
                {
                    for (int jobVarSize = job[x]; jobVarSize <= blockMax; jobVarSize++)
                    {
                        loopCount++;
                        for (int y = 0; y < blockNo; y++)
                        {
                            loopCount++;
                            if (jobVarSize == block[y, 0] && block[y, 1] == -1 && jobAssigned == false)
                            {
                                block[y, 1] = x;
                                jobAssigned = true;
                                break;
                            }
                        }
                    }
                }
            }
            timer.Stop();

            Console.WriteLine("\nResults:\n");
            for (int i = 0; i != blockNo; i++) //Loops for number of blocks to output their details
            {
                Console.WriteLine("Block: " + i + ", Block Size: " + block[i, 0]);
                Console.ForegroundColor = ConsoleColor.Yellow; 
                if (block[i, 1] != -1) { Console.WriteLine("    Job Assigned: " + block[i, 1] + ", Size: " + job[block[i, 1]]); }
                else { Console.WriteLine("    No job assigned."); }
                Console.ResetColor(); 
            }
            Console.WriteLine("\nLoop Count: " + loopCount+"\nTime enlapsed " + timer.ElapsedMilliseconds + "ms");
            Console.Read();
        }
    }
}