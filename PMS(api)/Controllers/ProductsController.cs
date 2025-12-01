using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMS_api_.Models;
using System.Security.Cryptography.X509Certificates;

namespace PMS_api_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductsContext _context;

        public ProductController(ProductsContext context)
        {
            _context = context;

        }


        [HttpGet("all")]

        public async Task<IActionResult> GetAll()
        {
            var prod = await _context.Prods.ToListAsync();
            return Ok(prod);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            var prod = await _context.Prods.FindAsync(id);


            if (prod == null)
            {
                return NotFound(new { message = "Product Not Found" });


            }
            return Ok(prod);





        }

        [HttpPost]

        public async Task<IActionResult> Create(Prod prod)

        {
            if (ModelState.IsValid)
            {
                _context.Prods.Add(prod);
                await _context.SaveChangesAsync();


            }
            return Ok(prod);

        }
    
        [HttpPut("{id}")]
        
        public async Task <IActionResult>Edit(int id , Prod updateProd)
    {
        if(id != updateProd.Id)
        {
            return BadRequest(new { message = "Id missed Matched" });
        }

            var existprod = await _context.Prods.FindAsync(id);

            if (existprod == null)
            {
                return NotFound(new { message = "Product Not Found" });
            }

            existprod.Name = updateProd.Name;
            existprod.Description = updateProd.Description;
            existprod.Price = updateProd.Price;

            _context.Prods.Update(existprod);
            await _context.SaveChangesAsync();

            return Ok(existprod);
         
        
    }

        [HttpPatch("{id}")]

        public async Task<IActionResult>Patch(int id , Prod patchData)
        {
            var prod = await _context.Prods.FindAsync(id);
            if (prod == null)
            {
                return NotFound(new { message = "Product Not found" });
            }


            if (!String.IsNullOrEmpty(patchData.Name))
                prod.Name = patchData.Name;

            if (!String.IsNullOrEmpty(patchData.Description))
                prod.Description = patchData.Description;

            if (patchData.Price != 0)
                prod.Price = patchData.Price;

             await _context.SaveChangesAsync();
            
            return Ok(prod);

        }

        [HttpDelete("id")]

        public async Task <IActionResult>Delete(int id)
        {
            var prod = await _context.Prods.FindAsync(id);

            if (id == null)
            {
                return NotFound(new { message = "Product Not Found" });
            }

             _context.Prods.Remove(prod);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Product Deleted Successfully" });

        }
        
    }
}

