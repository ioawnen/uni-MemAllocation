
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace OSAss2b_Shortest_Remaining_Time_First
{
    class Program
    {
        //SETTINGS
        const int ProcessNo = 10;           //Number of processes to be generated    
        const int processTimeMin = 1;       //Minimum process time
        const int ProcessTimeMax = 10;      //Maximum process time
        const int genWaitMin = 1000;        //Minimun wait between generating processes
        const int genWaitMax = 2000;        //Maximum wait between generating processes
        const int speed = 500;              //Process speed

        private static List<int> processQueue = new List<int>(); //Used to store pending processes. There was no point using an array this time to store so little information

        private static string processesMade = ""; //Stores a string with all the processes in it. Simple way of checking what's being generated.

        public static void generateProcesses()
        {
            var r = new Random(); // generates random ints
            for (int i = 0; i != ProcessNo; i++)
            {
                Thread.Sleep(r.Next(genWaitMin, genWaitMax)); //Time to wait between generating new processes
                int process = r.Next(processTimeMin, ProcessTimeMax);
                processQueue.Add(process); //Adding new random process to que4ue         
                processesMade = processesMade + " " + process;
            }
        }

        private static void sortLoop() //Processes the processes, looks for smallest process and works through it.
        {
            int processCount = 0;
            while(processCount!=ProcessNo)
            {
                try 
                { 
                    int minProcess = processQueue.Min(); //Find shortest remaining time process

                    int location = -1;
                    for(int i = 0; i<processQueue.Count; i++) //iterate through list, find location of min value
                    {
                        if (processQueue[i] == minProcess) { location = i; }
                    }

                    for (int i = minProcess; i != 0; i--) //Loops until process time is decremented to 0 (or interrupted)
                    {
                        printQueue(); //Output queue every loop
                        minProcess--; //Decrementing process time value
                        processQueue[location] = minProcess; //Setting value in list to new value
                                                
                        Thread.Sleep(speed); // Delay between loops

                        if (processQueue.Min() != minProcess) { break; } //If another process has shorter time, break out of loop
                        else if (minProcess == 0) //If process time at 0, remove process from queue, add 1 to process count
                        {
                            printQueue(); //Output queue when about to remove process
                            processQueue.RemoveAt(location);  //Remove finished process from queue
                            processCount++;
                        } 
                    }
                }
                catch (InvalidOperationException ex) { /*Console.WriteLine(ex);*/ } //Spams this when there is nothing in the list. Uncomment if you like walls of text.
            }
        }
        
        private static void printQueue() //Loops through queue list, outputs results in a satisfying way
        {   
            Console.WriteLine();
            foreach(int i in processQueue)
            {
                Console.Write(i+"\t");
                
            }
        }

        static void Main(string[] args)
        {

            //makes new thread, generates processes in background
            Thread processThread = new Thread(new ThreadStart(generateProcesses));
            processThread.Start(); //Start process generator thread
            sortLoop(); //Start sorting

            Console.WriteLine("--END--"); //Shown once everything is complete
            Console.WriteLine("Processes Generated: " + processesMade); //Print out the processes that were made
            Console.Read(); 
        }
    }
}
