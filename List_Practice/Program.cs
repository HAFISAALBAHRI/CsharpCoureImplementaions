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
                    //case "2":
                    //    StudentScoreBoard();
                    //    break;
                    //case "3":
                    //    ProductPriceFinder();
                    //    break;
                    //case "4":
                    //    RaceFinishTimes();
                    //    break;
                    //case "5":
                    //    ClassroomGradeReport();
                    //    break;
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