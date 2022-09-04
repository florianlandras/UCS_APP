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

    public class AlbumsController : Controller
    {
        private readonly UCS_APPContext _context;

        private bool StringValidator(string stringToValidate)
        {
            // check if it is letter 97 to 122 and space 32
            var charArray = stringToValidate.ToCharArray();
            foreach (var letter in charArray)
            {
                int numericRepresentation = (int)letter;

                if (!(numericRepresentation >= 97 && numericRepresentation <= 122) && numericRepresentation != 32)
                {
                    return false;
                }


            }
            return true;
        }
        public AlbumsController(UCS_APPContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            // Get Json
            var httpClient = new HttpClient();
            // TODO Add Http errror
            var json = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/albums");
            var albums = JsonConvert.DeserializeObject<Album[]>(json);
            //Album albums = (Album)JsonConvert.DeserializeObject(json, typeof(Album));   



            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            var album_list = from s in albums select s;



            // Searching and Validation for a name in album
            if (!String.IsNullOrEmpty(searchString) && StringValidator(searchString))
            {
                album_list = album_list.Where(s => s.Title.Contains(searchString));
            }

            // Sorting album in ascendant and descendant order
            switch (sortOrder)
            {
                case "name_desc":
                    album_list = album_list.OrderByDescending(s => s.Title);
                    break;
                default:
                    album_list = album_list.OrderBy(s => s.Title);
                    break;



            }

            return View(album_list.ToList());
        }



        private bool AlbumExists(int id)
        {
            return (_context.Album?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
