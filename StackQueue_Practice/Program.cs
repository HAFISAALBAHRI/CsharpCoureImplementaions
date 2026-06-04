using System;
using System.Collections.Generic;

namespace StackQueue_Practice
{
    internal class Program
    {
        static void ShowMenu()
        {
            Console.WriteLine("************************************");
            Console.WriteLine("=== Stack & Queue Practice Menu ===");
            Console.WriteLine("************************************");
            Console.WriteLine("1 - Browser History Tracker (Stack)");
            Console.WriteLine("2 - Hotel Check-In Queue (Queue)");
            Console.WriteLine("3 - Text Editor Undo System (Stack)");
            Console.WriteLine("4 - Hospital Emergency Room Triage (Queue)");
            Console.WriteLine("5 - Parenthesis Validator (Stack)");
            Console.WriteLine("6 - Print Spooler with Priority Re-Insertion (Queue)");
            Console.WriteLine("7 - Reverse a Sentence Word by Word (Stack)");
            Console.WriteLine("8 - Multi-Level Undo with Redo (Stack)");
            Console.WriteLine("9 - Ticket Counter Simulation (Queue)");
            Console.WriteLine("10 - Order Processing Pipeline with Statistics (Queue)");
            Console.WriteLine("0 - Exit");
        }

        public static void BrowserHistoryTracker()
        {
            Stack<string> browserHistory = new Stack<string>();
            browserHistory.Push("home.html");
            browserHistory.Push("about.html");
            browserHistory.Push("products.html");
            browserHistory.Push("contact.html");
            browserHistory.Push("questions.html");

            Console.WriteLine("=== Browser History ===");
            foreach (string page in browserHistory)
                Console.WriteLine(page);

            Console.WriteLine("Current Page (Peek): " + browserHistory.Peek());

            Console.WriteLine("Back pressed: " + browserHistory.Pop());
            Console.WriteLine("Back pressed: " + browserHistory.Pop());

            Console.WriteLine("=== Remaining History ===");
            foreach (string page in browserHistory)
                Console.WriteLine(page);

            string checkUrl = "about.html";
            if (browserHistory.Contains(checkUrl))
   
                Console.WriteLine(checkUrl + " is still in history.");
            else
                Console.WriteLine(checkUrl + " is not in history.");
            Console.WriteLine("Total Pages: " + browserHistory.Count);
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
                        BrowserHistoryTracker(); 
                        break;
                    //case "2":
                    //    HotelCheckInQueue(); 
                    //    break;
                    //case "3":
                    //    TextEditorUndoSystem();
                    //    break;
                    //case "4": 
                    //    HospitalTriage(); 
                    //    break;
                    //case "5":
                    //    ParenthesisValidator();
                    //    break;
                    //case "6":
                    //    PrintSpooler();
                    //    break;
                    //case "7":
                    //    ReverseSentence(); 
                    //    break;
                    //case "8": 
                    //    MultiLevelUndoRedo();
                    //    break;
                    //case "9": 
                    //    TicketCounter(); 
                    //    break;
                    //case "10":
                    //    OrderProcessing();
                    //    break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice."); 
                        break;
                }

                if (!exit)
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
    




