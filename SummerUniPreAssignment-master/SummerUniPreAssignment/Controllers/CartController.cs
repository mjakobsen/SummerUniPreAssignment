using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummerUniPreAssignment.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SummerUniPreAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ShoppingCartContext _context;

        public  CartController(ShoppingCartContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public ActionResult<Cart> Get(int id)
        {
            var cart = _context.Carts.Include(c => c.Items).SingleOrDefault(c => c.Id == id);

            return Ok(cart);
        }

        [HttpGet]
        //[ResponseCache(Duration = 3600)]
        public ActionResult<List<Cart>> GetAll()
        {
            var allCarts = _context.Carts.Include(c => c.Items);

            return Ok(allCarts);
        }

        [HttpGet("[action]")]
        public ActionResult<Cart> GetForUser(int userId)
        {
            if (userId == 0)
            {
                return BadRequest(new { error = "userId required noob" });
            }

            var cart = _context.Carts.Include(c => c.Items).SingleOrDefault(c => c.UserId == userId);

            return Ok(cart);
        } 

        [HttpPost]
        public ActionResult Create(Cart cart)
        {
            if (cart.UserId == 0)
            {
                return BadRequest(new { error = "user required" });
            }

            if (_context.Carts.Any(c => c.UserId == cart.UserId))
            {
                return BadRequest(new { error = "User already have cart" });
            }

            cart.Items = new List<Item>();

            cart.Id = 0; // Workaround of no DTOs

            _context.Carts.Add(cart);
            _context.SaveChanges();

            return Created($"/{cart.Id}", cart);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<object>>> Events([FromQuery]int skip, [FromQuery]int take)
        {
            var items = await _context.ItemEvents.AsNoTracking().OrderBy(i => i.Id).Skip(skip).Take(take).ToListAsync();

            return Ok(items);
         }
    }
}