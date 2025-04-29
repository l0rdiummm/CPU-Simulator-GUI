using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpuSchedulingWinForms
{
    public static class Algorithms
    {
        public static void fcfsAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            int[] burstTime = new int[np];
            int[] completionTime = new int[np];
            int[] turnaroundTime = new int[np];
            int[] waitingTime = new int[np];
            int[] arrivalTime = new int[np];  // Assume AT = 0
            for (int i = 0; i < np; i++)
            {
                bool validInput = false;
                while (!validInput)
                {
                    string input = Microsoft.VisualBasic.Interaction.InputBox($"Enter Burst Time for P{i + 1}:", "", "0", -1, -1);
                    if (int.TryParse(input, out burstTime[i]) && burstTime[i] > 0)
                    {
                        validInput = true;
                    }
                    else
                    {
                        MessageBox.Show("Invalid burst time entered. Please enter a positive number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                arrivalTime[i] = 0;
            }
            completionTime[0] = burstTime[0];
            for (int i = 1; i < np; i++)
            {
                completionTime[i] = completionTime[i - 1] + burstTime[i];
            }
            int totalWT = 0, totalTAT = 0;
            string result = "Process\tAT\tBT\tCT\tTAT\tWT\n";
            for (int i = 0; i < np; i++)
            {
                turnaroundTime[i] = completionTime[i] - arrivalTime[i];
                waitingTime[i] = turnaroundTime[i] - burstTime[i];
                totalWT += waitingTime[i];
                totalTAT += turnaroundTime[i];

                result += $"P{i + 1}\t{arrivalTime[i]}\t{burstTime[i]}\t{completionTime[i]}\t{turnaroundTime[i]}\t{waitingTime[i]}\n";
            }

            // Calculate performance metrics
            int totalBurstTime = burstTime.Sum();
            int totalTime = completionTime.Max();
            float cpuUtilization = ((float)totalBurstTime / totalTime) * 100;
            float throughput = (float)np / totalTime;

            result += $"\nAverage Waiting Time (AWT): {(float)totalWT / np:F2}";
            result += $"\nAverage Turnaround Time (ATT): {(float)totalTAT / np:F2}";
            result += $"\nCPU Utilization: {cpuUtilization:F2}%";
            result += $"\nThroughput: {throughput:F2} processes/unit time";

            MessageBox.Show(result);
        }


        public static void sjfAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            int[] burstTime = new int[np];
            int[] completionTime = new int[np];
            int[] turnaroundTime = new int[np];
            int[] waitingTime = new int[np];
            int[] arrivalTime = new int[np];  // Assume AT = 0
            for (int i = 0; i < np; i++)
            {
                bool validInput = false;
                while (!validInput)
                {
                    string input = Microsoft.VisualBasic.Interaction.InputBox($"Enter Burst Time for P{i + 1}:", "", "0", -1, -1);
                    if (int.TryParse(input, out burstTime[i]) && burstTime[i] > 0)
                    {
                        validInput = true;
                    }
                    else
                    {
                        MessageBox.Show("Invalid burst time entered. Please enter a positive number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                arrivalTime[i] = 0;
            }
            int[] index = Enumerable.Range(0, np).ToArray();
            Array.Sort(index, (a, b) => burstTime[a].CompareTo(burstTime[b]));
            int currentTime = 0;
            for (int i = 0; i < np; i++)
            {
                int idx = index[i];
                currentTime += burstTime[idx];
                completionTime[idx] = currentTime;
            }
            int totalWT = 0, totalTAT = 0;
            string result = "Process\tAT\tBT\tCT\tTAT\tWT\n";

            for (int i = 0; i < np; i++)
            {
                turnaroundTime[i] = completionTime[i] - arrivalTime[i];
                waitingTime[i] = turnaroundTime[i] - burstTime[i];
                totalWT += waitingTime[i];
                totalTAT += turnaroundTime[i];

                result += $"P{i + 1}\t{arrivalTime[i]}\t{burstTime[i]}\t{completionTime[i]}\t{turnaroundTime[i]}\t{waitingTime[i]}\n";
            }

            // Calculate performance metrics
            int totalBurstTime = burstTime.Sum();
            int totalTime = completionTime.Max();
            float cpuUtilization = ((float)totalBurstTime / totalTime) * 100;
            float throughput = (float)np / totalTime;

            result += $"\nAverage Waiting Time (AWT): {(float)totalWT / np:F2}";
            result += $"\nAverage Turnaround Time (ATT): {(float)totalTAT / np:F2}";
            result += $"\nCPU Utilization: {cpuUtilization:F2}%";
            result += $"\nThroughput: {throughput:F2} processes/unit time";

            MessageBox.Show(result);
        }


        public static void priorityAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            int[] burstTime = new int[np];
            int[] priority = new int[np];
            int[] completionTime = new int[np];
            int[] turnaroundTime = new int[np];
            int[] waitingTime = new int[np];
            int[] arrivalTime = new int[np];  // Assume AT = 0
            for (int i = 0; i < np; i++)
            {
                bool validInput = false;
                while (!validInput)
                {
                    string input = Microsoft.VisualBasic.Interaction.InputBox($"Enter Burst Time for P{i + 1}:", "", "0", -1, -1);
                    if (int.TryParse(input, out burstTime[i]) && burstTime[i] > 0)
                    {
                        validInput = true;
                    }
                    else
                    {
                        MessageBox.Show("Invalid burst time entered. Please enter a positive number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                bool validPriority = false;
                while (!validPriority)
                {
                    string input = Microsoft.VisualBasic.Interaction.InputBox($"Enter Priority for P{i + 1} (Lower number = higher priority):", "", "0", -1, -1);
                    if (int.TryParse(input, out priority[i]))
                    {
                        validPriority = true;
                    }
                    else
                    {
                        MessageBox.Show("Invalid priority entered. Please enter a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                arrivalTime[i] = 0;
            }
            int[] index = Enumerable.Range(0, np).ToArray();
            Array.Sort(index, (a, b) => priority[a].CompareTo(priority[b]));
            int currentTime = 0;
            for (int i = 0; i < np; i++)
            {
                int idx = index[i];
                currentTime += burstTime[idx];
                completionTime[idx] = currentTime;
            }
            int totalWT = 0, totalTAT = 0;
            string result = "Process\tAT\tBT\tPriority\tCT\tTAT\tWT\n";

            for (int i = 0; i < np; i++)
            {
                turnaroundTime[i] = completionTime[i] - arrivalTime[i];
                waitingTime[i] = turnaroundTime[i] - burstTime[i];
                totalWT += waitingTime[i];
                totalTAT += turnaroundTime[i];

                result += $"P{i + 1}\t{arrivalTime[i]}\t{burstTime[i]}\t{priority[i]}\t{completionTime[i]}\t{turnaroundTime[i]}\t{waitingTime[i]}\n";
            }
            // Calculate performance metrics
            int totalBurstTime = burstTime.Sum();
            int totalTime = completionTime.Max();
            float cpuUtilization = ((float)totalBurstTime / totalTime) * 100;
            float throughput = (float)np / totalTime;

            result += $"\nAverage Waiting Time (AWT): {(float)totalWT / np:F2}";
            result += $"\nAverage Turnaround Time (ATT): {(float)totalTAT / np:F2}";
            result += $"\nCPU Utilization: {cpuUtilization:F2}%";
            result += $"\nThroughput: {throughput:F2} processes/unit time";

            MessageBox.Show(result);
        }

        public static void roundRobinAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            int[] arrivalTime = new int[np];
            int[] burstTime = new int[np];
            int[] remainingTime = new int[np];
            int[] completionTime = new int[np];
            int[] waitingTime = new int[np];
            int[] turnaroundTime = new int[np];
            for (int i = 0; i < np; i++)
            {
                arrivalTime[i] = Convert.ToInt32(Microsoft.VisualBasic.Interaction.InputBox($"Enter Arrival Time for P{i + 1}:", "", "0", -1, -1));
                burstTime[i] = Convert.ToInt32(Microsoft.VisualBasic.Interaction.InputBox($"Enter Burst Time for P{i + 1}:", "", "0", -1, -1));
                remainingTime[i] = burstTime[i];
            }
            int quantum = Convert.ToInt32(Microsoft.VisualBasic.Interaction.InputBox($"Enter Time Quantum:", "", "0", -1, -1));
            Helper.QuantumTime = quantum.ToString();
            int time = 0, completed = 0;
            Queue<int> queue = new Queue<int>();
            bool[] inQueue = new bool[np];
            for (int i = 0; i < np; i++)
            {
                if (arrivalTime[i] == 0)
                {
                    queue.Enqueue(i);
                    inQueue[i] = true;
                }
            }
            while (completed < np)
            {
                if (queue.Count == 0)
                {
                    time++;
                    for (int i = 0; i < np; i++)
                    {
                        if (!inQueue[i] && arrivalTime[i] <= time)
                        {
                            queue.Enqueue(i);
                            inQueue[i] = true;
                        }
                    }
                    continue;
                }
                int idx = queue.Dequeue();
                int execTime = Math.Min(quantum, remainingTime[idx]);
                time += execTime;
                remainingTime[idx] -= execTime;
                for (int i = 0; i < np; i++)
                {
                    if (!inQueue[i] && arrivalTime[i] <= time)
                    {
                        queue.Enqueue(i);
                        inQueue[i] = true;
                    }
                }
                if (remainingTime[idx] > 0)
                {
                    queue.Enqueue(idx);
                }
                else
                {
                    completionTime[idx] = time;
                    completed++;
                }
            }
            int totalWT = 0, totalTAT = 0;
            string result = "Process\tAT\tBT\tCT\tTAT\tWT\n";
            for (int i = 0; i < np; i++)
            {
                turnaroundTime[i] = completionTime[i] - arrivalTime[i];
                waitingTime[i] = turnaroundTime[i] - burstTime[i];
                totalWT += waitingTime[i];
                totalTAT += turnaroundTime[i];

                result += $"P{i + 1}\t{arrivalTime[i]}\t{burstTime[i]}\t{completionTime[i]}\t{turnaroundTime[i]}\t{waitingTime[i]}\n";
            }
            // Calculate performance metrics
            int totalBurstTime = burstTime.Sum();
            int totalTime = completionTime.Max();
            float cpuUtilization = ((float)totalBurstTime / totalTime) * 100;
            float throughput = (float)np / totalTime;

            result += $"\nAverage Waiting Time (AWT): {(float)totalWT / np:F2}";
            result += $"\nAverage Turnaround Time (ATT): {(float)totalTAT / np:F2}";
            result += $"\nCPU Utilization: {cpuUtilization:F2}%";
            result += $"\nThroughput: {throughput:F2} processes/unit time";

            MessageBox.Show(result);
        }

        //Start of new algorithm, Shortest Remaining Time First
        public static void srtfAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            int[] arrivalTime = new int[np];
            int[] burstTime = new int[np];
            int[] remainingTime = new int[np];
            int[] completionTime = new int[np];
            int[] waitingTime = new int[np];
            int[] turnaroundTime = new int[np];

            // Collect arrival and burst times
            for (int i = 0; i < np; i++)
            {
                string atInput = Microsoft.VisualBasic.Interaction.InputBox($"Enter Arrival Time for P{i + 1}:", "", "0", -1, -1);
                arrivalTime[i] = Convert.ToInt32(atInput);

                string btInput = Microsoft.VisualBasic.Interaction.InputBox($"Enter Burst Time for P{i + 1}:", "", "0", -1, -1);
                burstTime[i] = Convert.ToInt32(btInput);

                remainingTime[i] = burstTime[i];
            }
            int complete = 0, time = 0;
            while (complete != np)
            {
                int minm = int.MaxValue;
                int shortest = -1;
                bool check = false;
                for (int j = 0; j < np; j++)
                {
                    if (arrivalTime[j] <= time && remainingTime[j] > 0 && remainingTime[j] < minm)
                    {
                        minm = remainingTime[j];
                        shortest = j;
                        check = true;
                    }
                }
                if (!check)
                {
                    int nextArrival = int.MaxValue;
                    for (int j = 0; j < np; j++)
                    {
                        if (remainingTime[j] > 0 && arrivalTime[j] > time)
                        {
                            nextArrival = Math.Min(nextArrival, arrivalTime[j]);
                        }
                    }
                    if (nextArrival != int.MaxValue)
                    {
                        time = nextArrival;
                    }
                    else
                    {
                        break;
                    }
                    continue;
                }
                remainingTime[shortest]--;
                if (remainingTime[shortest] == 0)
                {
                    complete++;
                    int finishTime = time + 1;
                    completionTime[shortest] = finishTime;
                    waitingTime[shortest] = finishTime - burstTime[shortest] - arrivalTime[shortest];
                    if (waitingTime[shortest] < 0)
                    {
                        waitingTime[shortest] = 0;
                    }
                }
                time++;
            }
            int totalWaitingTime = 0, totalTurnaroundTime = 0;
            string result = "Process\tAT\tBT\tCT\tTAT\tWT\n";

            for (int i = 0; i < np; i++)
            {
                turnaroundTime[i] = completionTime[i] - arrivalTime[i];
                totalWaitingTime += waitingTime[i];
                totalTurnaroundTime += turnaroundTime[i];

                result += $"P{i + 1}\t{arrivalTime[i]}\t{burstTime[i]}\t{completionTime[i]}\t{turnaroundTime[i]}\t{waitingTime[i]}\n";
            }
            int totalBurstTime = 0;
            for (int i = 0; i < np; i++)
            {
                totalBurstTime += burstTime[i];
            }

            float avgWT = (float)totalWaitingTime / np;
            float avgTAT = (float)totalTurnaroundTime / np;
            int totalTime = completionTime.Max();
            float cpuUtilization = ((float)totalBurstTime / totalTime) * 100;
            float throughput = (float)np / totalTime;

            // Calculate performance metrics
            result += $"\nAverage Waiting Time (AWT): {avgWT:F2}";
            result += $"\nAverage Turnaround Time (ATT): {avgTAT:F2}";
            result += $"\nCPU Utilization: {cpuUtilization:F2}%";
            result += $"\nThroughput: {throughput:F2} processes/unit time";
            MessageBox.Show(result);
        }
        //Start of new algorithm, Multi-Level Feedback Queue
        public static void mlfqAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput); // Number of processes
            int[] arrivalTime = new int[np];
            int[] burstTime = new int[np];
            int[] remainingTime = new int[np];
            int[] completionTime = new int[np];
            int[] waitingTime = new int[np];
            int[] turnaroundTime = new int[np];

            // Collect arrival and burst times
            for (int i = 0; i < np; i++)
            {
                string atInput = Microsoft.VisualBasic.Interaction.InputBox($"Enter Arrival Time for P{i + 1}:", "", "0", -1, -1);
                arrivalTime[i] = Convert.ToInt32(atInput);

                string btInput = Microsoft.VisualBasic.Interaction.InputBox($"Enter Burst Time for P{i + 1}:", "", "0", -1, -1);
                burstTime[i] = Convert.ToInt32(btInput);

                remainingTime[i] = burstTime[i];
            }
            Queue<int> queue1 = new Queue<int>();
            Queue<int> queue2 = new Queue<int>();
            Queue<int> queue3 = new Queue<int>();
            int quantum1 = 4;
            int quantum2 = 8;
            int time = 0;
            int completed = 0;
            bool[] added = new bool[np];  // Tracks which processes have been added
            while (completed < np)
            {
                // Enqueue the arriving processes into Queue 1
                for (int i = 0; i < np; i++)
                {
                    if (arrivalTime[i] <= time && remainingTime[i] > 0 && !added[i])
                    {
                        queue1.Enqueue(i);
                        added[i] = true;
                    }
                }
                // Process Queue 1
                if (queue1.Count > 0)
                {
                    int index = queue1.Dequeue();
                    int execTime = Math.Min(quantum1, remainingTime[index]);
                    time += execTime;
                    remainingTime[index] -= execTime;

                    if (remainingTime[index] == 0)
                    {
                        completionTime[index] = time;
                        completed++;
                    }
                    else
                    {
                        queue2.Enqueue(index);
                    }
                }
                // Process Queue 2
                else if (queue2.Count > 0)
                {
                    int index = queue2.Dequeue();
                    int execTime = Math.Min(quantum2, remainingTime[index]);
                    time += execTime;
                    remainingTime[index] -= execTime;

                    if (remainingTime[index] == 0)
                    {
                        completionTime[index] = time;
                        completed++;
                    }
                    else
                    {
                        queue3.Enqueue(index);
                    }
                }
                // Process Queue 3
                else if (queue3.Count > 0)
                {
                    int index = queue3.Dequeue();
                    time += remainingTime[index];
                    remainingTime[index] = 0;
                    completionTime[index] = time;
                    completed++;
                }
                else
                {
                    time++;
                }
            }
            // Calculate turnaround and waiting times
            int totalWaitingTime = 0, totalTurnaroundTime = 0;
            string result = "Process\tAT\tBT\tCT\tTAT\tWT\n";

            for (int i = 0; i < np; i++)
            {
                turnaroundTime[i] = completionTime[i] - arrivalTime[i];
                waitingTime[i] = turnaroundTime[i] - burstTime[i];
                totalWaitingTime += waitingTime[i];
                totalTurnaroundTime += turnaroundTime[i];

                result += $"P{i + 1}\t{arrivalTime[i]}\t{burstTime[i]}\t{completionTime[i]}\t{turnaroundTime[i]}\t{waitingTime[i]}\n";
            }
            int totalBurstTime = 0;
            for (int i = 0; i < np; i++)
            {
                totalBurstTime += burstTime[i];
            }

            float avgWT = (float)totalWaitingTime / np;
            float avgTAT = (float)totalTurnaroundTime / np;
            int totalTime = completionTime.Max();  // Time when the last process completes
            float cpuUtilization = ((float)totalBurstTime / totalTime) * 100;
            float throughput = (float)np / totalTime;

            // Calculate performance metrics
            result += $"\nAverage Waiting Time (AWT): {avgWT:F2}";
            result += $"\nAverage Turnaround Time (ATT): {avgTAT:F2}";
            result += $"\nCPU Utilization: {cpuUtilization:F2}%";
            result += $"\nThroughput: {throughput:F2} processes/unit time";
            MessageBox.Show(result);
        }


    }
}


