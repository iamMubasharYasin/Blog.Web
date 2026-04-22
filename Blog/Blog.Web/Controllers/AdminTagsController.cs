using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        // ---------------- ADD ----------------

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> SubmitTag(AddTagRequest addTagRequest)
        {
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await tagRepository.AddAsync(tag);

            return RedirectToAction("ListallTags");
        }

        // ---------------- LIST ----------------

        public async Task<IActionResult> ListallTags()
        {
            var tags = await tagRepository.GetAllAsync();
            return View(tags);
        }

        // ---------------- EDIT (GET) ----------------

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await tagRepository.GetByIdAsync(id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }

            return View(null);
        }

        // ---------------- EDIT (POST) ----------------

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var updatedTag = await tagRepository.UpdateAsync(tag);

            if (updatedTag != null)
            {
                return RedirectToAction("ListallTags");
            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        // ---------------- DELETE ----------------

        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedTag = await tagRepository.DeleteAsync(id);

            if (deletedTag != null)
            {
                return RedirectToAction("ListallTags");
            }

            return RedirectToAction("ListallTags");
        }
    }
}