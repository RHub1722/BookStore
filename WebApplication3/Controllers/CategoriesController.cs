using Microsoft.AspNetCore.Mvc;
using Services;
using Shared.DTOS;

namespace BookStore.Controllers
{
    [Route("api/Categories")]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }
        /// <summary>
        /// GetBook
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetByID")]
        [HttpGet]
        public IActionResult GetByID(int id)
        {
            var dto = _categoriesService.GetCategory(id);
            if (dto == null)
                return NotFound();

            return new ObjectResult(dto);
        }

        /// <summary>
        /// All Categories
        /// </summary>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var dto = _categoriesService.GetAllCategories();
            if (dto == null)
                return NotFound();

            return new ObjectResult(dto);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [CustomAuthorize("Admin")]
        [Route("Create")]
        [HttpPost]
        public IActionResult Create([FromBody] CategoryDto item)
        {
            if (item == null)
                return BadRequest();

            _categoriesService.CreateCategory(item);
            return Ok();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [CustomAuthorize("Admin")]
        [Route("Update")]
        [HttpPost]
        public IActionResult Update(int id, [FromBody] CategoryDto item)
        {
            var bookDto = _categoriesService.GetCategory(id);
            if (bookDto == null)
                return NotFound();

            _categoriesService.UpdateCategory(id, item);
            return Ok();
        }


        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [CustomAuthorize("Admin")]
        [Route("Delete")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _categoriesService.DeleteCategory(id);
            return Ok();
        }
    }
}