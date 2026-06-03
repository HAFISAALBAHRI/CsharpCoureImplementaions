namespace List_Practice_Task
{
    internal class Program
    {
        static void ShowMenu()
        {
            Console.WriteLine("****************************");
            Console.WriteLine("=== Hotel Management System ===");
            Console.WriteLine("****************************");
            Console.WriteLine("1 - Room Service Menu");
            Console.WriteLine("2 - Guest Check-In Queue");
            Console.WriteLine("3 - Housekeeping Floor Assignment");
            Console.WriteLine("4 - Booking Conflict Resolver");
            Console.WriteLine("0 - Exit");
        }

        public static void RoomServiceMenu()
        {
            List<string> menuItems = new List<string> { "Burger", "Pizza", "Salad", "Soup" };

            Console.WriteLine("--- Original Menu ---");
            for (int i = 0; i < menuItems.Count; i++)
                Console.WriteLine((i + 1) + ". " + menuItems[i]);

            menuItems.Add("Pasta");
            menuItems.Add("Sandwich");
            Console.WriteLine("--- Updated Menu (After Adding) ---");
            for (int i = 0; i < menuItems.Count; i++)
                Console.WriteLine((i + 1) + ". " + menuItems[i]);

            menuItems.Remove("Soup");
            Console.WriteLine("--- Updated Menu (After Removing Soup) ---");
            for (int i = 0; i < menuItems.Count; i++)
                Console.WriteLine((i + 1) + ". " + menuItems[i]);

            string checkDish = "Pizza";
            Console.WriteLine(menuItems.Contains(checkDish) ? checkDish + " is available." : checkDish + " is not available.");

            Console.WriteLine("Total items: " + menuItems.Count);
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
                        RoomServiceMenu(); 
                        break;
                 //case "2":
                 //       GuestCheckInQueue();
                 //       break;
                 // case "3":
                 //       HousekeepingFloorAssignment(); 
                 //       break;
                 //  case "4":
                 //       BookingConflictResolver();
                 //       break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

                if (exit == false)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            Console.WriteLine("Exiting system.");


        }
    }
}
