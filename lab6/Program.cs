using System.IO.Compression;
using System.Security.AccessControl;
using System.Text.RegularExpressions;

namespace lab6
{
    internal class Program
    {
        static string ChooseExample(string text)
        {
            Console.WriteLine("Выберете один из предложенных вариантов:");
            Console.WriteLine("1) В лесу родилась елочка. В лесу она росла. Зимой и летом стройная, зеленая была. ");
            Console.WriteLine("2)   ///  /!,");
            Console.WriteLine("3) ........привет......");
            Console.WriteLine("4) Всем привет");
            Console.WriteLine("5) оавр, цыурт, шцур, Ф34л...");
            Console.WriteLine("\n\n\nДля возврвщения нажмите 0.");
            int answer = ChooseAnswer(0, 5);
            switch (answer)
            {
                case 1:
                    return "В лесу родилась елочка. В лесу она росла. Зимой и летом стройная, зеленая была. ";                   
                case 2:
                    return "   ///  /!,";
                case 3:
                    return "........привет......";
                case 4:
                    return "Всем привет";
                case 5:
                    return " оавр, цыурт, шцур, Ф34л...";
                case 0:
                    return text;
            }
            return text;    
        }
        static void PrintMenu(int i = 0)   //вывод меню
        {
            switch (i)
            {
                case 0:
                    Console.WriteLine("Данная программа работает с текстами, выберете одну из представленных функций:\n\n\n");
                    Console.WriteLine("1) Распечатать текущий текст. \n2) Ввести текст. \n3) Поменять местами первое и последнее слово в тексте.\n\n\n");
                    Console.WriteLine("Для завершения программы введите 0.");
                    break;
                case 1:
                    Console.WriteLine("Вы можете: \n\n\n");
                    Console.WriteLine("1) Ввести текст с клавиатуры. \n2) Выбрать один из предложенных текстов.\n\n\n");
                    Console.WriteLine("Для возвращения на предыдущий этап введите 0.");
                    break;
                case 2:
                    Console.WriteLine("При перестановке слов нужно поменять регистр слов? \n\n\n");
                    Console.WriteLine("1) Да. \n2) Нет.\n\n\n");
                    Console.WriteLine("Для возвращения на предыдущий этап введите 0.");
                    break;
            }
        }
        static string InputText(string text)
        {
            try
            {
                Console.WriteLine("Введите ваш текст: ");
                string newText = Console.ReadLine();
                if (newText == "")
                    return null;
                else
                    return newText;
            }
            catch(Exception)
            {
                Console.WriteLine("Размер текста слишком большой");
                return text;
            }
        }
        static void PrintText(string text)
        {
            Regex space = new Regex(@"\w+");
            if (text != null && space.IsMatch(text))
            {
                Console.WriteLine(text);
            }
            else 
            {
                Console.WriteLine("В тексте нет отображаемых символов.");
            }
        }
        static int ChooseAnswer(int a, int b)   //выбор действия из целых
        {
            int answer = 0;
            bool checkAnswer;
            do
            {
                checkAnswer = int.TryParse(Console.ReadLine(), out answer);
                if ((answer > b || answer < a) || (!checkAnswer))
                {
                    Console.WriteLine("Вы некорректно ввели число, повторите ввод еще раз. Обратите внимание на то, что именно нужно ввести.");
                }
            } while ((answer > b || answer < a) || (!checkAnswer));

            return answer;
        }
        static string ChangeFirstLetter(string word, int way = 0)
        {
            
            char[] letters = word.ToCharArray();
            if (way == 0)
            {
                letters[0] = char.ToUpper(letters[0]);
                return new string(letters);
            }
            else
            {
                letters[0] = char.ToLower(letters[0]);
                return new string(letters);
            }
           
        }
        static string SwapWords(string text, int way = 0)
        {
            Regex space = new Regex(@"\w+");
            if (text != null && space.IsMatch(text))
            {
                Regex regex = new Regex(@"(!)|(\.)|(,)|( )|(;)|(\t)|(\x020)");
                string[] substring = regex.Split(text);
                string temp;
                Regex isWord = new Regex(@"\w+");
                int left = 0;
                int right = substring.Length - 1;
                bool ok = false;

                while (right >= 1 && !ok)
                {
                    ok = isWord.IsMatch(substring[right]);
                    if (!ok)
                        right--;
                }
                if (right!=0)
                {
                    ok = false;
                    while (left <= substring.Length - 1 && !ok)
                    {
                        ok = isWord.IsMatch(substring[left]);
                        if (!ok)
                            left++;
                    }
                    if (right != left)
                    {
                        string newText = null;

                        temp = substring[left];

                        substring[left] = substring[right];
                        substring[right] = temp;

                        if (way == 0)
                        {
                            substring[left] = ChangeFirstLetter(substring[left]);
                            substring[right] = ChangeFirstLetter(substring[right], 1);
                        }

                        foreach (string word in substring)
                        {
                            newText += word;
                        }
                        Console.WriteLine("Текст успешно переделан.");
                        return newText;
                    }
                    else
                    {
                        Console.Clear();
                        string newText = null;
                        if (way == 0)
                            for (int i = 0; i < substring.Length; i++)
                            {
                                if (isWord.IsMatch(substring[i]))
                                {
                                    substring[i] = ChangeFirstLetter(substring[left]);
                                }
                                newText += substring[i];
                            }
                        Console.WriteLine("В тесте только одно слово.");
                        return newText;
                    }
                }
                else 
                {
                    Console.Clear();
                    string newText = null;
                    if (way == 0)
                        for (int i = 0; i < substring.Length; i++)
                        {
                            if (isWord.IsMatch(substring[i]))
                            {
                                substring[i] = ChangeFirstLetter(substring[left]);
                            }
                            newText += substring[i];
                        }
                    Console.WriteLine("В тесте только одно слово.");
                    return newText;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Текст не инициализирован, создайте его для работы с функцией");
                return null;
            }
        }
        static void Main(string[] args)
        {
            bool start = true;
            int answer = 0;
            string text = null;

            while (start)
            {
                PrintMenu();
                answer = ChooseAnswer(0, 3);

                switch(answer)
                {
                    case 1:
                        Console.Clear();
                        PrintText(text);
                        break;
                    case 2:

                        Console.Clear();
                        PrintMenu(1);
                        answer = ChooseAnswer(0, 2);

                        switch (answer)
                        {
                            case 1:
                                Console.Clear();
                                text = InputText(text);
                                Console.Clear();
                                Console.WriteLine("Текст инициализирован");

                                break;
                            case 2:
                                Console.Clear();
                                text = ChooseExample(text);
                                Console.Clear();
                                Console.WriteLine("Текст инициализирован");

                                break;
                            case 0:
                                Console.Clear();
                                break;
                            }
                        
                            break;
                    case 3:
                        if(text!=null && text != "")
                        {


                            Console.Clear();
                            PrintMenu(2);
                            answer = ChooseAnswer(0, 2);

                            switch (answer)
                            {
                                case 1:
                                    Console.Clear();
                                    text = SwapWords(text);
                                         
                                    break;
                                case 2:
                                    Console.Clear();
                                    text = SwapWords(text,1);
                                    break;
                                case 0:
                                    Console.Clear();
                                    break;
                            }                          
                        }
                        break;
                    case 0:
                        start = false;
                        break;
                }

            }
        }
    }
}