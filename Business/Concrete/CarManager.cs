﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }
        public IResult Add(Car car)
        {
            if (car.Description.Length >= 2 && car.DailyPrice > 0)
            {
                _carDal.Add(car);
                return new SuccessResult(Messages.EntityAdded);
            }
            else
            {
                return new ErrorResult(Messages.EntityNameInvalid);
            }
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.EntityDeleted);
        }

        public IDataResult<Car> GetById(Car car)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == car.Id));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        public IDataResult<List<Car>> GetCars()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }

        public IDataResult<List<Car>> GetCarsByBrandId(Car car)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == c.BrandId));
        }

        public IDataResult<List<Car>> GetCarsByColorId(Car car)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == car.ColorId));
        }

        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.EntityUpdated);
        }
    }
}
