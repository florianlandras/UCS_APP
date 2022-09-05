using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UCS_APP.Data;
using UCS_APP.Models;

namespace UCS_APP.Controllers
{
    public class PhotosController : Controller
    {
        private readonly UCS_APPContext _context;

        // GET: Photos/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            // Get Json and parse it
            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/photos");
            var photos = JsonConvert.DeserializeObject<Photo[]>(json);
            
            // Get Photos by Album Id
            var photo_list = from s in photos select s;
            photo_list = photo_list.Where(s => s.AlbumId == id);
           
            return View(photo_list);


          
        }


        private bool PhotoExists(int id)
        {
          return (_context.Photo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
