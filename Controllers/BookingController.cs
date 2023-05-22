using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_bookingServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        public readonly IBookingService bookingService;

        public BookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        [HttpPost("view-all-booking")]
        public async Task<IActionResult> ViewAllBooking(PageDto page)
        {
            try
            {
                var list = await bookingService.View(page)!;
                return Ok(list);
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                return BadRequest(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
            }
        }

        [HttpPost("check-booking")]
        public async Task<IActionResult> CheckBooking(BookingCheckRequestDto bookingCheckRequestDto){
            try
            {
                var list = await bookingService.IsBookingAvailable(bookingCheckRequestDto);
                return Ok(list);
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                return BadRequest(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
            }
        }

        [HttpGet("detail-booking/{id}")]
        public async Task<IActionResult> DetailBooking(int id)
        {
            try
            {
                var car = await bookingService.Detail(id);
                return Ok(car);
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                return BadRequest(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
            }
        }

        [HttpPost("create-booking")]
        public async Task<IActionResult> CreateBooking(BookingCreateRequestDto bookingCreateRequestDto)
        {
            try
            {
                if (await bookingService.Create(bookingCreateRequestDto))
                {
                    return Ok("Successfully!");
                };
                return BadRequest("Fail!");
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                return BadRequest(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
            }
        }

        [HttpPut("update-booking")]
        public async Task<IActionResult> UpdateBooking(BookingUpdateRequestDto bookingUpdateRequestDto)
        {
            try
            {
                if (await bookingService.Update(bookingUpdateRequestDto))
                {
                    return Ok("Successfully!");
                }
                return BadRequest("Fail!");
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                return BadRequest(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
            }
        }
    }
}