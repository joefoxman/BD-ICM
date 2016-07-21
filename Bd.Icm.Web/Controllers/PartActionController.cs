using System;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Bd.Icm.Core;

namespace Bd.Icm.Web.Controllers
{
    [AuthorizedRoles(Roles = new[] { RoleType.Administrator, RoleType.Contributor, RoleType.ReadOnly })]
    public class PartActionController : ApiControllerBase
    {
        [Route("api/partActions/new")]
        [HttpGet]
        public IHttpActionResult Create()
        {
            var action = PartAction.NewPartAction();
            action.ActionDate = DateTime.Now.Date;
            return Ok(Mapper.Map<Dto.PartAction>(action));
        }

        [Route("api/partActions/{partId}/{partActionId}")]
        [HttpGet]
        public IHttpActionResult Get(int partId, int partActionId)
        {
            var part = Part.GetPart(partId);
            var action = part.Actions.SingleOrDefault(x => x.Id == partActionId);
            if (action == null)
            {
                return BadRequest("Part action not found in part.");
            }
            return Ok(Mapper.Map<Dto.PartAction>(action));
        }

        [Route("api/partActions/getAll/{partId}")]
        [HttpGet]
        public IHttpActionResult GetAll(int partId)
        {
            var part = Part.GetPart(partId);
            return Ok(part.Actions.Select(Mapper.Map<Dto.PartAction>));
        }

        [AuthorizedRoles(Roles = new[] { RoleType.Administrator, RoleType.Contributor })]
        [Route("api/partActions/save/{partId}")]
        [HttpPost]
        public IHttpActionResult Post(int partId, [FromBody]Dto.PartAction dto)
        {
            try
            {
                var part = Part.GetPart(partId);
                PartAction obj;
                if (dto.IsNew)
                {
                    obj = PartAction.NewPartAction();
                    part.Actions.Add(obj);
                }
                else
                {
                    obj = part.Actions.SingleOrDefault(x => x.Id == dto.Id);
                    if (obj == null)
                    {
                        return BadRequest("Part action not found in part.");
                    }
                }
                Mapper.Map(dto, obj);
                if (obj.IsValid == false)
                {
                    foreach (var brokenRule in obj.BrokenRulesCollection)
                    {
                        ModelState.AddModelError(brokenRule.Property, brokenRule.Description);
                    }
                    return BadRequest(ModelState);
                }
                part = part.Save();
                return Ok(Mapper.Map<Dto.PartAction>(obj));

            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return BadRequest("Error saving part action.");
            }
        }

        [AuthorizedRoles(Roles = new[] { RoleType.Administrator, RoleType.Contributor })]
        [Route("api/partActions/delete/{partId}/{partActionId}")]
        [HttpDelete]
        public IHttpActionResult Delete(int partId, int partActionId)
        {
            try
            {
                var part = Part.GetPart(partId);
                var action = part.Actions.SingleOrDefault(x => x.Id == partActionId);
                if (action == null)
                    return BadRequest("Part action not found in part.");
                part.Actions.Remove(action);
                part = part.Save();
                return Ok(true);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return BadRequest("Error deleting part.");
            }
        }
    }
}
