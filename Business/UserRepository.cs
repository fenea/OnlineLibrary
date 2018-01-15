using System;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Persistance;

namespace Business
{
    public class UserRepository : IUserRepository
    {

        private DatabaseContext _db;
        private readonly UserManager<User> _userManager;

        public UserRepository(DatabaseContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public User GetUserById()
        {
            throw new NotImplementedException();
        }

        /*
        public User GetUserById()
        {
            var idUser = _userManager.GetUserId(User);
            var currentUser = _db.Users.Find(idUser);
            return currentUser;

        }
        */


    }
}
