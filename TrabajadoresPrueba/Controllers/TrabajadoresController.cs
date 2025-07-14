using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TrabajadoresPrueba.Data;
using TrabajadoresPrueba.Models;

namespace TrabajadoresPrueba.Controllers
{
    public class TrabajadoresController : Controller
    {
        public readonly ApplicationDbContext _contexto;

        public TrabajadoresController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            var trabajadores = _contexto.VistaTrabajadores.FromSqlRaw("EXEC listarTrabajadores @id={0}", DBNull.Value).ToList();
            return View(trabajadores);
        }
        public IActionResult ObtenerDepartamentos()
        {
            var departamentos = _contexto.Departamento.Select(de => new { de.Id, de.NombreDepartamento })
                .ToList();

            return Json(departamentos);
        }

        public IActionResult ObtenerProvincias(int idDepartamento)
        {
            var provincias = _contexto.Provincia.Where(p => p.IdDepartamento == idDepartamento)
                .Select(de => new { de.Id, de.NombreProvincia })
                .ToList();

            return Json(provincias);
        }

        public IActionResult ObtenerDistritos(int idProvincia)
        {
            var distritos = _contexto.Distrito.Where(di => di.IdProvincia == idProvincia)
                .Select(de => new { de.Id, de.NombreDistrito })
                .ToList();

            return Json(distritos);
        }

        [HttpPost]
        public IActionResult RegistrarTrabajador(Trabajador trabajador)
        {
            if (ModelState.IsValid)
            {
                _contexto.Trabajadores.Add(trabajador);
                _contexto.SaveChanges();
                return RedirectToAction("index");
            }

            return View("index", Index());
        }

        [HttpPost]
        public async Task<IActionResult> EliminarTrabajador(int id)
        {
            var trabajador = await _contexto.Trabajadores.FindAsync(id);
            if (trabajador == null)
            {
                return NotFound();
            }
            _contexto.Trabajadores.Remove(trabajador);
            await _contexto.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTrabajador(int id)
        {
            var trabajador = await _contexto.Trabajadores.FindAsync(id);
            if (trabajador == null)
            {
                return NotFound();
            }
            return Json(trabajador);
        }

        [HttpPost]
        public async Task<IActionResult> EditarTrabajador(Trabajador nuevo)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var trabajador = await _contexto.Trabajadores.FindAsync(nuevo.Id);
            if (trabajador == null)
                return NotFound();

            trabajador.TipoDocumento = nuevo.TipoDocumento;
            trabajador.NumeroDocumento = nuevo.NumeroDocumento;
            trabajador.Nombres = nuevo.Nombres;
            trabajador.Sexo = nuevo.Sexo;
            trabajador.IdDepartamento = nuevo.IdDepartamento;
            trabajador.IdProvincia = nuevo.IdProvincia;
            trabajador.IdDistrito = nuevo.IdDistrito;

            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
