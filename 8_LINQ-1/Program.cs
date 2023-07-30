using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ1
{
    internal class Program
    {
        static void Main()
        {
            Database database = new Database();

            database.Work();
        }
    }

    class Database
    {
        private readonly List<Criminal> _criminals = new List<Criminal>();
        private readonly Random _random = new Random();

        public Database()
        {
            CreateCriminals();
        }

        public void Work()
        {
            bool isWork = true;

            string commandStartProgram = "1";
            string commandExit = "2";

            Console.WriteLine($"Запустить программу - {commandStartProgram} \nВыйти из программы - {commandExit}");

            while (isWork)
            {
                Console.Write("\nВвод: ");
                string userInput = Console.ReadLine();

                if (userInput == commandStartProgram)
                {
                    Console.Write("\nВведите вес преступника: ");
                    string weightCriminal = Console.ReadLine();

                    int weight = GetNumberFromUser(weightCriminal);

                    Console.Write("Введите рост преступника: ");
                    string heightCriminal = Console.ReadLine();

                    int height = GetNumberFromUser(heightCriminal);

                    Console.Write("Введите национальность преступника: ");
                    string nationalityCriminal = Console.ReadLine().ToLower();

                    var filteredCriminals = _criminals.Where(criminal => criminal.Weight ==
                    weight).Where(criminal => criminal.Height == height).Where(criminal => criminal.Nationality.ToLower() ==
                    nationalityCriminal).Where(criminal => criminal.IsIncarcerated == true);

                    ShowInfoCriminals(filteredCriminals);
                }
                else if (userInput == commandExit)
                {
                    isWork = false;
                }
                else
                {
                    Console.WriteLine("Ошибка. Попробуйте ещё раз.");
                }
            }
        }

        private int GetNumberFromUser(string userInput) 
        {
            if (int.TryParse(userInput, out int number))
            {
                return number;
            }

            return 0;
        }

        private void CreateCriminals()
        {
            int numberCriminals = 100;

            for (int i = 0; i < numberCriminals; i++)
            {
                _criminals.Add(GetCriminal());
            }
        }

        private void ShowInfoCriminals(IEnumerable<Criminal> filteredCriminals)
        {
            foreach (var criminal in filteredCriminals)
            {
                Console.WriteLine($"ФИО - {criminal.FullName}, Рост - {criminal.Height}" +
                  $", Вес - {criminal.Weight}, Национальность - {criminal.Nationality}.");
            }
        }

        private Criminal GetCriminal()
        {
            string[] fullName = new string[] { "Петров Петр Петрович", "Иванов Иван Иванович", "Гусева Екатерина Сергеевна", "Кузнецова Анна Денисовна",
            "Орехов Дмитрий Викторович", "Королёв Николай Сергеевич", "Малышев Илья Николаевич", "Целиков Павел Михайлович", "Фролов Дмитрий Алексеевич",
        "Галкин Илья Михайлович", "Толиков Иван Иванович", "Смирнов Владимир Сергеевич", "Сироткина Алина Сергеевна", "Соколов Дмитрий Викторович"};
            string[] nationality = new string[] { "Русский", "Американец" };

            int[] weight = new int[] { 60, 70, 80 };
            int[] height = new int[] { 160, 170, 180 };
            int minimumRandomNumber = 1;
            int maximumRandomNumber = 2;

            bool isIncarcerated = _random.Next(maximumRandomNumber) == minimumRandomNumber;

            return new Criminal(fullName[_random.Next(fullName.Length)], nationality[_random.Next(nationality.Length)], isIncarcerated,
                weight[_random.Next(weight.Length)], height[_random.Next(height.Length)]);
        }
    }

    class Criminal
    {
        public Criminal(string completeName, string origin, bool isImprisoned, int mass, int growth)
        {
            FullName = completeName;
            Nationality = origin;
            IsIncarcerated = isImprisoned;
            Weight = mass;
            Height = growth;
        }

        public string FullName { get; private set; }

        public string Nationality { get; private set; }

        public bool IsIncarcerated { get; private set; }

        public int Weight { get; private set; }

        public int Height { get; private set; }
    }
}
