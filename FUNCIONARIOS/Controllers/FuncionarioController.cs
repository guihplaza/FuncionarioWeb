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

namespace FUNCIONARIOS.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly EmpresaContexto _context;

        public FuncionarioController(EmpresaContexto context)
        {
            _context = context;
        }
        // GET: FuncionarioController
        public async Task<IActionResult> Index(string ordem, string filtroAtual, string filtro, int? pagina)
        {
            ViewData["NomeParm"] = String.IsNullOrEmpty(ordem) ? "nome_desc" : "";
            ViewData["CPF"] = ordem == "CPF" ? "cpf_desc" : "Cpf";




            var funcionarios = from func in _context.Funcionarios
                             select func;
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
                funcionarios = funcionarios.Where(s => s.Nome.Contains(filtro)
                                       || s.CPF.Contains(filtro));
            }

            switch (ordem)
            {
                case "nome_desc":
                    funcionarios = funcionarios.OrderByDescending(func => func.Nome);
                    break;
                case "Cpf":
                    funcionarios = funcionarios.OrderBy(func => func.CPF);
                    break;

            }
            int pageSize = 3;
            return View(await PaginatedList<Funcionario>.CreateAsync(funcionarios.AsNoTracking(), pagina ?? 1, pageSize));
        }

        // GET: FuncionarioController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .Include(s => s.IdFuncionario)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.IdFuncionario == id);

            if (funcionario == null)
            {
                return NotFound();
            }
            return View(funcionario);
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
                    _context.Add(funcionario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Logar o erro (descomente a variável ex e escreva um log
                ModelState.AddModelError("", "Não foi possível salvar. " +
                    "Tente novamente, e se o problema persistir " +
                    "chame o suporte.");
            }

            return View(funcionario);
        }

        // GET: FuncionarioController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var funcionario = await _context.Funcionarios.SingleOrDefaultAsync(s => s.IdFuncionario == id);

            return View(funcionario);
        }

        // POST: FuncionarioController/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var atualizarFuncionario = await _context.Funcionarios.SingleOrDefaultAsync(s => s.IdFuncionario == id);
            if (await TryUpdateModelAsync<Funcionario>(
                atualizarFuncionario,
                "",
                s => s.Nome, s => s.CPF, s => s.Sexo, s => s.Salario, s => s.Data_admissao, s => s.Email, s => s.Pis))
            {
                try
                {
                    await _context.SaveChangesAsync();
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
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(s => s.IdFuncionario == id);


            return View(funcionario);
        }

        // POST: FuncionarioController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.IdFuncionario == id);
            if (funcionario == null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _context.Funcionarios.Remove(funcionario);
                await _context.SaveChangesAsync();
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
            funcionario.TotalSalarios = await _context.Funcionarios.OrderByDescending(o => o.Salario).ToListAsync();
            ViewBag.TotalSalarios = string.Format("{0:c}", funcionario.TotalSalarios.Sum(w => w.Salario));

            return View(funcionario.TotalSalarios);
        }
    }
}
