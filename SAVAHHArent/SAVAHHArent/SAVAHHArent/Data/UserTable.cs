using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SAVAHHArent.Data
{
    [Table("Users")]
    public class UserTable
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
