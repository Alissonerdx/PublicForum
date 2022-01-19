using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicForum.Application.Interfaces;
using PublicForum.Application.ViewModels;
using PublicForum.Auth.Interface;
using System.Security.Claims;

namespace PublicForum.Web.Controllers
{
    [Authorize()]
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;
        private readonly IUserResolver _userResolver;
        public TopicController(ITopicService topicService, IUserResolver userResolver)
        {
            _topicService = topicService;
            _userResolver = userResolver;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetTableData(
            int page,
            int size,
            List<Dictionary<string, string>> filter)
        {
            var userId = _userResolver.GetUserId();
            var result = await _topicService.GetPaginated(page, size, filter, userId);
            return Json(result);
        }

       [HttpGet]
        public async Task<IActionResult> GetDetail(Guid id)
        {
            var result = await _topicService.GetById(id);
            return Json(new
            {
                Id = result.Id,
                Title = result.Title,
                Description = result.Description,
                OwnerId = result.OwnerId,
                Owner = $"{result.Owner.FirstName} {result.Owner.LastName}",
                Date = result.Created.ToLongDateString(),
                Info = $"Posted on {result.Created.ToLongDateString()} by {result.Owner.FirstName} {result.Owner.LastName}"
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(TopicViewModel viewModel)
        {
            viewModel.OwnerId = _userResolver.GetUserId();

            if (!ModelState.IsValid)
                return Json(new { Success = false, Message = "Fill in all mandatory fields" });

            var result = await _topicService.Create(viewModel);
            if (result.Validation != null && !result.Validation.IsValid)
                return Json(new { Success = false, Message = String.Join("<br/>", result.Validation.Errors.Select(e => e.ErrorMessage)) });

            return Json(new { Success = true, Message = "Topic created successfully" });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TopicViewModel viewModel)
        {
            viewModel.OwnerId = _userResolver.GetUserId();

            if (!ModelState.IsValid)
                return Json(new { Success = false, Message = "Fill in all mandatory fields" });

            var result = await _topicService.Update(viewModel);
            if (result.Validation != null && !result.Validation.IsValid)
                return Json(new { Success = false, Message = String.Join("<br/>", result.Validation.Errors.Select(e => e.ErrorMessage)) });

            return Json(new { Success = true, Message = "Topic updated successfully" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {   
            var userLogged = _userResolver.GetUserId();
            var result = await _topicService.Delete(Guid.Parse(id), Guid.Parse(userLogged));
            if (result != null)
                return Json(new { Success = true, Message = "Topic deleted successfully" });

            return Json(new { Success = false, Message = "Topic not found" });
        }
    }
}
