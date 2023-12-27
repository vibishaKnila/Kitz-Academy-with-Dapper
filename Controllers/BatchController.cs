using Academy.Model;
using Academy.Repository;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using Sieve.Services;
using System.Data;

namespace Academy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class BatchController : ControllerBase
    {
        private readonly IGenericRepository _genericRepository;
        readonly ILogger<BatchController> _logger;



        public BatchController(IGenericRepository genericRepository, ILogger<BatchController> logger)
        {
            _genericRepository = genericRepository;
            _logger = logger;

        }


        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var sql = "dbo.sp_GetBatchById";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                var result = await _genericRepository.QueryFirstOrDefaultAsync<Batch>(sql, parameters, CommandType.StoredProcedure);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location BatchController.GetById : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var sql = "select * from Batch ";
                var parameters = new DynamicParameters();
                var result = await _genericRepository.QueryAsync<Batch>(sql, parameters, CommandType.Text);




                if (!result.Any())
                {
                    return NotFound("No records found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location BatchController.GetAll : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }



        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Batch batch)
        {
            try
            {
                if (batch == null)
                {
                    return BadRequest("Invalid model");
                }

                var sql = "dbo.sp_InsertBatch";
                var parameters = new DynamicParameters();
                parameters.Add("@CourseName", batch.CourseName, DbType.String);
                parameters.Add("@Skills", batch.Skills, DbType.String);
                parameters.Add("@TrainerName", batch.TrainerName, DbType.String);
                parameters.Add("@BatchType", batch.BatchType, DbType.String);
                parameters.Add("@BatchDuration", batch.BatchDuration, DbType.String);
                parameters.Add("@BatchDate", batch.BatchDate, DbType.DateTime);
                parameters.Add("@BatchStatus", batch.BatchStatus, DbType.String);

                await _genericRepository.QueryAsync<Batch>(sql, parameters, CommandType.StoredProcedure);

                _logger.LogInformation("A new batch was created for :" + batch.CourseName);


                return Ok("Entity created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location BatchController.Create : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Batch batch)
        {
            try
            {
                if (batch == null)
                {
                    return BadRequest("Invalid model");
                }

                var sql = "dbo.sp_UpdateBatch";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.Int64);
                parameters.Add("@CourseName", batch.CourseName, DbType.String);
                parameters.Add("@Skills", batch.Skills, DbType.String);
                parameters.Add("@TrainerName", batch.TrainerName, DbType.String);
                parameters.Add("@BatchType", batch.BatchType, DbType.String);
                parameters.Add("@BatchDuration", batch.BatchDuration, DbType.String);
                parameters.Add("@BatchDate", batch.BatchDate, DbType.DateTime);
                parameters.Add("@BatchStatus", batch.BatchStatus, DbType.String);


                await _genericRepository.QueryAsync<Batch>(sql, parameters, CommandType.StoredProcedure);

                return Ok("Entity updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location BatchController.GetById : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var sql = "sp_DeleteBatch";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);



                await _genericRepository.QueryAsync<Batch>(sql, parameters, CommandType.StoredProcedure);

                return Ok("Entity deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location BatchController.GetById : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
