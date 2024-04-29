using Ferramenta.Models;
using Ferramenta.Repositori;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ferramenta.Controller
{
    [ApiController]
    [Route("api/prodotto")]
    public class ControllerProdotto : ControllerBase
    {
        
        public IActionResult Index()
        {
            throw new NotImplementedException();
        }


        [HttpPost("create")]
        public IActionResult insert(Prodotto p)
        {
            if (RepositoriProdotto.getIstanza().Insert(p))
                return Ok("va tutto bene");
            return BadRequest();
        }

        [HttpGet("read")]
        public IActionResult getAll()
        {
            List<Prodotto> prodottoList = RepositoriProdotto.getIstanza().getAll();

            if (prodottoList != null)
                return Ok(prodottoList);
            return BadRequest();
        }


        [HttpPut("update"), HttpPost("update")]
        public IActionResult update(Prodotto p)
        {
            if (RepositoriProdotto.getIstanza().UpdateValue(p))
                return Ok("va tutto bene");
            return BadRequest();
        }


        [HttpPut("delete"), HttpPost("delete")]
        public IActionResult delete(Prodotto p)
        {
            if (RepositoriProdotto.getIstanza().DeleteValue(p))
                return Ok("va tutto bene");
            return BadRequest();
        }

    }
}
