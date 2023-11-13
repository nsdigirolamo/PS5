using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models;
using OCTOBER.Server.Controllers.Base;
using OCTOBER.Shared.DTO;
using System.Diagnostics;
using Telerik.Blazor.Components;
using Telerik.DataSource.Extensions;
using Telerik.SvgIcons;
using static System.Collections.Specialized.BitVector32;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OCTOBER.Server.Controllers.UD
{
    public class SectionController : BaseController, GenericRestController<SectionDTO>
    {
        public SectionController(OCTOBEROracleContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }

        [HttpDelete]
        [Route("Delete/{SectionId}")]

        public async Task<IActionResult> Delete(int SectionId)
        {
            Debugger.Launch();
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Sections.Where(x => x.SectionId == SectionId).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Sections.Remove(itm);
                }
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            Debugger.Launch();
            try
            {
                await _context.Database.BeginTransactionAsync();

                var result = await _context.Sections.Select(sp => new SectionDTO
                {
                    SectionId = sp.SectionId,
                    CourseNo = sp.CourseNo,
                    SectionNo = sp.SectionNo,
                    StartDateTime = sp.StartDateTime,
                    Location = sp.Location,
                    InstructorId = sp.InstructorId,
                    Capacity = sp.Capacity,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    SchoolId = sp.SchoolId
                })
                .ToListAsync();
                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpGet]
        [Route("Get/{SectionId}")]
        public async Task<IActionResult> Get(int SectionId)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                SectionDTO? result = await _context.Sections
                    .Where(x => x.SectionId == SectionId)
                    .Select(sp => new SectionDTO
                    {
                        SectionId = sp.SectionId,
                        CourseNo = sp.CourseNo,
                        SectionNo = sp.SectionNo,
                        StartDateTime = sp.StartDateTime,
                        Location = sp.Location,
                        InstructorId = sp.InstructorId,
                        Capacity = sp.Capacity,
                        CreatedBy = sp.CreatedBy,
                        CreatedDate = sp.CreatedDate,
                        ModifiedBy = sp.ModifiedBy,
                        ModifiedDate = sp.ModifiedDate,
                        SchoolId = sp.SchoolId
                    })
                .SingleAsync();
                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        public Task<IActionResult> Post([FromBody] SectionDTO _T)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("Put")]
        public async Task<IActionResult> Put([FromBody] SectionDTO _SectionDTO)
        {

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Sections.Where(x => x.SectionId == _SectionDTO.SectionId && x.SchoolId == _SectionDTO.SchoolId).FirstOrDefaultAsync();

                itm.SectionNo = _SectionDTO.SectionNo;
                itm.CourseNo = _SectionDTO.CourseNo;
                itm.StartDateTime = _SectionDTO.StartDateTime;
                itm.Location = _SectionDTO.Location;
                itm.InstructorId = _SectionDTO.InstructorId;

                _context.Sections.Update(itm);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }
    }
}

