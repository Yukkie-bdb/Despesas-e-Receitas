using Microsoft.AspNetCore.Mvc;
using Despesas_e_Receitas.Models;

namespace ServicoDelicia.Controllers
{
    public class ServicoController : Controller
    {
        private ServicosDB _db;

        public ServicoController()
        {
            _db = new ServicosDB();
        }

        public IActionResult Index()
        {
            return View(_db.getList());
        }

        public IActionResult Filter(string inFiltro)
        {
            List<Servico> servicos = _db.filter(inFiltro);
            return View("Index", servicos);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Cancel()
        {
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string Id)
        {
            Servico aux = _db.getById(Id);
            return View(aux);
        }

        public IActionResult Delete(string id)
        {
            Servico aux = (_db.getById(id));
            return View(aux);
        }

        public IActionResult Save(string inId, string inNome, string inDescricao, char inCategoria, decimal Valor, char inCapexOpex)
        {
            Servico servico = new Servico
            {
                Nome = inNome,
                Descricao = inDescricao,
                Categoria = inCategoria,
                Valor = Valor,
                CapexOpex = inCapexOpex
            };
            if (inId == null)
            {
                servico.Id = Guid.NewGuid();
                _db.insert(servico);
            }
            else
            {
                servico.Id = new Guid(inId);
                _db.update(servico);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Remove(string id)
        {
            _db.delete(id);
            return RedirectToAction("Index");
        }
    }
}