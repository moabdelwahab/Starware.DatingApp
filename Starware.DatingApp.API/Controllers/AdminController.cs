using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Starware.DatingApp.Core.Domains;
using Microsoft.EntityFrameworkCore;

namespace Starware.DatingApp.API.Controllers
{

    public class AdminController : BaseApiController
    {
        private UserManager<AppUser> userManager;

        public AdminController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize("newPolicy")]
        [Route("users-with-roles")]
        public async Task<ActionResult> GetUsersWithRoles()
        {
            var users = await userManager.Users
                           .Include(user => user.UserRoles)
                           .ThenInclude(r => r.AppRole)
                           .OrderBy(u => u.UserName)
                           .Select(u => new
                           {
                               u.Id,
                               u.UserName,
                               Roles = u.UserRoles.Select(r => r.AppRole.Name)
                           }).ToListAsync();

            return Ok(users);
        }

        [Authorize()]
        [HttpPost]
        [Route("edit-roles/{username}")]
        public async Task<ActionResult> EditRoles(string username,[FromQuery] string roles)
        {
            var selectedRoles = roles.Split(",").ToArray();

            var user = await userManager.FindByNameAsync(username);

            var userRoles = await userManager.GetRolesAsync(user);

            var addingRoles = await userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!addingRoles.Succeeded) return BadRequest();

            var removeRoles = await userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            return Ok();

        }




    }
}
