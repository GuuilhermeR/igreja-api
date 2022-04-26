using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace igrejaextensions.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        public string UserId { get; set; }

        [BsonIgnore]
        public bool Exists { get; set; }

        public string Password { get; set; }

        public User(bool exists,
            string userId,
            string password)
        {
            this.Exists = exists;
            this.UserId = userId;
            this.Password = password;
        }
    }
}
