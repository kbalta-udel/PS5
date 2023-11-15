using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models;
using OCTOBER.Server.Controllers.Base;
using OCTOBER.Shared.DTO;
using Telerik.Blazor.Components;
using static Duende.IdentityServer.Models.IdentityResources;

namespace OCTOBER.Server.Controllers.UD
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController, GenericRestController<StudentDTO>
    {
        public StudentController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }
        // studentid schoolid
        [HttpDelete]
        [Route("Delete/ {SchoolID}/ {StudentID}")]
        public async Task<IActionResult> Delete(int SchoolId, int StudentID)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Students
                    .Where(x => x.SchoolId == SchoolId)
                    .Where(x => x.StudentId == StudentID)
                    .FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Students.Remove(itm);
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

                var result = await _context.Students.Select(sp => new StudentDTO
                {
                    StudentId = sp.StudentId,
                    SchoolId = sp.SchoolId,
                    Salutation = sp.Salutation,
                    FirstName = sp.FirstName,
                    LastName = sp.LastName,
                    StreetAddress = sp.StreetAddress,
                    Zip = sp.Zip,
                    Phone = sp.Phone,
                    Employer = sp.Employer,
                    RegistrationDate = sp.RegistrationDate,
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
        [Route("Get/ {SchoolID}/ {StudentID}")]
        public async Task<IActionResult> Get(int SchoolId, int StudentId)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                StudentDTO? result = await _context
                    .Students
                    .Where(x => x.SchoolId == SchoolId)
                    .Where(x => x.StudentId == StudentId)
                     .Select(sp => new StudentDTO
                     {
                         StudentId = sp.StudentId,
                         SchoolId = sp.SchoolId,
                         Salutation = sp.Salutation,
                         FirstName = sp.FirstName,
                         LastName = sp.LastName,
                         StreetAddress = sp.StreetAddress,
                         Zip = sp.Zip,
                         Phone = sp.Phone,
                         Employer = sp.Employer,
                         RegistrationDate = sp.RegistrationDate,
                         CreatedBy = sp.CreatedBy,
                         CreatedDate = sp.CreatedDate,
                         ModifiedBy = sp.ModifiedBy,
                         ModifiedDate = sp.ModifiedDate,
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
        public async Task<IActionResult> Post([FromBody] StudentDTO _StudentDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Students
                    .Where(x => x.StudentId == _StudentDTO.StudentId)
                    .Where(x => x.SchoolId == _StudentDTO.SchoolId).FirstOrDefaultAsync();

                if (itm == null)
                {
                    Student s = new Student
                    {
                        SchoolId = _StudentDTO.SchoolId,
                        StudentId = _StudentDTO.StudentId,
                        FirstName = _StudentDTO.FirstName,
                        LastName = _StudentDTO.LastName,
                        Phone = _StudentDTO.Phone,
                        Salutation = _StudentDTO.Salutation,
                        StreetAddress = _StudentDTO.StreetAddress,
                        Zip = _StudentDTO.Zip,
                        Employer = _StudentDTO.Employer,
                        RegistrationDate = _StudentDTO.RegistrationDate

                    };
                    _context.Students.Add(s);
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
        public async Task<IActionResult> Put([FromBody] StudentDTO _StudentDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Students
                     .Where(x => x.StudentId == _StudentDTO.StudentId)
                    .Where(x => x.SchoolId == _StudentDTO.SchoolId).FirstOrDefaultAsync();


                itm.FirstName = _StudentDTO.FirstName;
                itm.LastName = _StudentDTO.LastName;
                itm.Phone = _StudentDTO.Phone;
                itm.Salutation = _StudentDTO.Salutation;
                itm.StreetAddress = _StudentDTO.StreetAddress;
                itm.Zip = _StudentDTO.Zip;
                itm.Employer = _StudentDTO.Employer;
                itm.RegistrationDate = _StudentDTO.RegistrationDate;

                _context.Students.Update(itm);
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


