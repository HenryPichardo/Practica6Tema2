using ProyectoPropuestoPractica6Tema2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoPropuestoPractica6Tema2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            RegistroProducto rp = new RegistroProducto();
            return View(rp.RecuperarTodos());
        }
        public ActionResult Grabar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Grabar(FormCollection collection)
        {
            RegistroProducto rp = new RegistroProducto();
            Producto prod = new Producto
            {                
                Descripcion = collection["Descripcion"],
                Tipo = collection["Tipo"],
                Precio = double.Parse(collection["Precio"]),
               
            };
            rp.GrabarProducto(prod);
            return RedirectToAction("Index");
        }
        public ActionResult Borrar(int cod)
        {
            RegistroProducto prod = new RegistroProducto();
            prod.Borrar(cod);
            return RedirectToAction("Index");
        }
        public ActionResult Modificacion(int cod)
        {
            RegistroProducto prod = new RegistroProducto();
            Producto rpt = prod.Recuperar(cod);
            return View(rpt);
        }
        [HttpPost]
        public ActionResult Modificacion(FormCollection collection)
        {
            RegistroProducto prod = new RegistroProducto();
            Producto rpt = new Producto
            {
                
                Descripcion = collection["Descripcion"],
                Tipo = collection["Tipo"],
               Precio = double.Parse(collection["Precio"]),
            };
            prod.Modificar(rpt);
            return RedirectToAction("Index");
        }
    }
}