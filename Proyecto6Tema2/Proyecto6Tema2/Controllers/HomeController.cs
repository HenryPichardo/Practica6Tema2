using Proyecto6Tema2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto6Tema2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            RegistroPelicula rp = new RegistroPelicula();
            return View(rp.RecuperarTodos());
        }
        public ActionResult Grabar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Grabar(FormCollection collection)
        {
            RegistroPelicula rp = new RegistroPelicula();
            Pelicula peli = new Pelicula
            {
                //Codigo = int.Parse(collection["Codigo"]),
                Titulo = collection["Titulo"],
                Director = collection["Director"],
                ActorPrincipal = collection["ActorPrincipal"],
                No_Actores = int.Parse(collection["No_Actores"]),
                Duracion = float.Parse(collection["Duracion"]),
                Estreno = int.Parse(collection["Estreno"]),
            };
            rp.GrabarPelicula(peli);
            return RedirectToAction("Index");
        }
        public ActionResult Borrar(int cod)
        {
            RegistroPelicula peli = new RegistroPelicula();
            peli.Borrar(cod);
            return RedirectToAction("Index");
        }
        public ActionResult Modificacion(int cod)
        {
            RegistroPelicula peli = new RegistroPelicula();
            Pelicula rpt = peli.Recuperar(cod);
            return View(rpt);
        }
        [HttpPost]
        public ActionResult Modificacion(FormCollection collection)
        {
            RegistroPelicula peli = new RegistroPelicula();
            Pelicula rpt = new Pelicula
            {
                //Codigo = int.Parse(collection["Codigo"]),
                Titulo = collection["Titulo"],
                Director = collection["Director"],
                ActorPrincipal = collection["ActorPrincipal"],
                No_Actores = int.Parse(collection["No_Actores"]),
                Duracion = float.Parse(collection["Duracion"].ToString()),
                Estreno = int.Parse(collection["Estreno"]),
            };
            peli.Modificar(rpt);
            return RedirectToAction("Index");
        }



    }
}