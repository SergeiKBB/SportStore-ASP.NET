using System.Web.Security;
using SportsStore.WebUI.Infrastructure.Abstract;

namespace SportsStore.WebUI.Infrastructure.Concrete {

    public class FormsAuthProvider : IAuthProvider {
        //private  SportsStoreEntities2 db = new SportsStoreEntities2();

        public bool Authenticate(string username, string password) {
            bool result = false;

            //var com = db.Workers;

            //foreach (var item in com)
            //{
            //    if(username == item.Name && password == item.Passsword)
            //    {
            //        result = true;
            //    }
            //}

            //if (username == user && password == pass)
            //{
            //    result = true;
            //}
            //else
            //{
            //    result = false;
            //}
            //bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return result;
        }
    }
}
