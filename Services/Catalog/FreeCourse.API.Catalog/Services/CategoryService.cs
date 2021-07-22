using AutoMapper;
using FreeCourse.API.Catalog.Dtos;
using FreeCourse.API.Catalog.Models;
using FreeCourse.API.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.API.Catalog.Services
{
  public class CategoryService : ICategoryService
  {
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
      var client = new MongoClient(databaseSettings.ConnectionString);
      var db = client.GetDatabase(databaseSettings.DatabaseName);

      _categoryCollection = db.GetCollection<Category>(databaseSettings.CategoryCollectionName);
      _mapper = mapper;
    }

    public async Task<Response<IList<CategoryDto>>> GetAllAsync()
    {
      var categories = await _categoryCollection.Find(c => true).ToListAsync();

      var mapped = _mapper.Map<List<CategoryDto>>(categories);

      return Response<IList<CategoryDto>>.Success(mapped, 200);
    }

    public async Task<Response<CategoryDto>> CreateAsync(CategoryDto category)
    {
      var newCategory = _mapper.Map<Category>(category);

      await _categoryCollection.InsertOneAsync(newCategory);

      var mapped = _mapper.Map<CategoryDto>(newCategory);

      return Response<CategoryDto>.Success(mapped, 200);
    }

    public async Task<Response<CategoryDto>> GetByIdAsync(string id)
    {
      var category = await _categoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
      if (category == null)
      {
        return Response<CategoryDto>.Fail("Category not found", 404);
      }

      var mapped = _mapper.Map<CategoryDto>(category);

      return Response<CategoryDto>.Success(mapped, 200);
    }

  }
}
