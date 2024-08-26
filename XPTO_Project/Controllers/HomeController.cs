using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace XPTO_Project.Controllers
{
    public class HomeController : Controller
    {
        MVCCRUDDBContext _context = new MVCCRUDDBContext();

        public ActionResult Index(string sortOrder)
        {
            var listofData = _context.Notas.ToList();

            // Ordenação
            switch (sortOrder)
            {
                case "desc":
                    listofData = listofData.OrderByDescending(x => x.nOS).ToList();
                    ViewBag.SortOrder = "asc"; // Próxima ordenação será ascendente
                    break;
                default:
                    listofData = listofData.OrderBy(x => x.nOS).ToList();
                    ViewBag.SortOrder = "desc"; // Próxima ordenação será descendente
                    break;
            }

            return View(listofData);
        }
        [HttpGet] //Retrive data from the server
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost] //Send data to the server to create a new resource
        public ActionResult Create(Nota model)
        {
                if (ModelState.IsValid) // Verifica se o modelo é válido
                {
                    _context.Notas.Add(model);
                    _context.SaveChanges();
                    ViewBag.Message = "Data insert successful";
                    return RedirectToAction("Index");
                }
                return View(model); // Retorna a visão com os erros de validação
        } 
        [HttpGet]
        public ActionResult Edit(int id)
        { 
            var data = _context.Notas.Where(x => x.Id == id).FirstOrDefault();
             //This LINQ filters the 'Nota' collection to find the Nota whose 'Id' matches the id parameter.
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Nota Model)
        {
            var data = _context.Notas.Where(x => x.Id == Model.Id).FirstOrDefault(); //Filters the 'Nota' collection to find the Nota whose 'Id' matches the 'Model.Id'
            if (data != null)
            {
                data.nOS = Model.nOS;
                data.TituloServico = Model.TituloServico;
                data.CNPJCliente = Model.CNPJCliente;
                data.NomeCliente = Model.NomeCliente;
                data.CPFPrestadorServico = Model.CPFPrestadorServico;
                data.NomePrestadorServico = Model.NomePrestadorServico;
                data.DataExecucao = Model.DataExecucao;
                data.ValorServico = Model.ValorServico;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var data = _context.Notas.Where(x => x.Id == id).FirstOrDefault();
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }
        public ActionResult Delete(int id)
        {
            var data = _context.Notas.Where(x => x.Id == id).FirstOrDefault();
            _context.Notas.Remove(data);
            _context.SaveChanges();
            ViewBag.Message = "Record delete successfuly";
            return RedirectToAction("Index");
        } 
    }
}