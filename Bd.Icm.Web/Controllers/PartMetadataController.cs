using System;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Bd.Icm.Core;

namespace Bd.Icm.Web.Controllers
{
    [AuthorizedRoles(Roles = new[] { RoleType.Administrator, RoleType.Contributor, RoleType.ReadOnly })]
    public class PartMetadataController : ApiControllerBase
    {
        [Route("api/partMetadata/new")]
        [HttpGet]
        public IHttpActionResult Create()
        {
            var action = PartMetadata.NewPartMetadata();
            return Ok(Mapper.Map<Dto.PartMetadata>(action));
        }

        [Route("api/partMetadata/{partId}/{partMetadataId}")]
        [HttpGet]
        public IHttpActionResult Get(int partId, int partMetadataId)
        {
            var part = Part.GetPart(partId);
            var action = part.Metadata.SingleOrDefault(x => x.Id == partMetadataId);
            if (action == null)
            {
                return BadRequest("Part metadata not found in part.");
            }
            return Ok(Mapper.Map<Dto.PartMetadata>(action));
        }

        [Route("api/partMetadata/getAll/{partId}")]
        [HttpGet]
        public IHttpActionResult GetAll(int partId)
        {
            var part = Part.GetPart(partId);
            return Ok(part.Metadata.Select(Mapper.Map<Dto.PartMetadata>));
        }

        [AuthorizedRoles(Roles = new[] { RoleType.Administrator, RoleType.Contributor })]
        [Route("api/partMetadata/save/{partId}")]
        [HttpPost]
        public IHttpActionResult Post(int partId, [FromBody]Dto.PartMetadata dto)
        {
            try
            {
                var part = Part.GetPart(partId);
                PartMetadata obj;
                if (dto.IsNew)
                {
                    obj = PartMetadata.NewPartMetadata();
                    part.Metadata.Add(obj);
                }
                else
                {
                    obj = part.Metadata.SingleOrDefault(x => x.Id == dto.Id);
                    if (obj == null)
                    {
                        return BadRequest("Part metadata not found in part.");
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
                return Ok(Mapper.Map<Dto.PartMetadata>(obj));

            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return BadRequest("Error saving part metadata.");
            }
        }

        [AuthorizedRoles(Roles = new[] { RoleType.Administrator, RoleType.Contributor })]
        [Route("api/partMetadata/delete/{partId}/{partMetadataId}")]
        [HttpDelete]
        public IHttpActionResult Delete(int partId, int partMetadataId)
        {
            try
            {
                var part = Part.GetPart(partId);
                var action = part.Metadata.SingleOrDefault(x => x.Id == partMetadataId);
                if (action == null)
                    return BadRequest("Part metadata not found in part.");
                part.Metadata.Remove(action);
                part = part.Save();
                return Ok(true);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return BadRequest("Error deleting part metadata.");
            }
        }
    }
}
