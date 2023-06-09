﻿using System.Diagnostics;
using AutoMapper;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Product;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        private readonly IMapper mapper;
        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
        }

        public async Task<List<ProductListResponseDto>?> View(PageDto page)
        {
            try
            {
                var list = mapper
                .Map<List<ProductListResponseDto>>(await productRepository.View(page));
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
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public async Task<List<ProductListResponseDto>?> FilterAvailableProductForService(int serviceId)
        {
            try
            {
                var list = mapper
                .Map<List<ProductListResponseDto>>(await productRepository.FilterAvailableProductForService(serviceId));

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
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }


        public async Task<ProductDetailResponseDto?> Detail(int id)
        {
            try
            {
                var product = mapper
                .Map<ProductDetailResponseDto>(await productRepository.Detail(id));
                return product;
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
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public async Task Create(ProductCreateRequestDto requestDto)
        {
            try
            {
                var product = mapper.Map<ProductCreateRequestDto, Product>(requestDto,
                otp => otp.AfterMap((src, des) =>
                {
                    des.ProductStatus = Status.Activate;
                    des.CreatedAt = DateTime.Now;
                }));
                if (!await productRepository.IsDuplicatedProduct(product))
                {
                    await productRepository.Create(product);
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
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public async Task UpdatePrice(ProductPriceRequestDto requestDto)
        {
            try
            {
                if (await productRepository.IsProductExist(requestDto.ProductId))
                {
                    var p = await productRepository.Detail(requestDto.ProductId);
                    var product = mapper.Map<ProductPriceRequestDto, Product>(requestDto, p!,
                    otp => otp.AfterMap((src, des) =>
                    {
                        des.UpdatedAt = DateTime.Now;
                    }));

                    await productRepository.Update(product);
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
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public async Task UpdateStatus(ProductStatusRequestDto requestDto)
        {
            try
            {
                if (await productRepository.IsProductExist(requestDto.ProductId))
                {
                    var p = await productRepository.Detail(requestDto.ProductId);
                    var product = mapper.Map<ProductStatusRequestDto, Product>(requestDto, p!);
                    await productRepository.Update(product);
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
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }
        public async Task UpdateQuantity(ProductQuantityRequestDto requestDto)
        {
            try
            {
                if (await productRepository.IsProductExist(requestDto.ProductId))
                {
                    var p = await productRepository.Detail(requestDto.ProductId);
                    var product = mapper.Map<ProductQuantityRequestDto, Product>(requestDto, p!,
                    otp => otp.AfterMap((src, des) =>
                    {
                        des.UpdatedAt = DateTime.Now;
                    }));
                    await productRepository.Update(product);
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
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }
    }
}
