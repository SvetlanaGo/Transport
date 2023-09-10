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
    public class IndexModel : PageModel
    {
        private readonly WebLab2.DAL.Data.ApplicationDbContext _context;

        public IndexModel(WebLab2.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Transport> Transport { get;set; }

        public async Task OnGetAsync()
        {
            Transport = await _context.Transports
                .Include(t => t.Group).ToListAsync();
        }
    }
}
