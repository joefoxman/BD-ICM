using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Bd.Icm.Core;
using Bd.Icm.Web.Dto;

namespace Bd.Icm.Web.Controllers
{
    [AuthorizedRoles(Roles = new[] { RoleType.Administrator, RoleType.Contributor, RoleType.ReadOnly })]
    public class InstrumentController : ApiControllerBase
    {
        [Route("api/instruments")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var instruments = InstrumentList.GetInstrumentList();
            return Ok(instruments.Select(Mapper.Map<Dto.Instrument>));
        }

        [Route("api/instruments/searchParts/{instrumentId}/{searchKey}")]
        [HttpGet]
        public IHttpActionResult SearchParts(int instrumentId, string searchKey)
        {
            var searchResults = PartSearch.GetPartSearch(instrumentId, searchKey);
            return Ok(searchResults.Select(Mapper.Map<Dto.PartSearchResult>));
        }

        [Route("api/instruments/export/{id}/{version?}")]
        [HttpGet]
        public HttpResponseMessage ExportToExcel(int id, int? version)
        {
            var instrument = Instrument.GetInstrument(id, false, version ?? int.MaxValue);
            var ms = ExcelExporter.CreateInstrumentExport(new List<Instrument> {instrument});
            ms.Position = 0;
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StreamContent(ms)};
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            responseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            return responseMessage;
        }

        [Route("api/instruments/{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var instrument = Instrument.GetInstrument(id);
            var instrumentDto = Mapper.Map<Dto.Instrument>(instrument);
            return Ok(instrumentDto);
        }

        [Route("api/instruments/changeUsers/{id}")]
        [HttpGet]
        public IHttpActionResult CheckForChanges(int id)
        {
            var changeUsers = ChangeUserList.GetChangeUserList(id);
            return Ok(changeUsers.Select(Mapper.Map<ChangeUser, Dto.ChangeUser>));
        }

        [Route("api/instruments/new")]
        [HttpGet]
        public IHttpActionResult Create()
        {
            var instrument = Instrument.NewInstrument();
            return Ok(Mapper.Map<Dto.Instrument>(instrument));
        }

        //[Route("api/instruments/getMetadataChanges/{instrumentId}")]
        //[HttpGet]
        //public IHttpActionResult GetPartMetadataChanges(int instrumentId)
        //{
        //    var committer = InstrumentCommitUow.Create(instrumentId);
        //    return Ok(committer.MetadataChanges.Select(Mapper.Map<PartMetadataChange, Dto.PartMetadataChange>));
        //}

        [Route("api/instruments/getHistory/{instrumentId}")]
        [HttpGet]
        public IHttpActionResult GetPartHistory(int instrumentId)
        {
            try
            {
                var partHistory = PartChangeList.GetPartChangeList(instrumentId, new DateTime?(), new DateTime?());
                return Ok(partHistory.Select(Mapper.Map<PartChange, Dto.PartChange>));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString);
                return BadRequest("Error loading change history.");
            }
        }

        [Route("api/instruments/compare/{id}/{fromVersion}/{toVersion}")]
        [HttpGet]
        public async Task<IHttpActionResult> Compare(int id, int fromVersion, int toVersion)
        {
            try
            {
                var fromCommit = InstrumentCommit.GetInstrumentCommit(fromVersion);
                var toCommit = InstrumentCommit.GetInstrumentCommit(toVersion);
                var fromInstrument = Instrument.GetInstrument(id, false, fromCommit.EffectiveTo);
                var toInstrument = Instrument.GetInstrument(id, false, toCommit.EffectiveTo);
                var comparer = new InstrumentDiff(fromInstrument, toInstrument, fromCommit, toCommit);
                await comparer.CompareAsync();
                var diff = Mapper.Map<InstrumentDiff, Dto.InstrumentDiff>(comparer);
                return Ok(diff);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString);
                return BadRequest("Error comparing instruments.");
            }
        }

        [AuthorizedRoles(Roles = new[] { RoleType.Administrator, RoleType.Contributor })]
        [Route("api/instruments/copy")]
        [HttpPost]
        public IHttpActionResult Copy(Dto.Instrument dto)
        {
            var instrument = Instrument.GetInstrument(dto.InstrumentId);
            var copy = instrument.Copy();
            copy.Type = dto.Type;
            copy.SerialNumber = dto.SerialNumber;
            copy.NickName = dto.NickName;

            try
            {
                if (copy.IsValid == false)
                {
                    AddModelErrors(copy);
                    return BadRequest(ModelState);
                }
                copy = copy.Save();
                return Ok(Mapper.Map<Dto.Instrument>(copy));

            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return BadRequest("Error saving instrument.");
            }
        }

        [AuthorizedRoles(Roles = new[] { RoleType.Administrator, RoleType.Contributor })]
        [Route("api/instruments/save")]
        [HttpPost]
        public IHttpActionResult Save(Dto.Instrument dto)
        {
            try
            {
                var obj = dto.IsNew ? Instrument.NewInstrument() : Instrument.GetInstrument(dto.InstrumentId);
                Mapper.Map(dto, obj);
                if (obj.IsValid == false)
                {
                    AddModelErrors(obj);
                    return BadRequest(ModelState);
                }
                obj = obj.Save();
                return Ok(Mapper.Map<Dto.Instrument>(obj));

            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return BadRequest("Error saving instrument.");
            }
        }

    }
}
