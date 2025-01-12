using MainApp.Models;
using MainApp.Services;

namespace MainApp.Dialogs
{
    public class MenuDialogs
    {
        private readonly UserService _userService;

        public MenuDialogs(UserService userService)
        {
            _userService = userService;
        }

        public void MenuOptionsDialog()
        {
            while (true)
            {
                Dialogs.MenuHeading("MAIN MENU");
                Console.WriteLine(" 1. New User");
                Console.WriteLine(" 2. View Users");
                Console.WriteLine("--------------------------------");
                var options = Dialogs.Prompt(" Select Option: ");

                switch (options)
                {
                    case "1":
                        CreateUserDialog();
                        break;
                    case "2":
                        ViewAllUsersDialog();
                        break;
                }
            }
        }

        public void CreateUserDialog()
        {
            Console.Clear();
            User user = new();

            Console.WriteLine("Enter your first name: ");
            user.FirstName = Console.ReadLine()!;

            Console.WriteLine("Enter your last name: ");
            user.LastName = Console.ReadLine()!;

            Console.WriteLine("Enter your email: ");
            user.Email = Console.ReadLine()!;

            Console.WriteLine("Phonnumber: ");
            user.Phonenumber = Console.ReadLine()!;

            Console.WriteLine("Address: ");
            user.Address = Console.ReadLine()!;

            Console.WriteLine("City & ZipCode: ");
            user.ZipCode = Console.ReadLine()!;

            _userService.Add(user);
        }

        public void ViewAllUsersDialog()
        {
            Console.Clear();
            var users = _userService.GetAll();

            foreach (var user in users)
            {
                Console.WriteLine($"{"Id:",-15}{user.Id}");
                Console.WriteLine($"{"Name:",-15}{user.FirstName} {user.LastName}");
                Console.WriteLine($"{"Email:",-15}{user.Email}");
                Console.WriteLine($"{"Phonenumber:",-15}{user.Phonenumber}");
                Console.WriteLine($"{"Address:",-15}{user.Address}");
                Console.WriteLine($"{"Zip Code:",-15}{user.ZipCode}");
                Console.WriteLine("");
            }

            Console.ReadLine();
        }
    }
}
