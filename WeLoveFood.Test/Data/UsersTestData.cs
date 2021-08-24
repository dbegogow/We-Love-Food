using WeLoveFood.Data.Models;

namespace WeLoveFood.Test.Data
{
    public static class UsersTestData
    {
        public static Client GetClient()
        {
            var clientUser = new User { Id = "ClientId", UserName = "Client" };
            var client = new Client { User = clientUser };

            return client;
        }

        public static Manager GetManager()
        {
            var managerUser = new User { UserName = "Manager" };
            var manager = new Manager { User = managerUser };

            return manager;
        }
    }
}
