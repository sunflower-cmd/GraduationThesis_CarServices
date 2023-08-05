using System.Diagnostics;
using System.Globalization;
using System.Text;
using AutoMapper;
using Azure.Storage.Blobs;
using Firebase.Auth;
using Firebase.Storage;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;
using QRCoder;

namespace GraduationThesis_CarServices.Services.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingDetailRepository bookingDetailRepository;
        private readonly IBookingRepository bookingRepository;
        private readonly IProductRepository productRepository;
        private readonly ICouponRepository couponRepository;
        private readonly IServiceRepository serviceRepository;
        private readonly IGarageRepository garageRepository;
        private readonly IMechanicRepository mechanicRepository;
        private readonly ILotRepository lotRepository;
        private readonly ICarRepository carRepository;
        private readonly IConfiguration configuration;
        private readonly HttpClient httpClient;
        private readonly IMapper mapper;
        public BookingService(IBookingRepository bookingRepository, ILotRepository lotRepository,
        IMapper mapper, IBookingDetailRepository bookingDetailRepository, IProductRepository productRepository,
        IServiceRepository serviceRepository, IGarageRepository garageRepository, ICarRepository carRepository,
        ICouponRepository couponRepository, IMechanicRepository mechanicRepository, IConfiguration configuration)
        {
            this.mapper = mapper;
            this.httpClient = new HttpClient();
            this.bookingRepository = bookingRepository;
            this.bookingDetailRepository = bookingDetailRepository;
            this.lotRepository = lotRepository;
            this.productRepository = productRepository;
            this.serviceRepository = serviceRepository;
            this.garageRepository = garageRepository;
            this.carRepository = carRepository;
            this.couponRepository = couponRepository;
            this.mechanicRepository = mechanicRepository;
            this.configuration = configuration;
        }

        public async Task<List<BookingDetailStatusForBookingResponseDto>> GetBookingDetailStatusByBooking(int bookingId)
        {
            try
            {
                var list = await bookingDetailRepository.FilterBookingDetailByBookingId(bookingId);

                switch (false)
                {
                    case var isExist when isExist == (list is not null):
                        throw new MyException("The booking doesn't exist.", 404);
                }

                return mapper.Map<List<BookingDetailStatusForBookingResponseDto>>(list);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<BookingServiceStatusForStaffResponseDto> GetBookingServiceStatusByBooking(int bookingId)
        {
            try
            {
                var booking = await bookingRepository.Detail(bookingId);
                
                var bookingDto = mapper.Map<BookingServiceStatusForStaffResponseDto>(booking);

                return bookingDto;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<GenericObject<List<BookingListResponseDto>>> View(PageDto page)
        {
            try
            {
                (var listObj, var count) = await bookingRepository.View(page);

                var listDto = mapper.Map<List<BookingListResponseDto>>(listObj);

                var list = new GenericObject<List<BookingListResponseDto>>(listDto, count);

                return list;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<GenericObject<List<BookingListResponseDto>>> FilterBookingByStatus(FilterByStatusRequestDto requestDto)
        {
            try
            {
                var status = requestDto.BookingStatus;

                if (!typeof(BookingStatus).IsEnumDefined(status))
                {
                    throw new MyException("The status number is out of avaliable range.", 404);
                }

                var page = new PageDto { PageIndex = requestDto.PageIndex, PageSize = requestDto.PageSize };

                (var listObj, var count) = await bookingRepository.FilterBookingByStatus(status, page);

                var listDto = mapper.Map<List<BookingListResponseDto>>(listObj);

                var list = new GenericObject<List<BookingListResponseDto>>(listDto, count);

                return list;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<GenericObject<List<BookingListResponseDto>>> FilterBookingStatusAndDate(FilterByStatusAndDateRequestDto requestDto)
        {
            try
            {
                var status = requestDto.BookingStatus;
                DateTime? dateFrom = null;
                DateTime? dateTo = null;

                if (requestDto.DateFrom is not null)
                {
                    dateFrom = DateTime.Parse(requestDto.DateFrom!);
                }

                if (requestDto.DateTo is not null)
                {
                    dateTo = DateTime.Parse(requestDto.DateTo!);
                }

                if (status is not null && !typeof(BookingStatus).IsEnumDefined(status!))
                {
                    throw new MyException("The status number is out of avaliable range.", 404);
                }

                var page = new PageDto { PageIndex = requestDto.PageIndex, PageSize = requestDto.PageSize };

                (var listObj, var count) = await bookingRepository.FilterBookingStatusAndDate(dateFrom, dateTo, status, page);

                var listDto = mapper.Map<List<BookingListResponseDto>>(listObj);

                var list = new GenericObject<List<BookingListResponseDto>>(listDto, count);

                return list;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<GenericObject<List<BookingListResponseDto>>> SearchByBookingCode(SearchBookingByUserRequestDto requestDto)
        {
            try
            {
                var page = new PageDto { PageIndex = requestDto.PageIndex, PageSize = requestDto.PageSize };

                (var listObj, var count) = await bookingRepository.SearchByBookingCode(requestDto.UserId, requestDto.Search, page);

                var listDto = mapper.Map<List<BookingListResponseDto>>(listObj);

                var list = new GenericObject<List<BookingListResponseDto>>(listDto, count);

                return list;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<GenericObject<List<BookingListResponseDto>>> FilterBookingByGarageId(PagingBookingPerGarageRequestDto requestDto)
        {
            try
            {
                var isGarageExist = await garageRepository.IsGarageExist(requestDto.GarageId);

                switch (false)
                {
                    case var isExist when isExist == isGarageExist:
                        throw new MyException("The garage doesn't exist.", 404);
                }

                var page = new PageDto { PageIndex = requestDto.PageIndex, PageSize = requestDto.PageSize };

                (var listObj, var count) = await bookingRepository.FilterBookingByGarage(requestDto.GarageId, page);

                var listDto = mapper.Map<List<BookingListResponseDto>>(listObj);

                var list = new GenericObject<List<BookingListResponseDto>>(listDto, count);

                return list;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<GenericObject<List<FilterByCustomerResponseDto>>> FilterBoookingByCustomer(FilterByCustomerRequestDto requestDto)
        {
            try
            {
                var page = new PageDto
                {
                    PageIndex = requestDto.PageIndex,
                    PageSize = requestDto.PageSize
                };

                (var listObj, var count) = await bookingRepository.FilterBookingByCustomer(requestDto.UserId, page);

                var listDto = mapper.Map<List<FilterByCustomerResponseDto>>(listObj);

                var list = new GenericObject<List<FilterByCustomerResponseDto>>(listDto, count);

                return list;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<BookingDetailResponseDto?> Detail(int id)
        {
            try
            {
                var isBookingExist = await bookingRepository.IsBookingExist(id);

                switch (false)
                {
                    case var isExist when isExist == isBookingExist:
                        throw new MyException("The booking doesn't exist.", 404);
                }

                var booking = mapper.Map<Booking?, BookingDetailResponseDto>(await bookingRepository.Detail(id));

                return booking;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<List<BookingPerHour>> IsBookingAvailable(BookingCheckRequestDto requestDto)
        {
            try
            {
                var garage = await garageRepository.GetGarage(requestDto.GarageId);
                var currentDay = DateTime.Now;
                var dateSelect = DateTime.Parse(requestDto.DateSelected);

                switch (false)
                {
                    case var isExist when isExist == (garage != null):
                        throw new MyException("The garage doesn't exist.", 404);
                    case var isDate when isDate == (dateSelect.Date >= currentDay.Date):
                        throw new MyException("The selected date must be greater than or equal to the current date.", 404);
                }

                var openAt = DateTime.ParseExact(garage!.OpenAt, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay.Hours;
                var closeAt = DateTime.ParseExact(garage!.CloseAt, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay.Hours;

                var watch = System.Diagnostics.Stopwatch.StartNew();
                var listHours = new List<BookingPerHour>();

                await Task.Run(() =>
                {
                    CreateListHourPerDay(openAt, closeAt, listHours, dateSelect);
                });

                var listBooking = await bookingRepository.FilterBookingByDate(dateSelect, requestDto.GarageId);
                var lotCount = garage.Lots.Count;

                await Task.Run(() =>
                {
                    CheckIfGarageAvailablePerHour(openAt, closeAt, listBooking!, lotCount, listHours, dateSelect);
                });

                var isAvailableList = listHours.Where(l => l.IsAvailable.Equals(true)).AsParallel()
                .Select(l => DateTime.Parse(l.Hour).TimeOfDay.Hours).Order().ToList();
                var sequenceLength = 1;

                await Task.Run(() =>
                {
                    UpdateEstimatedTimeCanBeBook(sequenceLength, isAvailableList, listHours, requestDto.TotalEstimatedTimeServicesTake);
                });

                watch.Stop();
                Debug.WriteLine($"\nTotal run time (Milliseconds): {watch.ElapsedMilliseconds}\n");

                return listHours;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        private int GetMinEstimatedTime(int i, List<Booking> listBooking)
        {
            var minEstimatedTimePerHour = listBooking!.Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i))
            .Min(l => /*l.TotalEstimatedCompletionTime*/ l.CustomersCanReceiveTheCarTime);

            return minEstimatedTimePerHour;
        }

        private (int? bookingInFirstHourCount, int? bookingInNextHourCount) CountBookingPerHour(int num, int i, List<Booking> listBooking)
        {
            var bookingInFirstHourCount = listBooking?
            .Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i) && /*b.TotalEstimatedCompletionTime*/ b.CustomersCanReceiveTheCarTime > 1).Count();
            var bookingInNextHourCount = listBooking?
            .Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i + num)).Count();

            return (bookingInFirstHourCount, bookingInNextHourCount);
        }

        private void UpdateListHours(int num, List<BookingPerHour> listHours)
        {
            listHours.FirstOrDefault(l => DateTime.Parse(l.Hour).TimeOfDay.Hours.Equals(num))!.IsAvailable = false;
        }

        private void EstimatedTimeCanBeBook(int from, int to, int estimatedTime, int sequenceLength, List<int> isAvailableList, List<BookingPerHour> listHours, int totalEstimatedTimeServicesTake)
        {
            if (sequenceLength > 1)
            {
                for (int i = from; i <= to; i++)
                {
                    // listHours.AsParallel().FirstOrDefault(l => DateTime.Parse(l.Hour).TimeOfDay.Hours
                    // .Equals(i))!.EstimatedTimeCanBeBook = estimatedTime - i;

                    if (totalEstimatedTimeServicesTake > estimatedTime - i)
                    {
                        UpdateListHours(i, listHours);
                    }
                }
            }
        }

        private void CreateListHourPerDay(int openAt, int closeAt, List<BookingPerHour> listHours, DateTime dateSelect)
        {
            for (int i = openAt; i <= closeAt; i++)
            {
                var time = new TimeSpan(i, 00, 00);
                listHours.Add(new BookingPerHour { Hour = dateSelect.Add(time).ToString("HH:mm:ss") });
            }
        }

        private void UpdateEstimatedTimeCanBeBook(int sequenceLength, List<int> isAvailableList, List<BookingPerHour> listHours, int totalEstimatedTimeServicesTake)
        {
            for (int i = 0; i <= isAvailableList.Count - 1; i++)
            {
                if (isAvailableList[i + 1] - isAvailableList[i] == 1)
                {
                    sequenceLength++;
                }
                else
                {
                    // listHours.FirstOrDefault(l => DateTime.Parse(l.Hour).TimeOfDay.Hours
                    //     .Equals(isAvailableList[i]))!.EstimatedTimeCanBeBook = 1;

                    if (totalEstimatedTimeServicesTake > 1)
                    {
                        UpdateListHours(isAvailableList[i], listHours);
                    }

                    EstimatedTimeCanBeBook(isAvailableList[i] - sequenceLength + 1, isAvailableList[i] + 1, isAvailableList[i] + 1, sequenceLength, isAvailableList, listHours, totalEstimatedTimeServicesTake);

                    sequenceLength = 1;
                }

                if (isAvailableList[i + 1].Equals(isAvailableList.Last()))
                {
                    EstimatedTimeCanBeBook(isAvailableList[i] - sequenceLength + 2, isAvailableList[i] + 1, isAvailableList[i] + 2, sequenceLength, isAvailableList, listHours, totalEstimatedTimeServicesTake);
                    break;
                }
            }
        }

        private void CheckIfGarageAvailablePerHour(int openAt, int closeAt, List<Booking> listBooking, int lotCount, List<BookingPerHour> listHours, DateTime dateSelect)
        {
            for (int i = openAt; i <= closeAt; i++)
            {
                var current = DateTime.Now;
                var convertHour = 0;
                var selectedHour = i;

                switch (current.Hour)
                {
                    case var hour when hour > 12:
                        convertHour = hour - 12;
                        break;
                    default:
                        convertHour = current.Hour;
                        break;
                }

                var bookingCount = listBooking?
                .Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i)).Count();

                var bookingInOneHours = listBooking?
                .Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i) && /*b.TotalEstimatedCompletionTime*/ b.CustomersCanReceiveTheCarTime == 1).Count();

                var test1 = i - convertHour < 4;
                var test2 = openAt <= current.Hour && current.Hour <= closeAt;
                var test3 = current.Date == dateSelect.Date;

                switch (bookingCount)
                {
                    case var bookingCout when i - convertHour < 4 && (openAt <= current.Hour && current.Hour <= closeAt) && (current.Date == dateSelect.Date):
                        UpdateListHours(i, listHours);
                        break;
                    case var bookingCout when bookingCout == lotCount:
                        var minEstimatedTime = GetMinEstimatedTime(i, listBooking!);

                        //If all Booking have estimated time all > 2 skip to the next available Hour
                        for (int y = selectedHour; y <= selectedHour + minEstimatedTime - 1; y++)
                        {
                            UpdateListHours(y, listHours);
                        }

                        i = selectedHour + minEstimatedTime - 1;
                        break;
                    case var bookingCout when bookingCout == lotCount && bookingInOneHours > 0:
                        (int? bookingInFirstHourCount, int? bookingInNextHourCount) = CountBookingPerHour(1, i, listBooking!);

                        if (bookingInFirstHourCount + bookingInNextHourCount == lotCount)
                        {
                            UpdateListHours(i + 1, listHours);
                        }
                        break;
                    case var bookingCout when bookingCout != lotCount && bookingCount > 0:
                        var remainHour = closeAt - i;

                        var minEstimatedTimePerHour = GetMinEstimatedTime(i, listBooking!);

                        for (int z = 1; z <= remainHour; z++)
                        {
                            (int? bookingInFirstHourCout, int? bookingInNextHourCout) = CountBookingPerHour(z, i, listBooking!);

                            if (bookingInFirstHourCout + bookingInNextHourCout == lotCount && minEstimatedTimePerHour > 1)
                            {
                                var durationFirstHour = listBooking?.Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i) &&
                                 /*b.TotalEstimatedCompletionTime*/ b.CustomersCanReceiveTheCarTime > 1).FirstOrDefault()!./*TotalEstimatedCompletionTime*/CustomersCanReceiveTheCarTime;
                                if (durationFirstHour > 1)
                                {
                                    for (int b = i + 1; b < i + durationFirstHour; b++)
                                    {
                                        UpdateListHours(b, listHours);
                                    }
                                    z = (int)durationFirstHour;
                                    i = i + z - 1;
                                    var isBookin = listBooking?.Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i + 1) && /*b.TotalEstimatedCompletionTime*/ b.CustomersCanReceiveTheCarTime > 1).Count();
                                    if (isBookin + (int)bookingInFirstHourCout! == lotCount)
                                    {
                                        UpdateListHours(i + 1, listHours);
                                    }
                                }
                                else
                                {
                                    UpdateListHours(i + z, listHours);
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public async Task Create(BookingCreateRequestDto requestDto)
        {
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();

                var bookingTime = DateOnly.Parse(requestDto.DateSelected).ToDateTime(TimeOnly.Parse(requestDto.TimeSelected));
                var currentDay = DateTime.Now;

                var garage = await garageRepository.GetGarage(requestDto.GarageId);
                var isCarExist = await carRepository.IsCarExist(requestDto.CarId);

                var isCarAvalible = await carRepository.IsCarAvalible(requestDto.CarId);

                if (requestDto.CouponId > 0)
                {
                    if (!await couponRepository.IsCouponExist(requestDto.CouponId))
                    {
                        throw new MyException("The coupon doesn't exist.", 404);
                    }
                }

                var totalEstimated = 0;

                for (int i = 0; i < requestDto.ServiceList.Count; i++)
                {
                    var serviceDuration = await serviceRepository.GetDuration(requestDto.ServiceList[i]);
                    totalEstimated += serviceDuration;
                }

                var bookingAt = bookingTime.TimeOfDay.Hours;

                var bookingCheck = new BookingCheckRequestDto { DateSelected = requestDto.DateSelected, GarageId = requestDto.GarageId };
                var listHours = await IsBookingAvailable(bookingCheck);
                var isAvailableHours = listHours.FirstOrDefault(l => DateTime.Parse(l.Hour).TimeOfDay.Hours == bookingAt);

                var openAt = DateTime.ParseExact(garage!.OpenAt, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay.Hours;
                var closeAt = DateTime.ParseExact(garage!.CloseAt, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay.Hours;

                switch (false)
                {
                    case var isNull when isNull == (requestDto.CarId != 0):
                        throw new MyException("Car ID không được null!", 404);
                    case var isAvalible when isAvalible == isCarAvalible:
                        throw new MyException("Xin lỗi xe của bạn không khả dụng.", 404);
                    case var isExist when isExist == isCarExist:
                        throw new MyException("Xin lỗi xe của bạn không tồn tại.", 404);
                    case var isTime when isTime == (bookingTime >= currentDay):
                        throw new MyException("Ngày được chọn phải lớn hơn hoặc trùng ngày hiện tại.", 404);
                    case var isEmpty when isEmpty == (requestDto.ServiceList.Count > 0):
                        throw new MyException("Phải chọn ít nhất một dịch vụ trước khi đặt đơn.", 404);
                    case var isFalse when isFalse == (openAt <= bookingAt && bookingAt <= closeAt):
                        throw new MyException("Xin lỗi khung giờ khongo trong khung giờ làm việc.", 404);
                    case var isMany when isMany == (requestDto.ServiceList.Count <= 3):
                        throw new MyException("Chỉ đặt được tối đa là 3 dịch vụ.", 404);
                }

                var listBooking = await bookingRepository.FilterBookingByTimePerDay(bookingTime, requestDto.GarageId);
                var lotCount = garage!.Lots.Count;

                // if (lotCount - listBooking!.Count == 1)
                // {
                //     Debug.WriteLine($"{System.Text.Encoding.Default.GetString(garage!.VersionNumber)}");
                //     if (requestDto.VersionNumber.SequenceEqual(garage!.VersionNumber))
                //     {
                //         await garageRepository.Update(garage);
                //         await Run(requestDto, bookingTime, totalEstimated);
                //     }
                //     else
                //     {
                //         throw new MyException("Sorry, there is someone before you booked this.", 409);
                //     }
                // }
                // else
                // {
                //     await Run(requestDto, bookingTime, totalEstimated);
                // }

                await Run(requestDto, bookingTime, totalEstimated);

                watch.Stop();
                Debug.WriteLine($"\nTotal run time (Milliseconds) Create(): {watch.ElapsedMilliseconds}\n");
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        private string GenerateRandomString()
        {
            const string chars = "0123456789ABCDEF";
            var random = new Random();

            var result = new StringBuilder();
            result.Append(random.Next(10, 100)); // Two random digits
            result.Append(chars[random.Next(chars.Length)]); // One random character
            result.Append(random.Next(10, 100)); // Two random digits
            result.Append(chars[random.Next(chars.Length)]); // One random character
            result.Append(chars[random.Next(chars.Length)]); // One random character
            result.Append(random.Next(10, 100)); // Two random digits
            result.Append(chars[random.Next(chars.Length)]); // One random character

            return result.ToString();
        }

        private async Task Run(BookingCreateRequestDto requestDto, DateTime bookingTime, int totalEstimated)
        {
            try
            {
                var booking = mapper.Map<BookingCreateRequestDto, Booking>(requestDto,
                otp => otp.AfterMap((src, des) =>
                {
                    var now = DateTime.Now;
                    des.BookingCode = GenerateRandomString();
                    des.BookingTime = bookingTime;
                    des.PaymentStatus = PaymentStatus.Unpaid;
                    des.BookingStatus = BookingStatus.Pending;
                    des.IsAccepted = true;
                    des.CreatedAt = now;
                }));

                var bookingId = await bookingRepository.Create(booking);

                var checkOutDto = new CheckOutRequestDto() { ServiceList = requestDto.ServiceList, CouponId = requestDto.CouponId };

                var checkOut = await RunCheckOut(checkOutDto, true, bookingId, requestDto.MechanicId);

                if (requestDto.MechanicId != 0)
                {
                    var bookingMechanic = new BookingMechanic()
                    {
                        WorkingDate = bookingTime,
                        BookingId = bookingId,
                        MechanicId = requestDto.MechanicId
                    };

                    await mechanicRepository.CreateBookingMechanic(bookingMechanic);
                }

                booking.OriginalPrice = checkOut.Item1;
                booking.DiscountPrice = checkOut.Item2;
                booking.TotalPrice = checkOut.Item3;
                booking.FinalPrice = checkOut.Item3;
                booking.TotalEstimatedCompletionTime = totalEstimated;
                booking.CustomersCanReceiveTheCarTime = totalEstimated + 1;

                var car = await carRepository.Detail(requestDto.CarId);

                car!.CarBookingStatus = CarStatus.NotAvailable;

                await carRepository.Update(car);

                await GenerateQRCode(booking);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<(decimal, decimal, decimal)> RunCheckOut(CheckOutRequestDto requestDto, bool isCreateNew, int? bookingId, int? mechanicId)
        {
            try
            {
                switch (false)
                {
                    case var isFalse when isFalse == (requestDto.ServiceList.Any(s => s > 0)):
                        throw new MyException("Id can't take 0 value!", 404);
                    case var isFalse when isFalse == (requestDto.CouponId != 0):
                        requestDto.CouponId = null;
                        break;
                }

                decimal discountedPrice = 0;
                decimal originalPrice = 0;
                decimal totalPrice = 0;

                var listService = requestDto.ServiceList;
                var listBookingDetail = new List<BookingDetail>();

                for (int i = 0; i < listService.Count; i++)
                {
                    //get default product id
                    decimal productPrice = 0;
                    decimal servicePrice = serviceRepository.GetPrice(listService[i]);
                    var product = productRepository.GetDefaultProduct(listService[i]);

                    if (product is not null)
                    {
                        productPrice = product.ProductPrice;
                    }

                    originalPrice += productPrice + servicePrice;

                    if (isCreateNew == true)
                    {
                        var bookingDetail = new BookingDetail()
                        {
                            ProductPrice = productPrice,
                            ServicePrice = servicePrice,
                            BookingServiceStatus = BookingServiceStatus.NotStart,
                            ProductId = product is null ? null : product.ProductId,
                            ServiceDetailId = listService[i],
                            //MechanicId = mechanicId,
                            BookingId = bookingId
                        };

                        listBookingDetail.Add(bookingDetail);
                    }
                }

                if (isCreateNew == true)
                {
                    await bookingDetailRepository.Create(listBookingDetail);
                }

                if (requestDto.CouponId is not null)
                {
                    var coupon = await couponRepository.GetCouponTypeAndCouponValue(requestDto.CouponId);

                    switch (coupon!.Item1)
                    {
                        case CouponType.Percent:
                            discountedPrice = originalPrice * (coupon.Item2 / 100);
                            totalPrice = originalPrice - discountedPrice;
                            break;
                        case CouponType.FixedAmount:
                            discountedPrice = coupon.Item2;
                            totalPrice = originalPrice - discountedPrice;
                            break;
                    }
                }
                else
                {
                    totalPrice = originalPrice;
                }

                return (originalPrice, discountedPrice, totalPrice);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<CheckOutResponseDto> CheckOut(CheckOutRequestDto requestDto)
        {
            try
            {
                var checkOut = await RunCheckOut(requestDto, false, null, null);

                return new CheckOutResponseDto
                {
                    OriginalPrice = String.Format(CultureInfo.InvariantCulture, "{0:0.000} VND", checkOut.Item1),
                    DiscountedPrice = String.Format(CultureInfo.InvariantCulture, "{0:0.000} VND", checkOut.Item2),
                    TotalPrice = String.Format(CultureInfo.InvariantCulture, "{0:0.000} VND", checkOut.Item3)
                };
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task ConfirmAcceptedBooking(bool isAccepted, int bookingId)
        {
            try
            {
                if (isAccepted is true)
                {
                    var booking = await bookingRepository.Detail(bookingId);
                    booking!.IsAccepted = isAccepted;

                    await bookingRepository.Update(booking);
                }
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task UpdateStatus(int bookingId, BookingStatus bookingStatus)
        {
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();

                var booking = await bookingRepository.Detail(bookingId);

                if (booking is null)
                {
                    throw new MyException("The booking doesn't exist.", 404);
                }

                switch (bookingStatus)
                {
                    case BookingStatus.CheckIn:
                        await UpdateLotStatus(LotStatus.Assigned, booking!);
                        break;
                    // case BookingStatus.Processing:
                    //     await UpdateLotStatus(LotStatus.BeingUsed, booking!);
                    //     break;
                    case BookingStatus.Completed:

                        // switch (false)
                        // {
                        //     case var isAll when isAll == (!booking!.BookingDetails.All(b => b.BookingServiceStatus == BookingServiceStatus.NotStart)):
                        //         throw new MyException("All service detail must finish before updating the booking status.", 404);
                        //     case var isAccepted when isAccepted == (booking.IsAccepted is true):
                        //         throw new MyException("This booking is not accepted by customer.", 404);
                        // }

                        var bookingDetails = await bookingDetailRepository.FilterBookingDetailByBookingId(bookingId);

                        foreach (var bookingDetail in bookingDetails)
                        {
                            if (bookingDetail.BookingServiceStatus == BookingServiceStatus.Error &&
                            !bookingDetails.Any(b => b.BookingServiceStatus == BookingServiceStatus.NotStart))
                            {
                                booking!.FinalPrice -= bookingDetail.ServicePrice;
                            }
                        }

                        await bookingRepository.Update(booking);

                        var car = await carRepository.Detail(booking.Car.CarId);

                        car!.CarBookingStatus = CarStatus.NotAvailable;

                        await carRepository.Update(car);

                        await UpdateLotStatus(LotStatus.Free, booking!);
                        break;
                }

                booking!.BookingStatus = bookingStatus;

                await bookingRepository.Update(booking);

                watch.Stop();
                Debug.WriteLine($"Total run time (Milliseconds) Run(): {watch.ElapsedMilliseconds}");
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        private async Task UpdateLotStatus(LotStatus status, Booking booking)
        {
            switch (status)
            {
                case LotStatus.Assigned:
                    var lot1 = await lotRepository.GetFreeLotInGarage((int)booking.GarageId!);
                    var licensePlate = await carRepository.GetLicensePlate((int)booking.CarId!);

                    lot1.LotStatus = LotStatus.BeingUsed;
                    lot1.IsAssignedFor = licensePlate;

                    await AssigneMechanicForBooking(booking);

                    await lotRepository.Update(lot1);
                    break;
                default:
                    var lot = await lotRepository.GetLotByLicensePlate((int)booking.GarageId!, booking.Car.CarLicensePlate);

                    if (lot.LotStatus == LotStatus.Free)
                    {
                        lot.IsAssignedFor = null;
                    }

                    await lotRepository.Update(lot);
                    break;
            }
        }

        private async Task AssigneMechanicForBooking(Booking booking)
        {
            var isCustomerPickMainMechanic = await mechanicRepository.IsCustomerPickMainMechanic(booking.BookingTime);

            if (isCustomerPickMainMechanic is null)
            {
                var listLv3Mechanic = await mechanicRepository.FilterMechanicsByGarage((int)booking.GarageId!);

                if (listLv3Mechanic.Count == 0)
                {
                    throw new MyException("This garage don't have any Lv3 Mechanic.", 404);
                }

                var pickLv3MechanicMinWork = listLv3Mechanic.OrderBy(m => m.BookingMechanics.Count).First();

                var bookingMechanic = new BookingMechanic()
                {
                    WorkingDate = booking.BookingTime,
                    BookingId = booking.BookingId,
                    MechanicId = pickLv3MechanicMinWork.MechanicId
                };

                await mechanicRepository.CreateBookingMechanic(bookingMechanic);
            }

            var mechanicList = await mechanicRepository.FilterMechanicsAvailableByGarage((int)booking.GarageId!);

            var numService = booking.BookingDetails.Count();

            var pickRandomMechanic = mechanicList.OrderBy(m => m.BookingMechanics.Count).Take(numService).ToList();

            foreach (var mechanic in pickRandomMechanic)
            {
                var bookingMechanic = new BookingMechanic()
                {
                    WorkingDate = booking.BookingTime,
                    BookingId = booking.BookingId,
                    MechanicId = mechanic.MechanicId
                };

                await mechanicRepository.CreateBookingMechanic(bookingMechanic);
            }

            // var bookingDetailList = await bookingDetailRepository.FilterBookingDetailByBookingId(booking.BookingId);
            // var mechanicAvailableList = await mechanicRepository.FilterMechanicAvailableByGarageId((int)booking.GarageId!);

            // switch (false)
            // {
            //     case var isFalse when isFalse == (bookingDetailList.Count <= mechanicAvailableList.Count):
            //         throw new MyException("There are not enough mechanic for booking.", 404);
            // }

            // var minWorkingHour = mechanicAvailableList.Take(bookingDetailList.Count).ToList();

            // //Index out of range bug
            // for (int i = 0; i < bookingDetailList.Count; i++)
            // {
            //     //bookingDetailList[i].MechanicId = minWorkingHour[i].MechanicId;
            //     var estimatedTime = await serviceRepository.GetDuration((int)bookingDetailList[i].ServiceDetail.ServiceId!);
            //     //minWorkingHour[i].TotalBookingApplied += estimatedTime;
            // }

            // await bookingDetailRepository.Update(bookingDetailList);
        }

        private async Task GenerateQRCode(Booking booking)
        {
            try
            {
                //string url = $"https://carserviceappservice.azurewebsites.net/api/booking/run-qr/{booking.BookingId}";
                string url = $"{booking.BookingId}";

                var qrGenerator = new QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);
                var qrCodeImage = qrCode.GetGraphic(20);

                await using (MemoryStream stream = new MemoryStream())
                {
                    qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);

                    byte[] imageBytes = stream.ToArray();
                    var base64String = "data:image/png;base64," + Convert.ToBase64String(imageBytes);

                    byte[] imgBytes = Convert.FromBase64String(base64String.Split(',')[1]);

                    var blobServiceClient = new BlobServiceClient(configuration["BlobStorage:ConnectionString"]!);
                    var blobContainerClient = blobServiceClient.GetBlobContainerClient(configuration["BlobStorage:Container"]!);
                    var blobName = GenerateRandomString() + "_qr_code.jpg";

                    using (MemoryStream blobstream = new MemoryStream(imgBytes))
                    {
                        stream.Position = 0;
                        blobContainerClient.UploadBlob(blobName, blobstream);
                    }

                    var blobClient = blobContainerClient.GetBlobClient(blobName);

                    booking!.QrImage = blobClient.Uri.ToString();

                    await bookingRepository.Update(booking);
                }

                // string qrCodeImageBase64;
                // await using (var ms = new MemoryStream())
                // {
                //     qrCodeImage.Save(ms, ImageFormat.Png);
                //     byte[] imageBytes = ms.ToArray();
                //     qrCodeImageBase64 = "data:image/png;base64," + Convert.ToBase64String(imageBytes);
                // }

                // string qrCodeImageUrl = qrCodeImageBase64;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<BookingDetailForStaffResponseDto> RunQRCode(int bookingId)
        {
            // var url = $"https://carserviceappservice.azurewebsites.net/api/booking/update-status-booking/{bookingId}&2";
            // var data = "{\"status\": \"updated status\"}";

            // using (var httpClient = new HttpClient())
            // {
            //     var request = new HttpRequestMessage(HttpMethod.Put, url)
            //     {
            //         Content = new StringContent(data, System.Text.Encoding.UTF8, "application/json")
            //     };

            //     var response = await httpClient.SendAsync(request);
            //     var statusCode = response.StatusCode;

            //     Console.WriteLine($"Response Status Code: {statusCode}");
            // }

            var serviceSelectList = new List<GroupServiceBookingDetailDto>();

            var serviceGroupList = new List<string> { };

            var booking = await bookingRepository.DetailBookingForCustomer(bookingId);

            var listBookingDetails = await serviceRepository.GetServiceForBookingDetail(bookingId);

            foreach (var item in listBookingDetails)
            {
                serviceGroupList.Add(item.ServiceDetail.Service.ServiceGroup);
            }

            foreach (var item in serviceGroupList.Distinct())
            {
                var serviceList = listBookingDetails.Where(s => s.ServiceDetail.Service.ServiceGroup.Equals(item)).ToList();

                var serviceDtoList = mapper.Map<List<BookingDetail>, List<ServiceListBookingDetailDto>>(serviceList,
                    obj => obj.AfterMap((src, des) =>
                    {
                        for (int i = 0; i < src.Count; i++)
                        {
                            if (src[i].Product is not null)
                            {
                                var serviceName = src[i].ServiceDetail.Service.ServiceName + "@Sản phẩm đi kèm: " + src[i].Product.ProductName;
                                serviceName = serviceName.Replace("@", "@" + System.Environment.NewLine);
                                var price = String.Format(CultureInfo.InvariantCulture, "{0:0,0.000}", src[i].ServicePrice) + "@" + String.Format(CultureInfo.InvariantCulture, "{0:0,0.000}", src[i].ProductPrice);
                                price = price.Replace("@", "@" + System.Environment.NewLine);

                                des[i].ServiceName = serviceName;
                                des[i].ServicePrice = price;
                            }
                            else
                            {
                                des[i].ServiceName = src[i].ServiceDetail.Service.ServiceName;
                                des[i].ServicePrice = String.Format(CultureInfo.InvariantCulture, "{0:0,0.000}", src[i].ServicePrice);
                            }
                        }
                    }));
                serviceSelectList.Add(new GroupServiceBookingDetailDto { ServiceGroup = item, serviceListBookingDetailDtos = serviceDtoList });


            }

            var bookingDto = mapper.Map<BookingDetailForStaffResponseDto>(booking,
                obj => obj.AfterMap((src, des) =>
                {
                    des.groupServiceBookingDetailDtos = serviceSelectList;
                }));

            return bookingDto;
        }

        public async Task<BookingRevenueResponseDto> CountRevune(int garageId)
        {
            try
            {
                var isGarageExist = await garageRepository.IsGarageExist(garageId);

                switch (false)
                {
                    case var isExist when isExist == isGarageExist:
                        throw new MyException("The garage doesn't exist.", 404);
                }

                (var amountEarned, var serviceEarned,
                var productEarned, var sumPaid, var sumUnpaid,
                var countPaid, var countUnpaid) = await bookingRepository.CountRevenue(garageId);

                var revenue = new BookingRevenueResponseDto
                {
                    AmountEarned = amountEarned,
                    ServiceEarned = serviceEarned,
                    SumUnPaid = sumUnpaid,
                    CountPaid = countPaid,
                    CountUnpaid = countUnpaid
                };

                return revenue;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<CountBookingPerStatusDto> CountBookingPerStatus()
        {
            try
            {
                (var pendingCount,
                var canceledCount,
                var checkInCount,
                var processingCount,
                var completedCount) = await bookingRepository.CountBookingPerStatus();

                var count = new CountBookingPerStatusDto()
                {
                    Pending = pendingCount,
                    Canceled = canceledCount,
                    CheckIn = checkInCount,
                    Processing = processingCount,
                    Completed = completedCount
                };

                return count;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<List<FilterByBookingStatusResponseDto>> FilterBookingByStatusCustomer(int bookingStatus, int userId)
        {
            try
            {
                if (!typeof(BookingStatus).IsEnumDefined(bookingStatus))
                {
                    throw new MyException("The status number is out of avaliable range.", 404);
                }

                var list = await bookingRepository.FilterBookingByStatusCustomer(bookingStatus, userId);

                var listDto = mapper.Map<List<FilterByBookingStatusResponseDto>>(list);

                return listDto;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<BookingDetailForCustomerResponseDto> DetailBookingForCustomer(int bookingId)
        {
            try
            {
                var isBookingExist = await bookingRepository.IsBookingExist(bookingId);

                switch (false)
                {
                    case var isExist when isExist == isBookingExist:
                        throw new MyException("The booking doesn't exist.", 404);
                }

                var serviceSelectList = new List<GroupServiceBookingDetailDto>();

                var serviceGroupList = new List<string> { };

                var booking = await bookingRepository.DetailBookingForCustomer(bookingId);

                var listBookingDetails = await serviceRepository.GetServiceForBookingDetail(bookingId);

                foreach (var item in listBookingDetails)
                {
                    serviceGroupList.Add(item.ServiceDetail.Service.ServiceGroup);
                }

                foreach (var item in serviceGroupList.Distinct())
                {
                    var serviceList = listBookingDetails.Where(s => s.ServiceDetail.Service.ServiceGroup.Equals(item)).ToList();

                    var serviceDtoList = mapper.Map<List<BookingDetail>, List<ServiceListBookingDetailDto>>(serviceList,
                    obj => obj.AfterMap((src, des) =>
                    {
                        for (int i = 0; i < src.Count; i++)
                        {
                            if (src[i].Product is not null)
                            {
                                var serviceName = src[i].ServiceDetail.Service.ServiceName + "@Sản phẩm đi kèm: " + src[i].Product.ProductName;
                                serviceName = serviceName.Replace("@", "@" + System.Environment.NewLine);
                                var price = String.Format(CultureInfo.InvariantCulture, "{0:0,0.000}", src[i].ServicePrice) + "@" + String.Format(CultureInfo.InvariantCulture, "{0:0,0.000}", src[i].ProductPrice);
                                price = price.Replace("@", "@" + System.Environment.NewLine);

                                des[i].ServiceName = serviceName;
                                des[i].ServicePrice = price;
                            }
                            else
                            {
                                des[i].ServiceName = src[i].ServiceDetail.Service.ServiceName;
                                des[i].ServicePrice = String.Format(CultureInfo.InvariantCulture, "{0:0,0.000}", src[i].ServicePrice);
                            }
                        }
                    }));

                    serviceSelectList.Add(new GroupServiceBookingDetailDto { ServiceGroup = item, serviceListBookingDetailDtos = serviceDtoList });
                }

                var bookingDto = mapper.Map<BookingDetailForCustomerResponseDto>(booking,
                obj => obj.AfterMap((src, des) =>
                {
                    des.groupServiceBookingDetailDtos = serviceSelectList;
                }));

                return bookingDto;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<List<HourDto>> FilterListBookingByGarageAndDate(int bookingId, string date)
        {
            try
            {
                var dateSelect = DateTime.Parse(date);

                var list = await bookingRepository.FilterListBookingByGarageAndDate(bookingId, dateSelect);

                var listHours = new List<HourDto>();

                foreach (var booking in list)
                {
                    if (!listHours.Any(b => b.Hour.Equals(booking.BookingTime.ToString("hh:tt"))))
                    {
                        var bookingPerHour = list.Where(b => b.BookingTime.Equals(booking.BookingTime)).ToList();

                        var listDto = mapper.Map<List<BookingListForStaffResponseDto>>(bookingPerHour);

                        var hour = new HourDto() { Hour = booking.BookingTime.ToString("hh:tt"), bookingListForStaffResponseDtos = listDto };

                        listHours.Add(hour);
                    }
                }

                return listHours;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task VnPayPaymentGateway()
        {
            try
            {

            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}