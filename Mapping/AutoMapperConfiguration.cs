using AutoMapper;
using GraduationThesis_CarServices.Models.DTO.Authentication;
using GraduationThesis_CarServices.Models.DTO.User;
using GraduationThesis_CarServices.Models.DTO.Role;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Models.DTO.Coupon;
using GraduationThesis_CarServices.Models.DTO.Garage;
using GraduationThesis_CarServices.Models.DTO.Report;
using GraduationThesis_CarServices.Models.DTO.Review;
using GraduationThesis_CarServices.Models.DTO.Car;
using GraduationThesis_CarServices.Models.DTO.Category;
using GraduationThesis_CarServices.Models.DTO.Service;
using GraduationThesis_CarServices.Models.DTO.Product;
using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Mechanic;
using GraduationThesis_CarServices.Models.DTO.WorkingSchedule;
using GraduationThesis_CarServices.Models.DTO.GarageDetail;
using GraduationThesis_CarServices.Models.DTO.ServiceDetail;
using System.Text;

namespace GraduationThesis_CarServices.Mapping
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            //Authen
            CreateMap<User, LoginDto>();
            CreateMap<User, UserLoginDto>().ForMember(des => des.UserFullName,
                obj => obj.MapFrom(src => src.UserFirstName + src.UserLastName))
                .ForMember(des => des.RoleDto, obj => obj.MapFrom(src => src.Role));


            //Role
            CreateMap<Role, RoleDto>();


            //Coupon
            CreateMap<Coupon, CouponGarageDto>()
                .ForMember(des => des.CouponEndDate, obj => obj.MapFrom(src => src.CouponEndDate.ToString("MM/dd/yyyy")))
                .ReverseMap()
                .ForMember(des => des.Garage, obj => obj.Ignore());
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<Coupon, CouponListResponseDto>()
                .ForMember(des => des.CouponStartDate, obj => obj.MapFrom(src => src.CouponStartDate.ToString("MM/dd/yyyy")))
                .ForMember(des => des.CouponEndDate, obj => obj.MapFrom(src => src.CouponEndDate.ToString("MM/dd/yyyy")))
                .ForMember(des => des.CreatedAt, obj => obj.MapFrom(src => src.CreatedAt!.Value.ToString("MM/dd/yyyy")));
            CreateMap<Coupon, CouponDetailResponseDto>()
                .ForMember(des => des.CouponType, obj => obj.MapFrom(src => src.CouponType.ToString()))
                .ForMember(des => des.CouponStatus, obj => obj.MapFrom(src => src.CouponStatus.ToString()))
                .ForMember(des => des.CouponStartDate, obj => obj.MapFrom(src => src.CouponStartDate.ToString("MM/dd/yyyy")))
                .ForMember(des => des.CouponEndDate, obj => obj.MapFrom(src => src.CouponEndDate.ToString("MM/dd/yyyy")));
            CreateMap<CouponCreateRequestDto, Coupon>().ReverseMap();
            CreateMap<Coupon, CouponUpdateRequestDto>()
                .ForMember(des => des.CouponId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Coupon, CouponStatusRequestDto>().ReverseMap();


            //Garage
            CreateMap<Garage, GarageBookingDto>()
                .ForMember(des => des.FullAddress, obj => obj.MapFrom(src => src.GarageAddress + ", " + src.GarageWard + ", " + src.GarageDistrict + ", " + src.GarageCity))
                .ReverseMap().ForMember(des => des.Bookings, obj => obj.Ignore());
            CreateMap<Garage, GarageReviewDto>();
            CreateMap<Garage, GarageWorkingScheduleDto>();
            CreateMap<Garage, GarageServiceDto>();
            CreateMap<Garage, GarageOfGarageDetailDto>();
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<Garage, GarageListResponseDto>()
                .ForMember(des => des.GarageFullAddress, obj => obj.MapFrom(src => src.GarageAddress + ", " + src.GarageWard + ", " + src.GarageDistrict + ", " + src.GarageCity));
            CreateMap<Garage, GarageDetailResponseDto>()
                .ForMember(des => des.UserGarageDto, obj => obj.MapFrom(src => src.User))
                .ForMember(des => des.ReviewGarageDto, obj => obj.MapFrom(src => src.Reviews))
                .ForMember(des => des.CouponGarageDto, obj => obj.MapFrom(src => src.Coupons))
                .ForMember(des => des.GarageDetailGarageDto, obj => obj.MapFrom(src => src.GarageDetails))
                .ForMember(des => des.GarageFullAddress, obj => obj.MapFrom(src => src.GarageAddress + ", " + src.GarageWard + ", " + src.GarageDistrict + ", " + src.GarageCity));
            CreateMap<Garage, GarageCreateRequestDto>().ReverseMap();
            CreateMap<Garage, GarageUpdateRequestDto>()
                .ForMember(des => des.GarageId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Garage, GarageStatusRequestDto>()
                .ForMember(des => des.GarageId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Garage, LocationUpdateRequestDto>()
                .ForMember(des => des.GarageId, obj => obj.Ignore()).ReverseMap();


            //User
            CreateMap<Customer, UserCustomerDto>();
            CreateMap<User, UserBookingDto>()
                .ForMember(des => des.FullName, obj => obj.MapFrom(src => src.Customer.User.UserFirstName + src.Customer.User.UserLastName));
            CreateMap<User, MechanicBookingDetailDto>()
                .ForMember(des => des.UserEmail, obj => obj.MapFrom(src => Base64Decode(src.UserEmail)));
            CreateMap<User, CustomerBookingDto>()
                .ForMember(des => des.UserEmail, obj => obj.MapFrom(src => Base64Decode(src.UserEmail)));;
            CreateMap<User, UserReviewDto>()
                .ForMember(des => des.FullName, obj => obj.MapFrom(src => src.Customer.User.UserFirstName + src.Customer.User.UserLastName));
            CreateMap<User, UserGarageDto>().ForMember(des => des.FullName,
                obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName))
                .ForMember(des => des.RoleDto, obj => obj.MapFrom(src => src.Role));
            CreateMap<User, UserMechanicDto>().ForMember(des => des.FullName,
                obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName));
            CreateMap<User, UserDetailMechanicDto>().ForMember(des => des.FullName,
                obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName))
                .ForMember(des => des.RoleDto, obj => obj.MapFrom(src => src.Role))
                .ForMember(des => des.UserGender, obj => obj.MapFrom(src => src.UserGender.ToString()));
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<User, UserListResponseDto>().ForMember(des => des.FullName,
                obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName))
                .ForMember(des => des.RoleDto, obj => obj.MapFrom(src => src.Role))
                .ForMember(des => des.UserStatus, obj => obj.MapFrom(src => src.UserStatus.ToString()))
                .ForMember(des => des.UserEmail, obj => obj.MapFrom(src => Base64Decode(src.UserEmail)));
            CreateMap<User, UserDetailResponseDto>()
                .ForMember(des => des.FullName, obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName))
                .ForMember(des => des.RoleDto, obj => obj.MapFrom(src => src.Role))
                .ForMember(des => des.UserCustomerDto, obj => obj.MapFrom(src => src.Customer))
                .ForMember(des => des.UserGender, obj => obj.MapFrom(src => src.UserGender.ToString()))
                .ForMember(des => des.UserEmail, obj => obj.MapFrom(src => Base64Decode(src.UserEmail)))
                .ForMember(des => des.UserDateOfBirth, obj => obj.MapFrom(src => src.UserDateOfBirth!.Value.ToString("MM/dd/yyyy")));
            CreateMap<User, CustomerDetailResponseDto>()
                .ForMember(des => des.FullName, obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName))
                .ForMember(des => des.UserEmail, obj => obj.MapFrom(src => Base64Decode(src.UserEmail)))
                .ForMember(des => des.CreatedAt, obj => obj.MapFrom(src => src.CreatedAt!.Value.ToString("MM/dd/yyyy hh:mm tt")))
                .ForMember(des => des.UserCustomerDto, obj => obj.MapFrom(src => src.Customer));
            CreateMap<UserCreateRequestDto, User>().ReverseMap()
                .ForMember(des => des.UserPassword, obj => obj.Ignore())
                .ForMember(des => des.PasswordConfirm, obj => obj.Ignore());
            CreateMap<UserUpdateRequestDto, User>();
            CreateMap<UserRoleRequestDto, User>()
                .ForMember(des => des.UserId, obj => obj.Ignore());
            CreateMap<UserStatusRequestDto, User>()
                .ForMember(des => des.UserId, obj => obj.Ignore());
            CreateMap<UserLocationRequestDto, User>()
                .ForMember(des => des.UserId, obj => obj.Ignore());
            CreateMap<User, UserWorkingScheduleDto>()
                .ForMember(des => des.FullName,
                obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName));    

            //Mechanic
            CreateMap<Mechanic, MechanicWorkingScheduleDto>()
                .ForMember(des => des.UserWorkingScheduleDto, obj => obj.MapFrom(src => src.User));

            //Working Schedule
            // CreateMap<GarageMechanic, WorkingScheduleListResponseDto>();
            // CreateMap<GarageMechanic, WorkingScheduleByGarageDto>()
            //     .ForMember(des => des.MechanicWorkingScheduleDto, obj => obj.MapFrom(src => src.Mechanic))
            //     .ForMember(des => des.WorkingScheduleStatus, obj => obj.MapFrom(src => src.WorkingScheduleStatus.ToString()));
            // CreateMap<GarageMechanic, WorkingScheduleByMechanicDto>()
            //     .ForMember(des => des.GarageWorkingScheduleDto, obj => obj.MapFrom(src => src.Garage));
            // CreateMap<GarageMechanic, WorkingScheduleDetailResponseDto>()
            //     .ForMember(des => des.GarageWorkingScheduleDto, obj => obj.MapFrom(src => src.Garage))
            //     .ForMember(des => des.MechanicWorkingScheduleDto, obj => obj.MapFrom(src => src.Mechanic));
            // CreateMap<GarageMechanic, WorkingScheduleCreateRequestDto>().ReverseMap();
            // CreateMap<GarageMechanic, WorkingScheduleUpdateStatusDto>().ForMember(des => des.WorkingScheduleId, obj => obj.Ignore()).ReverseMap();

            //Customer
            CreateMap<Customer, CustomerReviewDto>()
                .ForMember(des => des.UserReviewDto, obj => obj.MapFrom(src => src.User));
            CreateMap<Customer, UserCustomerDto>()
                .ForMember(des => des.CustomerCarDtos, obj => obj.MapFrom(src => src.Cars));


            //Mechanic
            CreateMap<Mechanic, MechanicListResponseDto>()
                .ForMember(des => des.UserMechanicDto, obj => obj.MapFrom(src => src.User));
            CreateMap<Mechanic, MechanicDetailResponseDto>()
                .ForMember(des => des.UserDetailMechanicDto, obj => obj.MapFrom(src => src.User));

            //Review
            CreateMap<Review, ReviewGarageDto>().ReverseMap()
                .ForMember(des => des.Garage, obj => obj.Ignore());
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<Review, ReviewListResponseDto>()
                .ForMember(des => des.CreatedAt, obj => obj.MapFrom(src => src.CreatedAt!.Value.ToString("MM/dd/yyyy")))
                .ForMember(des => des.UserReviewDto, obj => obj.MapFrom(src => src.Customer.User))
                .ForMember(des => des.GarageReviewDto, obj => obj.MapFrom(src => src.Garage));
            CreateMap<Review, ReviewDetailResponseDto>()
                .ForMember(des => des.CustomerReviewDto, obj => obj.MapFrom(src => src.Customer))
                .ForMember(des => des.GarageReviewDto, obj => obj.MapFrom(src => src.Garage));
            CreateMap<ReviewCreateRequestDto, Review>();
            CreateMap<Review, ReviewUpdateRequestDto>()
                .ForMember(des => des.ReviewId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Review, ReviewStatusRequestDto>()
                .ForMember(des => des.ReviewId, obj => obj.Ignore()).ReverseMap();


            //GarageDetail
            CreateMap<GarageDetail, GarageDetailGarageDto>()
                .ForMember(des => des.ServiceGarageDto, obj => obj.MapFrom(src => src.Service));
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<GarageDetail, GarageDetailServiceDto>()
                .ForMember(des => des.GarageServiceDto, obj => obj.MapFrom(src => src.Garage));
            CreateMap<GarageDetail, GarageDetailListResponseDto>()
                .ForMember(des => des.ServiceOfGarageDetailDto, obj => obj.MapFrom(src => src.Service));
            CreateMap<GarageDetail, GarageDetailDetailResponseDto>()
                .ForMember(des => des.ServiceOfGarageDetailDto, obj => obj.MapFrom(src => src.Service))
                .ForMember(des => des.GarageOfGarageDetailDto, obj => obj.MapFrom(src => src.Garage));
            CreateMap<GarageDetail, GarageDetailCreateRequestDto>().ReverseMap();
            CreateMap<GarageDetail, GarageDetailUpdateRequestDto>().ForMember(des => des.GarageDetailId, obj => obj.Ignore()).ReverseMap();

            //ServiceDetail
            CreateMap<ServiceDetail, ServiceDetailServiceDto>();
            CreateMap<ServiceDetail, ServiceDetailListResponseDto>()
                .ForMember(des => des.ServiceOfServiceDetailDto, obj => obj.MapFrom(src => src.Service));
            CreateMap<ServiceDetail, ServiceDetailDetailResponseDto>()
                .ForMember(des => des.ServiceOfServiceDetailDto, obj => obj.MapFrom(src => src.Service));
            CreateMap<ServiceDetail, ServiceDetailCreateRequestDto>().ReverseMap();
            CreateMap<ServiceDetail, ServiceDetailUpdateRequestDto>().ForMember(des => des.ServiceDetailId, obj => obj.Ignore()).ReverseMap();
            CreateMap<ServiceDetail, ServiceDetailPriceRequestDto>().ForMember(des => des.ServiceDetailId, obj => obj.Ignore()).ReverseMap();


            //Service
            CreateMap<Service, ServiceGarageDto>();
            CreateMap<Service, ServiceProductDto>();
            CreateMap<Service, ServiceOfGarageDetailDto>()
                .ForMember(des => des.ProductServiceDtos, obj => obj.MapFrom(src => src.Products))
                .ForMember(des => des.ServiceDetailServiceDtos, obj => obj.MapFrom(src => src.ServiceDetails));
            CreateMap<Service, ServiceOfServiceDetailDto>();
            CreateMap<Service, ServiceListResponseDto>();
            CreateMap<Service, ServiceDetailResponseDto>()
                .ForMember(des => des.ProductServiceDtos, obj => obj.MapFrom(src => src.Products))
                .ForMember(des => des.ServiceDetailServiceDtos, obj => obj.MapFrom(src => src.ServiceDetails))
                .ForMember(des => des.GarageDetailServiceDtos, obj => obj.MapFrom(src => src.GarageDetails))
                .ForMember(des => des.ServiceStatus, obj => obj.MapFrom(src => src.ServiceStatus.ToString()));
            CreateMap<Service, ServiceCreateRequestDto>().ReverseMap();
            CreateMap<Service, ServiceUpdateRequestDto>().ForMember(des => des.ServiceId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Service, ServiceStatusRequestDto>().ForMember(des => des.ServiceId, obj => obj.Ignore()).ReverseMap();
            //CreateMap<Service, DeleteServiceDto>().ReverseMap();

            //Report
            CreateMap<Report, ReportBookingDto>();
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<Report, ReportDto>().ReverseMap();
            CreateMap<Report, CreateReportDto>().ReverseMap();
            CreateMap<Report, UpdateReportDto>().ForMember(des => des.ReportId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Report, DeleteReportDto>().ReverseMap();


            //Car
            CreateMap<Car, CustomerCarDto>();
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<Car, CarListResponseDto>().ReverseMap();
            CreateMap<Car, CarDetailResponseDto>().ReverseMap();
            CreateMap<Car, CarCreateRequestDto>().ReverseMap();
            CreateMap<Car, CarUpdateRequestDto>()
                .ForMember(des => des.CarId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Car, CarStatusRequestDto>()
                .ForMember(des => des.CarId, obj => obj.Ignore()).ReverseMap();

            // // Subcategory
            // CreateMap<Subcategory, SubcategoryDto>()
            //     .ForMember(des => des.SubcategoryStatus, obj => obj.MapFrom(src => src.SubcategoryStatus.ToString()));
            // CreateMap<Subcategory, SubcategoryProductDto>()
            //     .ForMember(des => des.CategoryProductDto, obj => obj.MapFrom(src => src.Category));
            // CreateMap<Subcategory, CreateSubcategoryDto>().ReverseMap();
            // CreateMap<Subcategory, UpdateSubcategoryDto>().ForMember(des => des.SubcategoryId, obj => obj.Ignore()).ReverseMap();
            // CreateMap<Subcategory, DeleteSubcategoryDto>().ReverseMap();


            // Category
            CreateMap<Category, CategoryListResponseDto>()
                .ForMember(des => des.CategoryStatus, obj => obj.MapFrom(src => src.CategoryStatus.ToString()));
            CreateMap<Category, CategoryDetailResponseDto>()
                .ForMember(des => des.CategoryStatus, obj => obj.MapFrom(src => src.CategoryStatus.ToString()));
            CreateMap<CategoryCreateRequestDto, Category>();
            CreateMap<CategoryUpdateRequestDto, Category>()
                .ForMember(des => des.CategoryId, obj => obj.Ignore());
            CreateMap<CategoryStatusRequestDto, Category>()
                .ForMember(des => des.CategoryId, obj => obj.Ignore());
            CreateMap<Category, CategoryProductDto>();


            // Product
            CreateMap<Product, ProductBookingDetailDto>();
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<Product, ProductServiceDto>();
            CreateMap<Product, ProductListResponseDto>()
                //.ForMember(des => des.SubcategoryProductDto, obj => obj.MapFrom(src => src.Subcategory))
                .ForMember(des => des.CategoryProductDto, obj => obj.MapFrom(src => src.Category));
            CreateMap<Product, ProductDetailResponseDto>()
                //.ForMember(des => des.SubcategoryProductDto, obj => obj.MapFrom(src => src.Subcategory))
                .ForMember(des => des.ServiceProductDto, obj => obj.MapFrom(src => src.Service))
                .ForMember(des => des.ProductStatus, obj => obj.MapFrom(src => src.ProductStatus.ToString()));
            CreateMap<ProductCreateRequestDto, Product>();
            CreateMap<Product, ProductPriceRequestDto>().ForMember(des => des.ProductId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Product, ProductStatusRequestDto>().ForMember(des => des.ProductId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Product, ProductQuantityRequestDto>().ForMember(des => des.ProductId, obj => obj.Ignore()).ReverseMap();

            //ServiceBooking
            CreateMap<BookingDetail, ServiceListDto>()
                .ReverseMap()
                .ForMember(des => des.MechanicId, obj => obj.Ignore());
            CreateMap<BookingDetail, BookingDetailDto>()
                .ForMember(des => des.MechanicBookingDetailDto, obj => obj.MapFrom(src => src.Mechanic.User))
                .ForPath(des => des.MechanicBookingDetailDto.FullName, obj => obj.MapFrom(src => src.Mechanic.User.UserFirstName + " " + src.Mechanic.User.UserLastName))
                .ForMember(des => des.ProductBookingDetailDto, obj => obj.MapFrom(src => src.Product))
                .ForMember(des => des.ServiceBookingDetailDto, obj => obj.MapFrom(src => src.ServiceDetail))
                .ForPath(des => des.ServiceBookingDetailDto.ServiceName, obj => obj.MapFrom(src => src.ServiceDetail.Service.ServiceName))
                .ForPath(des => des.ServiceBookingDetailDto.ServiceImage, obj => obj.MapFrom(src => src.ServiceDetail.Service.ServiceImage))
                .ForPath(des => des.ServiceBookingDetailDto.ServicePrice, obj => obj.MapFrom(src => src.ServiceDetail.ServicePrice));


            CreateMap<ServiceDetail, ServiceBookingDetailDto>();

            //Booking
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<Booking, FilterByCustomerResponseDto>()
                .ForMember(des => des.BookingTime, obj => obj.MapFrom(src => src.BookingTime.ToString("MM/dd/yyyy hh:mm tt")));
            CreateMap<Booking, BookingListResponseDto>()
                .ForMember(des => des.UserBookingDto, obj => obj.MapFrom(src => src.Car.Customer.User))
                .ForMember(des => des.GarageBookingDto, obj => obj.MapFrom(src => src.Garage));
            CreateMap<Booking, BookingDetailResponseDto>()
                // .ForMember(des => des.UserBookingDto, obj => obj.MapFrom(src => src.Car))
                .ForMember(des => des.CustomerBookingDto, obj => obj.MapFrom(src => src.Car.Customer.User))
                .ForPath(des => des.CustomerBookingDto.FullName, obj => obj.MapFrom(src => src.Car.Customer.User.UserFirstName + " " + src.Car.Customer.User.UserLastName))
                .ForMember(des => des.GarageBookingDto, obj => obj.MapFrom(src => src.Garage))
                .ForMember(des => des.BookingDetailDtos, obj => obj.MapFrom(src => src.BookingDetails));
            CreateMap<Booking, BookingCreateRequestDto>()
                .ReverseMap();
            CreateMap<Booking, BookingStatusRequestDto>()
                .ForMember(des => des.BookingId, obj => obj.Ignore()).ReverseMap();
        }

        public string Base64Decode(string decodeStr)
        {
            var strBytes = Convert.FromBase64String(decodeStr);
            return Encoding.UTF8.GetString(strBytes);
        }
    }
}