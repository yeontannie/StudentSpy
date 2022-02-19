using StudentSpy.Core;

namespace StudentSpy.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        IEnumerable<User> Sort(string field);
        void Edit(int idToEdit, User userEdit);
        void Delete(int id);
        User GetUserById(int id);
        IEnumerable<User> Search(string searchPhrase);
        bool UserExists(string name, string email);
        User CheckUser(string email);
        void Subscribe(int courseId, int studentId);
    }
}