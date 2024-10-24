using CustomerDatabaseProto.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerDatabaseProto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly CustomerContext _context;

        public ValuesController(CustomerContext context)
        {
            _context = context;

            if (_context.CustomerItems.ToList().Count == 0)
            {

                CustomerItem customer1 = new CustomerItem();
                customer1.FirstName = "Charles";
                customer1.LastName = "Dustin";


                CustomerItem customer2 = new CustomerItem();
                customer2.FirstName = "William";
                customer2.LastName = "Winters";

                CustomerItem customer3 = new CustomerItem();
                customer3.FirstName = "Cato";
                customer3.LastName = "Sicarius";

                context.CustomerItems.Add(customer1);
                context.CustomerItems.Add(customer2);
                context.CustomerItems.Add(customer3);

                _context.SaveChanges();
            }
        }

        // GET: Returns all items from the database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerItem>>> Get()
        {
            List<CustomerItem> items = _context.CustomerItems.ToList();

            return items;
        }

        // GET: Returns a specific item from the database
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerItem>> Get(long id)
        {
            var CustomerItem = await _context.CustomerItems.FindAsync(id);

            if (CustomerItem == null)
            {
                return NotFound();
            }

            return CustomerItem;
        }

        // PUT: Edits an item in the database
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, CustomerItem updatedCustomerItem)
        {
            if (id != updatedCustomerItem.Id)
            {
                return BadRequest();
            }

            CustomerItem itemToUpdate = _context.CustomerItems.FirstOrDefault(x => x.Id == id);

            itemToUpdate.FirstName = updatedCustomerItem.FirstName;
            itemToUpdate.LastName = updatedCustomerItem.LastName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerItemExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: Adds a new item to the database
        [HttpPost]
        public async Task<ActionResult<CustomerItem>> Post(CustomerItem CustomerItem)
        {
            _context.CustomerItems.Add(CustomerItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerItem", new { id = CustomerItem.Id }, CustomerItem);
        }

        // DELETE: Deletes an item from the database
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var CustomerItem = await _context.CustomerItems.FindAsync(id);
            if (CustomerItem == null)
            {
                return NotFound();
            }

            _context.CustomerItems.Remove(CustomerItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Check if an item exists in the database
        private bool CustomerItemExists(long id)
        {
            return _context.CustomerItems.Any(e => e.Id == id);
        }
    }
}
