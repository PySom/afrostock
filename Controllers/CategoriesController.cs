//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AfrroStock.Models;
//using AfrroStock.Repository.Generic;
//using AfrroStock.Models.ViewModels;
//using AfrroStock.Models.DTOs;

//namespace AfrroStock.Controllers
//{
//    [Route("api/[controller]")]
//    public class CategoriesController : GenericController<Category, CategoryVM, CategoryDTO>
//    {
//        public CategoriesController(IModelManager<Category> repo, IMapper mapper): base(repo, mapper)
//        {}

//        [HttpGet]
//        public override async ValueTask<IActionResult> Get()
//        {
//            ICollection<Category> categories = await _repo
//                                                .Item()
//                                                .Include(c => c.Images)
//                                                    .ThenInclude(i => i.Tags)
//                                                .ToListAsync();
//            return Ok(_mapper.Map<ICollection<Category>, ICollection<CategoryDTO>>(categories));

//        }

//        [HttpGet("{id:int}")]
//        public override async ValueTask<IActionResult> Get(int id)
//        {
//            Category model = await _repo
//                                .Item()
//                                .Where(c => c.Id == id)
//                                .Include(c => c.Images)
//                                    .ThenInclude(i => i.Tags)
//                                .FirstOrDefaultAsync();
//            if (model != null)
//            {
//                return Ok(_mapper.Map<Category, CategoryDTO>(model));
//            }
//            return NotFound();
//        }
//    }
//}
