using AutoMapper;
using FreeCourse.API.Catalog.Dtos;
using FreeCourse.API.Catalog.Models;
using FreeCourse.API.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using Mass = MassTransit;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeCourse.Shared.Messages;

namespace FreeCourse.API.Catalog.Services
{
  public class CourseService : ICourseService
  {
    private readonly IMongoCollection<Course> _courseCollection;
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;
    private readonly Mass.IPublishEndpoint _publishEndpoint;

    public CourseService(IMapper mapper, IDatabaseSettings databaseSettings, Mass.IPublishEndpoint publishEndpoint)
    {
      var client = new MongoClient(databaseSettings.ConnectionString);
      var db = client.GetDatabase(databaseSettings.DatabaseName);

      _courseCollection = db.GetCollection<Course>(databaseSettings.CourseCollectionName);
      _categoryCollection = db.GetCollection<Category>(databaseSettings.CategoryCollectionName);

      _mapper = mapper;

      _publishEndpoint = publishEndpoint;
    }

    public async Task<Response<IList<CourseDto>>> GetAllAsync()
    {
      var categories = await _courseCollection.Find(c => true).ToListAsync();
      if (categories == null || !categories.Any())
      {
        return Response<IList<CourseDto>>.Success(new List<CourseDto>(), 200);
      }

      foreach (var item in categories)
      {
        item.Category = await _categoryCollection.Find(x => x.Id == item.CategoryId).FirstAsync();
      }

      var mapped = _mapper.Map<List<CourseDto>>(categories);

      return Response<IList<CourseDto>>.Success(mapped, 200);
    }

    public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto course)
    {
      var newCourse = _mapper.Map<Course>(course);
      newCourse.CreatedTime = DateTime.UtcNow;

      await _courseCollection.InsertOneAsync(newCourse);

      var mapped = _mapper.Map<CourseDto>(newCourse);

      return Response<CourseDto>.Success(mapped, 200);
    }

    public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto course)
    {
      var newCourse = _mapper.Map<Course>(course);

      var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == newCourse.Id, newCourse);
      if (result == null)
      {
        return Response<NoContent>.Fail("Course not found", 404);
      }

      await _publishEndpoint.Publish(new CourseNameChangedEvent
      {
        CourseId = newCourse.Id,
        UpdatedName = course.Name,
        UserId = newCourse.UserId
      });

      return Response<NoContent>.Success(204);
    }

    public async Task<Response<NoContent>> DeleteAsync(string id)
    {
      var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);
      if (result.DeletedCount < 1)
      {
        return Response<NoContent>.Fail("Course not found", 404);
      }

      return Response<NoContent>.Success(204);
    }

    public async Task<Response<CourseDto>> GetByIdAsync(string id)
    {
      var course = await _courseCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
      if (course == null)
      {
        return Response<CourseDto>.Fail("Course not found", 404);
      }

      course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstAsync();

      var mapped = _mapper.Map<CourseDto>(course);

      return Response<CourseDto>.Success(mapped, 200);
    }

    public async Task<Response<IList<CourseDto>>> GetAllByUserIdAsync(string id)
    {
      var categories = await _courseCollection.Find(x => x.UserId == id).ToListAsync();
      if (categories == null || !categories.Any())
      {
        return Response<IList<CourseDto>>.Success(new List<CourseDto>(), 200);
      }

      foreach (var item in categories)
      {
        item.Category = await _categoryCollection.Find(x => x.Id == item.CategoryId).FirstAsync();
      }

      var mapped = _mapper.Map<List<CourseDto>>(categories);

      return Response<IList<CourseDto>>.Success(mapped, 200);
    }

  }
}
