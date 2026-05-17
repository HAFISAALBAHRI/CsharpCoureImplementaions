namespace FirstConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // region 1 : system storage (variables)
            string patientName = "";
            int patientAge = 0;
            string patientPhone = "";
            string patientAddress = "";
            bool hasPatientProfile = false; // patient profile exists or not
            bool hasAppointment = false;
            bool hasPrescription = false;   // flag to check if any prescription exists
            string medicineName = "";
            string dosage = "";
            string instructions = "";
            string appointment = "";
            string doctor1 = "Dr. Ahmed Al-Harthy";
            string doctor2 = "Dr. Fatima Al-Mazrouei";
            string doctor3 = "Dr. Khalid Al-Balushi";
            string date1 = "20/05/2026";
            string date2 = "25/05/2026";
            string date3 = "01/06/2026";

            // region 2 : system processing (menu functions)
            bool exit = false;
            while (exit == false)
            {
                Console.WriteLine("===== CLINIC MANAGEMENT SYSTEM (For Patient) =====");
                Console.WriteLine("1. Add Patient Profile");
                Console.WriteLine("2. print Patient Profile");
                Console.WriteLine("3. Edit Patient Profile");
                Console.WriteLine("4. Book Appointment");
                Console.WriteLine("5. print Appointments");
                Console.WriteLine("6. Add Prescription");
                Console.WriteLine("7. print Prescriptions");
                Console.WriteLine("8. Exit");
                Console.Write("Enter your choice: ");

                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1: // add patient profile
                        if (hasPatientProfile == true)
                        {
                            Console.WriteLine("Patient profile already exists. Use Edit option to change.");
                        }
                        else
                        {
                            Console.Write("Enter patient name: ");
                            patientName = Console.ReadLine();
                            Console.Write("Enter patient age: ");
                            patientAge = int.Parse(Console.ReadLine());
                            Console.Write("Enter patient phone: ");
                            patientPhone = Console.ReadLine();
                            Console.Write("Enter patient address: ");
                            patientAddress = Console.ReadLine();

                            hasPatientProfile = true;
                            Console.WriteLine("Patient profile created successfully.");
                        }
                        break;
                    case 2: // print patient profile
                        if (hasPatientProfile == false)
                        {
                            Console.WriteLine("No patient profile found.");
                        }
                        else
                        {
                            Console.WriteLine("Patient Name   : " + patientName);
                            Console.WriteLine("Age            : " + patientAge);
                            Console.WriteLine("Phone          : " + patientPhone);
                            Console.WriteLine("Address        : " + patientAddress);
                        }
                        break;

                    case 3: // edit patient profile
                        if (hasPatientProfile == false)
                        {
                            Console.WriteLine("No patient profile found.");
                        }
                        else
                        {
                            Console.WriteLine("Choose field to edit:");
                            Console.WriteLine("1. Name");
                            Console.WriteLine("2. Age");
                            Console.WriteLine("3. Phone");
                            Console.WriteLine("4. Address");
                            int editChoice = int.Parse(Console.ReadLine());

                            switch (editChoice)
                            {
                                case 1:
                                    Console.Write("Enter new name: ");
                                    patientName = Console.ReadLine();
                                    break;
                                case 2:
                                    Console.Write("Enter new age: ");
                                    patientAge = int.Parse(Console.ReadLine());
                                    break;
                                case 3:
                                    Console.Write("Enter new phone: ");
                                    patientPhone = Console.ReadLine();
                                    break;
                                case 4:
                                    Console.Write("Enter new address: ");
                                    patientAddress = Console.ReadLine();
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice.");
                                    break;
                            }
                            Console.WriteLine("Patient profile updated.");
                        }
                        break;
                    case 4: // book appointment
                        Console.WriteLine("Choose Doctor:");
                        Console.WriteLine("1. Dr. Ahmed Al-Harthy");
                        Console.WriteLine("2. Dr. Fatima Al-Mazrouei");
                        Console.WriteLine("3. Dr. Khalid Al-Balushi");
                        Console.Write("Enter choice: ");
                        int doctorChoice = int.Parse(Console.ReadLine());

                        string doctorName = "";
                        if (doctorChoice == 1) doctorName = "Dr. Ahmed Al-Harthy";
                        else if (doctorChoice == 2) doctorName = "Dr. Fatima Al-Mazrouei";
                        else if (doctorChoice == 3) doctorName = "Dr. Khalid Al-Balushi";
                        else Console.WriteLine("Invalid choice.");

                        Console.WriteLine("Choose Date:");
                        Console.WriteLine("1. " + date1);
                        Console.WriteLine("2. " + date2);
                        Console.WriteLine("3. " + date3);

                        Console.Write("Enter choice: ");
                        int dateChoice = int.Parse(Console.ReadLine());

                        string appointmentDate = "";
                        if (dateChoice == 1) appointmentDate = date1;
                        else if (dateChoice == 2) appointmentDate = date2;
                        else if (dateChoice == 3) appointmentDate = date3;
                        else Console.WriteLine("Invalid choice.");
                        if (doctorName != "" && appointmentDate != "")
                        {
                            appointment = "Doctor: " + doctorName + ", Date: " + appointmentDate;
                            hasAppointment = true; // now we have an appointment
                            Console.WriteLine("Appointment booked successfully.");

                        }


                        break;
                    case 5: // print appointments
                        if (hasAppointment == false)
                        {
                            Console.WriteLine("No appointments found.");
                        }
                        else
                        {
                            Console.WriteLine("Appointments: " + appointment);

                        }
                        break;


                    case 6: // add prescription
                        Console.Write("Enter Medicine Name: ");
                        medicineName = Console.ReadLine();

                        Console.Write("Enter Dosage (e.g. 500mg): ");
                        dosage = Console.ReadLine();

                        Console.Write("Enter Instructions (e.g., Twice daily after meals): ");
                        instructions = Console.ReadLine();

                        hasPrescription = true; // now we have a prescription
                        Console.WriteLine("Prescription added successfully.");
                        break;

                    case 7: // print prescriptions
                        if (hasPrescription == false)
                        {
                            Console.WriteLine("No prescriptions found.");
                        }
                        else
                        {
                            Console.WriteLine("Prescription:");
                            Console.WriteLine("Medicine    : " + medicineName);
                            Console.WriteLine("Dosage      : " + dosage);
                            Console.WriteLine("Instructions: " + instructions);
                        }
                        break;
                    case 8: // exit
                        exit = true;
                        break;


                    default:// invalid option
                        Console.WriteLine("invalid option please try again");
                        break;
                }
                if (exit != true)
                {
                    Console.WriteLine("press any key to continue...");
                    Console.ReadKey();
                    Console.Clear(); // clear the console for better user experience
                }
               

                
            }
        }
    }
} 