using System.Linq;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers {

    public class CartController : Controller {
        private IProductRepository repository;
        private SSEntities db = new SSEntities();
                                                                                                                                                                                                                                                            //private IOrderProcessor orderProcessor;
        public CartController(IProductRepository repo)
        {                                                                                                                                                                                                                                                   //, IOrderProcessor proc
            repository = repo;                                                                                                                                                                                                                              //orderProcessor = proc;
        }


        public ViewResult Index(Cart cart, string returnUrl) {
            if (Session["user"] != null)
            {
                ViewBag.ID = (int)Session["user"];
            }
            ViewBag.Return = Session["return"];
            if (Session["Check"] == null)
            {
                return View(new CartIndexViewModel
                {
                    ReturnUrl = returnUrl,
                    Cart = cart
                });
            }
            else if((int)Session["Check"] == 1)
            {
                cart.Clear();
                Session["Check"] = 0;
            }

            return View(new CartIndexViewModel
            {
                ReturnUrl = returnUrl,
                Cart = cart
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId,
                string returnUrl) {
            ViewBag.Return = null;
            Session["Check"] = 0;
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            foreach (var item in cart.Lines)
            {
                if(item.Product.ProductID == productId)
                {
                    Session["message"] = "The given product has been added to the basket";
                    return RedirectToAction("List", "Product");
                }
            }
            if (product != null) {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId,
                string returnUrl) {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null) {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart) {
            return PartialView(cart);
        }

        public ActionResult Checkout(int id, Cart cart, string returnUrl)
        {

            var list = cart.Lines.ToList();
            Order order = new Order { UserID = id };
            int count = list.Count;

            if (count == 0)
            {
                Session["return"] = "Please add product to basket";
                Session["Check"] = 0;
            }
            else
            {
                Session["return"] = "Thank you for your order";
                Session["Check"] = 1;
                foreach (var el in list)
                {
                    Product prod = db.Products.Find(el.Product.ProductID);
                    prod.Orders.Add(order);
                    db.Products.Find(el.Product.ProductID).Orders.Add(order);
                }
                db.SaveChanges();
                cart = new Cart();

            }
            return RedirectToAction("Index", new { returnUrl });
        }

    }
}
