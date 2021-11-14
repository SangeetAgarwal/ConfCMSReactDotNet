using Api.ConfCMSReactDotNet.Models;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Api.ConfCMSReactDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("AddRolesToUser")]
        [Authorize]
        public async Task AddRolesToUser([FromBody] User user)
        {
            var usr = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(user.Email);

            if (usr == null) throw new Exception($"user with email of {user.Email} not found");

            var claims = new Dictionary<string, object>
            {
                {
                   ClaimTypes.Role, user.Roles
                }
            };

            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(usr.Uid, claims);

        }

        [HttpGet("GetListOfUsers")]
        [Authorize]
        public async Task<List<ExportedUserRecord>> GetUsers()
        {
            var users = new List<ExportedUserRecord>();
            var enumerator = FirebaseAuth.DefaultInstance.ListUsersAsync(null).GetAsyncEnumerator();
            while (await enumerator.MoveNextAsync())
            {
                ExportedUserRecord user = enumerator.Current;
                Debug.WriteLine($"User: {user.Uid}");
                users.Add(user);
            }

            return users;
        }
    }
}
