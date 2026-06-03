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
            if (menuItems.Contains(checkDish))
            {
                Console.WriteLine(checkDish + " is available.");
            }
            else
            {
                Console.WriteLine(checkDish + " is not available.");
            }


            Console.WriteLine("Total items: " + menuItems.Count);
        }

        public static void GuestCheckInQueue()
        {
            List<string> checkInQueue = new List<string> { "Ali", "Sara", "John", "Mary", "Omar" };

            Console.WriteLine("--- Original Queue ---");
            for (int i = 0; i < checkInQueue.Count; i++)
                Console.WriteLine((i + 1) + ". " + checkInQueue[i]);

            checkInQueue.RemoveAt(0);
            Console.WriteLine("--- Queue After First Check-In ---");
            foreach (string g in checkInQueue) Console.WriteLine(g);

            checkInQueue.RemoveAt(0);
            Console.WriteLine("--- Queue After Second Check-In ---");
            foreach (string g in checkInQueue) Console.WriteLine(g);

            checkInQueue.Add("Fatima");
            checkInQueue.Add("Hassan");
            checkInQueue.Add("Noor");
            Console.WriteLine("--- Queue After Adding New Guests ---");
            foreach (string g in checkInQueue) Console.WriteLine(g);

            string checkGuest = "Sara";
            if (checkInQueue.Contains(checkGuest))
            {
                Console.WriteLine(checkGuest + " is still waiting.");
            }
            else
            {
                Console.WriteLine(checkGuest + " is not in the queue.");
            }


            Console.WriteLine("Total guests: " + checkInQueue.Count);
        }

        public static void HousekeepingFloorAssignment()
        {
            List<int> assignedRooms = new List<int> { 305, 210, 412, 108, 520, 315 };

            Console.WriteLine("--- Original Assignment ---");
            for (int i = 0; i < assignedRooms.Count; i++)
                Console.WriteLine((i + 1) + ". Room " + assignedRooms[i]);

            assignedRooms.Add(220);
            assignedRooms.Add(330);
            assignedRooms.Remove(412);

            assignedRooms.Sort();
            Console.WriteLine("--- Sorted Assignment ---");
            foreach (int r in assignedRooms) Console.WriteLine("Room " + r);

            int targetRoom = 315;
            int index = assignedRooms.IndexOf(targetRoom);
            Console.WriteLine("Room " + targetRoom + " found at index " + index);

            assignedRooms.Insert(2, 405);
            Console.WriteLine("--- Final Assignment ---");
            foreach (int r in assignedRooms) Console.WriteLine("Room " + r);

            Console.WriteLine("Total rooms: " + assignedRooms.Count);
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
                    case "2":
                        GuestCheckInQueue();
                        break;
                    case "3":
                        HousekeepingFloorAssignment();
                        break;
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
