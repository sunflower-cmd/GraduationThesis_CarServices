using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class GarageDetailRepository : IGarageDetailRepository
    {
        private readonly DataContext context;
        public GarageDetailRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<GarageDetail>?> View(PageDto page)
        {
            try
            {
                var list = await PagingConfiguration<GarageDetail>.Get(context.GarageDetails
                .Include(g => g.Garage)
                .Include(g => g.Service)
                .ThenInclude(s => s.Products)
                .Include(g => g.Service)
                .ThenInclude(s => s.ServiceDetails), page);
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GarageDetail>?> FilterServiceByGarage(int garageId)
        {
            try
            {
                var list = await context.GarageDetails
                .Where(s => s.GarageId == garageId)
                .Include(s => s.Service)
                .ThenInclude(s => s.Products)
                .ToListAsync();
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GarageDetail?> Detail(int id)
        {
            try
            {
                var serviceGarage = await context.GarageDetails
                .Where(g => g.GarageDetailId == id)
                .Include(g => g.Garage)
                .Include(g => g.Service)
                .ThenInclude(s => s.Products)
                .Include(g => g.Service)
                .ThenInclude(s => s.ServiceDetails)
                .FirstOrDefaultAsync();
                return serviceGarage;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(GarageDetail garageDetail)
        {
            try
            {
                context.GarageDetails.Add(garageDetail);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(GarageDetail garageDetail)
        {
            try
            {
                context.GarageDetails.Update(garageDetail);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}