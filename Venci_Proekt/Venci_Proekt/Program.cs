using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Venci_Proekt
{
    class Program
    {

        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("Please write which command you want to use from 1 to 6:");
                string command = Console.ReadLine();
                if (command == "1")
                {
                    Option1();
                }
                else if (command == "2")
                {
                    Option2();
                }
                else if (command == "3")
                {
                    Option3();
                }
                else if (command == "4")
                {
                    Option4();
                }
                else if (command == "5")
                {
                    Option5();
                }
                else if (command == "6")
                {
                    break;
                }
            }
        }

        static void Option1()
        {
            Console.WriteLine("Write your file name:");
            string fileName = Console.ReadLine();
            string[] input = File.ReadAllLines(fileName);
            List<string> output = new List<string>();

            int outputSum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                outputSum += int.Parse(input[i]);
            }
            output.Add(outputSum.ToString());

            double outputAritmetichno = outputSum / input.Length;
            output.Add(outputAritmetichno.ToString());
            int numMax = int.Parse(input[0]);
            int secondMax = 0;
            for (int i = 1; i < input.Length; i++)
            {
                int num = int.Parse(input[i]);
                if (numMax < num)
                {
                    secondMax = numMax;
                    numMax = num;
                }

            }
            output.Add(numMax.ToString());
            output.Add(secondMax.ToString());


            int numMin = int.Parse(input[0]);
            int secondMin = 0;
            for (int i = 1; i < input.Length; i++)
            {
                int num = input.Length;
                if (numMin > num)
                {
                    secondMin = numMin;
                    numMin = num;
                }
            }
            output.Add(numMin.ToString());
            output.Add(secondMin.ToString());

            int[] sort = input.Select(int.Parse).ToArray();
            for (int i = 0; i < sort.Length - 1; i++)
            {
                if (sort[i] > sort[i + 1])
                {
                    int first = sort[i];
                    sort[i] = sort[i + 1];
                    sort[i + 1] = first;
                }
            }
            List<string> sort1 = new List<string>();
            foreach (var item in sort)
            {
                sort1.Add(item.ToString());
            }

            foreach (var item in output)
            {
                Console.WriteLine(item);
            }
            foreach (var item in sort)
            {
                Console.WriteLine(item);
            }

        }
        static void Option2()
        {
            Console.WriteLine("Write your file");
            string fileName = Console.ReadLine();

            Dictionary<string, int> save = new Dictionary<string, int>();


            string text = File.ReadAllText(fileName);

            string[] words = text.Split(' ');
            foreach (var item in words)
            {
                if (!save.ContainsKey(item))
                {
                    save.Add(item, 0);
                }
                save[item]++;

            }

            foreach (var item in save)
            {
                Console.WriteLine($"{item.Key} -> {item.Value}");
            }

            string[] lower = new string[words.Length];

            var dictionary = new Dictionary<string, int>();
            for (int i = 0; i < words.Length; i++)
            {
                lower[i] = words[i].ToLower();
            }

            foreach (var item in lower)
            {
                if (!dictionary.ContainsKey(item))
                {
                    dictionary.Add(item, 0);
                }
                dictionary[item]++;

            }

            foreach (var item in dictionary)
            {
                Console.WriteLine($"{item.Key} -> {item.Value}");
            }


        }
        static void Option3()
        {
            Console.WriteLine("Write your file name:");
            string fileName = Console.ReadLine();
            string input = File.ReadAllText(fileName);

            Stack<char> skoba = new Stack<char>();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(' || input[i] == '{' || input[i] == '[')
                {
                    skoba.Push(input[i]);
                }
                else if (input[i] == ')' || input[i] == '}' || input[i] == ']')
                {
                    skoba.Pop();
                }

            }
            if (skoba.Count() == 0)
            {
                Console.WriteLine("OK");
            }
            else if (skoba.Count() > 0 || skoba.Count() < 0)
            {
                Console.WriteLine("Not OK");
            }
        }
        static void Option4()
        {
            Console.WriteLine("Write your file name:");
            string fileName = Console.ReadLine();
            Dictionary<char, double> dic = new Dictionary<char, double>();
            string file = File.ReadAllText(fileName);

            for (int i = 0; i < file.Length; i++)
            {
                if (!dic.ContainsKey(file[i]))
                {
                    dic.Add(file[i], 0);
                }
                dic[file[i]]++;
            }
            foreach (var item in dic)
            {
                Console.WriteLine($"{item.Key} -> {item.Value / file.Length * 100:F2}%");
            }


        }
        public static void Option5()
        {
            Console.Write("Please write your file : ");
            string fileName = Console.ReadLine();
            string[] input = File.ReadAllLines(fileName);
            for (int i = 0; i < input.Length; i++)
            {
                int output = Calculation(input[i]);
                input[i] += " = " + output;
            }
            File.WriteAllLines(@"haralampi.txt", input);
        }
        static int Calculation(string a)
        {
            string[] input = a.Split();
            Queue<string> output = new Queue<string>();
            Stack<string> @operator = new Stack<string>();
            for (int i = 0; i < input.Length; i++)
            {
                int temp;
                if (int.TryParse(input[i], out temp))
                {
                    output.Enqueue(input[i]);
                }
                else if (input[i] == "+" || input[i] == "-" || input[i] == "*" || input[i] == "/")
                {
                    while (@operator.Count > 0 && ((@operator.Peek() == "*" || @operator.Peek() == "/") || @operator.Peek() == "-") && @operator.Peek() != "(")
                    {
                        output.Enqueue(@operator.Pop());
                    }
                    @operator.Push(input[i]);
                }
                else if (input[i] == "*" || input[i] == "/")
                {
                    while (@operator.Count > 0 && @operator.Peek() == "/" && @operator.Peek() != "(")
                    {
                        output.Enqueue(@operator.Pop());
                    }
                    @operator.Push(input[i]);
                }
                else if (input[i] == "(")
                {
                    @operator.Push(input[i]);
                }
                else if (input[i] == ")")
                {
                    while (@operator.Peek() != "(")
                    {
                        output.Enqueue(@operator.Pop());
                    }
                    @operator.Pop();
                }
            }
            while (@operator.Count > 0)
            {
                output.Enqueue(@operator.Pop());
            }
            string RPN = "";
            while (output.Count > 1)
            {
                RPN += output.Dequeue() + " ";
            }
            RPN += output.Dequeue();
            int finalOutput = Program.Calculator(RPN);
            return finalOutput;
        }
        static int Calculator(string read)
        {
            string[] input = read.Split();
            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < input.Length; i++)
            {
                int temp;
                if (int.TryParse(input[i], out temp))
                {
                    stack.Push(temp);
                }
                else
                {
                    if (input[i] == "-")
                    {
                        int minus = stack.Pop();
                        stack.Push(stack.Pop() - minus);
                    }
                    else if (input[i] == "+")
                    {
                        int plus = stack.Pop();
                        stack.Push(stack.Pop() + plus);
                    }
                    else if (input[i] == "*")
                    {
                        int times = stack.Pop();
                        stack.Push(stack.Pop() * times);
                    }
                    else if (input[i] == "/")
                    {
                        int divide = stack.Pop();
                        stack.Push(stack.Pop() / divide);
                    }
                }
            }

            return stack.Pop();
        }
    }
}


