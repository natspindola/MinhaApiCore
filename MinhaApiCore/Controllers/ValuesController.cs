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
    [ApiController]
    public class ValuesController : ControllerBase
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
                return BadRequest();

            return Ok(valores);
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
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)] //retorna um statusCode específico
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post(Product product)
        {
            if (product.Id == 0) return BadRequest();

            //add no banco

            return CreatedAtAction("Post", product);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromForm] Product value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete([FromQuery]int id)
        {
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
