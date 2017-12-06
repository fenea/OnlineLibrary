using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Business
{
        public class UserRepository : Domain.Interfaces.IUserRepository
        {
            private readonly IDatabaseContext _databaseService;

            public UserRepository(IDatabaseContext databaseService)
            {
                _databaseService = databaseService;
            }

            public IReadOnlyList<User> GetAllUsers()
            {
                IEnumerable<User> users = _databaseService.Users.Include("User");
                return users.ToList();
            }

            public User GetUserById(Guid id)
            {
            return _databaseService.Users.FirstOrDefault(t => t.IdUser == id);
            }

            public void AddUser(User user)
            {
                _databaseService.Users.Add(user);
                _databaseService.SaveChanges();
            }

            public void EditUser(User user)
            {
                _databaseService.Users.Update(user);
                _databaseService.SaveChanges();
            }

            public void DeleteUser(Guid userid)
            {
                var user = GetUserById(userid);
                _databaseService.Users.Remove(user);
                _databaseService.SaveChanges();
            }


    }
}
