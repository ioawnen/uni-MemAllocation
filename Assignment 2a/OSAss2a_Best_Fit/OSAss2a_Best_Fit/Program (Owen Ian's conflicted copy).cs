using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSAss2a_Best_Fit
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] job = new int[] { 925, 150, 200, 950, 75, 450 }; // Assigning jobs and sizes
            int jobNo = job.Length;
            // Blocks are assigned here with their size and assigned jobs. '-1' = No job assigned.
            int[,] block = new int[,] { { 175, -1 }, { 350, -1 }, { 500, -1 }, { 300, -1 }, { 800, -1 }, { 100, -1 }, { 650, -1 }, { 400, -1 }, {1000, -1 } };
            int blockNo = block.Length / 2; //Works out the number of blocks
            int blockMax = block.Cast<int>().Max(); // Gets the max value in the block array. Not a good idea to run this after assigning any jobs since it doesn't discriminate between values in the array.

            for (int x = 0; x < jobNo; x++ ) //Loops for number of jobs
            {
                bool jobAssigned = false; //This is used to stop it from assigning a job to more than one block
                if (job[x] > blockMax) { Console.WriteLine("JOB TOO BIG"); }
                else
                {
                    for (int jobVarSize = job[x]; jobVarSize <= blockMax; jobVarSize++)
                    { 
                        for (int y = 0; y != blockNo; y++)
                        {
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
            Console.Write("Best Fit Sorting\n\nJob Sizes: ");
            foreach (int i in job) { Console.Write(i + " "); } //Lists out the job sizes
            Console.WriteLine("\n\nResults:\n");
            for (int i = 0; i != blockNo; i++) //Loops for number of blocks to output their details
            {
                Console.WriteLine("Block: " + i + ", Block Size: " + block[i, 0]);
                Console.ForegroundColor = ConsoleColor.Yellow; 
                if (block[i, 1] != -1) { Console.WriteLine("    Job Assigned: " + block[i, 1] + ", Size: " + job[block[i, 1]]); }
                else { Console.WriteLine("    No job assigned."); }
                Console.ResetColor(); 
            }
            Console.Read();
        }
    }
}