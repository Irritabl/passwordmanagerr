using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection.Metadata;
using System.Text;

namespace MyApplication
{
    class Program
    {


        static void Main(string[] args)
        {
            Prop(args, 0,12);
        }


        public static void Prop(string[] args, double accepts,int length)
        {
            
            if (File.Exists("password.txt"))
            {
                Console.WriteLine("Enter vault password:");
                string masterPassword = Console.ReadLine();
                if (masterPassword == File.ReadAllText("password.txt"))
                {
                    Console.WriteLine("Would you like to add information? y/n:");
                    string addinfo = Console.ReadLine();
                    if (addinfo == "y")
                    {
                        try
                        {
                            File.AppendAllText("info.txt", Environment.NewLine);
                            Console.WriteLine("Enter website name:");
                            string website = Console.ReadLine();
                            Console.WriteLine("Enter username:");
                            string username = Console.ReadLine();
                            Console.WriteLine("would you like me to generate a password for you? y/n");
                            string generate = Console.ReadLine();
                            if (generate== "y") 
                            {
                                static string GeneratePassword(int length)
                                {
                                    const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890!@#$%^&*";
                                    const string specialChars = "!@#$%^&*";
                                    StringBuilder res = new StringBuilder();
                                    Random rnd = new Random();

                                    
                                    res.Append(specialChars[rnd.Next(specialChars.Length)]);
                                    res.Append(validChars[rnd.Next(validChars.Length - 10) + 10]);

                                    while (res.Length < length)
                                    {
                                        res.Append(validChars[rnd.Next(validChars.Length)]);
                                    }

                                    
                                    char[] password = res.ToString().ToCharArray();
                                    for (int i = password.Length - 1; i > 0; i--)
                                    {
                                        int swapIndex = rnd.Next(i + 1);
                                        if (swapIndex != i)
                                        {
                                            char temp = password[i];
                                            password[i] = password[swapIndex];
                                            password[swapIndex] = temp;
                                        }
                                    }
                                    return new string(password);
                                }
                                File.AppendAllText("info.txt", $"{website}|{username}|{GeneratePassword(12)}");
                                    Console.WriteLine(File.ReadAllText("info.txt"));
                                    Main(args);
                                
                                }
                            else if (generate == "n")
                            {
                                Console.WriteLine("Enter password:");
                                string password = Console.ReadLine();
                                File.AppendAllText("info.txt", $"{website}|{username}|{password}");
                                Console.WriteLine(File.ReadAllText("info.txt"));
                                Main(args);
                            }
                            
                        }

                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                    }
                    else if (addinfo == "n")
                    {
                        try
                        {
                            Console.WriteLine(File.ReadAllText("info.txt"));
                            Main(args);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                    }
                    else if (addinfo == "reset")
                    {
                        try
                        {
                            File.Delete("password.txt");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                    }

                }
                else
                {
                    Console.WriteLine($"Sorry, that's incorrect. Attempts left: {3 - accepts}");
                    accepts++;

                    if (accepts >= 3)
                    {
                        try
                        {
                            File.Delete("info.txt");
                            Console.WriteLine("Data erased");
                            accepts = 0;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                    }
                    Prop(args, accepts,12);






                }






            }
            else
            {
                Console.WriteLine("what would you like the vault password to be?");
                File.WriteAllText("password.txt", Console.ReadLine());
                Main(args);
            }





        }

        
    }

}
