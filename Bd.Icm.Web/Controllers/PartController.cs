using System;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Bd.Icm.Core;

namespace Bd.Icm.Web.Controllers
{
    [AuthorizedRoles(Roles = new[] { RoleType.Administrator, RoleType.Contributor, RoleType.ReadOnly })]
    public class PartController : ApiControllerBase
    {
        [Route("api/parts/new")]
        [HttpGet]
        public IHttpActionResult Create()
        {
            var part = Part.NewPart();
            return Ok(Mapper.Map<Dto.Part>(part));
        }

        [Route("api/parts/{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var part = Part.GetPart(id);
            return Ok(Mapper.Map<Dto.Part>(part));
        }

        [Route("api/parts/nodes")]
        [HttpGet]
        public IHttpActionResult GetNodes(int? instrumentId, int? parentPartId)
        {
            var nodes = PartNodeList.GetPartNodeList(instrumentId, parentPartId);
            return Ok(nodes.Select(Mapper.Map<Dto.PartNode>));
        }

        [AuthorizedRoles(Roles = new[] { RoleType.Administrator, RoleType.Contributor })]
        [Route("api/parts/save")]
        [HttpPost]
        public IHttpActionResult Post(Dto.Part dto)
        {
            try
            {
                var obj = dto.IsNew ? Part.NewPart() : Part.GetPart(dto.Id);
                Mapper.Map(dto, obj);
                if (obj.IsValid == false)
                {
                    foreach (var brokenRule in obj.BrokenRulesCollection)
                    {
                        ModelState.AddModelError(brokenRule.Property, brokenRule.Description);
                    }
                    return BadRequest(ModelState);
                }
                obj = obj.Save();
                return Ok(Mapper.Map<Dto.Part>(obj));

            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return BadRequest("Error saving part.");
            }
        }

        [AuthorizedRoles(Roles = new[] { RoleType.Administrator, RoleType.Contributor })]
        [Route("api/parts/delete/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var obj = Part.GetPart(id);
                obj.Delete();
                obj = obj.Save();
                return Ok(true);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return BadRequest("Error deleting part.");
            }
        }

        [Route("api/parts/searchNames/{key}")]
        [HttpGet]
        public IHttpActionResult SearchNames(string key)
        {
            var parts = PartList.GetPartList(key);
            return Ok(parts.Select(Mapper.Map<PartInfo, Dto.Part>));
        }

    }
}
