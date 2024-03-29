﻿using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Product;
using GraduationThesis_CarServices.Paging;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IProductService
    {
        Task<GenericObject<List<ProductListResponseDto>>> View(PageDto page);
        Task<List<ProductListResponseDto>?> FilterAvailableProductForService(int serviceId);
        Task<ProductDetailResponseDto?> Detail(int id);
        Task Create(ProductCreateRequestDto requestDto);
        Task Update(ProductUpdateRequestDto requestDto);
        Task UpdateStatus(int productId);
        Task<GenericObject<List<ProductListResponseDto>>> SearchByName(SearchByNameRequestDto requestDto);
    }
}
