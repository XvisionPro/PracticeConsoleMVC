using PracticeFitnessBL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace PracticeFitnessBL.Controller
{
    /// <summary>
    /// Контроллер пользователя.
    /// </summary>
    public class UserController
    {
        /// <summary>
        /// Пользователь приложения.
        /// </summary>
        public List<User> Users { get; }
        public User CurrentUser { get; }

        public bool IsNewUser { get; } = false;
        /// <summary>
        /// Создание нового пользоватля.
        /// </summary>
        /// <param name="userName">Пользователь.</param>
        public UserController(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException("Имя пользователя не может быть пустым", nameof(userName));
            }

            Users = GetUsersData();

            CurrentUser = Users.SingleOrDefault(u => u.Name == userName);
            
            if(CurrentUser == null)
            {
                CurrentUser = new User(userName);
                Users.Add(CurrentUser);
                IsNewUser = true;
                Save();
            }
        }
        /// <summary>
        /// Сохранить данные пользователя.
        /// </summary>
        public void Save()
        {
            var formatter = new BinaryFormatter();

            using (var fs = new FileStream("users.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Users);
            }
        }
        /// <summary>
        /// Получить список пользователей.
        /// </summary>
        /// <returns>Пользователь приложения.</returns>
        private List<User> GetUsersData()
        {
            var formatter = new BinaryFormatter();

            using (var fs = new FileStream("users.dat", FileMode.OpenOrCreate))
            {
               if(formatter.Deserialize(fs) is List<User> users)
               {
                    return users;
               }
               else
               {
                   return new List<User>();
               }
            }
        }

        public void SetNewUserData(string genderName, DateTime birtDate, double weight = 1, double height = 1)
        {
            // Проверка

            CurrentUser.Gender = new Gender(genderName);
            CurrentUser.BirthDate = birtDate;
            CurrentUser.Weight = weight;
            CurrentUser.Height = height;
            Save();
        }
    }
}
