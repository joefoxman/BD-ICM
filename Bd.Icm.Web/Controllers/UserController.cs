using System;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Bd.Icm.Core;

namespace Bd.Icm.Web.Controllers
{
    [Authorize]
    [AuthorizedRoles(Roles = new[] { RoleType.Administrator, RoleType.Contributor, RoleType.ReadOnly })]
    public class UserController : ApiControllerBase
    {
        [Route("api/users")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            Logger.Info("/api/users");
            try
            {
                var users = UserList.GetUserList(true);
                var dtos = Mapper.Map<UserInfo[], Dto.User[]>(users.ToArray());
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        [Route("api/users/create")]
        [HttpGet]
        public IHttpActionResult Create()
        {
            Logger.Info("/api/users/create");
            try
            {
                var user = Icm.User.NewUser();
                var dto = Mapper.Map<User, Dto.User>(user);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        [AuthorizedRoles(Roles = new[] { RoleType.Administrator, RoleType.Contributor, RoleType.ReadOnly })]
        [Route("api/users/current")]
        [HttpGet]
        public IHttpActionResult Current()
        {
            Logger.Info("/api/users/current");
            try
            {
                var user = Icm.User.GetUser(AppUserState.UserId);
                var dto = Mapper.Map<User, Dto.User>(user);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        [AuthorizedRoles(Roles = new[] { RoleType.Administrator, RoleType.Contributor, RoleType.ReadOnly })]
        [Route("api/users/{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var user = Icm.User.GetUser(id);
            var dto = Mapper.Map<User, Dto.User>(user);
            return Ok(dto);
        }

        [AuthorizedRoles(Roles = new[] { RoleType.Administrator, RoleType.Contributor, RoleType.ReadOnly })]
        [Route("api/users/{username}")]
        [HttpGet]
        public IHttpActionResult Get(string username)
        {
            var user = Icm.User.GetUser(username);
            var dto = Mapper.Map<User, Dto.User>(user);
            return Ok(dto);
        }

        [AuthorizedRoles(Roles = new[] { RoleType.Administrator })]
        [Route("api/users/save")]
        [HttpPost]
        public IHttpActionResult Save(Dto.User user)
        {
            try
            {
                var obj = user.IsNew ? Icm.User.NewUser() : Icm.User.GetUser(user.Id);
                Mapper.Map(user, obj);
                MapRoles(user.Roles, obj);
                if (user.RemovePassword)
                {
                    obj.Password = "";
                }
                else if (!string.IsNullOrWhiteSpace(user.NewPassword))
                {
                    obj.Password = Icm.User.HashPassword(user.NewPassword);
                }
                if (obj.IsValid == false)
                {
                    AddModelErrors(obj);
                    return BadRequest(ModelState);
                }
                obj = obj.Save();
                var dto = Mapper.Map<User, Dto.User>(obj);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return BadRequest("Error saving user.");
            }
        }

        private void MapRoles(RoleType[] selectedRoles, Icm.User user)
        {
            // Delete roles that are no longer selected.
            for (int i = user.Roles.Count - 1; i >= 0; i--)
            {
                var existingRole = user.Roles[i];
                var exists = selectedRoles.Contains(existingRole.Role);
                if (exists == false)
                {
                    user.Roles.Remove(existingRole);
                }
            }
            // Add roles that are selected but don't exist in the User.
            foreach (var selectedRole in selectedRoles)
            {
                var exists = user.Roles.SingleOrDefault(x => x.Role == selectedRole);
                {
                    if (exists == null)
                    {
                        var newRole = UserRole.NewUserRole();
                        newRole.Role = selectedRole;
                        user.Roles.Add(newRole);
                    }
                }
            }
        }

        [AuthorizedRoles(Roles = new[] { RoleType.Administrator })]
        [Route("api/users/delete/{id:int}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var obj = Icm.User.GetUser(id);
            obj.Delete();
            obj = obj.Save();
            var dto = Mapper.Map<User, Dto.User>(obj);
            return Ok(dto);
        }
    }
}
