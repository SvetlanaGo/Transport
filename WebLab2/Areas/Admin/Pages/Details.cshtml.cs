using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebLab2.DAL.Data;
using WebLab2.DAL.Entities;

namespace WebLab2.Areas.Admin.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly WebLab2.DAL.Data.ApplicationDbContext _context;

        public DetailsModel(WebLab2.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Transport Transport { get; set; }

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
            return Page();
        }
    }
}
