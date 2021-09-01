using FUNCIONARIOS.Data;
using FUNCIONARIOS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.Colaboradores.BusinessLogic.Services;

namespace FUNCIONARIOS.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioServices _FuncionarioService;

        public FuncionarioController(IFuncionarioServices funcionarioService)
        {
            _FuncionarioService = funcionarioService;
        }
        // GET: FuncionarioController
        public async Task<IActionResult> Index(string ordem, string filtroAtual, string filtro, int? pagina)
        {
            ViewData["NomeParm"] = String.IsNullOrEmpty(ordem) ? "nome_desc" : "";
            ViewData["CPF"] = ordem == "CPF" ? "cpf_desc" : "Cpf";

            var funcionarios = _FuncionarioService.Listar();

            if (filtro != null)
            {
                pagina = 1;
            }
            else
            {
                filtro = filtroAtual;
            }

            if (!String.IsNullOrEmpty(filtro))
            {
                 funcionarios = _FuncionarioService.Listar(s => s.Nome.Contains(filtro)
                                       || s.CPF.Contains(filtro));
            }

            switch (ordem)
            {
                case "nome_desc":
                    funcionarios = funcionarios.OrderByDescending(w => w.Nome).ToList();
                    break;
                case "Cpf":
                    funcionarios = funcionarios.OrderByDescending(w => w.CPF).ToList();
                    break;

            }
            int pageSize = 3;
            return View(PaginatedList<Funcionario>.CreateAsync(funcionarios.AsQueryable(), pagina ?? 1, pageSize));
        }

        // GET: FuncionarioController/Create
        public ActionResult Create()
        {
            ViewBag.Sexo = Funcionario.GetSexos().Select(c => new SelectListItem() { Text = c.Sexo, Value = c.Sexo }).ToList();
            return View();

        }

        // POST: FuncionarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Funcionario funcionario)
        {
            ViewBag.Sexo = Funcionario.GetSexos().Select(c => new SelectListItem() { Text = c.Sexo, Value = c.Sexo }).ToList();

            try
            {
                if (ModelState.IsValid)
                {
                    _FuncionarioService.inserir(funcionario);
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException /* ex */)
            {
    
                ModelState.AddModelError("", "Não foi possível salvar. " +
                    "Tente novamente, e se o problema persistir " +
                    "chame o suporte.");
            }

            return View(funcionario);
        }

        // GET: FuncionarioController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var funcionario = _FuncionarioService.GetById(s => s.IdFuncionario == id);

            return View(funcionario);
        }

        // POST: FuncionarioController/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            var atualizarFuncionario = _FuncionarioService.GetById(s => s.IdFuncionario == id);
            if (await TryUpdateModelAsync<Funcionario>(
                atualizarFuncionario,
                "",
                s => s.Nome, s => s.CPF, s => s.Sexo, s => s.Salario, s => s.Data_admissao, s => s.Email, s => s.Pis))
            {
                try
                {
                    _FuncionarioService.alterar(atualizarFuncionario);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Não foi possível salvar. " +
                        "Tente novamente, e se o problema persistir " +
                        "chame o suporte.");
                }
            }
            return View(atualizarFuncionario);
        }

        // GET: FuncionarioController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var funcionario = _FuncionarioService.GetById(s => s.IdFuncionario == id);


            return View(funcionario);
        }

        // POST: FuncionarioController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var funcionario = _FuncionarioService.GetById(m => m.IdFuncionario == id);
            if (funcionario == null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _FuncionarioService.excluir(funcionario.IdFuncionario);

                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {

                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
        }

        public async Task<IActionResult> DespesasSalColaborador()
        {
            var funcionario = new Funcionario();
            var funcionarios = _FuncionarioService.Listar();

            funcionario.TotalSalarios = funcionarios.OrderByDescending(o => o.Salario).ToList();

            //funcionario.TotalSalarios = await _context.Funcionarios.OrderByDescending(o => o.Salario).ToListAsync();

            ViewBag.TotalSalarios = string.Format("{0:c}", funcionario.TotalSalarios.Sum(w => w.Salario));

            return View(funcionario.TotalSalarios);
        }
    }
}
