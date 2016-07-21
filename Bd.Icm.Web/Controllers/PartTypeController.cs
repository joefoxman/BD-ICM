using System.Collections.Generic;
using System.Web.Http;
using Bd.Icm.Core;

namespace Bd.Icm.Web.Controllers
{
    [AuthorizedRoles(Roles = new[] { RoleType.Administrator, RoleType.Contributor, RoleType.ReadOnly })]
    public class PartTypeController : ApiControllerBase
    {
        [Route("api/partTypes")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var partTypes = new List<Dto.PartType>
            {
                new Dto.PartType {PartTypeId = 0, Name = "N/A"},
                new Dto.PartType {PartTypeId = 1, Name = "HALB"},
                new Dto.PartType {PartTypeId = 2, Name = "ROH"},
                new Dto.PartType {PartTypeId = 3, Name = "ZICP"}
            };
            return Ok(partTypes);
        }
    }
}
