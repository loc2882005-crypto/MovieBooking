using Microsoft.AspNetCore.Mvc;
using MovieBooking.Application.Movies.DTOs;
using MovieBooking.Application.Movies.Interfaces;

namespace MovieBooking.WebApi.Controllers;

//Controller : tiếp nhận request từ client, gọi IMovieServices để xử lý và trả về kết quả

//quy định đường dẫn : api/movies

[Route("api/[controller]")]
[ApiController] 
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    //API lấy danh sách phim

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var movies = await _movieService.GetAllActiveMoviesAsync();
        return Ok(movies);
    }


    //API lấy phim theo ID : VD : api/movies/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var movie = await _movieService.GetMovieByIdAsync(id);
        if (movie == null)
        {
            return NotFound(new { message = $"Không tìm thấy phim có Id = {id}" });
        }
        return Ok(movie);
    }

    //API tạo phim mới
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMovieDto request)
    {
        var createdMovie = await _movieService.CreateMovieAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = createdMovie.Id }, createdMovie);//trả về HTTP 201
    }
}