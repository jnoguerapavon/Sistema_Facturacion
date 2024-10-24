using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentaAngular.DTOs;
using SistemaVentaAngular.Models;
using SistemaVentaAngular.Repository.Contratos;
using SistemaVentaAngular.Repository.Implementacion;
using SistemaVentaAngular.Utilidades;

namespace SistemaVentaAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoriaRepositorio _categoriaRepositorio;
        public CategoriaController(ICategoriaRepositorio categoriaRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _categoriaRepositorio = categoriaRepositorio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            Response<List<CategoriaDTO>> _response = new Response<List<CategoriaDTO>>();

            try
            {
                List<CategoriaDTO> _listaCategorias = new List<CategoriaDTO>();
                _listaCategorias = _mapper.Map<List<CategoriaDTO>>(await _categoriaRepositorio.Lista());

                if (_listaCategorias.Count > 0)
                    _response = new Response<List<CategoriaDTO>>() { status = true, msg = "ok", value = _listaCategorias };
                else
                    _response = new Response<List<CategoriaDTO>>() { status = false, msg = "sin resultados", value = null };


                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                _response = new Response<List<CategoriaDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }


        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] CategoriaDTO request)
        {
            Response<CategoriaDTO> _response = new Response<CategoriaDTO>();
            try
            {
                Categoria _categoria = _mapper.Map<Categoria>(request);

                Categoria _categoriaCreado = await _categoriaRepositorio.Crear(_categoria);

                if (_categoriaCreado.IdCategoria != 0)
                    _response = new Response<CategoriaDTO>() { status = true, msg = "ok", value = _mapper.Map<CategoriaDTO>(_categoriaCreado) };
                else
                    _response = new Response<CategoriaDTO>() { status = false, msg = "No se pudo crear la categoria" };

                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                _response = new Response<CategoriaDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] CategoriaDTO request)
        {
            Response<CategoriaDTO> _response = new Response<CategoriaDTO>();
            try
            {
                Categoria _categoria = _mapper.Map<Categoria>(request);
                Categoria _categoriaParaEditar = await _categoriaRepositorio.Obtener(u => u.IdCategoria == _categoria.IdCategoria);

                if (_categoriaParaEditar != null)
                {

                    _categoriaParaEditar.Descripcion = _categoria.Descripcion;
                    _categoriaParaEditar.EsActivo = _categoria.EsActivo;

                    bool respuesta = await _categoriaRepositorio.Editar(_categoriaParaEditar);

                    if (respuesta)
                        _response = new Response<CategoriaDTO>() { status = true, msg = "ok", value = _mapper.Map<CategoriaDTO>(_categoriaParaEditar) };
                    else
                        _response = new Response<CategoriaDTO>() { status = false, msg = "No se pudo editar la categoria" };
                }
                else
                {
                    _response = new Response<CategoriaDTO>() { status = false, msg = "No se encontró la categoria" };
                }

                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                _response = new Response<CategoriaDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }



        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            Response<string> _response = new Response<string>();
            try
            {
                Categoria _categoriaEliminar = await _categoriaRepositorio.Obtener(u => u.IdCategoria == id);
                if (_categoriaEliminar != null)
                {

                    bool respuesta = await _categoriaRepositorio.Eliminar(_categoriaEliminar);

                    if (respuesta)
                        _response = new Response<string>() { status = true, msg = "ok", value = "" };
                    else
                        _response = new Response<string>() { status = false, msg = "No se pudo eliminar la categoria", value = "" };
                }

                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                _response = new Response<string>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }





    }
}
