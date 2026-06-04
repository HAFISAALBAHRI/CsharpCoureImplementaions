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

        public static void HotelCheckInQueue()
        {
            Queue<string> checkInQueue = new Queue<string>();
            checkInQueue.Enqueue("Ali");
            checkInQueue.Enqueue("Sara");
            checkInQueue.Enqueue("Omar");
            checkInQueue.Enqueue("Fatima");
            checkInQueue.Enqueue("Hassan");

            Console.WriteLine("=== Waiting Guests ===");
            foreach (string guest in checkInQueue)
                Console.WriteLine(guest);

            Console.WriteLine("Next Guest (Peek): " + checkInQueue.Peek());

            Console.WriteLine("Serving: " + checkInQueue.Dequeue());
            Console.WriteLine("Serving: " + checkInQueue.Dequeue());

            Console.WriteLine("=== Remaining Queue ===");
            foreach (string guest in checkInQueue)
                Console.WriteLine(guest);

            string checkGuest = "Sara";
            if (checkInQueue.Contains(checkGuest))

                Console.WriteLine("Contains " + checkGuest);
            else
                Console.WriteLine("not Contains " + checkGuest);
           
            Console.WriteLine("Total Guests: " + checkInQueue.Count);
        }

        public static void TextEditorUndoSystem()
        {
            Stack<string> undoStack = new Stack<string>();
            Stack<string> tempStack = new Stack<string>();
            undoStack.Push("Typed: Hello");
            undoStack.Push("Typed: World");
            undoStack.Push("Deleted: o");
            undoStack.Push("Typed: name");
            undoStack.Push("Bold: Hello");
            undoStack.Push("Italic: World");
            undoStack.Push("Undo Bold");
            Console.WriteLine("=== Undo History ===");
            foreach (string act in undoStack)
                Console.WriteLine(act);

            Console.WriteLine("Next Undo (Peek): " + undoStack.Peek()); // Peek is like “looking at the top card of a deck” without taking it out.

            Console.WriteLine("Undo: " + undoStack.Pop());
            Console.WriteLine("Undo: " + undoStack.Pop());

            Console.WriteLine("=== Remaining Undo History ===");
            foreach (string act in undoStack) 
            Console.WriteLine(act);

            string target = "Bold: Hello";  // select target 
            Console.WriteLine("Selective Undo: Removing " + target);

            while (undoStack.Count > 0) // loop untell finsh 
            {
                string current = undoStack.Pop();   // removes the top action from the undo stack and current holds that action.
                if (current != target) tempStack.Push(current); //If the action is not the target it push it into tempStack but If it is the target we skip
            }
            while (tempStack.Count > 0)
                undoStack.Push(tempStack.Pop());  // We pop everything from tempStack back into undoStack in the same order but without target

            Console.WriteLine("=== After Selective Undo ===");
            foreach (string act in undoStack)
            Console.WriteLine(act);

            Console.WriteLine("Final Count: " + undoStack.Count);
        }

        public static void HospitalTriage()
        {
            Queue<string> triageQueue = new Queue<string>();
            Queue<string> tempQueue = new Queue<string>();

            triageQueue.Enqueue("Ali");
            triageQueue.Enqueue("Sara");
            triageQueue.Enqueue("Omar");
            triageQueue.Enqueue("Fatima");
            triageQueue.Enqueue("Hassan");
            triageQueue.Enqueue("Layla");
            triageQueue.Enqueue("Nasser");
            triageQueue.Enqueue("Aisha");

            Console.WriteLine("=== Full Queue ===");
            int pos = 1;
            foreach (string p in triageQueue)
                Console.WriteLine("Position " + pos++ + ": " + p);

            Console.WriteLine("Next Patient (Peek): " + triageQueue.Peek());

            Console.WriteLine("Processing: " + triageQueue.Dequeue());
            Console.WriteLine("Processing: " + triageQueue.Dequeue());
            Console.WriteLine("Processing: " + triageQueue.Dequeue());

            Console.WriteLine("=== Remaining Queue ===");
            foreach (string p in triageQueue) Console.WriteLine(p);

            string target = "Hassan";
            Console.WriteLine("Removing patient: " + target);

            while (triageQueue.Count > 0)
            {
                string current = triageQueue.Dequeue();
                if (current != target) tempQueue.Enqueue(current);
            }
            while (tempQueue.Count > 0)
                triageQueue.Enqueue(tempQueue.Dequeue());

            Console.WriteLine("=== Final Queue ===");
            foreach (string p in triageQueue) Console.WriteLine(p);
            Console.WriteLine("Final Count: " + triageQueue.Count);
        }

        public static void ParenthesisValidator()
        {
            string test1 = "(a+[b*c]-{d/e})";
            string test2 = "(a+[b*c}-{d/e})";
            string test3 = "(a+[b*c]-{d/e)";
            string[] tests = { test1, test2, test3 };

            foreach (string test in tests)
            {
                Stack<char> bracketStack = new Stack<char>();
                bool valid = true;

                for (int i = 0; i < test.Length; i++)
                {
                    char ch = test[i];

                    if (ch == '(' || ch == '[' || ch == '{')
                    {
                        bracketStack.Push(ch);
                    }

                    else if (ch == ')' || ch == ']' || ch == '}')
                    {
                        if (bracketStack.Count == 0)
                        {
                            valid = false;
                            break;
                        }

                        char top = bracketStack.Peek();


                        if (ch == ')')
                        {
                            if (top == '(') bracketStack.Pop();
                            else { valid = false; break; }
                        }
                        else if (ch == ']')
                        {
                            if (top == '[') bracketStack.Pop();
                            else { valid = false; break; }
                        }
                        else if (ch == '}')
                        {
                            if (top == '{') bracketStack.Pop();
                            else { valid = false; break; }
                        }
                    }
                }


                if (bracketStack.Count > 0) valid = false;


                if (valid)
                    Console.WriteLine("Test: " + test + " => Valid");
                else
                    Console.WriteLine("Test: " + test + " => Invalid");
            }
        }
     

        public static void ReverseSentence()
        {
            string sentence1 = "C# is fun to learn";
            Stack<string> wordStack1 = new Stack<string>();
            wordStack1.Push("C#");
            wordStack1.Push("is");
            wordStack1.Push("fun");
            wordStack1.Push("to");
            wordStack1.Push("learn");

            Console.WriteLine("Original: " + sentence1);
            Console.WriteLine("Stack contents:");
            foreach (string w in wordStack1) Console.WriteLine(w);
            string reversed1 = "";
            while (wordStack1.Count > 0)
            {
                string word = wordStack1.Pop();   // take the top word Using a variable 
                reversed1 += word + " ";          // add it to the reversed sentence
            }
            Console.WriteLine("Reversed: " + reversed1.Trim());
            Console.WriteLine("-----------------------------");

            string sentence2 = "Stack and Queue are useful";
            Stack<string> wordStack2 = new Stack<string>();
            wordStack2.Push("Stack");
            wordStack2.Push("and");
            wordStack2.Push("Queue");
            wordStack2.Push("are");
            wordStack2.Push("useful");

            Console.WriteLine("Original: " + sentence2);
            Console.WriteLine("Stack contents:");
            foreach (string w in wordStack2) Console.WriteLine(w);

            string reversed2 = "";
            while (wordStack2.Count > 0) 

                reversed2 += wordStack2.Pop() + " "; //Direct append

            Console.WriteLine("Reversed: " + reversed2.Trim());
            Console.WriteLine("-----------------------------");
        }

        public static void TicketCounter()
        {
            Queue<string> regularQueue = new Queue<string>();
            Queue<string> vipQueue = new Queue<string>();

            regularQueue.Enqueue("R001");
            regularQueue.Enqueue("R002");
            regularQueue.Enqueue("R003");
            regularQueue.Enqueue("R004");
            regularQueue.Enqueue("R005");
            vipQueue.Enqueue("V001");
            vipQueue.Enqueue("V002");
            vipQueue.Enqueue("V003");

            Console.WriteLine("=== Regular Queue ===");
            foreach (string t in regularQueue) Console.WriteLine(t);
            Console.WriteLine("=== VIP Queue ===");
            foreach (string t in vipQueue) Console.WriteLine(t);

            int served = 0;
            //while (vipQueue.Count > 0)
            //{
            //    Console.WriteLine("Serving VIP: " + vipQueue.Dequeue());
            //    served++;
            //}

            //while (regularQueue.Count > 0)
            //{
            //    Console.WriteLine("Serving Regular: " + regularQueue.Dequeue());
            //    served++;
            //}

            while (regularQueue.Count > 0 || vipQueue.Count > 0)
            {
                if (vipQueue.Count > 0)
                {
                    Console.WriteLine("Serving VIP: " + vipQueue.Dequeue());
                    served++;
                }
                if (regularQueue.Count > 0)
                {
                    Console.WriteLine("Serving Regular: " + regularQueue.Dequeue());
                    served++;
                }
            }
            Console.WriteLine("Total Tickets Served: " + served);
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
                    case "2":
                        HotelCheckInQueue();
                        break;
                    case "3":
                        TextEditorUndoSystem();
                        break;
                    case "4":
                        HospitalTriage();
                        break;
                    case "5":
                        ParenthesisValidator();
                        break;
                    //case "6":
                    //    PrintSpooler();
                    //    break;
                    case "7":
                        ReverseSentence();
                        break;
                    //case "8": 
                    //    MultiLevelUndoRedo();
                    //    break;
                    case "9":
                        TicketCounter();
                        break;
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
    




