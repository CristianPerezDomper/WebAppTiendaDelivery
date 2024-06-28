using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Dominio.Entidades;
using Dominio.Negocio;

using Microsoft.Reporting.WebForms;
using ReportViewerForMvc;
using WebAppTiendaDelivery.Reportes;

namespace WebAppTiendaDelivery.Controllers
{
    public class ClienteController : Controller
    {
        Pais_BL pais = new Pais_BL();
        Cliente_BL cliente = new Cliente_BL();
        int numreg = 10;

        // GET: Cliente

        public ActionResult ListaClientes(int? pag = null)
        {
            var listadoClientes = cliente.listado();
            int c = listadoClientes.Count;
            ViewBag.numreg = c % numreg != 0 ? c / numreg + 1 : c / numreg;

            int pageact = pag ?? 0; // Usar el operador de coalescencia nula para simplificar

            int reginicio = pageact * numreg;
            int regfin = Math.Min(reginicio + numreg, c); // Usar Math.Min para evitar excepciones

            List<Cliente> lista = new List<Cliente>();

            for (int i = reginicio; i < regfin; i++)
            {
                lista.Add(listadoClientes[i]);
            }

            return View(lista);

        }

        public ActionResult Create() 
        { 
            ViewBag.paises = new SelectList(pais.listado(), "IdPais","NombrePais");
            return View(new Cliente());
        }

        [HttpPost]
        public ActionResult Create(Cliente reg)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.paises = new SelectList(pais.listado(), "IdPais", "NombrePais", reg.Idpais);
                return View(reg);
            }
            ViewBag.mensaje = cliente.Agregar(reg);
            return RedirectToAction("ListaClientes");
        }

        public ActionResult Edit(string id)
        {
            Cliente reg = cliente.obtenerRegistro(id);
            ViewBag.paises = new SelectList(pais.listado(), "IdPais", "NombrePais", reg.Idpais);
            return View(reg);
        }

        [HttpPost]
        public ActionResult Edit(Cliente reg) 
        {
            if (!ModelState.IsValid) 
            { 
                ViewBag.paises = new SelectList(pais.listado(), "IdPais", "NombrePais", reg.Idpais);
                return View(reg);
            }
            ViewBag.mensaje= cliente.Actualizar(reg);
            return RedirectToAction("ListaClientes");

        }

        public ActionResult Details(string id) 
        {
            return View(cliente.obtenerRegistro(id));
        }

        public ActionResult Delete(string id)
        {
            ViewBag.mensaje = cliente.Eliminar(id);
            return RedirectToAction("ListaClientes");
        }

        public ActionResult ReporteCliente() 
        { 
            ReportViewer rpv = new ReportViewer();
            rpv.ProcessingMode = ProcessingMode.Local;
            rpv.SizeToReportContent = true;

            rpv.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reportes\rptClientes.rdlc";
            rpv.LocalReport.DataSources.Add(new ReportDataSource("dsNegocios", cliente.obtenerReporteCliente()));

            ViewBag.ReportViewer = rpv;

            return View();
        }

        public FileResult DownloadPDFReport()
        {
            string FileNameReportPDF = "clientes" + "-" + DateTime.Now + ".pdf";
            LocalReport localReport = new LocalReport();
            localReport.DataSources.Clear();
            localReport.DataSources.Add(new ReportDataSource("dsNegocios", cliente.obtenerReporteCliente()));
            localReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reportes\rptClientes.rdlc";

            byte[] bytes = localReport.Render("PDF");
            return File(bytes, "application/pdf", FileNameReportPDF);
        }

        public FileResult DownloadXLSReport()
        {
            string FileNameReportXLS = "clientes" + "-" + DateTime.Now + ".xls";
            LocalReport localReport = new LocalReport();
            localReport.DataSources.Clear();
            localReport.DataSources.Add(new ReportDataSource("dsNegocios", cliente.obtenerReporteCliente()));
            localReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reportes\rptClientes.rdlc";

            byte[] bytes = localReport.Render("Excel");
            return File(bytes, "application/vnd.ms-excel", FileNameReportXLS);
        }

        public FileResult DownloadWORDReport()
        {
            string FileNameReportWORD = "clientes" + "-" + DateTime.Now + ".doc";
            LocalReport localReport = new LocalReport();
            localReport.DataSources.Clear();
            localReport.DataSources.Add(new ReportDataSource("dsNegocios", cliente.obtenerReporteCliente()));
            localReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reportes\rptClientes.rdlc";

            byte[] bytes = localReport.Render("WORD");
            return File(bytes, "application/msword", FileNameReportWORD);
        }

    }
}