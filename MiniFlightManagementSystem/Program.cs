using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FlightManagementSystem
{
    internal class Program
    {
        static List<string> passengerNames = new List<string> { "Ali", "Sara", "Omar", "Fatima", "Hassan" };
        static List<string> ticketNumbers = new List<string> { "TKT-001", "TKT-002", "TKT-003", "TKT-004", "TKT-005" };
        static string[] flightNumbers = { "OA101", "OA102", "OA103", "OA104", "OA105", "OA106" };
        static List<string> availableDates = new List<string> { "12-Jan-2026", "15-Jan-2026", "20-Jan-2026", "25-Jan-2026" };
        static Dictionary<string, string> bookingRecord = new Dictionary<string, string>();
        static Queue<string> checkedInQueue = new Queue<string>();
        static Stack<string> boardingStack = new Stack<string>();
        static List<string> cancelledTickets = new List<string>();
        static Dictionary<string, string> passengerSeatMap = new Dictionary<string, string>();
        static Queue<string> waitlistQueue = new Queue<string>();

     
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

        static void RegisterPassenger()
        {
            Console.Write("Enter passenger full name: ");
            string name = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(name))
      {
           Console.WriteLine("Error: Name cannot be empty.");
       }
         else if (passengerNames.Contains(name, StringComparer.OrdinalIgnoreCase))
       {
          Console.WriteLine("Error: Passenger already exists.");
        }
         else
         {
                string ticketId = "TKT-" + (passengerNames.Count + 1).ToString("D3");
                passengerNames.Add(name);
                ticketNumbers.Add(ticketId);
                Console.WriteLine("*************************************");
                Console.WriteLine("Passenger Registered Successfully!");
                Console.WriteLine("Name: "+name);
                Console.WriteLine("Ticket ID: "+ticketId);
                Console.WriteLine("*************************************");
           
        }
        }

        static void ViewPassengers()
        {
            if (passengerNames.Count == 0)
            {
                Console.WriteLine("No passengers registered yet.");
                return;
            }

            Console.WriteLine("No. | Passenger Name | Ticket ID | Status");
            Console.WriteLine("------------------------------------------");

            for (int i = 0; i < passengerNames.Count; i++)
            {
                string name = passengerNames[i];
                string ticket = ticketNumbers[i];
                string status;

                if (cancelledTickets.Contains(ticket))
                {
                    status = "CANCELLED";
                }
                else
                {
                    status = "Active";
                }

                Console.WriteLine((i + 1)  +  "    |      " +(name.Trim())+    "   |   "  +(ticket.Trim())+"   |  "+( status) );
            }

            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Total passengers: " +passengerNames.Count);
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

            Console.WriteLine("Available Flights:");
            int counter = 1;
            foreach (string flight in flightNumbers)
            {
                Console.WriteLine($"{counter} . {flight}");
                counter++;
            }
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
                // جلب بيانات الحجز من الـ Dictionary
                string bookingInfo = bookingRecord[ticketId];
                string[] parts = bookingInfo.Split('|');
                string flight = parts[0];
                string date = parts[1];

                // جلب اسم الراكب من نفس الفهرس
                int passengerIndex = ticketNumbers.IndexOf(ticketId);
                string passengerName = passengerNames[passengerIndex];

                // عرض التفاصيل
                Console.WriteLine("*************************************");
                Console.WriteLine("Booking Details:");
                Console.WriteLine($"Passenger: {passengerName}");
                Console.WriteLine($"Ticket ID: {ticketId}");
                Console.WriteLine($"Flight: {flight}");
                Console.WriteLine($"Date: {date}");
                Console.WriteLine("*************************************");
            }
        }

        static void UpdateBooking()
        {
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

