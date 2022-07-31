using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.ReservationServices;
using MovieTheater.Application.UserServices;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MovieTheater.Application.CustomerServices;
using MovieTheater.Application.ReservationServices.Reservations;
using MovieTheater.BackEnd.Payment;
using MovieTheater.Common.Constants;
using MovieTheater.Data.Models;
using MovieTheater.Models.User;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationController : BaseController
    {
        private readonly IReservationService _reservationService;
        private readonly IConfiguration _configuration;
        private readonly ICustomerService _customerService;
        private readonly IVnPayService _vnPayService;

        public ReservationController(IReservationService reservationService, IUserService userService,
            IConfiguration configuration,
            ICustomerService customerService, IVnPayService vnPayService) : base(
            userService)
        {
            _reservationService = reservationService;
            _configuration = configuration;
            _customerService = customerService;
            _vnPayService = vnPayService;
        }

        [HttpPost("Create")]
        public async Task<ApiResult<int>> CreateAsync(ReservationCreateRequest model)
        {
            var result = await _reservationService.CreateAsync(model);
            return result;
        }

        [HttpPut("Update")]
        public async Task<ApiResult<bool>> UpdateAsync(ReservationUpdateRequest request)
        {
            var result = await _reservationService.UpdateAsync(request);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            var result = await _reservationService.DeleteAsync(id);
            return result;
        }


        [HttpPost("GetReservationPaging")]
        public async Task<ApiResult<PageResult<ReservationVMD>>> GetPeoplePaging(ReservationPagingRequest request)
        {
            var result = await _reservationService.GetPagingAsync(request);
            return result;
        }

        [HttpGet("GetById/{id}")]
        public async Task<ApiResult<ReservationVMD>> GeReservationByIdAsync(int id)
        {
            var result = await _reservationService.GetById(id);
            return result;
        }

        [HttpGet("GetByUserId/{id}")]
        public async Task<ApiResult<List<ReservationVMD>>> GeReservationByUserIdAsync(Guid id)
        {
            var result = await _reservationService.GetByUserId(id);
            return result;
        }

        [HttpPost("CalPrePrice")]
        public async Task<ApiResult<decimal>> CalPrePriceAsync(List<TicketCreateRequest> tickets)
        {
            var result = await _reservationService.CalPrePriceAsync(tickets);
            return result;
        }
        //[AllowAnonymous]
        //[HttpPost("Test")]
        //public async Task<ApiResult<string>> Test()
        //{
        //    var vnp = new VnPayService(_configuration);
        //    UserVMD user = new UserVMD()
        //    {
        //        Email = "thanhhienm4@gmail.com",
        //        PhoneNumber = "0912413004",
        //        FirstName = "Nguyễn Thanh Hiền"
        //    };
        //    //return new ApiSuccessResult<string>(vnp.CreateRequest(user));
        //}

        [HttpPost("Payment")]
        public async Task<ApiResult<string>> CreateAsync([FromBody] int id)
        {
            var reservation = (await _reservationService.GetById(id)).ResultObj;
            var customer = (await _customerService.GetById(reservation.Customer)).ResultObj;
            var url = _vnPayService.CreateRequest(reservation, customer);
            await _reservationService.UpdatePaymentStatus(new ReservationUpdatePaymentRequest()
            {
                Id = reservation.Id,
                Status = PaymentStatusType.Inprogress
            });
            return new ApiSuccessResult<string>(url);
        }

        [HttpPut("UpdatePayment")]
        public async Task<ApiResult<bool>> UpdatePaymentAsync(ReservationUpdatePaymentRequest request)
        {
            var result = await _reservationService.UpdatePaymentStatus(request);
            return result;
        }
    }
}