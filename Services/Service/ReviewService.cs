using System.Diagnostics;
using AutoMapper;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Review;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IMapper mapper;
        private readonly IReviewRepository reviewRepository;
        private readonly IUserRepository userRepository;
        private readonly IGarageRepository garageRepository;

        public ReviewService(IMapper mapper, IReviewRepository reviewRepository, IUserRepository userRepository, IGarageRepository garageRepository)
        {
            this.mapper = mapper;
            this.reviewRepository = reviewRepository;
            this.userRepository = userRepository;
            this.garageRepository = garageRepository;
        }

        public async Task<List<ReviewListResponseDto>?> View(PageDto page)
        {

            try
            {
                var list = mapper.Map<List<ReviewListResponseDto>>(await reviewRepository.View(page));

                return list;
            }
            catch (Exception e)
            {
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

        public async Task<List<ReviewListResponseDto>?> FilterReviewByGarageId(PagingReviewPerGarageRequestDto requestDto)
        {
            try
            {
                var isGarageExist = await garageRepository.IsGarageExist(requestDto.GarageId);

                switch (false)
                {
                    case var isExist when isExist == isGarageExist:
                        throw new NullReferenceException("The garage doesn't exist.");
                }

                var page = new PageDto
                {
                    PageIndex = requestDto.PageIndex,
                    PageSize = requestDto.PageSize
                };

                var list = mapper.Map<List<ReviewListResponseDto>>(await reviewRepository.FilterReviewByGarageId(requestDto.GarageId, page));
                
                return list;
            }
            catch (Exception e)
            {
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

        public async Task<ReviewDetailResponseDto?> Detail(int reviewId)
        {
            try
            {
                var isReviewExist = await reviewRepository.IsReviewExist(reviewId);

                switch (false)
                {
                    case var isExist when isExist == isReviewExist:
                        throw new Exception("The review doesn't exist.");
                }

                var review = mapper.Map<ReviewDetailResponseDto>(await reviewRepository.Detail(reviewId));

                return review;
            }
            catch (Exception e)
            {
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

        public async Task Create(ReviewCreateRequestDto requestDto)
        {
            try
            {
                var isInRange = false;
                var isCustomerExist = await userRepository.IsCustomerExist(requestDto.CustomerId);
                var isGarageExist = await garageRepository.IsGarageExist(requestDto.GarageId);
                if (requestDto.Rating >= 0 && requestDto.Rating <= 5)
                {
                    isInRange = true;
                }

                switch (false)
                {
                    case var isExist when isExist == isCustomerExist:
                        throw new Exception("The customer doesn't exist.");
                    case var isExist when isExist == isGarageExist:
                        throw new Exception("The garage doesn't exist.");
                    case var isRange when isRange == isInRange:
                        throw new Exception("Rating is outside of the range allowed.");
                    default:
                        var review = mapper.Map<ReviewCreateRequestDto, Review>(requestDto,
                            otp => otp.AfterMap((src, des) =>
                            {
                                des.ReviewStatus = Status.Activate;
                                des.CreatedAt = DateTime.Now;
                            }));
                        await reviewRepository.Create(review);

                        throw new Exception("Successfully.");
                }
            }
            catch (Exception e)
            {
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

        public async Task Update(ReviewUpdateRequestDto requestDto)
        {
            try
            {
                var isInRange = false;
                var isReviewExist = await reviewRepository.IsReviewExist(requestDto.ReviewId);
                if (requestDto.Rating >= 0 && requestDto.Rating <= 5)
                {
                    isInRange = true;
                }

                switch (false)
                {
                    case var isExist when isExist == isReviewExist:
                        throw new Exception("The review doesn't exist.");
                    case var isRange when isRange == isInRange:
                        throw new Exception("Rating is outside of the range allowed.");
                    default:
                        var r = await reviewRepository.Detail(requestDto.ReviewId);
                        var review = mapper.Map<ReviewUpdateRequestDto, Review>(requestDto, r!,
                        otp => otp.AfterMap((src, des) =>
                        {
                            des.UpdatedAt = DateTime.Now;
                        }));
                        await reviewRepository.Update(review);

                        throw new Exception("Successfully.");
                }
            }
            catch (Exception e)
            {
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

        public async Task UpdateStatus(ReviewStatusRequestDto requestDto)
        {
            try
            {
                var isReviewExist = await reviewRepository.IsReviewExist(requestDto.ReviewId);

                switch (false)
                {
                    case var isExist when isExist == isReviewExist:
                        throw new Exception("The review doesn't exist.");
                    default:
                        var r = await reviewRepository.Detail(requestDto.ReviewId);
                        var review = mapper.Map<ReviewStatusRequestDto, Review>(requestDto, r!);
                        await reviewRepository.Update(review);

                        throw new Exception("Successfully.");
                }
            }
            catch (Exception e)
            {
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