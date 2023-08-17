using System.Linq;

namespace PasswordValidator
{
    internal class Program
    {
        static void InfoScreen()
        {
            Console.WriteLine("= PASSWORD VALIDATOR =");
            Console.WriteLine("\n");
            Console.WriteLine("+ The password must at least consist of 12 characters and at most 64 characters.");
            Console.WriteLine("+ The usage of both UPPERCASE and lowercase letters is a requirement.");
            Console.WriteLine("+ There must be a combination of symbols, as well as numbers.");
            Console.WriteLine("+ Lastly, there has to be at least one special symbol in the password.\n");
            Console.WriteLine("These are very important rules to follow in order to ensure a higher level\n" +
                "of security on the internet, as well as workplace. In fact, it is no exaggeration\n" +
                "to say that following these rules is paramount to proper online protection.\n");

        }

        static void PassState(int stateNum)
        {
            switch (stateNum)
            {
                case 0:
                    Console.WriteLine("The password length does not meet the requirements.");
                    break;
                case 1:
                    Console.WriteLine("The password must contain a mix of UPPERCASE and lowercase letters.");
                    break;
                case 2:
                    Console.WriteLine("The password must contain a mix of symbols and numbers.");
                    break;
                case 3:
                    Console.WriteLine("There must be at least one special symbol.");
                    break;
                case 4:
                    Console.WriteLine("Avoid using repeated characters, i.e., \"FFFF\" or \"7777\".");
                    break;
                case 5:
                    Console.WriteLine("Avoid using sequenced numbers, i.e., \"2345\" or \"4321\".");
                    break;
                case 6:
                    Console.WriteLine("The password did not meet the requirements.");
                    break;
                case 7:
                    Console.WriteLine("The password has passed the requirements, but could be enforced for further security.");
                    break;
                case 8:
                    Console.WriteLine("The password has all passed the requirements!");
                    break;
                default:
                    Console.WriteLine("The password must be within the specified length, have a mix of UPPERCASE, " +
                        "lowercase letters\nsymbols, numbers and at least one special symbol.");
                    return;
            }
        }

        static void PassValid()
        {
            const int PASS_MIN_LEN = 12;
            const int PASS_MAX_LEN = 64;
            const int NUM_SPIT = 4;
            int[] numArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            string numStr = "0123456789";
            char[] numCharArray = numStr.ToCharArray();
            string spcSym = "!\"#¤%&/()=?`§@ £$€{[]}|½´¨'\\<^*~>_-.:,;";
            char[] spcSymArray = spcSym.ToCharArray();

            string userPassIn;
            bool bApprovePass = false;
            bool bNotRepeatingChar = false;
            bool bNotSequenceChar = false;

            do
            {
                /*
                var keyPress = Console.ReadKey(false).Key;
                switch (keyPress)
                {
                    case ConsoleKey.Escape:  // Allows the user to quit the console
                        Environment.Exit(0);
                        return;
                    default:
                        break;
                }
                */

                //userPassIn = Console.ReadLine();
                //userPassIn = "KrisFlyingFish!19+";
                //userPassIn = "KrisFlyingFish19";
                //userPassIn = "KrisFlyingFish1234!";
                userPassIn = "KrisFFFFFlyingFish0000!";


                if (userPassIn.Length < PASS_MIN_LEN || userPassIn.Length > PASS_MAX_LEN)
                {
                    PassState(0);
                }
                else if (!userPassIn.Any(char.IsUpper) || !userPassIn.Any(char.IsLower))
                {
                    PassState(1);
                }
                else if (!userPassIn.Any(char.IsNumber))
                {
                    PassState(2);
                }
                else
                {
                    for (int chkCnt = 0; chkCnt < spcSymArray.Length; chkCnt++)  // Loops through the special symbols array,
                                                                                 // in order to check if any has been used
                    {
                        char elmnt = spcSymArray[chkCnt];
                        //Console.Write("{0}", elmnt);

                        if (userPassIn.Contains(elmnt))
                        {
                            bApprovePass = true;
                        }
                        else
                        {
                            PassState(3);
                        }
                        break;
                    }
                }
            } while (bApprovePass == false);
            Console.WriteLine("Yay");

            // Extra checking (repetition or easily brute forced combinations)
            userPassIn = "KrisFlgyingFisgh332332!";

            for (int repCnt = 0; repCnt < userPassIn.Length - 5; repCnt++)
            {
                while (userPassIn[repCnt] == userPassIn[repCnt + 1])
                {
                    repCnt++;
                    if (repCnt + 1 == userPassIn.Length)
                    {
                        break;
                    }
                    else
                    {
                        PassState(7);
                        PassState(4);
                        return;
                        
                    }
                }
            }


            /*
            for (int seqCnt = 0; seqCnt < numCharArray.Length; seqCnt++)
            {


                if (userPassIn.Contains(numCharArray[seqCnt+1]))
                {

                    Console.Write(numArray[seqCnt]);
                    Console.Write("I need sleep....");
                    break;
                }

                else
                {
                    Console.Write(numArray[seqCnt]);
                    Console.Write("What's happening..?");
                    break;

                }
            */




            Console.WriteLine("What?");
        }

        static void Main(string[] args)
        {
            //InfoScreen();
            PassValid();
        }
    }
}