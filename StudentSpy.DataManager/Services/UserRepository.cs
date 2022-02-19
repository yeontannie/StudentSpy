using StudentSpy.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSpy.Repositories
{
    public class UserRepository : IUserRepository
    {
        //private readonly AppDbContext context;

        //public void Add(User user)
        //{
        //    context.Users.Add(user);
        //    context.SaveChanges();
        //}

        //public User CheckUser(string email)
        //{
        //    return context.Users.SingleOrDefault(x => x.Email == email);
        //}

        //public void Delete(int id)
        //{
        //    context.Users.Remove(GetUserById(id));
        //    context.SaveChanges();
        //}

        //public void Edit(int idToEdit, User userEdit)
        //{
        //    var user = context.Users.Find(idToEdit);
        //    if (user.Name != userEdit.Name)
        //    {
        //        user.Name = userEdit.Name;
        //    }
        //    if (user.LastName != userEdit.LastName)
        //    {
        //        user.LastName = userEdit.LastName;
        //    }
        //    if (user.PhotoPath != userEdit.PhotoPath)
        //    {
        //        user.PhotoPath = userEdit.PhotoPath;
        //    }
        //    if (user.StudyDate != userEdit.StudyDate)
        //    {
        //        user.StudyDate = userEdit.StudyDate;
        //    }
        //    context.SaveChanges();
        //}

        //public IEnumerable<User> GetAll()
        //{
        //    return context.Users;
        //}

        //public User GetById(int id)
        //{
        //    var user = context.Users.Find(id);
        //    if (user == null) throw new KeyNotFoundException("User not found");
        //    return user;
        //}

        //public User GetUserById(int id)
        //{
        //    return context.Users.Find(id);
        //}

        //public IEnumerable<User> Search(string searchPhrase)
        //{
        //    return context.Users.Where(x => x.Name.Contains(searchPhrase)
        //    || x.LastName.Contains(searchPhrase) || x.Email.Contains(searchPhrase)).ToList();
        //}

        //public IEnumerable<User> Sort(string field)
        //{
        //    if (field == "Name")
        //    {
        //        return context.Users.OrderBy(x => x.Name).Reverse();
        //    }
        //    else if (field == "LastName")
        //    {
        //        return context.Users.OrderBy(x => x.LastName).Reverse();
        //    }
        //    return context.Users.OrderBy(x => x.Email).Reverse();
        //}

        //public void Subscribe(int courseId, int studentId)
        //{
        //}

        //public bool UserExists(string name, string email)
        //{
        //    return context.Users.Any(x => x.Name == name && x.Email == email);
        //}
        public void Add(User user)
        {
            throw new NotImplementedException();
        }

        public User CheckUser(string email)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(int idToEdit, User userEdit)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> Search(string searchPhrase)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> Sort(string field)
        {
            throw new NotImplementedException();
        }

        public void Subscribe(int courseId, int studentId)
        {
            throw new NotImplementedException();
        }

        public bool UserExists(string name, string email)
        {
            throw new NotImplementedException();
        }
    }
}
