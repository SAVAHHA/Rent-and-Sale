using System;
using System.Collections.Generic;
using System.Text;
using SAVAHHArent.Model;

namespace SAVAHHArent.Data
{
    public static class UserData
    {
        public static IList<User> Users { get; private set; }

        static UserData()
        {
            Users = new List<User>();

            Users.Add(new User { ID_User = 1, Name = "Anna", Age = 19, Login = "savahha", Card = "0000 0000 0000 0000", Liecense = "00 0000", Passport = "4515 00000", Password = "1111", Phone = "+79160249905" });
        }
    }
}
