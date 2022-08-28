using System;
using System.Data.SqlClient;
using ClinicLib;
namespace ClinicLibClient
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("************************************");
            Console.WriteLine("********* Eurofins Clinic **********");
            Console.WriteLine("************************************");
            Console.WriteLine("~~ Welcome ~~");
            while (true)
            {

                try
                {
                    
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("*** Login ***");
                    Console.WriteLine("Enter the username:");
                    string username = Console.ReadLine();
                    Console.WriteLine("Enter the password:");
                    string password = Console.ReadLine();
                    Login login = new Login();
                    IHome home = new Home();
                    IScheduleAppointments sa = new ScheduleAppointments();
                    ICancelSchedule cs = new CancelSchedule();
                    login.UserLogin(username, password);
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("User login successful!");
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Home Page");
                    Console.WriteLine("-----------------------------------");
                    while (true)
                    {
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine("1.View all the Doctors in the Clinic");
                        Console.WriteLine("2.Add new Patient");
                        Console.WriteLine("3.Schedule your Appointment");
                        Console.WriteLine("4.Cancel your Appointment");
                        Console.WriteLine("5.Logout");
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine("Enter the option");
                        int option = Convert.ToInt32(Console.ReadLine());
                        if (option == 5)
                        {
                            break;
                        }
                        switch (option)
                        {
                            case 1:
                                {
                                    List<Doctors> doc = home.viewAllDoctors();
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
                                        Console.WriteLine("Enter the first name:");
                                        string first_name = Console.ReadLine();
                                        Console.WriteLine("Enter the last name:");
                                        string last_name = Console.ReadLine();
                                        Console.WriteLine("Enter the sex:");
                                        string sex = Console.ReadLine();
                                        Console.WriteLine("Enter the age:");
                                        int age = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Enter the dob:");
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
                                    try
                                    {
                                        Console.WriteLine("-------------------------------------");
                                        Console.WriteLine("Enter the patient id");
                                        int pid = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("-------------------------------------");
                                        Console.WriteLine("Available specializations : General, Orthopedics, Internal Medicine, Pediatrics, Opthalmology");
                                        Console.WriteLine("-------------------------------------");
                                        Console.WriteLine("Enter the specialization");
                                        string spec = Console.ReadLine();

                                        sa.ValidateScheduleAppointment(pid, spec);
                                        List<Doctors> doc = sa.displayDoctorsBasedOnSpecialization(spec);
                                        foreach (Doctors d in doc)
                                        {
                                            Console.WriteLine("-------------------------------------");
                                            Console.WriteLine($"doctor id : {d.doctorID} \nfirstName : {d.first_name}\n" +
                                                $"lastName : {d.last_name} \nsex : {d.sex} \nspecialization : {d.specialization} " +
                                                $"\nvisiting from : {d.visiting_from} \nvisiting to : {d.visiting_to}");

                                            Console.WriteLine("-------------------------------------");
                                        }
                                        Console.WriteLine("The available dates for booking are  : [26/08/2022,27/08/2022,28/08/2022]");
                                        Console.WriteLine("Enter the Date of Appointment");
                                        string dateofapp = Console.ReadLine();
                                        sa.ValidateIndianFormatDate(dateofapp);
                                        Console.WriteLine("Enter the Doctor id");
                                        int docid = Convert.ToInt32(Console.ReadLine());
                                        DateTime dateofappointment = DateTime.Parse(dateofapp);
                                        List<Appointments> app = sa.getAllSlotsForDoctor(docid, dateofappointment);
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
                                        Console.WriteLine("Enter the Appointment ID");
                                        int appointmentId = Convert.ToInt32(Console.ReadLine());
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

                            case 4:
                                {
                                    try
                                    {
                                        Console.WriteLine("Enter the Patient ID");
                                        int pid = Convert.ToInt32(Console.ReadLine());

                                        cs.ValidatePatientID(pid);
                                        Console.WriteLine("The available dates for cancellation  are : [26/08/2022,27/08/2022,28/08/2022]");
                                        Console.WriteLine("Enter the Cancel date");
                                        string date = Console.ReadLine();
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

                                            Console.WriteLine("Enter the Appointment ID to cancel");
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