﻿using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Service;
using GraduationThesis_CarServices.Models.DTO.GarageDetail;
using GraduationThesis_CarServices.Paging;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IServiceService
    {
        Task<GenericObject<List<ServiceListResponseDto>>> View(PageDto page);
        Task<List<GarageDetailListResponseDto>?> FilterServiceByGarage(int garageId);
        Task<ServiceDetailResponseDto?> Detail(int id);
        Task Create(ServiceCreateRequestDto requestDto);
        Task Update(ServiceUpdateRequestDto requestDto);
        Task UpdateStatus(ServiceStatusRequestDto requestDto);

    }
}
