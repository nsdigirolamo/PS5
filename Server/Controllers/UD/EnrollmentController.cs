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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OCTOBER.Server.Controllers.UD
{
    public class EnrollmentController : BaseController, GenericRestController<EnrollmentDTO>
    {
        public EnrollmentController(OCTOBEROracleContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }

        [HttpDelete]
        [Route("Delete/{StudentId}")]

        public async Task<IActionResult> Delete(int StudentId)
        {
            Debugger.Launch();
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Enrollments.Where(x => x.StudentId == StudentId).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Enrollments.Remove(itm);
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

                var result = await _context.Enrollments.Select(sp => new EnrollmentDTO
                {
                    StudentId = sp.StudentId,
                    SectionId = sp.SectionId,
                    EnrollDate = sp.EnrollDate,
                    FinalGrade = sp.FinalGrade,
                    SchoolId = sp.SchoolId,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
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
        [Route("Get/{StudentId}")]
        public async Task<IActionResult> Get(int StudentId)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                EnrollmentDTO? result = await _context.Enrollments
                    .Where(x => x.StudentId == StudentId)
                    .Select(sp => new EnrollmentDTO
                    {
                        StudentId = sp.StudentId,
                        SectionId = sp.SectionId,
                        EnrollDate = sp.EnrollDate,
                        FinalGrade = sp.FinalGrade,
                        SchoolId = sp.SchoolId,
                        CreatedBy = sp.CreatedBy,
                        CreatedDate = sp.CreatedDate,
                        ModifiedBy = sp.ModifiedBy,
                        ModifiedDate = sp.ModifiedDate,
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

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public Task<IActionResult> Post([FromBody] EnrollmentDTO _T)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("Put")]
        public async Task<IActionResult> Put([FromBody] EnrollmentDTO _EnrollmentDTO)
        {

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Enrollments.Where(
                    x => x.StudentId == _EnrollmentDTO.StudentId &&
                    x.SectionId == _EnrollmentDTO.SectionId &&
                    x.SchoolId == _EnrollmentDTO.SchoolId).FirstOrDefaultAsync();

                itm.EnrollDate = _EnrollmentDTO.EnrollDate;
                itm.FinalGrade = _EnrollmentDTO.FinalGrade;

                _context.Enrollments.Update(itm);
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

