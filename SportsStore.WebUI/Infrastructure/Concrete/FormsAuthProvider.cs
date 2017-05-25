using System.Web.Security;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Infrastructure.Concrete {

    public class FormsAuthProvider : IAuthProvider {
        private SSEntities  db = new SSEntities();

        public bool Authenticate(string username, string password) {
            bool result = false;

            var com = db.Workers;

            foreach (var item in com)
            {
                if (username == item.Name && password == item.Passsword)
                {
                    result = true;
                }
            }
            
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return result;
        }
    }
}
