using DemoAPI.Data;
using DemoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactApiDbContext dbContext;
        public ContactsController(ContactApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
            //return View();
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var con = await dbContext.Contacts.FindAsync(id);
            if(con != null)
                return Ok(con);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContacts addContacts)
        {
            var Contact = new Contact()
            {
                Id = Guid.NewGuid(),
                FullName = addContacts.FullName,
                Email = addContacts.Email,
                Address = addContacts.Address,
                MobileNO = addContacts.MobileNO,
            };

            await dbContext.Contacts.AddAsync(Contact);   
            await dbContext.SaveChangesAsync();

            return Ok(Contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, updateContact updateContact)
        {
            var con = await dbContext.Contacts.FindAsync(id);
            if(con != null)
            {
                con.FullName = updateContact.FullName;
                con.Email = updateContact.Email;
                con.Address = updateContact.Address;
                con.MobileNO = updateContact.MobileNO;

                await dbContext.SaveChangesAsync();

                return Ok(con);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("id:guid")]

        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var con = await dbContext.Contacts.FindAsync(id);

            if(con != null)
            {
                dbContext.Remove(con);
                await dbContext.SaveChangesAsync();

                return Ok(con);
            }

            return NotFound();
        }
    }
}
