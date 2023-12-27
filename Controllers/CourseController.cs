using Academy.Model;
using Academy.Repository;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using Sieve.Services;
using System.Data;

namespace Academy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourseController : ControllerBase
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ILogger<CourseController> _logger;

        public CourseController(IGenericRepository genericRepository, ILogger<CourseController> logger)
        {
            _genericRepository = genericRepository;
            _logger = logger;   
        }


        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var sql = "dbo.sp_GetCourseById ";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                var result = await _genericRepository.QueryFirstOrDefaultAsync<Course>(sql, parameters, CommandType.StoredProcedure);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location CourseController.GetById : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var sql = "SELECT * FROM Course";
                var parameters = new DynamicParameters();
                var result = await _genericRepository.QueryAsync<Course>(sql, parameters, CommandType.Text);
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location CourseController.GetAll : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }



        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]Course course)
        {
            try
            {
                if (course == null)
                {
                    return BadRequest("Invalid model");
                }

                var sql = "dbo.sp_InsertCourse";
                var parameters = new DynamicParameters();
                parameters.Add("@CourseName", course.CourseName, DbType.String);
                parameters.Add("@Duration", course.Duration, DbType.String);
                parameters.Add("@BatchDetail", course.BatchDetail, DbType.String);
                parameters.Add("@No_Of_Assessment", course.No_Of_Assessment, DbType.Int64);
                parameters.Add("@No_Of_Project", course.No_Of_Project, DbType.Int64);

                await _genericRepository.QueryAsync<Course>(sql, parameters, CommandType.StoredProcedure);

                _logger.LogInformation("A new Course was created :" + course.CourseName);

                return Ok("Entity created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location CourseController.Create : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Course course)
        {
            try
            {
                if (course == null)
                {
                    return BadRequest("Invalid model");
                }

                var sql = "dbo.sp_UpdateCourse";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.Int64);
                parameters.Add("@CourseName", course.CourseName, DbType.String);
                parameters.Add("@Duration", course.Duration, DbType.String);
                parameters.Add("@BatchDetail", course.BatchDetail, DbType.String);
                parameters.Add("@No_Of_Assessment", course.No_Of_Assessment, DbType.Int64);
                parameters.Add("@No_Of_Project", course.No_Of_Project, DbType.Int64);



                await _genericRepository.QueryAsync<Batch>(sql, parameters, CommandType.StoredProcedure);

                return Ok("Entity updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location CourseController.Update : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var sql = "sp_DeleteCourse";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);



                await _genericRepository.QueryAsync<Course>(sql, parameters, CommandType.StoredProcedure);

                return Ok("Entity deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location CourseController.Delete : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }   
}
}
