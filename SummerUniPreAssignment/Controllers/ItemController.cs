using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummerUniPreAssignment.Entity;
using System.Collections.Generic;
using System.Linq;

namespace SummerUniPreAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ShoppingCartContext _context;
        public ItemController(ShoppingCartContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult Create(int cardId, Item item)
        {
            item.Id = 0; // Simple fix to avoid problems with users trying to set it themselfs. Should use DTOs instead
            var cart = _context.Carts.Include(c => c.Items).SingleOrDefault(c => c.Id == cardId);
            if (cart == null)
            {
                return BadRequest(new { error = "Cart not found" });
            }

            if(cart.Items.Any(i => i.Id == item.Id))
            {
                return BadRequest(new { error = "Item with that id exsists in cart" });
            }

            cart.Items.Add(item);

            _context.SaveChanges();

            return Ok(cart);
        }

        [HttpDelete]
        public ActionResult Delete(int cardId, int itemId)
        {
            var cart = _context.Carts.Include(c => c.Items).SingleOrDefault(c => c.Id == cardId);
            if (cart == null)
            {
                return BadRequest(new { error = "Cart not found" });
            }

            var item = cart.Items.SingleOrDefault(i => i.Id == itemId);

            if (item == null)
            {
                return BadRequest(new { error = "No Item with that id exsists in cart" });
            }

            cart.Items.Remove(item);

            _context.SaveChanges();
            var lol = "lol";
            return Ok(cart);
        }
    }
}