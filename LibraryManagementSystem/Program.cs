using Microsoft.VisualBasic.FileIO;

namespace LibraryManagementSystem
{
    internal class Program
    {
        static string MemberName = "";
        static string MemberID = "";
        static string MemberEmail = "";
        static string MembershipExpiry = "";
        static string MemberTier = ""; // "STANDARD" or "PREMIUM"
        static bool MemberRegistered = false;
        static string BookTitle = "";
        static string BookAuthor = "";
        static string BookGenre = "";
        static DateTime RegistrationDate;
        static DateTime BorrowDate ;
        static DateTime ReturnDate;
        static int BookAvailableCopies = 0;
        static bool BookRegistered = false;
        static int TotalBooksBorrowedThisSession = 0;
        static double TotalFinesPaidThisSession = 0.0;

        // Prints the main menu (Case 0 requirement: void, no parameters)
        public static void ShowMenu()
        {
            Console.WriteLine("===**** City Public Library Menu ****===");
            Console.WriteLine();
            Console.WriteLine("0  - Register Member");
            Console.WriteLine("1  - Display Member Profile");
            Console.WriteLine("2  - Search Book by Title");
            Console.WriteLine("3  - Borrow a Book");
            Console.WriteLine("4  - Return a Book");
            Console.WriteLine("5  - Calculate Late Fine");
            Console.WriteLine("6  - Apply Member Discount");
            Console.WriteLine("7  - Check Borrowing Eligibility");
            Console.WriteLine("8  - Register Book");
            Console.WriteLine("9  - Generate Member ID");
            Console.WriteLine("10 - Display Book Details");
            Console.WriteLine("11 - Calculate Renewal Fee");
            Console.WriteLine("12 - Update Member Email");
            Console.WriteLine("13 - Session Summary");
            Console.WriteLine("14 - Member Statistics (bonus)");
            Console.WriteLine("15 - Exit");
        }

        public static void AddMemberInformation()
        {
            if (MemberRegistered == false)
            {
                Console.WriteLine("Enter member name: ");
                MemberName = Console.ReadLine();
                Console.WriteLine("Enter member ID:");
                MemberID = Console.ReadLine();
                Console.WriteLine("Enter member email: ");
                MemberEmail = Console.ReadLine();
                Console.WriteLine("Enter membership expiry date (dd/mm/yyyy) :");
                MembershipExpiry = Console.ReadLine();
                Console.WriteLine("Enter membership tier (Standard/Premium):");
                MemberTier = Console.ReadLine();
               // MemberID = MemberName.ToUpper().Substring(0, 2) + "-" + MemberID; // so the ID will be xx-ID number 
                RegistrationDate = DateTime.Now;
                Console.WriteLine("Registration completed at: " + RegistrationDate.ToString("yyyy-MM-dd"));
                MemberRegistered = true;
                Console.WriteLine("Member information added successfully. Member ID: " + MemberID);
            }
            else
            {
                Console.WriteLine("Member information already exists. Please edit if you want to change it.");
            }
        }

        public static void MemberProfile()
        {
            if (MemberRegistered == true)
            {
                Console.WriteLine("=== Member Profile ===");
                Console.WriteLine("Name:".PadLeft(12) + " " + MemberName);
                Console.WriteLine("ID:".PadLeft(12) + " " + MemberID);
                Console.WriteLine("Email:".PadLeft(12) + " " + MemberEmail);
                Console.WriteLine("Expiry:".PadLeft(12) + " " + MembershipExpiry);
                Console.WriteLine("Tier:".PadLeft(12) + " " + MemberTier);
                Console.WriteLine("Registered:".PadLeft(12) + " " + Convert.ToString(MemberRegistered));
            }
            else
            {
                Console.WriteLine(" NO Profilen , Plaes Register First!!");
            }
        }

        public static bool SearchBookByTitle(string title)
        {
            if (BookRegistered && BookTitle.ToLower() == title.ToLower())
            {
                Console.WriteLine("Book found!");
                Console.WriteLine("Title: " + BookTitle);
                Console.WriteLine("Author: " + BookAuthor);
                Console.WriteLine("Genre: " + BookGenre);
                Console.WriteLine("Available Copies: " + Convert.ToString(BookAvailableCopies));
                return true;
            }
            else
            {
                Console.WriteLine("Book not found.");
                return false;
            }
        }

        public static void BorrowBook(ref int availableCopies)
        {
            if (BookRegistered == false)
            {
                Console.WriteLine("No book registered yet.");
                return;
            }
            if (availableCopies == 0)
            {
                Console.WriteLine("There are no copies available to borrow.");
            }
            else
            {
                availableCopies = Math.Max(availableCopies - 1, 0);
                BorrowDate = DateTime.Now;
                Console.WriteLine("You borrowed 1 copy of " + BookTitle);
                Console.WriteLine("Remaining copies: " + availableCopies);
                Console.WriteLine("Registration completed at: " + BorrowDate.ToString("yyyy-MM-dd "));
            }
        }

        public static void ReturnBook(ref int availableCopies)
        {

            availableCopies = availableCopies + 1;
            ReturnDate = DateTime.Now;
            Console.WriteLine("You returned 1 copy." + BookTitle);
            Console.WriteLine("Available copies now: " + availableCopies);
            Console.WriteLine("Registration completed at: " + ReturnDate.ToString("yyyy-MM-dd "));
        }

        public static double ComputeFine(int days)
        {
            double fine = Math.Sqrt(days);
            return Math.Round(fine, 2);
        }

        public static double ApplyDiscount(double amount)
        {

            double discounted = amount * 0.05;
            return Math.Round(discounted, 2);

        }

        public static double ApplyDiscount(double amount, string memberTier)
        {
            string tier = memberTier.ToUpper();
            double rate;

            if (tier == "PREMIUM")
                rate = 0.80;
            else
                rate = 0.90;

            double discounted = amount * rate;
            return Math.Round(discounted, 2);
        }

        public static bool IsEligibleToBorrow(string expiryDateString)
        {
            DateTime expiryDate = DateTime.Parse(expiryDateString);
            return expiryDate >= DateTime.Today;
        }

        public static void AddBookInformation(string defaultGenre = "General")
        {
            Console.WriteLine("Enter book title:");
            BookTitle = Console.ReadLine().Trim();

            Console.WriteLine("Enter book author:");
            BookAuthor = Console.ReadLine().Trim();

            Console.WriteLine("Enter available copies:");
            BookAvailableCopies = int.Parse(Console.ReadLine().Trim());

            Console.WriteLine("Enter book genre (optional):");
            string inputGenre = Console.ReadLine().Trim();

            if (inputGenre.Length == 0)
            {
                BookGenre = defaultGenre;
            }
            else
            {
                BookGenre = inputGenre;
            }

            BookRegistered = true;
            Console.WriteLine("Book information added successfully");
            Console.WriteLine("Title: " + BookTitle);
            Console.WriteLine("Author: " + BookAuthor);
            Console.WriteLine(" Genre: " + BookGenre);
            Console.WriteLine("Copies: " + BookAvailableCopies);

        }

        public static string GenerateMemberID()
        {
            MemberID = MemberName.ToUpper().Substring(0, 2) + "-" + MemberID; // so the ID will be xx-ID number 
            return MemberID;
        }

        public static void DisplayBookDetails()
        {
            if (BookRegistered)
            {
                Console.WriteLine("===*** Book Details ****===");
                Console.WriteLine("Title: " + BookTitle);
                Console.WriteLine("Author: " + BookAuthor);
                Console.WriteLine("Genre: " + BookGenre);
                Console.WriteLine("Available Copies: " + BookAvailableCopies);
            }
            else
            {
                Console.WriteLine("No book registered yet.");
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
                    case "0":
                       
                            AddMemberInformation();
                       
                        break;
                    case "1":

                        MemberProfile();

                        break;
                    case "2":
                        if (BookRegistered)
                        {
                            Console.Write("Enter book title to search: ");
                            string searchTitle = Console.ReadLine();
                            SearchBookByTitle(searchTitle);
                        }
                        else
                        {
                            Console.WriteLine("No book registered yet.");
                        }
                        break;
                    case "3":
                        BorrowBook(ref BookAvailableCopies);
                        break;
                    case "4":
                        ReturnBook(ref BookAvailableCopies);
                        break;
                    case "5":
                        Console.Write("Enter number of overdue days: ");
                        int days = int.Parse(Console.ReadLine());

                        double fine = ComputeFine(days);
                        Console.WriteLine("Fine amount = " + fine + " OMR ");
                        break;
                    case "6":
                        Console.Write("Enter amount: ");
                        double amount = double.Parse(Console.ReadLine());
                        if (MemberTier == "")
                        {
                            double discounted = ApplyDiscount(amount);
                            Console.WriteLine("Discounted amount = " + discounted + " OMR");
                        }
                        else
                        {

                            double discounted = ApplyDiscount(amount, MemberTier);
                            Console.WriteLine("Discounted amount (" + MemberTier + ") = " + discounted + " OMR");

                        }

                        break;
                    case "7":
                        if (MembershipExpiry == "")
                        {
                            Console.WriteLine("No expiry date set for membership.");
                        }
                        else
                        {
                            bool eligible = IsEligibleToBorrow(MembershipExpiry);

                            Console.WriteLine("Registration Date: "+ RegistrationDate);

                            if (eligible)
                                Console.WriteLine("Member is eligible to borrow.");
                            else
                                Console.WriteLine("Membership expired. Not eligible to borrow.");
                        }
                        break;
                    case "8":
                        AddBookInformation();
                        break;
                    case "9":
                        if (MemberName == "")
                        {
                            Console.WriteLine("Please register member first.");
                        }
                        else
                        {
                            MemberID = GenerateMemberID();
                            Console.WriteLine("Generated Member ID: " + MemberID);
                        }
                        break;
                    case "10":
                        DisplayBookDetails();
                        break;







                    case "15":
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








                Console.WriteLine("Exiting system. Goodbye!");
            
        }
    }
}