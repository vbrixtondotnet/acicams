using ACIC.AMS.DataStore.Interfaces;
using ACIC.AMS.Dto;
using ACIC.AMS.Repository;
using ACIC.AMS.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACIC.AMS.DataStore
{
    public class UserDataStore : BaseDataStore, IUserDataStore
    {
        public UserDataStore(ACICDBContext context, IMapper mapper) : base(context, mapper)
        {
            
        }
        public User GetUserById(int id)
        {
            User retval = null;
            var dbUser = this._context.User.Where(u => u.Id == id).FirstOrDefault();

            if (dbUser != null)
            {
                retval = _mapper.Map<Dto.User>(dbUser);
                retval.Password = EncryptionService.Decrypt(retval.Password);
            }

            return retval;
        }

        public User Login(string emailAddress, string password)
        {
            User retval = null;
            string encryptedPassword = EncryptionService.Encrypt(password);
            var dbUser = this._context.User.Where(u => u.EmailAddress == emailAddress && u.Password == encryptedPassword).FirstOrDefault();

            if (dbUser != null)
            {
                retval = _mapper.Map<User>(dbUser);
            }

            return retval;
        }

        public Dto.User SaveUser(Dto.User user)
        {
            Domain.Models.User dbUser = _mapper.Map<Domain.Models.User>(user);
            dbUser.Password = EncryptionService.Encrypt(dbUser.Password);
            if (user.Id != 0)
            {
                Domain.Models.User dbUserOrig = _context.User.Where(u => u.Id == user.Id).AsNoTracking().FirstOrDefault();
                dbUser.DateModified = DateTime.Now;
                dbUser.DateCreated = dbUserOrig.DateCreated;
                _context.User.Update(dbUser);
            }
            else
            {
                dbUser.DateCreated = DateTime.Now;
                _context.User.Add(dbUser);
                user.Id = dbUser.Id;
            }

            _context.SaveChanges();
            return user;
        }

        

        public List<User> GetUsers(bool deleted = false)
        {
            List<User> retval = new List<User>();

            List<Domain.Models.User> dbUsers = _context.User.Where(u => u.Active == !deleted).ToList();

            dbUsers.ForEach( u => {
                retval.Add(_mapper.Map<Dto.User>(u));
            });

            return retval;
        }

        public bool SetActive(int id, bool active)
        {
            var dbUser = _context.User.Where(u => u.Id == id).FirstOrDefault();
            if(dbUser != null)
            {
                dbUser.Active = active;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteUser(int id)
        {
            var dbUser = _context.User.Where(u => u.Id == id).FirstOrDefault();
            if (dbUser != null)
            {
                _context.User.Remove(dbUser);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
