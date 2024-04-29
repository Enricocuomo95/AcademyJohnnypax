using MarioKart.DTO;
using MarioKart.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarioKart.Controllers
{
    [ApiController]
    [Route("personaggio")]
    public class PersonaggioController : Controller
    {
        private readonly PersonaggioService personaggioService;
        private readonly GiocatoreService giocatoreService;

        public PersonaggioController(PersonaggioService personaggioService, GiocatoreService giocatoreService)
        {
            this.personaggioService = personaggioService;
            this.giocatoreService = giocatoreService;
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

        private bool controllaPersonaggio(PersonaggioDTO personaggio)
        {
            if (personaggio.Nome is null || personaggio.Nome.Trim().Equals(""))
                return false;
            if (personaggio.Costo is null || personaggio.Costo.Trim().Equals(""))
                return false;
            if (personaggio.Categoria is null || personaggio.Categoria.Trim().Equals(""))
                return false;
            if (personaggio.Disponibile == false)
                return false;
            if (personaggio.Giocatore is not null)
                return false;
            return (true);
        }

        [HttpPost("aggiungiPersonaggio/{Username}")]
        public IActionResult aggiungiPersonaggio(PersonaggioDTO personaggio, String Username)
        {
            GiocatoreDTO player = giocatoreService.getGiocatoreForName(Username);

            if (!controllaPlayer(player))
                return BadRequest();
            
            if (!controllaPersonaggio(personaggio))
                return BadRequest();

            if (personaggioService.addPersonaggio(personaggio,player) == 2)
                return Ok();
            return BadRequest();
        }

        [HttpGet("personaggi")]
        public ActionResult<List<PersonaggioDTO>> ElencoPersonaggi()
        {
            return personaggioService.getPersonaggi();
        }

        [HttpPost("rimuoviPersonaggio/{Username}")]
        public IActionResult rimuoviPersonaggio(PersonaggioDTO personaggio, String Username)
        {
            GiocatoreDTO player = giocatoreService.getGiocatoreForName(Username);
            if (!controllaPlayer(player))
                return BadRequest();

            if (!controllaPersonaggio(personaggio))
                return BadRequest();

            if (personaggioService.removePersonaggio(personaggio, player))
                return Ok();
            return BadRequest();
        }
    }
}
