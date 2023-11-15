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

    public class GradeController : BaseController, GenericRestController<GradeDTO>
    {
        public GradeController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }



        [HttpDelete]
        [Route("Delete/{SchoolID}/ {StudentID}/ {SectionID}/")]
        public async Task<IActionResult> Delete(int SchoolID, int StudentID, int SectionID, string GradeTypeCode, int GradeCodeOccurrence)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Grades
                    .Where(x => x.SchoolId == SchoolID)
                    .Where(x => x.StudentId == StudentID)
                    .Where(x => x.SectionId == SectionID)
                    .Where(x => x.GradeTypeCode == GradeTypeCode)
                    .Where(x => x.GradeCodeOccurrence == GradeCodeOccurrence).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Grades.Remove(itm);
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

                var result = await _context.Grades.Select(sp => new GradeDTO
                {
                    SectionId = sp.SectionId,
                    StudentId = sp.StudentId,
                    SchoolId = sp.SchoolId,
                    Comments = sp.Comments,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    GradeCodeOccurrence = sp.GradeCodeOccurrence,
                    GradeTypeCode  = sp.GradeTypeCode,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    NumericGrade = sp.NumericGrade,
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
        [Route("Get/{SchoolID}/{StudentID}/ {SectionID}")]
        public async Task<IActionResult> Get(int SchoolID, int StudentID, int SectionID, string GradeTypeCode, int GradeCodeOccurrence)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                GradeDTO? result = await _context
                    .Grades
                    .Where(x => x.SchoolId == SchoolID)
                    .Where(x => x.StudentId == StudentID)
                    .Where(x => x.SectionId == SectionID)
                    .Where(x => x.GradeTypeCode == GradeTypeCode)
                    .Where(x => x.GradeCodeOccurrence == GradeCodeOccurrence)
                     .Select(sp => new GradeDTO
                     {
                         GradeTypeCode = sp.GradeTypeCode,
                         GradeCodeOccurrence = sp.GradeCodeOccurrence,
                         ModifiedBy = sp.ModifiedBy,
                         NumericGrade = sp.NumericGrade,
                         CreatedDate = sp.CreatedDate,
                         CreatedBy = sp.CreatedBy,
                         Comments = sp.Comments,
                         ModifiedDate = sp.ModifiedDate
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
        public async Task<IActionResult> Post([FromBody] GradeDTO _GradeDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Grades
                    .Where(x => x.SchoolId == _GradeDTO.SchoolId)
                    .Where(x => x.StudentId == _GradeDTO.StudentId)
                    .Where(x => x.SectionId == _GradeDTO.SectionId)
                    .Where(x => x.GradeTypeCode == _GradeDTO.GradeTypeCode)
                    .Where(x => x.GradeCodeOccurrence == _GradeDTO.GradeCodeOccurrence).FirstOrDefaultAsync();

                if (itm == null)
                {
                    Grade g = new Grade
                    {
                        GradeCodeOccurrence = _GradeDTO.GradeCodeOccurrence,
                        GradeTypeCode = _GradeDTO.GradeTypeCode,
                        NumericGrade = _GradeDTO.NumericGrade,
                        Comments = _GradeDTO.Comments,
                    };
                    _context.Grades.Add(g);
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
        public async Task<IActionResult> Put([FromBody] GradeDTO _GradeDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Grades
                    .Where(x => x.SchoolId == _GradeDTO.SchoolId)
                    .Where(x => x.StudentId == _GradeDTO.StudentId)
                    .Where(x => x.SectionId == _GradeDTO.SectionId)
                    .Where(x => x.GradeTypeCode == _GradeDTO.GradeTypeCode)
                    .Where(x => x.GradeCodeOccurrence == _GradeDTO.GradeCodeOccurrence).FirstOrDefaultAsync();

                itm.GradeCodeOccurrence = _GradeDTO.GradeCodeOccurrence;
                itm.GradeTypeCode = _GradeDTO.GradeTypeCode;
                itm.NumericGrade = _GradeDTO.NumericGrade;
                itm.Comments = _GradeDTO.Comments;

                _context.Grades.Update(itm);
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
