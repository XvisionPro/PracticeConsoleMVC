using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeFitnessBL
{
    [Serializable]
    public class Food
    {
        public string Name { get; }
        /// <summary>
        /// Белки
        /// </summary>
        public double Proteins { get; set; }

        /// <summary>
        /// Жиры
        /// </summary>
        public double Fats { get; }

        /// <summary>
        /// Углеводы
        /// </summary>
        public double Carbohydrates { get; }

        /// <summary>
        /// Калории за 100 грамм продукта
        /// </summary>
        public double Calories { get; }

        /// <summary>
        /// Калории за 1 грамм продукта
        /// </summary>
        private double CaloriesOneGram { get { return Calories / 100.0; } }
        /// <summary>
        /// Жиры за грамм
        /// </summary>
        private double ProteinsOneGram { get { return Proteins / 100.0; } }
        private double FatsOneGram { get { return Fats / 100.0; } }
        private double CarbohydratesOneGram { get { return Carbohydrates / 100.0; } }

        public Food(string name) : this(name, 0,0,0,0)
        {
        }

        public Food(string name, double calories, double proteins, double fats, double carbohydrates)
        {
            // TODO: Checkout

            Name = name;
            Calories = calories / 100.0;
            Proteins = proteins / 100.0;
            Fats = fats / 100.0;
            Carbohydrates = carbohydrates / 100.0;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
