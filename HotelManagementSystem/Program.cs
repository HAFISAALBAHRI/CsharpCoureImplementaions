namespace HotelManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
     
                // region 1 : system storage (variables)
                string guestName = "";
                string guestPhone = "";
                string roomType = "";
                string roomNotes = "";
                int roomNumber = 0;
                int nights = 0;
                int loyaltyPoints = 0;
                double nightlyRate = 0;
                double discount = 0;
                bool hasGuestProfile = false;
                bool isCheckedIn = false;
                bool hasReceipt = false;
                double total = 0;
            

                // region 2 : system processing (menu functions)
                bool exit = false;
                while (exit == false)
                {
                Console.WriteLine("=======****************************=====");
                Console.WriteLine("=====    HOTEL MANAGEMENT SYSTEM    =====");
                Console.WriteLine("=====******************************=====");
                    Console.WriteLine("0. Register New Guest");
                    Console.WriteLine("1. View Guest Information");
                    Console.WriteLine("2. Check-In Guest");
                    Console.WriteLine("3. Check-Out & Bill");
                    Console.WriteLine("4. Apply Discount");
                    Console.WriteLine("5. Upgrade Room");
                    Console.WriteLine("6. Add Room Service Note");
                    Console.WriteLine("7. Search Guest by Name");
                    Console.WriteLine("8. Calculate Loyalty Points");
                    Console.WriteLine("9. Print Receipt");
                    Console.WriteLine("10. Edit Guest Name");
                    Console.WriteLine("11. Exit");
                    Console.Write("Enter your choice: ");

                    int option = int.Parse(Console.ReadLine());

                    switch (option)
                    {
                        case 0: // Register Guest
                            if (hasGuestProfile == true)
                            {
                                Console.WriteLine("Guest profile already exists. Use Edit option to change.");
                            }
                            else
                            {
                            Console.Write("Enter guest name: ");
                                guestName = Console.ReadLine().Trim();
                                Console.Write("Enter phone: ");
                                guestPhone = Console.ReadLine().Trim(); ///هذه دالة تُستخدم لإزالة المسافات الفارغة من بداية ونهاية النص
                                Console.Write("Enter room type:(single,double,king) ");
                                roomType = Console.ReadLine().Trim();
                                Console.Write("Enter nightly rate: ");
                                nightlyRate = double.Parse(Console.ReadLine());
                               roomNumber = new Random().Next(1, 100); //// give me room number by it silf  هذه دالة تُستخدم لتوليد عدد صحيح عشوائي بين حدين (الحد الأدنى والحد الأقصى)
                            Console.Write("Enter room Notes: ");
                            roomNotes = Console.ReadLine();
                            hasGuestProfile = true;
                            Console.WriteLine("Guest profile created successfully. Room :" + roomNumber);
                            }
                            break;

                        case 1: // View Guest Info
                            if (hasGuestProfile == false)
                            {
                                Console.WriteLine("No guest profile found.");
                            }
                            else
                            {
                                Console.WriteLine("Guest Name   : " + guestName.ToUpper());
                                Console.WriteLine("Phone        : " + guestPhone);
                                Console.WriteLine("Room         : " + Convert.ToString(roomNumber) + " (" + roomType + ")"); // تحويل رقم الغرفة إلى نص
                                // Console.WriteLine("Room         : " + roomNumber + " (" + roomType + ")");
                                Console.WriteLine("Rate         : " + Convert.ToString(Math.Round(nightlyRate, 2)) + " OMR"); // Math.Round يقرب الرقم إلى منزلتين عشريتين

                            //Console.WriteLine("Rate         : " + Math.Round(nightlyRate, 2) + " OMR"); //  if it is 2.4567 it will be 2.46
                            // Console.WriteLine("Rate         : " + nightlyRate.ToString("F2") + " OMR"); other way but even if it zero it will be 0.00 
                        }
                        break;

                        case 2: // Check-In
                            if (hasGuestProfile == false)
                            {
                                Console.WriteLine("No guest profile found.");
                            }
                            else
                            {
                            // سجل تاريخ الدخول من النظام
                            DateTime checkInDate = DateTime.Now;
                            Console.WriteLine("Check-In Date: " + checkInDate.ToString());

                            // عدد الليالي كان ممكن إضافته في case 0، لكن هنا نستخدمه لحساب الخروج
                            Console.Write("Enter number of nights: ");
                            nights = int.Parse(Console.ReadLine());

                            // حساب تاريخ الخروج بناءً على عدد الليالي
                            DateTime checkOutDate = checkInDate.AddDays(nights);
                            Console.WriteLine("Expected Check-Out Date: " + checkOutDate.ToString("yyyy-MM-dd"));

                            isCheckedIn = true;
                            Console.WriteLine("Guest checked in successfully.");



                            //Console.Write("Enter number of nights: ");
                                //nights = int.Parse(Console.ReadLine());
                                //isCheckedIn = true;
                                //Console.WriteLine("Guest checked in successfully.");
                            }
                            break;

                        case 3: // Check-Out & Bill
                            if (isCheckedIn == false)
                            {
                                Console.WriteLine("Guest not checked in.");
                            }
                            else
                            {
                                total = nights * nightlyRate;
                                Console.WriteLine("Check-Out Date: " + DateTime.Now);
                                Console.WriteLine("Total Bill: " + Math.Round(total, 2) + " OMR");
                                isCheckedIn = false;
                                hasReceipt = true;
                            }
                            break;
                        case 4: // Apply Discount
                        if (total == 0)
                        {
                            Console.WriteLine("No bill available yet. Please check out first.");
                        }
                        else
                        {
                            Console.Write("Enter discount percentage: ");
                            discount = double.Parse(Console.ReadLine());

                            if (discount > 0)
                            {
                                discount = Math.Round(Math.Abs(discount), 2);

                                double discountAmount = Math.Round(total * (discount / 100), 2);
                                double finalBill = Math.Round(total - discountAmount, 2);

                                Console.WriteLine("Discount of " + discount + "% applied.");
                                Console.WriteLine("Discount Amount: " + discountAmount + " OMR");
                                Console.WriteLine("Final Bill: " + finalBill + " OMR");
                            }
                            else
                            {
                                Console.WriteLine("No discount applied. Final Bill: " + Math.Round(total, 2) + " OMR");
                            }
                        }
                    

                        //case 4: // Apply Discount
                        //Console.Write("Enter discount percentage: ");
                        //discount = double.Parse(Console.ReadLine());

                        //if (discount > 0)
                        //{
                        //    // Ensure discount is positive and rounded
                        //    discount = Math.Round(Math.Abs(discount), 2);

                        //    double discountAmount = total * (discount / 100);
                        //    double finalBill = Math.Round(total - discountAmount, 2);

                        //    Console.WriteLine("Discount of " + discount + " applied.");
                        //    Console.WriteLine("Final Bill: " + finalBill + " OMR");
                        //}
                        //else
                        //{
                        //    Console.WriteLine("No discount applied. Final Bill: " + Math.Round(total, 2) + " OMR");
                        //}


                        //    Console.Write("Enter discount percentage: ");
                        //    discount = double.Parse(Console.ReadLine());
                        //double finalBill = total - (total * discount / 100);
                        //Console.WriteLine("Final Bill: " + Math.Round(finalBill, 2) + " OMR");

                        break;

                        case 5: // Upgrade Room
                        Console.Write("Enter new room type: ");
                        string newRoomType = Console.ReadLine();

                        Console.Write("Enter new nightly rate: ");
                        double newRate = double.Parse(Console.ReadLine());

                        Console.WriteLine("Room upgraded successfully to " + newRoomType);
                        double higherRate = Math.Max(nightlyRate, newRate);// Compare old vs new rate
                        double lowerRate = Math.Min(nightlyRate, newRate);
                        double difference = Math.Abs(newRate - nightlyRate);

                        Console.WriteLine("Higher Rate: " + higherRate + " OMR");
                        Console.WriteLine("Lower Rate: " + lowerRate + " OMR");
                        Console.WriteLine("Difference per night: " + difference + " OMR");
                        roomType = newRoomType;    // Update the room type and rate
                        nightlyRate = newRate;
                        break;


                        case 6: // Add Room Service Note
                        Console.Write("Enter new  room service note: ");
                        string newNote = Console.ReadLine().Trim(); // Trim spaces at start/end

                        if (newNote.Length == 0)
                        {
                            Console.WriteLine("Note cannot be blank. Please try again.");
                        }
                        else
                        {
                            // Replace old note with new one
                            roomNotes = roomNotes.Replace(roomNotes, newNote);
                            Console.WriteLine("Total notes length: " + roomNotes.Length);
                            Console.WriteLine("Note added successfully:  " + roomNotes);
                           
                        }
                        break;

                        case 7: // Search Guest
                            Console.Write("Enter name to search: ");
                            string searchName = Console.ReadLine();
                        if (guestName.ToLower().Contains(searchName.ToLower()))
                        {
                                Console.WriteLine("Guest found!");
                            }
                            else
                            {
                                Console.WriteLine("Guest not found.");
                            }
                            break;

                     
                    case 8: // Loyalty Points
                    
                        if (isCheckedIn == false)
                        {
                            Console.WriteLine("Error: No guest registered.");
                            break;
                        }

                        // Math.Pow returns double, so round first, then cast to int
                        int pointsEarned = (int)Math.Round(Math.Pow(nights, 2), 0);

                        loyaltyPoints = loyaltyPoints + pointsEarned; // add past points to new points

                        Console.WriteLine("Points earned this stay: " + pointsEarned);
                        Console.WriteLine("Total points: " + loyaltyPoints);
                        break;



                    case 9: // Print Receipt
                            if (hasReceipt == false)
                            {
                                Console.WriteLine("No receipt available.");
                            }
                            else
                            {
                                Console.WriteLine("=== Receipt ===");
                                Console.WriteLine("Guest: " + guestName);
                                Console.WriteLine("Room: " + roomNumber + " (" + roomType + ")");
                                Console.WriteLine("Nights: " + nights);
                                Console.WriteLine("Rate: " + nightlyRate.ToString("F2") + " OMR");
                                Console.WriteLine("Discount: " + discount + "%");
                            Console.WriteLine("Notes: " + roomNotes);
                            DateTime ReceiptDate = DateTime.Now;
                            Console.WriteLine("Receipt Date: " + ReceiptDate.ToString());
                            
                        }
                            break;

                        case 10: // Edit Guest Name
                            if (hasGuestProfile == true)
                            {
                                Console.Write("Enter new name: ");
                                guestName = Console.ReadLine().Trim().ToLower();
                                Console.WriteLine("Guest name updated: " + guestName.ToUpper());
                            Console.WriteLine("Total name length: " + guestName.Length);
                        }
                            else 
                        {
                                Console.WriteLine("No guest profile found.");
                            }
                            break;

                        case 11: // Exit
                            Console.WriteLine("Session ended at " + DateTime.Now);
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Invalid option, please try again.");
                            break;
                    }

                    if (exit == false)
                    {
                        Console.WriteLine(" Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }
        }
    }


    