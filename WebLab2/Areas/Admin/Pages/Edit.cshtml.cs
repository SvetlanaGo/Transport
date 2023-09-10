using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebLab2.DAL.Data;
using WebLab2.DAL.Entities;

namespace WebLab2.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly WebLab2.DAL.Data.ApplicationDbContext _context;
        private IWebHostEnvironment _environment;

        public EditModel(WebLab2.DAL.Data.ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _environment = env;
        }

        [BindProperty]
        public Transport Transport { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Transport = await _context.Transports
                .Include(t => t.Group).FirstOrDefaultAsync(m => m.TransportId == id);

            if (Transport == null)
            {
                return NotFound();
            }
           ViewData["TransportGroupId"] = new SelectList(_context.TransportGroups, "TransportGroupId", "GroupName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Image != null)
            {
                var fileName = $"{Transport.TransportId}" + Path.GetExtension(Image.FileName);
                Transport.Image = fileName;
                var path = Path.Combine(_environment.WebRootPath, "Images", fileName);
                using (var fStream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(fStream);
                }
            }

            _context.Attach(Transport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransportExists(Transport.TransportId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TransportExists(int id)
        {
            return _context.Transports.Any(e => e.TransportId == id);
        }
    }
}
