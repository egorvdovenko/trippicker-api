using System.Collections.Generic;
using demo_web_api.Entities;

namespace demo_web_api.Models.Account
{
    public class UserModel
    {
        public UserModel()
        { }

        public UserModel(UserEntity user)
        {
            Id = user.Id;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            MiddleName = user.MiddleName;
            PhoneNumber = user.PhoneNumber;
            Confirmed = user.Confirmed;
        }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// E-mail пользователя
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }
        /// <summary>
        /// Номер телефона
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Подтвержден ли пользователь
        /// </summary>
        public bool Confirmed { get; set; }
        public List<int> RegionsIds { get; set; } = new List<int>();
        public List<int> ResponsiblesIds { get; set; } = new List<int>();
        public List<int> RolesIds { get; set; } = new List<int>();
    }
}
