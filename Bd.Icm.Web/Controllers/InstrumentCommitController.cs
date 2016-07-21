using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Bd.Icm.Core;
using Bd.Icm.Web.Dto;

namespace Bd.Icm.Web.Controllers
{
    [AuthorizedRoles(Roles = new[] { RoleType.Administrator, RoleType.Contributor })]
    public class InstrumentCommitController : ApiControllerBase
    {
        [Route("api/instrumentCommits/{id}")]
        [HttpGet]
        public IHttpActionResult GetAll(int id)
        {
            try
            {
                var commits = InstrumentCommitList.GetInstrumentCommitList(id);
                return Ok(commits.Select(Mapper.Map<Dto.InstrumentCommit>));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return BadRequest("Error loading commit history.");
            }
        }

        [Route("api/instrumentCommit/getUncommittedChanges/{instrumentId}")]
        [HttpGet]
        public IHttpActionResult GetUncommittedChanges(int instrumentId)
        {
            var committer = InstrumentCommitUow.Create(instrumentId);
            var uncommittedChanges = new List<UncommittedChange>();
            uncommittedChanges.AddRange(committer.Changes.Select(Mapper.Map<PartChange, UncommittedChange>));
            uncommittedChanges.AddRange(committer.MetadataChanges.Select(Mapper.Map<PartMetadataChange, UncommittedChange>));
            return Ok(uncommittedChanges.OrderBy(x => x.EffectiveFrom));
        }

        [AuthorizedRoles(Roles = new[] { RoleType.Committer })]
        [Route("api/instrumentCommit/commitChanges")]
        [HttpPost]
        public IHttpActionResult Post(Dto.CommitData commitData)
        {
            try
            {
                var committer = InstrumentCommitUow.Create(commitData.InstrumentCommit.InstrumentId);
                Mapper.Map(commitData.InstrumentCommit, committer.Commit);
                committer.LastChangeVersion = commitData.LastChange.EffectiveFrom;
                committer = committer.Save();
                return Ok(Mapper.Map<Dto.Instrument>(committer.Instrument));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return BadRequest("Error committing changes.");
            }
        }
    }
}
