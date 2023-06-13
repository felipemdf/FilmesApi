using FilmesApi.Models;
using FilmesApi.Dtos;
using FilmesApi.Data;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Controllers;


[ApiController]
[Route("[controller]")]
public class CinemaController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public CinemaController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaCinema(
        [FromBody] CreateCinemaDto cinemaDto)
    {
        Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
        _context.Cinemas.Add(cinema);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaCinemaPorId),
            new { id = cinema.Id },
            cinema);
    }


    [HttpGet]
    [ProducesResponseType(200)]
    public IEnumerable<ReadCinemaDto> RecuperaCinemas([FromQuery] int? enderecoId = null)
    {
        if (enderecoId == null)
        {
            return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.ToList());
        }
        return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.FromSqlRaw($"SELECT Id, Nome, EnderecoId FROM cinemas where cinemas.EnderecoId = {enderecoId}").ToList());
    }


    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public IActionResult RecuperaCinemaPorId(int id)
    {
        var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

        if (cinema == null)
            return NotFound();

        var cinemaDto = _mapper.Map<Cinema, ReadCinemaDto>(cinema);

        return Ok(cinemaDto);
    }


    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
    {

        var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

        if (cinema == null)
        {
            return NotFound();
        }

        _mapper.Map(cinemaDto, cinema);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeletaCinema(int id)
    {
        var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

        if (cinema == null)
            return NotFound();

        _context.Remove(cinema);
        _context.SaveChanges();

        return NoContent();
    }
}
