using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Trans.Infrastructure.Exceptions;

namespace Trans.Core.Domain
{
    public class User : Entity
    {
        private static readonly Regex EmailRegex = new Regex(@"[a-z0-9]+@[a-z0-9]+\.[a-z0-9]{2,3}");

        private static readonly List<string> _roles = new List<string>()
        {
            "user",
            "admin",
        };
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string Username { get; protected set; }
        public string Fullname { get; protected set; }
        public string Role { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdateAt { get; protected set; }

        protected User()
        {
        }

        public User(Guid userId, string email, string password, string username, string fullname, string salt, string role)
        {
            Id = userId;
            SetEmail(email.ToLowerInvariant());
            SetPassword(password, salt);
            SetUsername(username);
            SetFullname(fullname);
            SetRole(role);
            CreatedAt = DateTime.UtcNow;
        }

        public void SetEmail(string email)
        {
            if(String.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Put data is incorect.");
            }
            if (!EmailRegex.IsMatch(email))
            {
                throw new DomainException(ErrorCodes.InvalidEmail, "Invalid Email");
            }
            if(email == Email)
            {
                return;
            }
            Email = email;
            UpdateAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, string salt)
        {
            if(string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Put data is incorect.");
            }
            if(Password == password)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new Exception("Put data is incorect.");
            }
            Password = password;
            Salt = salt;
            UpdateAt = DateTime.UtcNow;
        }

        public void SetUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new Exception("Put data is incorect.");
            }
            if(Username == username)
            {
                return;
            }
            Username = username;
            UpdateAt = DateTime.UtcNow;
        }

        public void SetFullname(string fullname)
        {
            if (string.IsNullOrWhiteSpace(fullname))
            {
                throw new Exception("Put data is incorect.");
            }
            if (Fullname == fullname)
            {
                return;
            }
            Fullname = fullname;
            UpdateAt = DateTime.UtcNow;
        }

        public void SetRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new DomainException(ErrorCodes.InvalidRole,$"Role: {role} is incorect.");
            }
            if (!_roles.Contains(role))
            {
                throw new DomainException(ErrorCodes.InvalidRole,$"Role: {role} is incorect.");
            }
            if (Role == role)
            {
                return;
            }
            Role = role;
            UpdateAt = DateTime.UtcNow;
        }
    }
}