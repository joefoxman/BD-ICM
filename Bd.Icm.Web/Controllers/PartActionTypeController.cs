using System.Collections.Generic;
using System.Web.Http;
using Bd.Icm.Core;

namespace Bd.Icm.Web.Controllers
{
    [AuthorizedRoles(Roles = new[] { RoleType.Administrator, RoleType.Contributor, RoleType.ReadOnly })]
    public class PartActionTypeController : ApiControllerBase
    {
        [Route("api/partActionTypes")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var partActionTypes = new List<Dto.PartActionType>
            {
                new Dto.PartActionType {PartActionTypeId = 1, Name = "Calibration"},
                new Dto.PartActionType {PartActionTypeId = 2, Name = "Repair"}
            };
            return Ok(partActionTypes);
        }
    }
}
