using System;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using DataBase.Model;

namespace ATMSystem
{
    public class InputFormat
    {

        public static string FirstName()
        {
            string NamePattern = @"[A-Za-z]{1,}";
            Regex reg = new Regex(NamePattern);

        Start: Console.Write("Enter FirstName \n(Note: NO Numbers are allowed ) \n ===>");
            string Firstname = Console.ReadLine();

            switch (reg.IsMatch(Firstname))
            {
                case true:
                    return Firstname;
                default:
                    Console.Clear();
                    Console.WriteLine($"{Firstname} was written with the wrong Format, NO NUMBERS ALLOWED");
                    goto Start;
            }
        }

        public static string LastName()
        {
            string NamePattern = @"[A-Za-z]{1,}";
            Regex reg = new Regex(NamePattern);

            Start:  Console.Write("Enter LastName \n(Note: NO Numbers are allowed )\n ===>");
            string Lastname = Console.ReadLine();

            switch (reg.IsMatch(Lastname))
            {
                case true:
                    return Lastname;
                default:
                    Console.Clear();
                    Console.WriteLine($"{Lastname} was written with the wrong Format, NO NUMBERS ALLOWED");
                    goto Start;
            }


        }

        public static string UserName()
        {
            string NamePattern = @"[A-Za-z1-9]{1,}";
            Regex reg = new Regex(NamePattern);

            Start:  Console.Write("Enter Username \n ===>");
            string Username = Console.ReadLine();

            switch (reg.IsMatch(Username))
            {
                case true:
                    return Username;
                default:
                    Console.Clear();
                    Console.WriteLine($"{Username} was written with the wrong Format, NO NUMBERS ALLOWED");
                    goto Start;
            }

        }
        public static string CardNo()
        {
            string CardNumberFormat = @"^([0-9]{15,16})$";
            Regex regex = new Regex(CardNumberFormat);

        Start: Console.Write("Enter CardNo \n ===>");
            string CardNo = Console.ReadLine();

            switch (regex.IsMatch(CardNo))
            {
                case true:
                    return CardNo;
                default:
                    Console.Clear();
                    Console.WriteLine($"{CardNo} is written in the wrong Format");
                    goto Start;
            }
        }
        public static string Balance()
        {
            string PinNumberFormat = @"^([0-9]{1,})$";
            Regex regex = new Regex(PinNumberFormat);

           Start: Console.Write("Enter Balance \n(Note: This must not contain any symbol or letters only numbers)\n ===>");
            string Balance = Console.ReadLine();

            switch (regex.IsMatch(Balance))
            {
                case true:
                    return Balance;
                default:
                    Console.Clear();
                    Console.WriteLine($"{Balance} is written in the wrong Format");
                    goto Start;
            }
        }


        public static string PinNo()
        {
            string PinNumberFormat = @"^([0-9]{4})$";
            Regex regex = new Regex(PinNumberFormat);

           Start: Console.Write("Enter Pin_No \n(Note: Pin must not be less than or more than four numbers)\n ===>");
            string Pin_No = Console.ReadLine();

            switch (regex.IsMatch(Pin_No))
            {
                case true:
                    return Pin_No;
                default:
                    Console.Clear();
                    Console.WriteLine($"{Pin_No} is written in the wrong Format");
                    goto Start;
            }
        }
        public static string PhoneNumber()
        {
            string PhoneNumberFormat = @"^([0-9]{3})-([0-9]{3})-([0-9]{4})$";
            Regex regex = new Regex(PhoneNumberFormat);


           Start: Console.Write("Enter Phone Number using this format \" 608-301-0103 \" \n ===>");
            string PhoneNo = Console.ReadLine();

            switch (regex.IsMatch(PhoneNo))
            {
                case true:
                    return PhoneNo;

                default:
                    Console.Clear();
                    Console.WriteLine($"{PhoneNo} is written in the wrong Format");
                    goto Start;
                  
            }
        }
        public static string Gender()
        {
            Console.Write("Enter Gender (Note: There are only two known gender (Male or Female), any other entry will be labeled Complicated)\n ===>");
            string Gender = Console.ReadLine();

            switch (Gender.ToUpper())
            {
                case "MALE":
                    return "Male";
                case "FEMALE":
                    return "Female";
                default:
                    return "Complicated";
            }

        }

        public static User EditList(User user)
        {
            
            Name: Console.WriteLine("Do you want to edit Names (Y / N)");
            string Name = Console.ReadLine();
            switch (Name.ToUpper())
            {
                case "Y":
                    user.First_name = FirstName();
                    user.Last_name = LastName();
                    user.UserName = UserName();
                    break;
                case "N":
                    break;
                default:
                    Console.WriteLine($"{Name} is invalid option");
                    goto Name;
            }

            CardNo: Console.WriteLine("Do you want to edit CardNo (Y / N)");
            string Card = Console.ReadLine();
            switch (Card.ToUpper())
            {
                case "Y":
                    user.Card_No = CardNo();
                    break;
                case "N":
                    break;
                default:
                    Console.WriteLine($"{Card} is invalid option");
                    goto CardNo;
            }

            Balance: Console.WriteLine("Do you want to edit Balance(Y / N)");
            string input = Console.ReadLine();
            switch (input.ToUpper())
            {
                case "Y":
                    user.Balance = Balance();
                    break;
                case "N":
                    break;
                default:
                    Console.WriteLine($"{input} is invalid option");
                    goto Balance;
            }

            Pin: Console.WriteLine("Do you want to edit Pin(Y / N)");
            string input1 = Console.ReadLine();
            switch (input1.ToUpper())
            {
                case "Y":
                    user.Pin_No = PinNo();
                    break;
                case "N":
                    break;
                default:
                    Console.WriteLine($"{input1} is invalid option");
                    goto Pin;
            }

            PhoneNumber: Console.WriteLine("Do you want to edit PhoneNumber(Y / N)");
            string input2 = Console.ReadLine();
            switch (input2.ToUpper())
            {
                case "Y":
                    user.Phone_Number = PhoneNumber();
                    break;
                case "N":
                    break;
                default:
                    Console.WriteLine($"{input2} is invalid option");
                    goto PhoneNumber;
            }

            return user;
        }


    }
}

