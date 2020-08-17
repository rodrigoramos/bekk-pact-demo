using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Console;

namespace Consumer.Console
{
    class Program
    {
        private static void Main(string[] args)
        {
            WriteLine("Consumidor Usuários");

            var usersNamesTasks = GetUsersNames();
            usersNamesTasks.Wait();

            WriteLine("Users: {0}", usersNamesTasks.Result.Aggregate((nm1, nm2) => $"{nm1}, {nm2}"));
        }

        static async Task<IEnumerable<string>> GetUsersNames()
        {
            var userApiClent = new UserServiceClient("http://localhost:5000/User");
            var users = await userApiClent.GetUsers();

            return users.Select(x => x.Name);
        }
    }
}