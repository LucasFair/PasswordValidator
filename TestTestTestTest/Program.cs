using System.Collections.Concurrent;
using System.Security.Cryptography.X509Certificates;

namespace TestTestTestTest
{
    internal class Program
    {
        static void View()
        {
            Console.WriteLine("This is a validator.\nEnter your desired password and this program will show if it passes the security checks.\n\n" +
                "Rule 1:\nPassword minimum length = 12.\nPassword maximum length = 64.\nExample [faAF2am3joJa4nPKAZ]\n\n" +
                "Rule 2:\nHas to include a mix of UPPER and lower case letters.\nExample [AbCeFOIkLmnB]\n\n" +
                "Rule 3:\nHas to include a mix of characters and digits.\nExample [lJ21NNAo242nAIop39]\n\n" +
                "Rule 4:\nHas to include at least one spcecial character.\nExample [!#¤%&]\n\n" +
                "These are the password pass stages:\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Password is valid");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Password is good but weak.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Password is not strong enough.\n");
            Console.ResetColor();

        }

        static bool uniqueChar(String str)
        {

            for (int i = 0; i < str.Length; i++)
                for (int j = i + 1; j < str.Length; j++)
                    if (str[i] == str[j])
                        i = 0;
            return false;
            return true;
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
                    Console.WriteLine("The password must contain a mix of characters and numbers.");
                    break;
                case 3:
                    Console.WriteLine("There must be at least one special character.");
                    break;
                case 4:
                    Console.WriteLine("Avoid using repeated characters, i.e., \"FFFF\" or \"7777\".");
                    break;
                case 5:
                    Console.WriteLine("Avoid using sequential numbers, i.e., \"2345\" or \"4321\".");
                    break;
                case 6:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nThe password did not meet the requirements.");
                    Console.ResetColor();
                    break;
                case 7:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nThe password has passed the requirements, but could be enforced for further security.");
                    Console.ResetColor();
                    break;
                case 8:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nThe password has all passed the requirements!");
                    Console.ResetColor();
                    break;
                default:
                    Console.WriteLine("The password must be within the specified length, have a mix of UPPERCASE, " +
                        "lowercase letters\nsymbols, numbers and at least one special symbol.");
                    return;
            }
        }

        static void Model()
        {
            // Data types
            const int PASS_MIN_LEN = 12;
            const int PASS_MAX_LEN = 64;
            const int SPLIT_LEN = 4;
            int[] numArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            string numStr = "0123456789";
            char[] numCharArray = numStr.ToCharArray();
            string spcSym = "!\"#¤%&/()=?`§@ £$€{[]}|½´¨'\\<^*~>_-.:,;";
            char[] spcSymArray = spcSym.ToCharArray();
            string userPassIn;
            bool bApprovePass = false;


            do
            {
                Console.WriteLine("Input a password");
                Console.Write("> ");
                //userPassIn = Console.ReadLine();
                userPassIn = "KrisFlyingFisgh38142!";

                if (userPassIn.Length < PASS_MIN_LEN || userPassIn.Length > PASS_MAX_LEN)
                {
                    PassState(6);
                    PassState(0);
                }
                else if (!userPassIn.Any(char.IsUpper) || !userPassIn.Any(char.IsLower))
                {
                    PassState(6);
                    PassState(1);
                }
                else if (!userPassIn.Any(char.IsNumber))
                {
                    PassState(6);
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
                            PassState(6);
                            PassState(3);
                        }
                        break;
                    }
                }
            } while (bApprovePass == false);
            //Console.WriteLine("\nYou passed!");

            // Extra checking (repetition or easily brute forced combinations

            int charChk;
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅÂÊÛÃÕÖÔ";
            string alphabetLower = alphabet.ToLower();
            string colStr = numStr + spcSym + alphabet + alphabetLower;
            char[] colStrToArray = colStr.ToCharArray();
            char[] userPassToArray = userPassIn.ToCharArray();

            for (int chk_0 = 0; chk_0 < userPassToArray.Length; chk_0++)
            {
                charChk = 1;
                for (int chk_1 = chk_0 + 1; chk_1 < userPassToArray.Length; chk_1++)
                {
                    if (userPassToArray[chk_0] == userPassToArray[chk_1])
                    {
                        charChk++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (charChk >= 4)
                {
                    PassState(7);
                    PassState(4);
                }
            }
            //Console.WriteLine("\nBanana!");

            // Checks for sequential numbers
            // Extracts numbers from password
            string emptyStr = string.Empty;
            string userPassNumStr = "";

            for (int numCnt = 0; numCnt < userPassIn.Length; numCnt++)
            {
                if (Char.IsDigit(userPassIn[numCnt]))
                    emptyStr += userPassIn[numCnt];
            }
            if (emptyStr.Length > 0)
                userPassNumStr = emptyStr.Substring(0, SPLIT_LEN);

            //Console.WriteLine("Extracted number: " + userPassNumStr);

            string[] seqNumArray = { "0123", "1234", "2345", "3456", "4567", "5678", "6789", "7890" };
            bool bIsSequential = seqNumArray.Contains(userPassNumStr);

            if (bIsSequential == true)
            {
                PassState(7);
                PassState(5);
            }
            else
            {
                PassState(8);
            }
        }

        static void Main(string[] args)
        {
            View();
            Model();
        }
    }
}