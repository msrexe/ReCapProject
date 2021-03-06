﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentCarDbContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter=null)
        {
            using (RentCarDbContext context = new RentCarDbContext())
            {
                var result = from cars in context.Cars
                             join colors in context.Colors
                             on cars.ColorId equals colors.Id
                             join brands in context.Brands
                             on cars.BrandId equals brands.Id
                             select new CarDetailDto
                             {
                                 CarId = cars.Id,
                                 BrandId = brands.Id,
                                 ColorId = colors.Id,
                                 BrandName = brands.Name,
                                 Description = cars.Description,
                                 ColorName = colors.Name,
                                 ModelYear = cars.ModelYear,
                                 DailyPrice = cars.DailyPrice
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}
