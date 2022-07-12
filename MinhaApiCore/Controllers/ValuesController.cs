using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaApiCore.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : MainController
    {
        // GET api/values
        [HttpGet] //actionResult tipado, retorna um BadRequest específico e um tipo
        public ActionResult<IEnumerable<string>> ObterTodos()
        {
            var valores = new string[] { "value1", "value2" };

            if (valores.Length < 5000)
                return BadRequest();

            return valores;
        }

        [HttpGet] //actionResult não tipado, retorna apenas o resultado e não o tipo
        public ActionResult ObterResultado()
        {
            var valores = new string[] { "value1", "value2" };

            if (valores.Length < 5000)
                return CustomResponse();

            return CustomResponse(valores);
        }

        [HttpGet("obter-valores")] //tipado sem actionResult, não retorna um resultado, apenas o tipo
        public IEnumerable<string> ObterValores()
        {
            var valores = new string[] { "value1", "value2" };

            if (valores.Length < 5000)
                return null;

            return valores;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        //[ProducesResponseType(typeof(Product), StatusCodes.Status201Created)] //retorna um statusCode específico
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))] //implementa os produces por convenção
        public ActionResult Post(Product product)
        {
            if (product.Id == 0) return BadRequest();

            //add no banco

            //return Ok(product); //retorna o status 200, sucesso
            return CreatedAtAction("Post", product); //retorna o status 201, sucesso um novo recurso foi criado
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public ActionResult Put([FromForm] int id, [FromForm] Product product)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (id != product.Id) return NotFound();

            //add no banco

            return NoContent(); //retorna o status 204, solicitação bem sucedida
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete([FromQuery] int id)
        {
        }
    }

    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(value: new
                {
                    sucess = true,
                    data = result
                });
            }

            return BadRequest(error: new
            {
                sucess = false,
                errors = ObterErros()
            });
        }

        public bool OperacaoValida()
        {
            // validações
            return true;
        }

        protected string ObterErros()
        {
            return "";
        }
    }

    public class Product //formulário para o Post e o Put
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
