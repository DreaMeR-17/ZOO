using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZOO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ZooFactory factory = new ZooFactory();

            Zoo zoo = factory.Create();
            zoo.Work();
        }
    }

    class ZooFactory
    {
        private int _minimumAviaryCapacity = 3;
        private int _maximumAviaryCapacity = 10;

        private List<Animal> _animals = new List<Animal>();

        public ZooFactory()
        {
            AddAnimals();
        }

        public Zoo Create()
        {
            List<Aviary> aviaries = new List<Aviary>();

            for (int i = 0; i < _animals.Count; i++)
            {
                aviaries.Add(new Aviary(_animals[i].Name, CreateAnimalList(_animals[i])));
            }

            return new Zoo(aviaries);
        }

        private void AddAnimals()
        {
            _animals.Add(new Animal("Тигр", "рычит"));
            _animals.Add(new Animal("Ленивец", "сопит"));
            _animals.Add(new Animal("Змея", "шипит"));
            _animals.Add(new Animal("Волк", "воет"));
        }

        private List<Animal> CreateAnimalList(Animal animal)
        {
            List<Animal> animals = new List<Animal>();

            int generatedAviaryCapacity = UserUtils.GenerateRandomNumber(_minimumAviaryCapacity, _maximumAviaryCapacity + 1);

            for (int i = 0; i < generatedAviaryCapacity; i++)
            {
                animals.Add(new Animal(animal.Name, animal.Sound));
            }

            return animals;
        }
    }

    class Zoo
    {
        private List<Aviary> _aviaries = new List<Aviary>();

        public Zoo(List<Aviary> aviaries)
        {
            _aviaries = new List<Aviary>(aviaries);
        }

        public void Work()
        {
            Console.WriteLine("ЗООПАРК");

            ShowAviaries();

            Console.Write("\nВведите номер загона, который вы хотели бы посетить: ");

            _aviaries[GetAviariesIndex()].ShowInfo();

            Console.ReadKey();
        }

        private void ShowAviaries()
        {
            int index = 1;

            for (int i = 0; i < _aviaries.Count; i++)
            {
                Console.WriteLine($"{index}) {_aviaries[i].Name}");

                index++;
            }
        }

        private int GetAviariesIndex()
        {
            int number;

            while (Int32.TryParse(Console.ReadLine(), out number) == false || number > _aviaries.Count || number <= 0)
            {
                Console.Write("\nНеверный ввод! Повторите: ");
            }

            return number - 1;
        }
    }

    class Aviary
    {
        private List<Animal> _animals = new List<Animal>();

        public Aviary(string name, List<Animal> animals)
        {
            _animals = new List<Animal>(animals);

            Name = name;
        }

        public string Name { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"\nПеред собой вы видите {Name}, в нем находятся {_animals.Count} особи\n");

            foreach (var animal in _animals)
            {
                Console.WriteLine($"{animal.Name}, {animal.Gender}, {animal.Sound}");
            }
        }
    }

    class Animal
    {
        public Animal(string name, string sound)
        {
            Name = name;
            Gender = GenerateRandomGender();
            Sound = sound;
        }

        public string Name { get; private set; }
        public string Gender { get; private set; }
        public string Sound { get; private set; }

        private string GenerateRandomGender()
        {
            List<string> genders = new List<string> { "самец", "самка" };

            return genders[UserUtils.GenerateRandomNumber(genders.Count)];
        }
    }

    class UserUtils
    {
        private static Random s_random = new Random();

        public static int GenerateRandomNumber(int minValue, int maxValue)
        {
            return s_random.Next(minValue, maxValue);
        }

        public static int GenerateRandomNumber(int maxValue)
        {
            return s_random.Next(maxValue);
        }
    }
}
