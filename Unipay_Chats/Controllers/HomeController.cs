using Unipay_Chats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chat_unipay.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("Verifier")]
        public ActionResult Verifier(Unipay_Chats.Models.unipay_chat_db user_unipay)
        {
            using (unipay_chatEntities db = new unipay_chatEntities())
            {
                var user_unipay_details = db.unipay_chat_db.Where(x => x.unipay_user == user_unipay.unipay_user && x.password == user_unipay.password).FirstOrDefault();
                if (user_unipay_details == null)
                {
                    user_unipay.LoginErrorMessage = " Oups Login ou mot de passe incorrect";
                    return View("Index", user_unipay);
                }
                else
                {
                    Session["unipay_id"] = user_unipay_details.unipay_id;
                    Session["unipay_user"] = user_unipay_details.unipay_user;
                    Session["email_user"] = user_unipay_details.email_user;
                    Session["telephone"] = user_unipay_details.telephone;
                    Session["nom_complet"] = user_unipay_details.nom_complet;
                    return RedirectToAction("Index", "Accueil");
                }
            }
        }
        public ActionResult Deconnexion()
        {
            int unipay_id = (int)Session["unipay_id"];
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

    }
}