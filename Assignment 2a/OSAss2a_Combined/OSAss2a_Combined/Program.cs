using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 *  Assignment 2a - Job Sorting algorithms
 *  Ian Owen - 1213654
 *  2014-03-09
 * 
 *  Merge of OSAss2a_First_Fit, OSAss2a_Best_Fit
 */

namespace OSAss2a_Combined
{
    class Program
    {
        //GENERATION SETTINGS
        const int jobNo = 500; //Number of jobs to be generated    
        const int jobMin = 10; //Minimum size of generated jobs
        const int jobMax = 1000; //Maximum size of generated jobs

        const int blockNo = 500; //Number of blocks to be generated
        const int blockMin = 10; //Minimun size of generated blocks
        const int blockMax = 1000; //Maximum size of generated blocks
        
        private static void writeResult(int[] job, int[,] block, int loopCount, System.Diagnostics.Stopwatch timer)
        {
            for (int i = 0; i != blockNo; i++) //Loops for number of blocks to output their details
            {
                Console.WriteLine("Block: " + i + ", Block Size: " + block[i, 0]);
                Console.ForegroundColor = ConsoleColor.Yellow; 
                if (block[i, 1] != -1) { Console.WriteLine("    Job Assigned: " + block[i, 1] + ", Size: " + job[block[i, 1]]); }
                else { Console.WriteLine("    No job assigned."); }
                Console.ResetColor(); 
            }
            Console.WriteLine("\nLoop Count: " + loopCount+"\nTime enlapsed " + timer.ElapsedMilliseconds + "ms");
        }

        public static void firstFit(int[] job, int[,] block)
        {
            var timer = System.Diagnostics.Stopwatch.StartNew(); //Making a stopwatch to time how long it takes to assign jobs
            int loopCount = 0; //Counts loops
            for (int x = 0; x < jobNo; x++) //Loops for number of jobs
            {
                loopCount++;
                for (int y = 0; y < blockNo; y++) //Loops for number of blocks
                {
                    loopCount++;
                    if (block[y, 0] >= job[x] && block[y, 1] == -1) //Checks for blocks with no jobs assigned and smaller or equal to size of block  
                    {
                        block[y, 1] = x; //Assigns job to block
                        break; //Breaks out of loop to stop search for this particular job.
                    }
                }
            }
            timer.Stop();
            writeResult(job, block, loopCount, timer); //Writing the results to console
        }

        private static void bestFit(int[] job, int[,] block)
        {
            var timer = System.Diagnostics.Stopwatch.StartNew(); //Making a stopwatch to time how long it takes to assign jobs
            int loopCount = 0; //Counts loops, throws out big number at the end.
            for (int x = 0; x < jobNo; x++) //Loops for number of jobs
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
            writeResult(job, block, loopCount, timer); //Writing the results to console
        }

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

            //Output jobs
            Console.Write("Jobs Generated: ");
            foreach (int i in job) { Console.Write(i + " "); } //Lists out the job sizes

            //Running the algorithms
            Console.WriteLine("\n\nFIRST FIT\n"); 
            firstFit(job, block);
            Console.WriteLine("\n\nBEST FIT\n");
            bestFit(job, block);

            Console.Read();
        }
    }
}
