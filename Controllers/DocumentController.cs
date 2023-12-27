using Academy.Model;
using Academy.Repository;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection.Metadata;
using System.Xml.Linq;
using Document = Academy.Model.Document;

namespace Academy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DocumentController : ControllerBase
    {
       private readonly IGenericRepository _genericRepository;
       private readonly ILogger<DocumentController> _logger;



        public DocumentController(IGenericRepository genericRepository, ILogger<DocumentController> logger)
        {
            _genericRepository = genericRepository;
            _logger = logger;
        }


        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var sql = "dbo.sp_GetDocumentById";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                var result = await _genericRepository.QueryFirstOrDefaultAsync<Document>(sql, parameters, CommandType.StoredProcedure);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location DocumentController.GetById : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var sql = "select * from Document";
                var parameters = new DynamicParameters();
                var result = await _genericRepository.QueryAsync<Document>(sql, parameters, CommandType.Text);
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location DocumentController.GetAll : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }



        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Document document)
        {
            try
            {
                if (document == null)
                {
                    return BadRequest("Invalid model");
                }

                var sql = "dbo.sp_InsertDocument ";
                var parameters = new DynamicParameters();
                parameters.Add("@CourseName", document.CourseName, DbType.String);
                parameters.Add("@DocumentDetails", document.DocumentDetails, DbType.String);
                parameters.Add("@DocumentStatus", document.DocumentStatus, DbType.String);
                parameters.Add("@CreatedDate", document.CreatedDate, DbType.DateTime);
                parameters.Add("@UploadedBy", document.UploadedBy, DbType.String);

                await _genericRepository.QueryAsync<Document>(sql, parameters, CommandType.StoredProcedure);

                _logger.LogInformation("A new document was created for:" +document.CourseName);

                return Ok("Entity created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location DocumentController.Create : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Document  document)
        {
            try
            {
                if (document == null)
                {
                    return BadRequest("Invalid model");
                }

                var sql = "dbo.sp_UpdateDocument";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.Int64);
                parameters.Add("@CourseName", document.CourseName, DbType.String);
                parameters.Add("@DocumentDetails", document.DocumentDetails, DbType.String);
                parameters.Add("@DocumentStatus", document.DocumentStatus, DbType.String);
                parameters.Add("@CreatedDate", document.CreatedDate, DbType.DateTime);
                parameters.Add("@UploadedBy", document.UploadedBy, DbType.String);



                await _genericRepository.QueryAsync<Document>(sql, parameters, CommandType.StoredProcedure);

                return Ok("Entity updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location DocumentController.Update : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var sql = "sp_DeleteDocument";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);



                await _genericRepository.QueryAsync<Document>(sql, parameters, CommandType.StoredProcedure);

                return Ok("Entity deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location DocumentController.Delete : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

    }
}
