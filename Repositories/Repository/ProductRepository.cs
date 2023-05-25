﻿using AutoMapper;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public ProductRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        public async Task<List<Product>?> View(PageDto page)
        {
            try
            {
                var list = await PagingConfiguration<Product>
                .Get(context.Products
                .Include(p => p.Subcategory)
                .ThenInclude(s => s.Category)
                .Include(p => p.Service)
                // .Include(p => p.ProductMediaFiles)
                //.ThenInclude(m => m.MediaFile)
                , page);
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsProductExist(int productId){
            try
            {
                var check = await context.Products
                .Where(p => p.ProductId == productId).AnyAsync();

                return check;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Product>?> FilterAvailableProductForService(int ServiceId)
        {
            try
            {
                var list = await context.Products
                .Where(p => p.ServiceId == ServiceId && p.ProductQuantity > 0)
                .Include(p => p.Subcategory)
                .ThenInclude(s => s.Category)
                .Include(p => p.Service)
                // .Include(p => p.ProductMediaFiles)
                // .ThenInclude(m => m.MediaFile)
                .ToListAsync();

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product?> Detail(int id)
        {
            try
            {
                var product = await context.Products
                .Where(p => p.ProductId == id)
                .Include(p => p.Subcategory)
                .ThenInclude(s => s.Category)
                .Include(p => p.Service)
                // .Include(p => p.ProductMediaFiles)
                // .ThenInclude(m => m.MediaFile)
                .FirstOrDefaultAsync();
                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsDuplicatedProduct(Product product){
            try
            {
                var check = await context.Products
                .Where(s => s.ProductName.Equals(product.ProductName)
                && s.ProductDetailDescription.Equals(product.ProductDetailDescription)
                && s.ProductPrice == product.ProductPrice).AnyAsync();

                return check;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(Product product)
        {
            try
            {
                context.Products.Add(product);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(Product product)
        {
            try
            {
                context.Products.Update(product);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public float GetPrice(int productId)
        {
            try
            {
                var price = context.Products
                .Where(p => p.ProductId.Equals(productId))
                .Select(p => p.ProductPrice).FirstOrDefault();

                return price;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
