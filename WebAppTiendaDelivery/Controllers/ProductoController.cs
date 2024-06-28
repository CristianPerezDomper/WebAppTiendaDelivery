using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Tree;
using Dominio.Entidades;
using Dominio.Negocio;

namespace WebAppTiendaDelivery.Controllers
{
    public class ProductoController : Controller
    {
        Categoria_BL categoria = new Categoria_BL();
        Proveedor_BL proveedor = new Proveedor_BL();
        Producto_BL producto = new Producto_BL();
        int numreg = 10;

        // GET: Producto
        
        public ActionResult ListaProductos(int? pag = null)
        {
            var listaProductos = producto.listado();
            int c = listaProductos.Count;
            ViewBag.numreg = c % numreg != 0 ? c / numreg + 1 : c / numreg;

            int pageact = pag ?? 0; // Usar el operador de coalescencia nula para simplificar

            int reginicio = pageact * numreg;
            int regfin = Math.Min(reginicio + numreg, c); // Usar Math.Min para evitar excepciones

            List<Producto> lista = new List<Producto>();

            for (int i = reginicio; i < regfin; i++)
            {
                lista.Add(listaProductos[i]);
            }

            return View(lista);

        }

        public ActionResult Create()
        {
            ViewBag.categorias = new SelectList(categoria.listado(),"IdCategoria","NombreCategoria");
            ViewBag.proveedores = new SelectList(proveedor.listado(), "IdProveedor", "NombreCia");

            //Pasamos un objeto nuevo con el código generado
            Producto objProducto = new Producto();
            objProducto.IdProducto = producto.obtenerSecuenciaIdproducto();
            return View(objProducto);
        }

        [HttpPost]
        public ActionResult Create(Producto reg) 
        {
            if (!ModelState.IsValid)
            {
                ViewBag.categorias = new SelectList(categoria.listado(), "IdCategoria", "NombreCategoria",reg.IdCategoria);
                ViewBag.proveedores = new SelectList(proveedor.listado(), "IdProveedor", "NombreCia", reg.IdProveedor);
                return View(reg);
            }
            ViewBag.mensaje = producto.Agregar(reg);
            return RedirectToAction("ListaProductos");
            
        }

        public ActionResult Edit(int id) 
        {
            Producto reg = producto.obtenerRegistro(id);
            ViewBag.categorias = new SelectList(categoria.listado(), "IdCategoria", "NombreCategoria", reg.IdCategoria);
            ViewBag.proveedores = new SelectList(proveedor.listado(), "IdProveedor", "NombreCia", reg.IdProveedor);
            return View(reg);
        }

        [HttpPost] 
        public ActionResult Edit(Producto reg) 
        {

            if (!ModelState.IsValid)
            {
                ViewBag.categorias = new SelectList(categoria.listado(), "IdCategoria", "NombreCategoria", reg.IdCategoria);
                ViewBag.proveedores = new SelectList(proveedor.listado(), "IdProveedor", "NombreCia", reg.IdProveedor);
                return View(reg);
            }

            ViewBag.mensaje = producto.Actualizar(reg);
            return RedirectToAction("ListaProductos");
        }

        public ActionResult Details (int id)
        {
            return View(producto.obtenerRegistro(id));
        }

        public ActionResult Delete(int id)
        {
            ViewBag.mensaje = producto.Eliminar(id);
            return RedirectToAction("ListaProductos");
        }
    }
}