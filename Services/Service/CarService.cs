using System.Diagnostics;
using AutoMapper;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Car;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class CarService : ICarService
    {
        private readonly ICarRepository carRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        public CarService(ICarRepository carRepository, IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.carRepository = carRepository;
            this.userRepository = userRepository;
        }

        public async Task<List<CarListResponseDto>?> FilterUserCar(int userId)
        {
            try
            {
                int customerId = await userRepository.GetCustomerId(userId);

                switch (false)
                {
                    case var isExist when isExist == (customerId != 0):
                        throw new MyException("The customer doesn't exist.", 404);
                }

                var list = mapper
                .Map<List<CarListResponseDto>>(await carRepository.FilterUserCar(customerId));

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

        public async Task<CarDetailResponseDto?> Detail(int id)
        {
            try
            {
                var car = mapper
                .Map<CarDetailResponseDto>(await carRepository.Detail(id));

                return false switch
                {
                    var isExist when isExist == (car != null) => throw new MyException("The car doesn't exist.", 404),
                    _ => car,
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

        public async Task Create(CarCreateRequestDto requestDto, int userId)
        {
            try
            {
                int customerId = await userRepository.GetCustomerId(userId);

                var isLicensePlateExist = await carRepository.IsLicensePlate(requestDto.CarLicensePlate);

                switch (false)
                {
                    case var isExist when isExist == (customerId != 0):
                        throw new MyException("The customer doesn't exist.", 404);
                    case var isExist when isExist != isLicensePlateExist:
                        throw new MyException("The license plate already exists.", 404);
                }

                var car = mapper.Map<CarCreateRequestDto, Car>(requestDto,
                otp => otp.AfterMap((src, des) =>
                {
                    des.CarBookingStatus = CarStatus.Available;
                    des.CarStatus = Status.Activate;
                    des.CreatedAt = DateTime.Now;
                    des.CustomerId = customerId;
                }));
                await carRepository.Create(car);
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

        public async Task Update(CarUpdateRequestDto requestDto)
        {
            try
            {
                var c = await carRepository.Detail(requestDto.CarId);

                switch (false)
                {
                    case var isExist when isExist == (c != null):
                        throw new MyException("The car doesn't exist.", 404);
                }

                var car = mapper.Map<CarUpdateRequestDto, Car>(requestDto, c!,
                otp => otp.AfterMap((src, des) =>
                {
                    des.UpdatedAt = DateTime.Now;
                }));
                await carRepository.Update(car);
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

        public async Task UpdateStatus(CarStatusRequestDto requestDto)
        {
            try
            {
                var c = await carRepository.Detail(requestDto.CarId);

                switch (false)
                {
                    case var isExist when isExist == (c != null):
                        throw new MyException("The car doesn't exist.", 404);
                }

                var car = mapper.Map<CarStatusRequestDto, Car>(requestDto, c!);
                await carRepository.Update(car);
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
    }
}