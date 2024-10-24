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

            foreach(CustomerItem item in _context.CustomerItems)
            {
                _context.CustomerItems.Remove(item);
            }

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

        // GET: api/CustomerItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerItem>>> Get()
        {
            List<CustomerItem> items = _context.CustomerItems.ToList();

            return items;
        }

        // GET: api/CustomerItems/5
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

        // PUT: api/CustomerItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, CustomerItem CustomerItem)
        {
            if (id != CustomerItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(CustomerItem).State = EntityState.Modified;

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
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CustomerItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerItem>> Post(CustomerItem CustomerItem)
        {
            _context.CustomerItems.Add(CustomerItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerItem", new { id = CustomerItem.Id }, CustomerItem);
        }

        // DELETE: api/CustomerItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
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

        private bool CustomerItemExists(long id)
        {
            return _context.CustomerItems.Any(e => e.Id == id);
        }
    }
}
