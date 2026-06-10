using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; 
namespace FlightManagementSystem
{
    internal class Program
    {
        static List<string> passengerNames = new List<string>();
        static List<string> ticketNumbers = new List<string>();
        //static List<string> passengerNames = new List<string> { "Ali", "Sara", "Omar", "Fatima", "Hassan" };
        // static List<string> ticketNumbers = new List<string> { "TKT-001", "TKT-002", "TKT-003", "TKT-004", "TKT-005" };
        static string[] flightNumbers = { "OA101", "OA102", "OA103", "OA104", "OA105", "OA106" };
        static List<string> availableDates = new List<string> { "12-Jan-2026", "15-Jan-2026", "20-Jan-2026", "25-Jan-2026" };
        static Dictionary<string, string> bookingRecord = new Dictionary<string, string>();
        static Queue<string> checkedInQueue = new Queue<string>();
        static Stack<string> boardingStack = new Stack<string>();
        static List<string> cancelledTickets = new List<string>();
        static Dictionary<string, string> passengerSeatMap = new Dictionary<string, string>();
        static Queue<string> waitlistQueue = new Queue<string>();

        
        static string passengerFile = "passengers.txt"; // File Path for Persistence
    
        static void ShowMenu()
        {
            Console.WriteLine("*******************************************");
            Console.WriteLine("=== SKY WINGS FLIGHT MANAGEMENT SYSTEM ===");
            Console.WriteLine("*******************************************");
            Console.WriteLine("1 - Register New Passenger");
            Console.WriteLine("2 - View All Passengers");
            Console.WriteLine("3 - Book a Flight Ticket");
            Console.WriteLine("4 - View Booking Details");
            Console.WriteLine("5 - Update a Booking");
            Console.WriteLine("6 - Cancel a Ticket");
            Console.WriteLine("7 - Passenger Check-In");
            Console.WriteLine("8 - Board Passengers");
            Console.WriteLine("9 - Generate Flight Manifest");
            Console.WriteLine("10 - Manage Waitlist & Seat Assignment");
            Console.WriteLine("0 - Exit");
            Console.WriteLine("*******************************************");
        }

        static void LoadPassengersFromFile()
        {
            passengerNames.Clear();
            ticketNumbers.Clear();
            cancelledTickets.Clear();

            if (File.Exists(passengerFile))
            {
                string[] lines = File.ReadAllLines(passengerFile);

                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');

                    // basic check
                    if (parts.Length == 3)
                    {
                        passengerNames.Add(parts[0]);   // Name
                        ticketNumbers.Add(parts[1]);   // Ticket ID

                        if (parts[2] == "CANCELLED")
                        {
                            cancelledTickets.Add(parts[1]); // add cancelled ticket
                        }
                    }
                }
            }
        }

        static void SavePassengersToFile()
        {
            var lines = passengerNames
                .Select((name, i) => $"{name}|{ticketNumbers[i]}|{(cancelledTickets.Contains(ticketNumbers[i]) ? "CANCELLED" : "Active")}")
                .ToList();
            File.WriteAllLines(passengerFile, lines);
        }
        static void RegisterPassenger()
        {
            try
            {
                Console.Write("Enter passenger full name: ");
                string name = Console.ReadLine()?.Trim();

                // Input validation
                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Error: Name cannot be empty.");
                    return;
                }
                else if (passengerNames.Contains(name, StringComparer.OrdinalIgnoreCase))
                {
                    // StringComparer.OrdinalIgnoreCase → ignores uppercase/lowercase differences
                    Console.WriteLine("Error: Passenger already exists.");
                    return;
                }
                else
                {
                    //  Generate ticket ID safely
                    string ticketId = "TKT-" + (passengerNames.Count + 1).ToString("D3");
                    // passengerNames.Count → current number of passengers
                    // +1 → ensures next sequential number
                    // ToString("D3") → pads with zeros (001, 002, etc.)

                    //  Add to lists
                    passengerNames.Add(name);
                    ticketNumbers.Add(ticketId);

                    //  File saving with error handling
                    try
                    {
                        using (StreamWriter writer = new StreamWriter(passengerFile, true))
                        {
                            writer.WriteLine($"{name}|{ticketId}|Active");
                        }
                    }
                    catch (IOException ioEx)
                    {
                        Console.WriteLine("File error while saving passenger: " + ioEx.Message);
                        return;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Unexpected error while saving passenger: " + ex.Message);
                        return;
                    }

                    //  Confirmation message
                    Console.WriteLine("*************************************");
                    Console.WriteLine("Passenger Registered Successfully!");
                    Console.WriteLine("Name: " + name);
                    Console.WriteLine("Ticket ID: " + ticketId);
                    Console.WriteLine("*************************************");
                }
            }
            catch (Exception ex)
            {
                //  Catch any unexpected errors in the whole method
                Console.WriteLine("Unexpected error occurred: " + ex.Message);
            }
        }

        //{
        //    Console.Write("Enter passenger full name: ");
        //    string name = Console.ReadLine().Trim();

        //    if (string.IsNullOrEmpty(name))
        //    {
        //        Console.WriteLine("Error: Name cannot be empty.");
        //    }
        //    else if (passengerNames.Contains(name, StringComparer.OrdinalIgnoreCase)) //StringComparer.OrdinalIgnoreCase : A comparer that ignores uppercase/lowercase differences.
        //    {
        //        Console.WriteLine("Error: Passenger already exists.");
        //    }
        //    else
        //    {
        //        string ticketId = "TKT-" + (passengerNames.Count + 1).ToString("D3");//passengerNames.Count → gives the current number of passengers in the list.
        //        passengerNames.Add(name); // add to list                                           //+ 1 → ensures the new passenger gets the next sequential number.
        //        ticketNumbers.Add(ticketId);                                         //Converts the number into a string with 3 digits, padded with zeros.
        //        using (StreamWriter writer = new StreamWriter(passengerFile, true))
        //        {
        //            writer.WriteLine($"{name}|{ticketId}|Active");
        //        }
        //        Console.WriteLine("*************************************");
        //        Console.WriteLine("Passenger Registered Successfully!");
        //        Console.WriteLine("Name: " + name);
        //        Console.WriteLine("Ticket ID: " + ticketId);
        //        Console.WriteLine("*************************************");

        //    }
        

        static void ViewPassengers()
        {
            if (passengerNames.Count == 0)// Check if no passengers are registered.
            {
                Console.WriteLine("No passengers registered yet.");
                return;
            }

            Console.WriteLine("No. |            Passenger Name |           Ticket ID            | Status");
            Console.WriteLine("-------------------------------------------------------------------------------");
            var passengerList = passengerNames
                .Select((name, i) => new {
                    No = i + 1,
                    Name = name,
                    Ticket = ticketNumbers[i],
                    Status = cancelledTickets.Contains(ticketNumbers[i]) ? "CANCELLED" : "Active"
                });

            foreach (var p in passengerList)
            {
                Console.WriteLine($"{p.No} | {p.Name.PadRight(22)} | {p.Ticket.PadRight(22)} | {p.Status.PadRight(22)}");
            }

            //for (int i = 0; i < passengerNames.Count; i++) //int i = 0 → loop counter starts at 0.i < passengerNames.Count → keep looping until i reaches the number of passengers.i++ → increase i by 1 each time.
            //{
            //    string name = passengerNames[i];//Gets the passenger’s name at position i.
            //    string ticket = ticketNumbers[i];
            //    string status;

            //    if (cancelledTickets.Contains(ticket)) // chek if it cancelled 
            //    {
            //        status = "CANCELLED";
            //    }
            //    else
            //    {
            //        status = "Active";
            //    }

            //Console.WriteLine((i + 1)  +  "    |      " +(name.PadRight(22)+    "   |   "  +(ticket.PadRight(22))+"   |  "+( status.PadRight(22))) );
            //    // Print row:
                // (i+1) → passenger number (starts at 1).
                // name.Trim() → clean spaces.
                // ticket.Trim() → clean spaces.
                // status → Active or Cancelled.
            //}

            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine("Total passengers: " +passengerNames.Count);
        }

        static string bookingFile = "bookings.txt";
        static void LoadBookingsFromFile()
        {
            bookingRecord.Clear();

            if (File.Exists(bookingFile))
            {
                var lines = File.ReadAllLines(bookingFile);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 3)
                    {
                        bookingRecord[parts[0]] = $"{parts[1]}|{parts[2]}"; // TicketID|Flight|Date
                    }
                }
            }
        }

        static void SaveBookingsToFile()
        {
            var lines = bookingRecord
                .Select(b => $"{b.Key}|{b.Value.Split('|')[0]}|{b.Value.Split('|')[1]}")
                .ToList();

            File.WriteAllLines(bookingFile, lines);
        }

        static void BookFlightTicket()
        {
            Console.Write("Enter Ticket ID: ");
            string ticketId = Console.ReadLine().Trim();
            if (ticketNumbers.Contains(ticketId) == false)
            {
                Console.WriteLine("Error: Ticket ID not found.");
                return;
            }
            if (cancelledTickets.Contains(ticketId))
            {
                Console.WriteLine("Error: This ticket has been cancelled.");
                return;
            }

            if (bookingRecord.ContainsKey(ticketId))
            {
                Console.WriteLine("Error: This ticket already has a booking. Use Update Booking instead.");
                return;
            }
            // === Show available flights ===
            Console.WriteLine("Available Flights:");
            for (int i = 0; i < flightNumbers.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {flightNumbers[i]}");
            }


            //Console.WriteLine("Available Flights:");
            //int counter = 1;
            //foreach (string flight in flightNumbers)
            //{
            //    Console.WriteLine($"{counter} . {flight}");
            //    counter++;
            //}
            //for (int i = 0; i < flightNumbers.Length; i++)  
            //{
            //    Console.WriteLine($"{i + 1} . {flightNumbers[i]}"); //i + 1 → لأن الفهرس يبدأ من 0، نضيف 1 حتى يظهر للمستخدم رقم تسلسلي يبدأ من
            //}

            Console.Write("Select flight (enter number): ");
            if (!int.TryParse(Console.ReadLine(), out int flightChoice) || flightChoice < 1 || flightChoice > flightNumbers.Length) //الشرط يتحقق إذا -الإدخال ليس رقمًا -أو الرقم أصغر من 1 -أو الرقم أكبر من عدد الرحلات المتاحة 
            {
                Console.WriteLine("Invalid flight selection.");
                return;
            }
            string selectedFlight = flightNumbers[flightChoice - 1]; // مثال: إذا المستخدم كتب 2 → نطرح 1 → الفهرس = 1 → الرحلة = OA102.  لان المستخدم يختار رقم من 1 والفهرس يبدأ من 0

            Console.WriteLine("Available Dates:");
            for (int i = 0; i < availableDates.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableDates[i]}");
            }

            Console.Write("Select date (enter number): ");
            if (!int.TryParse(Console.ReadLine(), out int dateChoice) || dateChoice < 1 || dateChoice > availableDates.Count)
            {
                Console.WriteLine("Invalid date selection.");
                return;
            }
            string selectedDate = availableDates[dateChoice - 1];  // 

            // تخزين الحجز في Dictionary
            bookingRecord[ticketId] = $"{selectedFlight}|{selectedDate}";
            SaveBookingsToFile();
            // جلب اسم الراكب من نفس الفهرس
            int passengerIndex = ticketNumbers.IndexOf(ticketId);
            string passengerName = passengerNames[passengerIndex];

            // رسالة تأكيد
            Console.WriteLine("*************************************");
            Console.WriteLine("Booking Confirmed!");
            Console.WriteLine($"Passenger: {passengerName}");
            Console.WriteLine($"Ticket ID: {ticketId}");
            Console.WriteLine($"Flight: {selectedFlight}");
            Console.WriteLine($"Date: {selectedDate}");
            Console.WriteLine("*************************************");
        }

        static void ViewBookingDetails()
        {
            LoadBookingsFromFile(); // refresh from file
            Console.Write("Enter Ticket ID: ");
            string ticketId = Console.ReadLine().Trim();

            // التحقق من أن التذكرة موجودة
            if (!ticketNumbers.Contains(ticketId))
            {
                Console.WriteLine("Error: Ticket ID not found.");
            }
            else if (cancelledTickets.Contains(ticketId))
            {
                Console.WriteLine("Error: This ticket has been cancelled.");
            }
            else if (!bookingRecord.ContainsKey(ticketId))
            {
                Console.WriteLine("Error: No booking found for this ticket.");
            }
            else
            {

                //  LINQ query to extract booking info
                var booking = bookingRecord
                    .Where(b => b.Key == ticketId)
                    .Select(b => new
                    {
                        Ticket = b.Key,
                        Flight = b.Value.Split('|')[0],
                        Date = b.Value.Split('|')[1],
                        Passenger = passengerNames[ticketNumbers.IndexOf(b.Key)]
                    })
                    .FirstOrDefault();
                //// جلب بيانات الحجز من الـ Dictionary
                //string bookingInfo = bookingRecord[ticketId];
                //string[] parts = bookingInfo.Split('|');
                //string flight = parts[0];
                //string date = parts[1];

                //// جلب اسم الراكب من نفس الفهرس
                int passengerIndex = ticketNumbers.IndexOf(ticketId);
                string passengerName = passengerNames[passengerIndex];

                // عرض التفاصيل
                Console.WriteLine("*************************************");
                Console.WriteLine("Booking Details:");
                Console.WriteLine($"Passenger: {booking.Passenger}");
                Console.WriteLine($"Ticket ID: {booking.Ticket}");
                Console.WriteLine($"Flight: {booking.Flight}");
                Console.WriteLine($"Date: {booking.Date}");
                Console.WriteLine("*************************************");
            }
        }

        static void UpdateBooking()
        {
            LoadBookingsFromFile(); // refresh from file
            Console.Write("Enter Ticket ID to update: ");
            string ticketId = Console.ReadLine().Trim();
            if (!ticketNumbers.Contains(ticketId))
            {
                Console.WriteLine("Error: Ticket ID not found.");
                return;
            }

            if (cancelledTickets.Contains(ticketId))
            {
                Console.WriteLine("Error: This ticket has been cancelled.");
                return;
            }
            if (!bookingRecord.ContainsKey(ticketId))
            {
                Console.WriteLine("Error: No booking found for this ticket.");
                return;
            }
            Console.WriteLine("Available Flights:");// عرض الرحلات المتاحة
            for (int i = 0; i < flightNumbers.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {flightNumbers[i]}");
            }

            Console.Write("Select new flight (enter number): ");
            if (!int.TryParse(Console.ReadLine(), out int flightChoice) || flightChoice < 1 || flightChoice > flightNumbers.Length)
            {
                Console.WriteLine("Invalid flight selection.");
                return;
            }
            string newFlight = flightNumbers[flightChoice - 1];
            Console.WriteLine("Available Dates:");
            for (int i = 0; i < availableDates.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableDates[i]}");
            }

            Console.Write("Select new date (enter number): ");
            if (!int.TryParse(Console.ReadLine(), out int dateChoice) || dateChoice < 1 || dateChoice > availableDates.Count)
            {
                Console.WriteLine("Invalid date selection.");
                return;
            }
            string newDate = availableDates[dateChoice - 1];
            bookingRecord[ticketId] = $"{newFlight}|{newDate}";

            SaveBookingsToFile();
            int passengerIndex = ticketNumbers.IndexOf(ticketId);
            string passengerName = passengerNames[passengerIndex];
            Console.WriteLine("*************************************");
            Console.WriteLine("Booking Updated!");
            Console.WriteLine($"Passenger: {passengerName}");
            Console.WriteLine($"Ticket ID: {ticketId}");
            Console.WriteLine($"New Flight: {newFlight}");
            Console.WriteLine($"New Date: {newDate}");
            Console.WriteLine("*************************************");
        }

        static void CancelBooking()
        {
            Console.Write("Enter Ticket ID to cancel: ");
            string ticketId = Console.ReadLine().Trim();

            if (!ticketNumbers.Contains(ticketId))
            {
                Console.WriteLine("Error: Ticket ID not found."); // if its there 
                return;
            }

            if (cancelledTickets.Contains(ticketId)) // if it alrady cancelled 
            {
                Console.WriteLine("Error: This ticket is already cancelled.");
                return;
            }

            if (!bookingRecord.ContainsKey(ticketId))
            {
                Console.WriteLine("Error: No booking found for this ticket."); // 
                return;
            }

            cancelledTickets.Add(ticketId);// add to cancelled tickt 
            bookingRecord.Remove(ticketId); // delet from Dictionary
            int passengerIndex = ticketNumbers.IndexOf(ticketId);// name of passenger 
            string passengerName = passengerNames[passengerIndex];
            Console.WriteLine("*************************************");
            Console.WriteLine("Booking Cancelled!");
            Console.WriteLine($"Passenger: {passengerName}");
            Console.WriteLine($"Ticket ID: {ticketId}");
            Console.WriteLine("*************************************");
        }

        static void PassengerCheckIn()
        {
            Console.Write("Enter Ticket ID for check-in: ");
            string ticketId = Console.ReadLine().Trim();
            if (!ticketNumbers.Contains(ticketId))
            {
                Console.WriteLine("Error: Ticket ID not found.");
                return;
            }
            if (cancelledTickets.Contains(ticketId))
            {
                Console.WriteLine("Error: This ticket has been cancelled.");
                return;
            }
            if (!bookingRecord.ContainsKey(ticketId))
            {
                Console.WriteLine("Error: No booking found for this ticket.");
                return;
            }
            if (checkedInQueue.Contains(ticketId))
            {
                Console.WriteLine("Error: Passenger already checked in.");
                return;
            }
            if (checkedInQueue.Count >= 10)
            {
                waitlistQueue.Enqueue(ticketId); // إضافة إلى قائمة الانتظار
                Console.WriteLine("Check-In queue is full. Passenger added to waitlist.");
                return;
            }
            checkedInQueue.Enqueue(ticketId);// إضافة الراكب إلى الطابور
            int passengerIndex = ticketNumbers.IndexOf(ticketId);
            string passengerName = passengerNames[passengerIndex];
            Console.WriteLine("*************************************");
            Console.WriteLine("Check-In Successful!");
            Console.WriteLine($"Passenger: {passengerName}");
            Console.WriteLine($"Ticket ID: {ticketId}");
            Console.WriteLine("*************************************");
            Console.WriteLine("\nCurrent Check-In Queue:");// IN LINE 
            foreach (string id in checkedInQueue)
            {
                int index = ticketNumbers.IndexOf(id);
                Console.WriteLine($"- {passengerNames[index]} (Ticket: {id})");
            }
        }

        static void BoardPassengers()
        {
            bool exit = false;
            int currentRow = 10;       // يبدأ من الصف 10
            char currentSeat = 'A';    // يبدأ من المقعد A

            while (exit == false)
            {
                Console.WriteLine("\nBoarding Menu:");
                Console.WriteLine("1. Load boarding stack from check-in queue");
                Console.WriteLine("2. Board next passenger");
                Console.WriteLine("3. View boarding stack");
                Console.WriteLine("4. View boarding log");
                Console.WriteLine("0. Back");
                Console.Write("Choose option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": // Load boarding stack
                        if (checkedInQueue.Count == 0)
                        {
                            Console.WriteLine("No passengers in check-in queue.");
                        }
                        else if (boardingStack.Count > 0)
                        {
                            Console.WriteLine("Boarding stack already loaded. Cannot load again.");
                        }
                        else
                        {
                            int loadedCount = 0;
                            while (checkedInQueue.Count > 0)
                            {
                                string ticketId = checkedInQueue.Dequeue();
                                boardingStack.Push(ticketId);
                                loadedCount++;
                            }
                            Console.WriteLine($"Loaded {loadedCount} passengers into boarding stack.");
                        }
                        break;

                    case "2": // Board next passenger
                        if (boardingStack.Count == 0)
                        {
                            Console.WriteLine("No passengers in boarding stack.");
                        }
                        else
                        {
                            string ticketId = boardingStack.Pop();
                            int passengerIndex = ticketNumbers.IndexOf(ticketId);
                            string passengerName = passengerNames[passengerIndex];

                            // تعيين المقعد
                            string seat = $"{currentRow}{currentSeat}";
                            passengerSeatMap[ticketId] = seat;

                            // تحديث المقعد التالي
                            if (currentSeat == 'F')
                            {
                                currentSeat = 'A';
                                currentRow++;
                            }
                            else
                            {
                                currentSeat++;
                            }

                            Console.WriteLine($"Boarded: {passengerName} (Ticket: {ticketId}) → Seat {seat}");
                        }
                        break;

                    case "3": // View boarding stack
                        if (boardingStack.Count == 0)
                        {
                            Console.WriteLine("Boarding stack is empty.");
                        }
                        else
                        {
                            Console.WriteLine("Current Boarding Stack (Top → Bottom):");
                            int pos = 1;
                            foreach (string ticketId in boardingStack)
                            {
                                int passengerIndex = ticketNumbers.IndexOf(ticketId);
                                Console.WriteLine($"{pos}. {passengerNames[passengerIndex]} (Ticket: {ticketId})");
                                pos++;
                            }
                        }
                        break;

                    case "4": // View boarding log
                        if (passengerSeatMap.Count == 0)
                        {
                            Console.WriteLine("No passengers boarded yet.");
                        }
                        else
                        {
                            Console.WriteLine("Boarding Log:");
                            foreach (var entry in passengerSeatMap)
                            {
                                int passengerIndex = ticketNumbers.IndexOf(entry.Key);
                                Console.WriteLine($"{passengerNames[passengerIndex]} → Seat {entry.Value}");
                            }
                        }
                        break;

                    case "0": // Back
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
                        Console.WriteLine("Register New Passenger");
                        RegisterPassenger();
                        break;

                    case "2":

                        Console.WriteLine("Case 02 - View All Passengers");
                        ViewPassengers();
                        break;

                    case "3":
                        Console.WriteLine("Case 03 - Book a Flight Ticket");
                        BookFlightTicket();
                        break;

                    case "4":
                        Console.WriteLine("Case 04 - View Booking Details");
                        ViewBookingDetails();
                        break;

                    case "5":
                        Console.WriteLine("Case 05 - Update a Booking");
                        UpdateBooking();
                        break;

                    case "6":
                        Console.WriteLine("Case 06 - Cancel a Ticket");
                        CancelBooking();
                        break;

                    case "7":
                        Console.WriteLine("Case 07 - Passenger Check-In");
                        PassengerCheckIn();
                        break;

                    case "8":
                        Console.WriteLine("Case 08 - Board Passengers");
                        BoardPassengers();
                        break;

                    case "9":
                        Console.WriteLine("Case 09 - Generate Flight Manifest");
                        break;

                    case "10":
                        Console.WriteLine("Case 10 - Manage Waitlist & Seat Assignment");
                        break;

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

