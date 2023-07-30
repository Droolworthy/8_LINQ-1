using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();

            database.Work();
        }
    }

    class Database
    {
        private readonly List<Criminal> _criminals = new List<Criminal>();
        private readonly Random _random = new Random();
        private readonly string[] _fullName = new string[] { "Петров Петр Петрович", "Иванов Иван Иванович", "Гусева Екатерина Сергеевна", "Кузнецова Анна Денисовна",
            "Орехов Дмитрий Викторович", "Королёв Николай Сергеевич", "Малышев Илья Николаевич", "Целиков Павел Михайлович", "Фролов Дмитрий Алексеевич",
        "Галкин Илья Михайлович", "Толиков Иван Иванович", "Смирнов Владимир Сергеевич", "Сироткина Алина Сергеевна", "Соколов Дмитрий Викторович"};
        private readonly string[] _nationality = new string[] { "Русский", "Американец" };
        private readonly int[] _weight = new int[] { 60, 70, 80 };
        private readonly int[] _height = new int[] { 160, 170, 180 };

        public Database()
        {
            CreateCriminals();
        }        

        public void Work()
        {
            bool isWork = true;

            string commandStartTheProgram = "1";
            string commandExit = "2";

            Console.WriteLine($"Запустить программу - {commandStartTheProgram} \nВыйти из программы - {commandExit}");

            while (isWork)
            {
                Console.Write("\nВвод: ");
                string userInput = Console.ReadLine();

                if(userInput == commandStartTheProgram)
                {
                    StartTheProgram();
                }
                else if(userInput == commandExit)
                {
                    isWork = false;
                }
                else
                {
                    Console.WriteLine("Ошибка. Попробуйте ещё раз.");
                }
            }
        }

        private void StartTheProgram()
        {
            Console.Write("\nВведите вес преступника: ");
            string weightCriminal = Console.ReadLine();

            Console.Write("Введите рост преступника: ");
            string heightCriminal = Console.ReadLine();

            Console.Write("Введите национальность преступника: ");
            string nationalityCriminal = Console.ReadLine();

            if (int.TryParse(weightCriminal, out int weight))
            {
                if (int.TryParse(heightCriminal, out int height))
                {
                    var filteredCriminals = _criminals.Where(criminal => criminal.Weight ==
                    weight).Where(criminal => criminal.Height == height).Where(criminal => criminal.Nationality == 
                    nationalityCriminal).Where(criminal => criminal.IsIncarcerated == true);

                    ShowInfoCriminals(filteredCriminals);
                }
            }
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
            int minimumRandomNumber = 1;
            int maximumRandomNumber = 2;

            bool isIncarcerated = _random.Next(maximumRandomNumber) == minimumRandomNumber;

            return new Criminal(_fullName[_random.Next(_fullName.Length)], _nationality[_random.Next(_nationality.Length)], isIncarcerated,
                _weight[_random.Next(_weight.Length)], _height[_random.Next(_height.Length)]);
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