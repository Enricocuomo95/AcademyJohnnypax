using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using GestionaleOfficina.Models;
using GestionaleOfficina.Repository;

namespace GestionaleOfficina.Controllers
{
    [Route("api/prodotto")]
    [ApiController]
    public class ProdottoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("create")]
        public IActionResult insert(Prodotto p)
        {
            if (ProdottoRepo.getIstanza().insert(p))
                return Ok("va tutto bene");
            return BadRequest();
        }

        [HttpGet("read")]
        public IActionResult getAll()
        {
            List<Prodotto> prodottoList = ProdottoRepo.getIstanza().findAll();

            if (prodottoList != null)
                return Ok(prodottoList);
            return BadRequest();
        }


        [HttpPut("update"), HttpPost("update")]
        public IActionResult update(Prodotto p)
        {
            if (ProdottoRepo.getIstanza().update(p))
                return Ok("va tutto bene");
            return BadRequest();
        }


        [HttpPut("delete"), HttpPost("delete")]
        public IActionResult delete(Prodotto p)
        {
            if (ProdottoRepo.getIstanza().delete(p))
                return Ok("va tutto bene");
            return BadRequest();
        }
    }
}
