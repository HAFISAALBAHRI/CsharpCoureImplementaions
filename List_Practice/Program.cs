namespace List_Practice
{
    internal class Program
    {

        static void ShowMenu()
        {
            Console.WriteLine("****************************");
            Console.WriteLine("=== List Practice Menu ===");
            Console.WriteLine("****************************");
            Console.WriteLine("1 - Temperature Log");
            Console.WriteLine("2 - Student Score Board");
            Console.WriteLine("3 - Product Price Finder");
            Console.WriteLine("4 - Race Finish Times");
            Console.WriteLine("5 - Classroom Grade Report");
            Console.WriteLine("6 - Warehouse Inventory Check");
            Console.WriteLine("7 - Library Book Shelf Scanner");
            Console.WriteLine("8 - Sales Performance Analyzer");
            Console.WriteLine("9 - Flight Seat Allocation Display");
            Console.WriteLine("10 - Hospital Patient Priority Queue");
            Console.WriteLine("0 - Exit");
        }

        public static void TemperatureLog()
        {
            List<double> temperatures = new List<double> { 44.5, 42.1, 40.0, 49.5, 41.2, 43.8, 45.0 };
            for (int i = 0; i < temperatures.Count; i++) 
            {
                Console.WriteLine("Day " + (i + 1) + " : " + temperatures[i] + " C");
            }
            Console.WriteLine("*****************************************");
            Console.WriteLine("Total readings : " + temperatures.Count);
            Console.WriteLine("*****************************************");
        }

        public static void StudentScoreBoard()
        {
            List<int> scores = new List<int> { 85, 72, 90, 66, 78, 95 };
            Console.WriteLine("***********************");
            Console.WriteLine("Original Scores :");
            Console.WriteLine("***********************");
            foreach (int score in scores)
            {
                Console.WriteLine(score);
            }

            scores.Reverse();
            Console.WriteLine("**********************");
            Console.WriteLine("Reversed Scores :");
            Console.WriteLine("**********************");
            foreach (int score in scores)
            {
                Console.WriteLine(score);
            }
        }

        public static void ProductPriceFinder()
        {
            List<double> prices = new List<double> { 4.99, 55.50, 12.25, 7.25, 15.20 };
            for (int i = 0; i < prices.Count; i++) 
            {
                Console.WriteLine("Product " + (i + 1) + " : " + prices[i] + " OMR");
            }
            double targetPrice = 7.25;

            if (prices.Contains(targetPrice))
            {
                int index = prices.IndexOf(targetPrice); // IndexOf works directly on List
                Console.WriteLine("**************************************************************");
                Console.WriteLine("Price " + targetPrice + " OMR found at Product " + (index + 1));
                Console.WriteLine("**************************************************************");
            }
            else
            {
                Console.WriteLine("Price " + targetPrice + " OMR not found.");
            }
        }

       public static void RaceFinishTimes()
    {
        List<int> finishTimes = new List<int> { 320, 275, 290, 310, 305, 280, 330, 300 };
        Console.WriteLine("*****************************");
        Console.WriteLine("--- Original Finish Times ---");
        Console.WriteLine("*****************************");
        foreach (int time in finishTimes)
            Console.WriteLine(time + " seconds");
         finishTimes.Sort();

        Console.WriteLine("********************************************");
        Console.WriteLine("--- Sorted Finish Times (Fastest First) ---");
        Console.WriteLine("********************************************");
        foreach (int time in finishTimes)
            Console.WriteLine(time + " seconds");
        Console.WriteLine("Total Participants : " + finishTimes.Count);
    }

       public static void ClassroomGradeReport()
         {
        List<int> grades = new List<int> { 85, 72, 90, 66, 100, 78, 55, 88, 95, 60 };
        grades.Sort(); // {55, 60, 66, 72, 78, 85, 88, 90, 95, 100}
        grades.Reverse(); // {100, 95, 90, 88, 85, 78, 72, 66, 60, 55}
        Console.WriteLine("******************************");
        Console.WriteLine("--- Classroom Grade Report ---");
        Console.WriteLine("******************************");
        for (int i = 0; i < grades.Count; i++) 
        {
            Console.WriteLine("Rank " + (i + 1) + " : " + grades[i]);
        }
    }



    static void Main(string[] args)
        {
            bool exit = false;
            while (exit == false)
            {
                ShowMenu();
                Console.Write("Enter choice: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        TemperatureLog();
                        break;
                    case "2":
                        StudentScoreBoard();
                        break;
                    case "3":
                        ProductPriceFinder();
                        break;
                    case "4":
                        RaceFinishTimes();
                        break;
                    case "5":
                        ClassroomGradeReport();
                        break;
                    //case "6":
                    //    WarehouseInventoryCheck();
                    //    break;
                    //case "7":
                    //    LibraryBookShelfScanner();
                    //    break;
                    //case "8":
                    //    SalesPerformanceAnalyzer();
                    //    break;
                    //case "9":
                    //    FlightSeatAllocationDisplay();
                    //    break;
                    //case "10":
                    //    HospitalPatientPriorityQueue();
                    //    break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

                if (exit == false)
                {
                    Console.WriteLine(" Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            Console.WriteLine("Exiting system. ");
        }
    }
}