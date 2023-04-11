using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

public class Calculator
{
    public static void Main(string[] args)
    {
        calculator();
    }

    public static void Welcome()
    {
        Console.WriteLine("Mayesha's Basic Calculator");
        Console.WriteLine("Arithmatic Operators:\nAddition: +\nSubtraction: -\nMultiplication: *\nPower Of: ^");
        Console.WriteLine("Sample Input:\n >> \n08 + 27954 - 16 * 4 ^ 2 / 4\n\n");
    }

    public static char initiate()       //confirm if user wants to start
    {
        char answer;
        while (true)
        {
            Console.WriteLine("Take input? y/n");
            try
            {
                answer = Convert.ToChar(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Input. Please Try Again.");
                continue;
            }
            if (answer == 'y' || answer == 'n')
                return answer;
            Console.WriteLine("Invalid Input. Please Try Again.");
            continue;
        }
    }

    public static void calculator()
    {
        Welcome();
        char answer = initiate();
        while (true)
        {
            string[] input = checkInputString(answer);
            if (input == "quit".Split())
            {
                Console.WriteLine("Exiting...");
                break;
            }
            else
            {
                if (checkOperatorSyntax(input) == false)
                {
                    Console.WriteLine("Invalid syntax. Please try again.");

                }
                Console.WriteLine(calculate(input));    
                continue;
            }
        }
    }

    public static string[] checkInputString(char answer)        //check input validity. returns a valid input or "quit" string[]
    {
        while (true)
        {
            if (answer == 'y')
            {
                Console.WriteLine(">> ");
                string[] input = Console.ReadLine().Split();
                int i = 0;
                int length = input.Length;
                if (Regex.IsMatch(input[0], @"[0-9]"))
                {
                    for (i = 1; i < length; i++)
                    {
                        if (Regex.IsMatch(input[i], @"[0-9-+*/]") == false)
                        {
                            Console.WriteLine("Invalid input. Please try again.");
                            continue;
                        }
                    }
                    return input;
                }
                Console.WriteLine("Follow sample input syntax. Please try again.");
                continue;
            }
            else if (answer == 'n')
            {
                string[] quit = "quit".Split();
                return quit;
            }
            Console.WriteLine("Invalid input. Please try again.");
            continue;
        }
    }

    public static bool checkOperatorSyntax(string[] input)   //checks input for operator having numbers before and after it
    {
        int length = input.Length;
        bool flag = Regex.IsMatch(input[0], @"[0-9]");
        if (flag == false)
            return false;
        int i = 0;
        for (i = 1; i < length; i++)
        {
            if (input[i] == "+" || input[i] == "-" || input[i] == "*" || input[i] == "/" || input[i] == "^")
            {
                try
                {
                    double before = Convert.ToDouble(input[i - 1]);
                    double after = Convert.ToDouble(input[i + 1]);
                    flag = true;
                }
                catch
                {
                    flag = false;
                }
            }
        }
        return flag;
    }

    public static string calculate(string[] input)
    {
        string[] pdmas = "^ / * + -".Split();
        bool flag = false;
        while (flag==false)
        {
            int i = 1;
            //power calculation
            for (i = 1; i < input.Length; i++)
            {
                if (input[i] == pdmas[0] && i < input.Length)
                {
                    double num1 = Convert.ToDouble(input[i - 1]);
                    double num2 = Convert.ToDouble(input[i + 1]);
                    input[i] = Convert.ToString(Math.Pow(num1, num2));
                    input[i - 1] = "$";
                    input[i + 1] = "$";
                    input = input.Where(val => val != "$").ToArray();
                }
            }
            //divide calculation
            for (i = 1; i < input.Length; i++)
            {
                if (input[i] == pdmas[1] && i < input.Length)
                {
                    double num1 = Convert.ToDouble(input[i - 1]);
                    double num2 = Convert.ToDouble(input[i + 1]);
                    input[i] = Convert.ToString(num1 / num2);
                    input[i - 1] = "$";
                    input[i + 1] = "$";
                    input = input.Where(val => val != "$").ToArray();
                }
            }
            //multiplication calculation
            for (i = 1; i < input.Length; i++)
            {
                if (input[i] == pdmas[2] && i < input.Length)
                {
                    double num1 = Convert.ToDouble(input[i - 1]);
                    double num2 = Convert.ToDouble(input[i + 1]);
                    input[i] = Convert.ToString(num1 * num2);
                    input[i - 1] = "$";
                    input[i + 1] = "$";
                    input = input.Where(val => val != "$").ToArray();
                }
            }
            //addition calculation
            for (i = 1; i < input.Length; i++)
            {
                if (input[i] == pdmas[3] && i < input.Length)
                {
                    double num1 = Convert.ToDouble(input[i - 1]);
                    double num2 = Convert.ToDouble(input[i + 1]);
                    input[i] = Convert.ToString(num1 + num2);
                    input[i - 1] = "$";
                    input[i + 1] = "$";
                    input = input.Where(val => val != "$").ToArray();
                }
            }
            //subtraction calculation
            for (i = 1; i < input.Length; i++)
            {
                if (input[i] == pdmas[4] && i < input.Length)
                {
                    double num1 = Convert.ToDouble(input[i - 1]);
                    double num2 = Convert.ToDouble(input[i + 1]);
                    input[i] = Convert.ToString(num1 - num2);
                    input[i - 1] = "$";
                    input[i + 1] = "$";
                    input = input.Where(val => val != "$").ToArray();
                }
            }
            //check if full string has been calculated or not
            for(int c = 0; c < input.Length; c++)
            {
                if (Regex.IsMatch(input[c],@"[/^/*+-]"))
                {
                    flag = false;
                    break;
                }
                flag = true;
            }
        }

        //returning result as string
        string result = "";
        for(int j = 0; j < input.Length; j++)
        {
            result += input[j];
        }
        return result;
    }

    public static string[] subtract(string[] input, int i)
    {
        int length = input.Length;
        double result = 0;

        input[i] = " ";
        int j = i;
        //extract number before symbol
        string num1 = "";
        for (j = i - 1; j >= 0; j--)
        {
            if (Regex.IsMatch(input[j], @"[0-9]") == true)
            {
                num1 += input[j];
                input[j] = " ";
            }
            else break;
        }
        double number1 = Convert.ToDouble(num1);
        //extract number after symbol
        string num2 = "";
        for (j = i + 1; j < length; j++)
        {
            if (Regex.IsMatch(input[j], @"[0-9]") == true)
            {
                num2 += input[j];
                input[j] = " ";
            }
            else break;
        }
        double number2 = Convert.ToDouble(num2);

        result = number1 - number2;

        //replace " " with result in input string
        int k = 0;
        for (k = 0; k < length; k++)
        {
            if (k == 0)
            {
                if (input[k] == " ")
                    input[k] = Convert.ToString(result);
            }
            if (k != 0)
            {
                bool flag = Regex.IsMatch(input[k - 1], @"[/^/*+-]");
                if (input[k] == " " && flag == true)
                    input[k] = Convert.ToString(result);
                if (input[k] == " " && flag == false)
                    input[k] = "";
            }
        }
        return input;
    }
}

