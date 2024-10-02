public class Program
{
    public static Semaphore semaphore;
    public static int semaphore_count = 1;
    static void Menu()
    {
        Console.WriteLine(" [-1] Exit");
        Console.WriteLine(" [1] Create new thread");
        Console.WriteLine(" [2] Click Thread (Created)");
        Console.WriteLine(" [3] Click Thread (Waiting)");
        Console.WriteLine(" [000] Show All Operation");
    }
    static void Main(string[] args)
    {
        List<Thread> CreatedThreads = new List<Thread>();
        List<Thread> WaitingThreads = new List<Thread>();
        semaphore = new Semaphore(semaphore_count, 4);
        int choice = -2;
        while (choice != -1)
        {
            Menu();
            Console.Write("Enter the choice: ");
            choice = Convert.ToInt32(Console.ReadLine());
            
            switch (choice)
            {
                case 1:
                    {
                        Thread thread = new Thread(() =>
                        {
                            Thread.Sleep(300);
                            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} working...");
                        });
                        CreatedThreads.Add(thread);
                        thread.Start();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Thread.Sleep(1000);
                        Console.ResetColor();
                        Console.Clear();
                        break;
                    }
                case 2:
                    {
                        List<Thread> threadsCopy = new List<Thread>(CreatedThreads);
                        Console.Clear();
                        foreach (Thread thread in CreatedThreads)
                        {
                            Console.WriteLine($"Thread {thread.ManagedThreadId}");
                        }
                        Console.Write("Select Thread (Only number enter): ");
                        int selected_thred = Convert.ToInt32(Console.ReadLine());
                        foreach (Thread thread in threadsCopy)
                        {
                            int currentThreadId = thread.ManagedThreadId;
                            if (selected_thred == currentThreadId)
                            {
                                CreatedThreads.Remove(thread);
                                WaitingThreads.Add(thread);
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("\n                 Waiting...");
                                Console.ResetColor();
                                Thread.Sleep(500);
                                break;
                            }
                            currentThreadId = 0;
                        }
                        Console.Clear();
                        break;
                    }
                case 3:
                    {
                        List<Thread> threadsCopy2 = new List<Thread>(WaitingThreads);
                        Console.Clear();
                        foreach (Thread thread in WaitingThreads)
                        {
                            Console.WriteLine($"Thread {thread.ManagedThreadId}");
                        }
                        Console.Write("Select Thread (Only number enter): ");
                        int selected_thred = Convert.ToInt32(Console.ReadLine());
                        foreach (Thread thread in threadsCopy2)
                        {
                            int currentThreadId = thread.ManagedThreadId;
                            if (selected_thred == currentThreadId)
                            {
                                WaitingThreads.Remove(thread);
                                ReleaseSemaphore();
                                break;
                            }
                            currentThreadId = 0;
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                case 000:
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Created Threads");
                        Console.ResetColor();
                        foreach (Thread thread in CreatedThreads)
                        {
                            Console.WriteLine($"Thread {thread.ManagedThreadId}");
                            Thread.Sleep(200);
                        }
                        Console.WriteLine("------------------------\n");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Waiting Threads");
                        Console.ResetColor();
                        foreach (Thread thread in WaitingThreads)
                        {
                            Console.WriteLine($"Thread {thread.ManagedThreadId}");
                            Thread.Sleep(200);
                        }
                        Console.WriteLine("------------------------");
                        Console.ReadKey();
                        break;
                    }
            }
        }
        
    }

    static void ReleaseSemaphore()
    {
        if(semaphore_count < 4)
        {
            semaphore.Release();
            semaphore_count++;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Semaphore worked");
            Console.ResetColor();
        }
    }
}