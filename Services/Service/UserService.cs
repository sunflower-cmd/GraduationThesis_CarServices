using System.Diagnostics;
using AutoMapper;
using GraduationThesis_CarServices.Encrypting;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Geocoder;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.User;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly EncryptConfiguration encryptConfiguration;
        private readonly GeocoderConfiguration geocoderConfiguration;

        public UserService(IMapper mapper, IUserRepository userRepository,
        EncryptConfiguration encryptConfiguration, GeocoderConfiguration geocoderConfiguration, ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.encryptConfiguration = encryptConfiguration;
            this.geocoderConfiguration = geocoderConfiguration;
        }

        public async Task<List<UserListResponseDto>?> View(PageDto page)
        {
            try
            {
                var list = mapper.Map<List<UserListResponseDto>>(await userRepository.View(page));

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

        public async Task<List<CustomerListResponseDto>> SearchCustomer(string search)
        {
            try
            {
                var list = mapper.Map<List<User>?, List<CustomerListResponseDto>>(await userRepository.SearchUser(search, 1),
                otp => otp.AfterMap((src, des) =>
                {
                    for (int i = 0; i < src!.Count; i++)
                    {
                        des[i].TotalBooking = userRepository.TotalBooking(src[i].Customer.CustomerId);
                    }
                }));

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

        public async Task<List<CustomerListResponseDto>> FilterCustomer(PageDto page)
        {
            try
            {
                var list = mapper.Map<List<User>, List<CustomerListResponseDto>>(await userRepository.FilterByRole(page, 1),
                otp => otp.AfterMap((src, des) =>
                {
                    for (int i = 0; i < src.Count; i++)
                    {
                        des[i].TotalBooking = userRepository.TotalBooking(src[i].Customer.CustomerId);
                    }
                }));

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

        public async Task<List<UserListResponseDto>?> SearchUser(string search, int roleId)
        {
            try
            {
                switch (false)
                {
                    case var isFalse when isFalse == (roleId != 0):
                        throw new MyException("Can't accept value 0.", 404);
                }

                var list = mapper.Map<List<UserListResponseDto>>(await userRepository.SearchUser(search, roleId));
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

        public async Task<List<UserListResponseDto>> FilterUser(PageDto page, int roleId)
        {
            try
            {
                switch (false)
                {
                    case var isFalse when isFalse == (roleId != 0):
                        throw new MyException("Can't accept value 0.", 404);
                }

                var list = new List<UserListResponseDto>();

                switch (roleId)
                {
                    case 3:
                        return list = mapper.Map<List<User>, List<UserListResponseDto>>(await userRepository.FilterByRole(page, roleId));
                    default:
                        return list = mapper.Map<List<User>, List<UserListResponseDto>>(await userRepository.FilterByRole(page, roleId));
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

        public async Task<CustomerDetailResponseDto> CustomerDetail(int userId)
        {
            try
            {
                var c = await userRepository.CustomerDetail(userId);
                var customer = mapper.Map<CustomerDetailResponseDto>(c);

                return customer;
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

        public async Task<UserDetailResponseDto?> Detail(int id)
        {
            try
            {
                var user = await userRepository.Detail(id);

                switch (false)
                {
                    case var isExist when isExist == (user != null):
                        throw new MyException("The user doesn't exist.", 404);
                }

                switch (user)
                {
                    case var userCustomer when userCustomer!.Customer != null:
                        return mapper.Map<UserDetailResponseDto>(user);
                }

                return mapper.Map<UserDetailResponseDto>(user);
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

        public async Task Create(UserCreateRequestDto requestDto)
        {
            try
            {
                switch (false)
                {
                    case var isExist when isExist == (requestDto.UserPassword.Equals(requestDto.PasswordConfirm)):
                        throw new MyException("Your password and confirm password isn't match.", 404);
                }

                encryptConfiguration.CreatePasswordHash(requestDto.UserPassword, out byte[] password_hash, out byte[] password_salt);
                var encryptEmail = encryptConfiguration.Base64Encode(requestDto.UserEmail);

                var user = mapper.Map<UserCreateRequestDto, User>(requestDto,
                opt => opt.AfterMap((src, des) =>
                {
                    des.UserEmail = encryptEmail;
                    des.PasswordHash = password_hash;
                    des.PasswordSalt = password_salt;
                    des.UserStatus = Status.Activate;
                    des.EmailConfirmed = 0;
                    des.CreatedAt = DateTime.Now;
                    des.RoleId = 2;
                }));

                await userRepository.Create(user);
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

        public async Task CustomerFirstLoginUpdate(UserUpdateRequestDto requestDto, int userId)
        {
            try
            {
                var u = await userRepository.Detail(userId);

                switch (false)
                {
                    case var isFalse when isFalse != (requestDto.UserFirstName.Equals("")):
                        throw new MyException("The user first name can't be empty.", 404);
                    case var isFalse when isFalse != (requestDto.UserEmail.Equals("")):
                        throw new MyException("The user email number can't be empty.", 404);
                    case var isExist when isExist == (u != null):
                        throw new MyException("The user doesn't exist.", 404);
                    case var isCustomer when isCustomer == (u!.RoleId == 1):
                        throw new MyException("The user don't have permission.", 403);
                    case var isFirstLogin when isFirstLogin == (u.UserFirstName == null):
                        throw new MyException("The user already update information.", 404);
                }

                var encodeEmail = encryptConfiguration.Base64Encode(requestDto.UserEmail);

                var user = mapper.Map<UserUpdateRequestDto, User>(requestDto, u!,
                opt => opt.AfterMap((src, des) =>
                {
                    des.UserEmail = encodeEmail;
                    des.UpdatedAt = DateTime.Now;
                }));
                await userRepository.Update(user);
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

        public async Task UpdateStatus(UserStatusRequestDto requestDto)
        {
            try
            {
                var u = await userRepository.Detail(requestDto.UserId);

                switch (false)
                {
                    case var isExist when isExist == (u != null):
                        throw new MyException("The user doesn't exist.", 404);
                }

                var user = mapper.Map<UserStatusRequestDto, User>(requestDto, u!);
                await userRepository.Update(user);
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