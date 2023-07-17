using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _21880159
{
    class Program
    {
        //so luong phep toan
        static int n = 0;
        //chua phep toan
        static LLQueue<string> data = new LLQueue<string>();
        //doc du lieu tu file
        public static void ReadData(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("This file does not exist.");
                Environment.Exit(1);
            }
            string[] lines = File.ReadAllLines(filePath);
            n = int.Parse(lines[0]);
            if (n > 10000000)
            {
                Console.WriteLine("Please input n <= 10^6!");
                Environment.Exit(1);
            }

            for (int i = 1; i < lines.Length; i++)
            {
                data.Enqueue(lines[i]);
            }
        }
        public static void WriteData(string path)
        {
            string[] result = new string[n];

            for (int i = 0; i < n; i++)
            {
                result[i] = ConvertAndCalculate(data.Front()).ToString();
                data.Dequeue();
            }
            try
            {
                File.WriteAllLines(path, result);
                Console.WriteLine("Write successful!");
            }
            catch (Exception)
            {
                Console.WriteLine("Write fail!");
            }
        }
        public static double ConvertAndCalculate(string expression)
        {
            char[] tokens = expression.ToCharArray();
            int len = tokens.Length;
            //chua so
            LLStack<double> values = new LLStack<double>();
            //chua dau
            LLStack<char> ops = new LLStack<char>();
            for (int i = 0; i < len; i++)
            {
                //nếu là khoảng trắng thì bỏ qua
                if (tokens[i] == ' ')
                {
                    continue;
                }
                //neu la so am
                if (tokens[i] == '-' && tokens[i + 1] != ' ')
                {
                    //StringBuilder temp = new StringBuilder();
                    string temp = "";
                    //them dau -
                    // temp.Append(tokens[i++]);
                    temp += tokens[i++];

                    //neu so co nhieu chu so
                    while (i < len && tokens[i] >= '0' && tokens[i] <= '9' || tokens[i] == '.')
                    {
                        // temp.Append(tokens[i++]);
                        temp += tokens[i++];
                    }
                    // Console.WriteLine(double.Parse(temp));
                    values.Push(double.Parse(temp.ToString()));
                    //tra con tro ve truoc 1 index
                    i--;
                }
                //ngược lại nếu ko là số âm
                else if (tokens[i] >= '0' && tokens[i] <= '9')
                {
                    //StringBuilder temp = new StringBuilder();
                    string temp = "";
                    while (i < len && (tokens[i] >= '0' && tokens[i] <= '9' || tokens[i] == '.'))
                    {
                        //temp.Append(tokens[i]);
                        temp += tokens[i];
                        i++;
                    }
                    i--;
                    //nếu là giai thừa
                    if ((i + 1) < len && tokens[i + 1] == '!')
                    {
                        try
                        {
                            double value = factorial(int.Parse(temp.ToString()));
                            values.Push(value);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                    //ngược lại thì thêm vào stack
                    else
                    {
                        //Console.WriteLine(double.Parse(temp.ToString()));
                        values.Push(double.Parse((temp.ToString())));
                    }
                }
                //ngược lại nếu là dấu mở ngoặc
                else if (tokens[i] == '(')
                {
                    //thêm vào stack chứa phép toán
                    ops.Push(tokens[i]);
                }
                //nếu là dấu đóng ngoặc thì tính trong ngoặc trước
                else if (tokens[i] == ')')
                {
                    //tính toán cho đến khi gặp dấu mở ngoặc lấu từ trong stack ra
                    while (ops.Peek() != '(')
                    {
                        char op = ops.Peek();
                        ops.Pop();
                        double a = values.Peek();
                        values.Pop();
                        double b = values.Peek();
                        values.Pop();
                        try
                        {
                            // Console.WriteLine(a);
                            // Console.WriteLine(applyOp(op, a, b));
                            values.Push(applyOp(op, a, b));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                    ops.Pop();
                }
                //ngược lại nếu là các dấu +,-,*,/,^ thì
                else if (tokens[i] == '+' || (tokens[i] == '-' && tokens[i + 1] == ' ') || tokens[i] == '*' || tokens[i] == '/' || tokens[i] == '^')
                {
                    //so sánh với phép tính trước đó có trong stack và tính toán theo độ ưu tiên tương ứng
                    while (!ops.isEmpty() && getPrecedence(tokens[i], ops.Peek()))
                    {
                        char op = ops.Peek();
                        ops.Pop();
                        double a = values.Peek();
                        values.Pop();
                        double b = values.Peek();
                        values.Pop();
                        try
                        {
                            values.Push(applyOp(op, a, b));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        };
                    }
                    ops.Push(tokens[i]);
                }
            }
            //cuối cùng tính toán cho đến khi hết các phép tính
            while (!ops.isEmpty())
            {
                char op = ops.Peek();
                ops.Pop();
                double a = values.Peek();
                values.Pop();
                double b = values.Peek();
                values.Pop();
                values.Push(applyOp(op, a, b));
            }
            double result = values.Peek();
            values.Pop();
            return result;
        }
        public static bool getPrecedence(char op1, char op2)
        {
            if (op2 == '(' || op2 == ')')
                return false;
            if (op1 == '^' || (op1 == '*' || op1 == '/') && (op2 == '+' || op2 == '-'))
                return false;
            else
                return true;
        }
        public static double applyOp(char op, double b, double a)
        {
            switch (op)
            {
                case '^':
                    return Math.Pow(a, b);
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '*':
                    return a * b;
                case '/':
                    if (b == 0)
                        throw new Exception("Cannot divide by zero");
                    return a / b;
            }
            return 0;
        }
        private static double factorial(int a)
        {
            int result = 1;
            if (a < 0)
            {
                throw new Exception("Input have to grater than 0");
            }
            else if (a == 0 || a == 1)
            {
                return 1;
            }
            else
            {
                for (int i = 2; i <= a; i++)
                {
                    result *= i;
                }
            }
            return result;
        }
        static void Main(string[] args)
        {
            //doc file
            string inputFilePath = @"..\..\..\..\..\Data\BIEUTHUC.INP";
            ReadData(inputFilePath);

            //luu file
            string outputFilePath = @"..\..\..\..\..\Data\KETQUA.OUT";
            WriteData(outputFilePath);
        }
    }
}

