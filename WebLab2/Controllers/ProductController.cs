using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebLab2.DAL.Entities;
using WebLab2.Extensions;
using WebLab2.Models;
using WebLab2.DAL.Data;
using Microsoft.Extensions.Logging;

namespace WebLab2.Controllers
{
    public class ProductController : Controller
    {
        //public List<Transport> _transports;
        //List<TransportGroup> _transportGroups;

        ApplicationDbContext _context;
        int _pageSize;
        //public ProductController()

        //private ILogger _logger;
        public ProductController(ApplicationDbContext context/*,
                                 ILogger<ProductController> logger*/)
        {
            _pageSize = 3;
            //SetupData();
            _context = context;
            //_logger = logger;
        }

        [Route("Catalog")]
        [Route("Catalog/Page_{pageNo}")]
        public IActionResult Index(int? group, int pageNo = 1)
        {
            //var items = _transports
            //.Skip((pageNo - 1) * _pageSize)
            //.Take(_pageSize)
            //.ToList();
            //return View(items);

            var groupMame = group.HasValue
                ? _context.TransportGroups.Find(group.Value)?.GroupName
                : "all groups";
            //_logger.LogInformation($"info: group={group}, page={pageNo}");

            //var transportsFiltered = _transports
            var transportsFiltered = _context.Transports
            .Where(d => !group.HasValue || d.TransportGroupId == group.Value);

            //ViewData["Groups"] = _transportGroups;
            ViewData["Groups"] = _context.TransportGroups;

            ViewData["CurrentGroup"] = group ?? 0;

            //return View(ListViewModel<Transport>.GetModel(transportsFiltered, pageNo, _pageSize));

            var model = ListViewModel<Transport>.GetModel(transportsFiltered, pageNo, _pageSize);
            //if (Request.Headers["x-requested-with"]
            //.ToString().ToLower().Equals("xmlhttprequest"))
            //    return PartialView("_listpartial", model);
            if (Request.IsAjaxRequest())
                return PartialView("_listpartial", model);
            else
                return View(model);
        }
        /// <summary>
        /// Lists
        /// </summary>
        //private void SetupData()
        //{
        //}
    }
}