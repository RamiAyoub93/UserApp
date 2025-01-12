using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainApp.Data;
using MainApp.Models;

namespace MainApp.Services
{
    public class UserService
    {
        private List<User> _users = [];
        private readonly FileService _fileService = new FileService();

        public virtual void Add(User user) // Marked as virtual
        {
            _users.Add(user);
            _fileService.SaveListToFile(_users);
        }

        public virtual IEnumerable<User> GetAll() // Marked as virtual
        {
            _users = _fileService.LoadListFromFile();
            return _users;
        }
    }

}
