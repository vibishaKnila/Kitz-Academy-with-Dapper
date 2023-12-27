using Academy.Model;
using Academy.Repository;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Academy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class PaymentController : ControllerBase
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IGenericRepository genericRepository, ILogger<PaymentController> logger)
        {
            _genericRepository = genericRepository;
            _logger = logger;

        }


        [HttpGet("get")]
        public async Task<IActionResult> GetById(int registrationid)
        {
            try
            {
                var sql = "dbo.GetPaymentById";
                var parameters = new DynamicParameters();
                parameters.Add("@RegistrationId", registrationid);

                var result = await _genericRepository.QueryFirstOrDefaultAsync<Payment>(sql, parameters, CommandType.StoredProcedure);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location PaymentController.GetById : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var sql = "select * from Payment";
                var parameters = new DynamicParameters();
                var result = await _genericRepository.QueryAsync<Payment>(sql, parameters, CommandType.Text);




                if (!result.Any())
                {
                    return NotFound("No records found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location PaymentController.GetAll : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }



        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Payment payment)
        {
            try
            {
                if (payment == null)
                {
                    return BadRequest("Invalid model");
                }

                var sql = "dbo.sp_InsertPayment ";
                var parameters = new DynamicParameters();
                parameters.Add("@StudentName", payment.StudentName, DbType.String);
                parameters.Add("@RegistrationId", payment.RegistrationId, DbType.String);
                parameters.Add("@CourseDetail", payment.CourseDetail, DbType.String);
                parameters.Add("@BatchDetail", payment.BatchDetail, DbType.String);
                parameters.Add("@TraineeName", payment.TraineeName, DbType.String);
                parameters.Add("@TotalFees", payment.TotalFees, DbType.Int32);
                parameters.Add("@No_Of_Installment", payment.No_Of_Installment, DbType.Int32);
                parameters.Add("@PaymentStatus", payment.PaymentStatus, DbType.String);
                parameters.Add("@DateOfPayment", payment.DateOfPayment, DbType.DateTime);

                await _genericRepository.QueryAsync<Payment>(sql, parameters, CommandType.StoredProcedure);

                _logger.LogInformation("A new payment was created for:"+ payment.StudentName);
                return Ok("Entity created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location PaymentController.Create : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Payment payment)
        {
            try
            {
                if (payment == null)
                {
                    return BadRequest("Invalid model");
                }

                var sql = "dbo.UpdatePayment ";
                var parameters = new DynamicParameters();
                parameters.Add("@StudentName", payment.StudentName, DbType.String);
                parameters.Add("@RegistrationId", payment.RegistrationId, DbType.Int32);
                parameters.Add("@CourseDetail", payment.CourseDetail, DbType.String);
                parameters.Add("@BatchDetail", payment.BatchDetail, DbType.String);
                parameters.Add("@TraineeName", payment.TraineeName, DbType.String);
                parameters.Add("@TotalFees", payment.TotalFees, DbType.Int32);
                parameters.Add("@No_Of_Installment", payment.No_Of_Installment, DbType.Int32);
                parameters.Add("@PaymentStatus", payment.PaymentStatus, DbType.String);
                parameters.Add("@DateOfPayment", payment.DateOfPayment, DbType.DateTime);


                await _genericRepository.QueryAsync<Payment>(sql, parameters, CommandType.StoredProcedure);

                return Ok("Entity updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location PaymentController.Update : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int registrationId)
        {
            try
            {
                var sql = "dbo.DeletePayment";
                var parameters = new DynamicParameters();
                parameters.Add("@RegistrationId", registrationId);
                await _genericRepository.QueryAsync<Payment>(sql, parameters, CommandType.StoredProcedure);

                return Ok("Entity deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location PaymentController.Delete : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}