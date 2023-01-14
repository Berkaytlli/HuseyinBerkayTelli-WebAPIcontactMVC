using AppEnvironment;
using Context;
using Entity.City;
using Entity.CityService;
using ViewModel;

namespace Business.CityBusinessService
{
    public class CityBusinessService : ICityBusinessService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICityRepositoryService _cityRepositoryService;
        public CityBusinessService(ApplicationDbContext dbContext, ICityRepositoryService cityRepositoryService)
        {
            _dbContext = dbContext;
            _cityRepositoryService = cityRepositoryService;
        }

        public Result<City> Create(CityVM model)
        {

            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                City city = new City
                {
                    CityName = model.CityName,
                    CountryName = model.CountryName,


                }; 
                var addCity = _cityRepositoryService.Add(city);
                transaction.Commit();
                return addCity;
            }
            catch (Exception e)
            {

                return new Result<City>(e);
            }
        }
        public Result<City> Edit(int id, CityVM ModelDTO)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var edit = _cityRepositoryService.GetFirst(u => u.Id == id);
                if (!edit.IsSuccess)
                {
                    return new Result<City>(MessageType.RecordNotFound);
                }
                edit.Data.CityName = ModelDTO.CityName;
                edit.Data.CountryName = ModelDTO.CountryName;

                var editCity = _cityRepositoryService.Update(edit.Data);
                transaction.Commit();
                return editCity;


            }
            catch (Exception e)
            {

                return new Result<City>(e);
            }
        }
        public Result Remove(int id)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var remove = _cityRepositoryService.GetFirst(u => u.Id == id);
                if (!remove.IsSuccess)
                {
                    return new Result(remove.MessageType ?? MessageType.RecordNotFound);
                }
                var deleteCity = _cityRepositoryService.Delete(remove.Data);
                if (deleteCity.MessageType != MessageType.DeleteSuccess)
                    return new Result(deleteCity.MessageType ?? MessageType.OperationFailed);
                transaction.Commit();
                return deleteCity;
            }
            catch (Exception e)
            {

                return new Result(e);
            }
        }
        public Result<List<City>> GetAll()
        {
            try
            {
                var getall = _cityRepositoryService.Get(whereCondition: u => u.DeletedAt == null).ToList();
                if (!getall.Any())
                {
                    return new Result<List<City>>(MessageType.RecordNotFound);
                }
                return new Result<List<City>>(getall);
            }
            catch (Exception e)
            {
                return new Result<List<City>>(e);
            }
        }

        public Result<City> GetById(int id)
        {
            try
            {
                var getByCity = _cityRepositoryService.GetFirst(u => u.Id == id);
                if (getByCity == null)
                {
                    return new Result<City>(MessageType.RecordNotFound);
                }
                return getByCity;
            }
            catch (Exception e)
            {

                return new Result<City>(e);
            }
        }

        
    }
} 
