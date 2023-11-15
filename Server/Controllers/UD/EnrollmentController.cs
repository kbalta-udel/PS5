using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models;
using OCTOBER.Server.Controllers.Base;
using OCTOBER.Shared.DTO;

namespace OCTOBER.Server.Controllers.UD
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : BaseController, GenericRestController<EnrollmentDTO>
    {
        public EnrollmentController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }


        [HttpDelete]
        [Route("Delete/{StudentID}/ {SectionID}/ {SchoolID}")]
        public async Task<IActionResult> Delete(int StudentId, int SectionId, int SchoolId)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Enrollments
                    .Where(x => x.StudentId == StudentId)
                    .Where(x => x.SectionId == SectionId)
                    .Where(x => x.SchoolId == SchoolId)
                    .FirstOrDefaultAsync();

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

                                                                public Task<IActionResult> Delete(int KeyVal)
                                                                {
                                                                    throw new NotImplementedException();
                                                                }
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var result = await _context.Enrollments.Select(sp => new EnrollmentDTO
                {
                 StudentId = sp.StudentId,
                 SectionId = sp.SectionId,
                 SchoolId = sp.SchoolId,
                 ModifiedDate = sp.ModifiedDate,
                 CreatedBy = sp.CreatedBy,
                 CreatedDate = sp.CreatedDate,
                 EnrollDate = sp.EnrollDate,
                 FinalGrade = sp.FinalGrade,
                 ModifiedBy = sp.ModifiedBy,
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
        [Route("Get/{StudentID}/ {SectionID}/ {SchoolID}")]
        public async Task<IActionResult> Get(int StudentId, int SectionId, int SchoolId)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                EnrollmentDTO? result = await _context
                    .Enrollments
                    .Where(x => x.StudentId == StudentId)
                    .Where(x => x.SchoolId == SchoolId)
                    .Where(x => x.SectionId == SectionId)
                     .Select(sp => new EnrollmentDTO
                     {
                         StudentId = sp.StudentId,
                         SectionId  = sp.SectionId,
                         ModifiedBy =sp.ModifiedBy,
                         FinalGrade = sp.FinalGrade,
                         EnrollDate = sp.EnrollDate,
                         CreatedDate = sp.CreatedDate,
                         CreatedBy  = sp.CreatedBy,
                         ModifiedDate   = sp.ModifiedDate,
                         SchoolId   = sp.SchoolId,
                     })
                .SingleOrDefaultAsync();

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

                                                                    public Task<IActionResult> Get(int KeyVal)
                                                                    {
                                                                        throw new NotImplementedException();
                                                                    }
        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody] EnrollmentDTO _EnrollmentDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Enrollments.Where(x => x.StudentId == _EnrollmentDTO.StudentId)
                    .Where(x => x.SectionId == _EnrollmentDTO.SectionId)
                    .Where(x => x.SchoolId == _EnrollmentDTO.SchoolId).FirstOrDefaultAsync();

                if (itm == null)
                {
                    Enrollment e = new Enrollment
                    {
                        StudentId = _EnrollmentDTO.StudentId,
                     SchoolId = _EnrollmentDTO.SchoolId,
                     SectionId = _EnrollmentDTO.SectionId,
                     EnrollDate = _EnrollmentDTO.EnrollDate,
                     FinalGrade = _EnrollmentDTO.FinalGrade,
                    };
                    _context.Enrollments.Add(e);
                    await _context.SaveChangesAsync();
                    await _context.Database.CommitTransactionAsync();
                }
                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

                                    
        [HttpPut]
        [Route("Put")]

        public async Task<IActionResult> Put([FromBody] EnrollmentDTO _EnrollmentDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Enrollments.Where(x => x.StudentId == _EnrollmentDTO.StudentId)
                    .Where(x => x.SectionId == _EnrollmentDTO.SectionId)
                    .Where(x => x.SchoolId == _EnrollmentDTO.SchoolId).FirstOrDefaultAsync();

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
