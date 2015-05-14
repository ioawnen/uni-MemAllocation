using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace OSAss2a_First_Fit
{
    class Program
    {

        //GENERATION SETTINGS
        const int jobNo = 10; //Number of jobs to be generated    
        const int jobMin = 8; //Minimum size of generated jobs
        const int jobMax = 1024; //Maximum size of generated jobs

        const int blockNo = 10;
        const int blockMin = 8;
        const int blockMax = 1024;

        static void Main(string[] args)
        {
            var r = new Random(); //making r to use when generating random int 
           
            //Generating random jobs from constraints
            int[] job = new int[jobNo];
            for(int i = 0; i!=jobNo; i++)
            {
                job[i] = r.Next(jobMin, jobMax);
            }
            //Generating random blocks from constraints
            int[,] block = new int[blockNo, 2];
            for (int i = 0; i != blockNo; i++)
            {
                block[i,0] = r.Next(jobMin, jobMax);
                block[i, 1] = -1;
            }

            Console.Write("First Fit Sorting\n\nJob Sizes: ");
            foreach (int i in job) { Console.Write(i + " "); } //Lists out the job sizes
            Console.WriteLine("\n\nSorting....");

            //THE SORTING
            var timer = System.Diagnostics.Stopwatch.StartNew(); //Making a stopwatch to time how long it takes to assign jobs
            int loopCount = 0; //Counts loops
            for (int x = 0; x < jobNo; x++) //Loops for number of jobs
            {
                loopCount++;
                for (int y = 0; y < blockNo; y++) //Loops for number of blocks
                {
                    loopCount++;
                    if (block[y, 0] >= job[x] && block[y, 1] == -1) //Checks for blocks with no jobs assigned and is smaller or equal to size of block  
                    {
                        block[y, 1] = x; //Assigns job to block
                        break; //Breaks out of loop to stop search for this particular job.
                    }  
                }
            }
            timer.Stop();
            Console.SetWindowSize(100, 500);


            Console.WriteLine("\nResults:\n");
            for (int i = 0; i != blockNo; i++) //Loops for number of blocks to output their details
            {
                Console.WriteLine("Block: " + i + ", Block Size: " + block[i, 0]);
                Console.ForegroundColor = ConsoleColor.Yellow; 
                if (block[i, 1] != -1) { Console.WriteLine("    Job Assigned: " + block[i, 1] + ", Size: " + job[block[i, 1]]); }
                else { Console.WriteLine("    No job assigned."); }
                Console.ResetColor();
            }
            Console.WriteLine("\nLoop Count: " + loopCount + "\nTime enlapsed " + timer.ElapsedMilliseconds + "ms");
            //Console.SetWindowSize(100, 500);
            Console.Read();
        }
    }
}