using PracticeFitnessBL;
using PracticeFitnessBL.Controller;
using PracticeFitnessBL.Model;
using System;
using System.Globalization;
using System.Resources;

namespace PracticeFitness.CMD
{
    class Program
    {
        static void Main(string[] args)
        {

            var culture = CultureInfo.CreateSpecificCulture("ru-Ru");
            var resourceManager = new ResourceManager("PracticeFitness.CMD.Languages.Messages", typeof(Program).Assembly);

            Console.WriteLine(resourceManager.GetString("Hello", culture));

            Console.WriteLine(resourceManager.GetString("InputUserName", culture));
            var name = Console.ReadLine();

            var userController = new UserController(name);
            var eatingController = new EatingController(userController.CurrentUser);


            if (userController.IsNewUser)
            {
                Console.Write(resourceManager.GetString("SetGender", culture));
                var gender = Console.ReadLine();
                var birthDate = ParseBirthDate();
                var weight = ParseDouble(resourceManager.GetString("SetWeight", culture));
                var height = ParseDouble(resourceManager.GetString("SetHeight", culture));

                userController.SetNewUserData(gender, birthDate, weight, height);
            }
            Console.WriteLine(userController.CurrentUser);

            Console.WriteLine("Что вы хотите сделать?");
            Console.WriteLine("E - ввести приём пищи");
            var key = Console.ReadKey();

            if(key.Key == ConsoleKey.E)
            {
                var foods = EnterEating();
                eatingController.Add(foods.Food, foods.Weight);

                foreach (var item in eatingController.Eating.Foods)
                {
                    Console.WriteLine($"\t{item.Key} - {item.Value}");
                }
            }
            Console.ReadLine();
        }

        private static (Food Food, double Weight) EnterEating()
        {
            Console.WriteLine("Введите имя продукта:");
            var food = Console.ReadLine();

            var calories = ParseDouble("Калорийность");
            var prot = ParseDouble("Белки");
            var fats = ParseDouble("Жиры");
            var carbs = ParseDouble("Углеводы");

            var weight = ParseDouble("Вес порции");
            var product = new Food(food, calories, prot,fats,carbs);

            return (Food: product, Weight: weight);
        }

        private static DateTime ParseBirthDate()
        {
            DateTime birthDate;
            while (true)
            {
                Console.Write("Введите Дату рождения(01.01.1900: ");
                if (DateTime.TryParse(Console.ReadLine(), out birthDate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный формат даты рождения");
                }
            }

            return birthDate;
        }

        private static double ParseDouble(string name)
        {
            while (true)
            {
                Console.Write($"Введите {name}: ");
                if (double.TryParse(Console.ReadLine(), out double value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine($"Неверный формат {name} ");
                }
            }
        }
    }
}
