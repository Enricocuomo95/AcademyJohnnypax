using MarioKart.DTO;
using MarioKart.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarioKart.Controllers
{
    [ApiController]
    [Route("player")]
    public class GiocatoreController : Controller
    {
        private readonly GiocatoreService service;

        public GiocatoreController(GiocatoreService service)
        {
            this.service = service;
        }

        private bool controllaPlayer(GiocatoreDTO player)
        {
            if (player.Username is null || player.Username.Trim().Equals(""))
                return false;
            if (player.Nominativo is null || player.Nominativo.Trim().Equals(""))
                return false;
            if (player.Passward is null || player.Passward.Trim().Equals(""))
                return false;
            return (true);
        }

        [HttpPost("inserisciPlayer")]
        public IActionResult InserisciGiocatore(GiocatoreDTO player)
        {
            if (!controllaPlayer(player))
                return BadRequest();

            if (service.addPlayer(player) >=0)
                return Ok();
            return BadRequest();
        }

        [HttpPost("avversari")]
        public ActionResult<List<GiocatoreDTO>> ElencoAvversari(GiocatoreDTO player)
        {
            if (!controllaPlayer(player))
                return BadRequest();
            return service.getAvversari(player);
        }

        [HttpPost("deleteGiocatore"), HttpDelete("deletePlayer")]
        public ActionResult<List<GiocatoreDTO>> DeleteGiocatore(GiocatoreDTO player)
        {
            if (!controllaPlayer(player))
                return BadRequest();
            if (service.delete(player))
                return Ok();
            return BadRequest();
        }
    }
}
