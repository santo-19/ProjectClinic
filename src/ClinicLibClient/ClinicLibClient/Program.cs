using System;
using System.Data.SqlClient;
using ClinicLib;
namespace ClinicLibClient
//Front-end application for the Eurofins Clinical Management System
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("********************************************************");
            Console.WriteLine("********* EUROFINS CLINICAL MANAGEMENT SYSTEM **********");
            Console.WriteLine("********************************************************");
            Console.WriteLine("~~~~~ Welcome ~~~~~");
            while (true)
            {

                try
                {
                    
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("*** Login ***");
                    Console.Write("Enter the username: ");
                    string username = Console.ReadLine();
                    Console.Write("Enter the password: ");
                    string password = Console.ReadLine();
                    Login login = new Login();
                    IHome home = new Home();
                    IScheduleAppointments sa = new ScheduleAppointments();
                    ICancelSchedule cs = new CancelSchedule();
                    login.UserLogin(username, password);
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Login Successful!");
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Home Page");
                    Console.WriteLine("-----------------------------------");
                    while (true)
                    {
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.WriteLine("Click option '1' to View all the Doctors in the Clinic");
                        Console.WriteLine("Click option '2' to Add new Patient");
                        Console.WriteLine("Click option '3' to View all the Patients in the Clinic");
                        Console.WriteLine("Click option '4' to Schedule your Appointment");
                        Console.WriteLine("Click option '5' to Cancel your Appointment");
                        Console.WriteLine("Click option '6' to Logout");
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.Write("Enter the option of your choice: ");
                        int option = Convert.ToInt32(Console.ReadLine());
                        if (option == 6)
                        {
                            Console.WriteLine("You are successfully logged out!");
                            Console.WriteLine("Thank You for trusting us!!!");
                            break;
                        }
                        switch (option)
                        {
                            case 1:
                                {
                                    List<Doctors> doc = home.viewAllDoctors();
                                    Console.WriteLine("\t\t---Doctor Details---");
                                    foreach (Doctors d in doc)
                                    {
                                        Console.WriteLine("-------------------------------------");
                                        Console.WriteLine($"doctor id : {d.doctorID} \nfirstName : {d.first_name}\n" +
                                            $"lastName : {d.last_name} \nsex : {d.sex} \nspecialization : {d.specialization} " +
                                            $"\nvisiting from : {d.visiting_from} \nvisiting to : {d.visiting_to}");

                                        Console.WriteLine("-------------------------------------");
                                    }
                                    break;
                                }

                            case 2:
                                {
                                    try
                                    {
                                        Console.WriteLine("-------------------------------------");
                                        Console.WriteLine("\t\t---Adding Patient Details---");
                                        Console.Write("Enter the first name: ");
                                        string first_name = Console.ReadLine();
                                        Console.Write ("Enter the last name: ");
                                        string last_name = Console.ReadLine();
                                        Console.Write ("Enter the sex: ");
                                        string sex = Console.ReadLine();
                                        Console.Write ("Enter the age: ");
                                        int age = Convert.ToInt32(Console.ReadLine());
                                        Console.Write("Enter the date of birth: ");
                                        string dob = Console.ReadLine();
                                        home.validatePatients(first_name, last_name, sex, age, dob);
                                        DateTime dateofbirth = Convert.ToDateTime(dob);
                                        Patients p = new Patients(first_name, last_name, sex, age, dateofbirth);
                                        int returnedid;
                                        int result = home.addPatients(p, out returnedid);
                                        if (result == 1)
                                        {
                                            Console.WriteLine("Patient " + first_name + " is added successfully!");
                                            Console.WriteLine("The patient id is :" + returnedid);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Patient is not added!! Error!");
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    Console.WriteLine("-------------------------------------");
                                    break;
                                }
                            case 3:
                                {
                                    List<Patients> pat = home.viewAllPatients();
                                    Console.WriteLine("\t\t\t---Patient Details---");

                                    foreach (Patients ps in pat)
                                    {
                                        Console.WriteLine($"Patient ID : {ps.patientID} \nFirstName : {ps.first_name}\n" +
                                            $"LastName : {ps.last_name} \nSex : {ps.sex} \nAge : {ps.age}" +
                                            $"\nDate Of Birth : {ps.dob}\n -------------------------------");
                                    }
                                    break;
                                }

                            case 4:
                                {
                                    try
                                    {
                                        Console.WriteLine("-------------------------------------");
                                        Console.Write("Enter the Patient ID: ");
                                        int pid = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("-------------------------------------");
                                        Console.WriteLine("Available specializations : General, Orthopedics, Internal Medicine, Pediatrics, Opthalmology");
                                        Console.WriteLine("-------------------------------------");
                                        Console.Write("Enter the specialization: ");
                                        string spec = Console.ReadLine();

                                        sa.ValidateScheduleAppointment(pid, spec);
                                        List<Doctors> doc = sa.displayDoctorsBasedOnSpecialization(spec);
                                        List<int> valdocid = new List<int>();
                                        Console.WriteLine("\t\t---Doctor Specialization Details---");

                                        foreach (Doctors d in doc)
                                        {
                                            Console.WriteLine("-------------------------------------");
                                            Console.WriteLine($"doctor id : {d.doctorID} \nfirstName : {d.first_name}\n" +
                                                $"lastName : {d.last_name} \nsex : {d.sex} \nspecialization : {d.specialization} " +
                                                $"\nvisiting from : {d.visiting_from} \nvisiting to : {d.visiting_to}");

                                            Console.WriteLine("-------------------------------------");
                                        }
                                        Console.WriteLine("The available dates for booking are  : [26/08/2022, 27/08/2022, 28/08/2022, 29/08/2022, 30/08/2022, 31/08/2022]");
                                        Console.Write("Enter the Date of Appointment: ");
                                        string dateofapp = Console.ReadLine();
                                        sa.ValidateDateLimit(dateofapp);
                                        sa.ValidateIndianFormatDate(dateofapp);
                                        Console.Write("Enter the Doctor ID: ");
                                        int docid = Convert.ToInt32(Console.ReadLine());
                                        sa.ValidateDoctorID(docid, valdocid);
                                        DateTime dateofappointment = DateTime.Parse(dateofapp);
                                        List<Appointments> app = sa.getAllSlotsForDoctor(docid, dateofappointment);
                                        List<int> apid = new List<int>();
                                        Console.WriteLine("-------------------------------------");
                                        Console.WriteLine("The available slots are");
                                        Console.WriteLine("-------------------------------------");
                                        foreach (Appointments a in app)
                                        {
                                            Console.WriteLine("-------------------------------------");
                                            Console.WriteLine($"Appointment id : {a.apt_id} \nDoctor id : {a.doctor_id}\n" +
                                                $"Date : {a.visiting_date} \nAppointment Time : {a.timeslot} \nStatus : {a.apt_status} " +
                                                $"\nPatient id : {a.patient_id} ");

                                            Console.WriteLine("-------------------------------------");
                                        }
                                        Console.WriteLine("-----------------------------------");
                                        Console.Write("Enter the Appointment ID: ");
                                        int appointmentId = Convert.ToInt32(Console.ReadLine());
                                        sa.ValidateAppointmentID(apid, appointmentId);
                                        sa.bookAppointment(appointmentId, pid);
                                        Console.WriteLine("Apoointment successfully booked for the Patient ID " + pid);
                                        Console.WriteLine("Your Appointment ID is " + appointmentId);


                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }


                                    Console.WriteLine("-------------------------------------");
                                    break;
                                }

                            case 5:
                                {
                                    try
                                    {
                                        Console.WriteLine("\t\t----Cancellation----");
                                        Console.Write("Enter the Patient ID: ");
                                        int pid = Convert.ToInt32(Console.ReadLine());

                                        cs.ValidatePatientID(pid);
                                        Console.WriteLine("The available dates for cancellation  are : [26/08/2022, 27/08/2022, 28/08/2022, 29/08/2022, 30/08/2022, 31/08/2022]");
                                        Console.Write("Enter the Cancel date: ");
                                        string date = Console.ReadLine();
                                        sa.ValidateDateLimit(date);
                                        cs.ValidateIndianFormatDate(date);

                                        DateTime dt = Convert.ToDateTime(date);
                                        List<Appointments> appP = cs.displayAppointmentsOfPatient(pid, dt);
                                        if (appP.Count == 0)
                                        {
                                            Console.WriteLine("Patient has no appointments!!!");
                                        }
                                        else
                                        {
                                            Console.WriteLine("-------------------------------------");
                                            Console.WriteLine("The booked appointments are");
                                            Console.WriteLine("-------------------------------------");
                                            foreach (Appointments a in appP)
                                            {
                                                Console.WriteLine("-------------------------------------");
                                                Console.WriteLine($"Appointment id : {a.apt_id} \nDoctor id : {a.doctor_id}\n" +
                                                    $"Date : {a.visiting_date} \nAppointment Time : {a.timeslot} \nStatus : {a.apt_status} " +
                                                    $"\nPatient id : {a.patient_id} ");

                                                Console.WriteLine("-------------------------------------");
                                            }
                                            Console.WriteLine("-----------------------------------");

                                            Console.Write("Enter the Appointment ID to cancel: ");
                                            int appid = Convert.ToInt32(Console.ReadLine());
                                            cs.cancelAppointment(appid, pid);
                                            Console.WriteLine("Appointment of ID " + appid + " is cancelled successfully");

                                        }


                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}