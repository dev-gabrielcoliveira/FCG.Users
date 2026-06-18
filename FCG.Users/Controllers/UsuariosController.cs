using FCG.Users.Application.DTOs;
using FCG.Users.Application.Interfaces.Repository;
using FCG.Users.Application.Services;
using FCG.Users.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FCG.Users.Controllers
{
    /// <summary>
    /// Responsável por gerenciar os usuários da plataforma.
    /// </summary>
    /// <remarks>
    /// Permite criar, atualizar, remover e autenticar usuários.
    /// </remarks>
    [ApiController]
    [Route("/[controller]")]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly UsuarioService _usuarioService;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioRepository usuarioRepository, UsuarioService usuarioService, ILogger<UsuarioController> logger)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;
            _logger = logger;
        }

        /// <summary>
        /// Busca todos usuários.
        /// </summary>
        /// <returns>Listagem de todos usuários ativo do sistema.</returns>
        /// <response code="200">Lista de usuários obtida com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpGet]
        [Authorize(Policy = "Administrador")]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                return Ok(_usuarioService.ObterTodos());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todos usuários");
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }

        /// <summary>
        /// Buscando usuário específico.
        /// </summary>
        /// <returns>Dados de um usuário específico.</returns>
        /// <response code="200">Dados do usuário obtido com sucesso</response>
        /// <response code="404">Usuário não encontrado</response>
        /// <response code="500">Erro interno</response>
        [HttpGet("{id:int}")]
        [Authorize(Policy = "AdministradorOuUsuario")]
        public async Task<IActionResult> ObterPorId([FromRoute, Range(1, int.MaxValue)] int id)
        {
            try
            {
                var usuario = _usuarioService.ObterPorId(id);
               
                if (usuario == null)
                   return NotFound();

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter usuário com Id: {Id}", id);
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="request">Dados do usuário a ser criado.</param>
        /// <returns>Usuário criado.</returns>
        /// <response code="200">Usuário criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost]
        [Authorize(Policy = "Administrador")]
        public async Task<IActionResult> Post([FromBody] UsuarioCriarInput usuarioInput)
        {
            try
            {
                var usuario = await _usuarioService.Criar(usuarioInput);

                // Usei o log com e-mail aqui para identificar qual usuário foi criado.
                _logger.LogInformation("Usuário {Email} foi criado", usuarioInput.Email);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao incluir usuário {Nome}", usuarioInput?.Nome);
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }

        /// <summary>
        /// Atualiza os dados de um usuário existente.
        /// </summary>
        /// <param name="request">Novos dados do usuário.</param>
        /// <returns>Usuário atualizado.</returns>
        /// <response code="200">Usuário atualizado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Usuário não encontrado</response>
        [HttpPut]
        [Authorize(Policy = "Administrador")]
        public async Task<IActionResult> Update([FromBody] UsuarioAtualizarInput usuarioInput)
        {
            try
            {
                var usuario = _usuarioService.ObterPorId(usuarioInput.Id);

                if (usuario == null)
                    return NotFound("Usuário não encontrado");

                try
                {
                    usuario.Nome = usuarioInput.Nome;
                    usuario.Email = usuarioInput.Email;
                    usuario.Senha = usuarioInput.Senha;

                    _usuarioService.Alterar(usuario);
                    _logger.LogInformation("Usuário {Id} foi alterado", usuario.Id);

                    return Ok(usuario);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao alterar usuário com Id: {Id}", usuario?.Id);
                    return StatusCode(500, new { message = "Erro interno no servidor" });
                }

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        /// <summary>
        /// Remove um usuário do sistema.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        /// <returns>Confirmação da remoção.</returns>
        /// <response code="204">Usuário removido com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Usuário não encontrado</response>
        [HttpPatch("{id:int}")]
        [Authorize(Policy = "Administrador")]
        public IActionResult Delete([FromRoute, Range(1, int.MaxValue)] int id)
        {
            try
            {
                var usuario = _usuarioService.ObterPorId(id);

                if (usuario == null)
                    return NotFound("Usuário não encontrado");

                _usuarioService.Excluir(id);

                _logger.LogInformation("Usuário {Id} foi removido", id);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir usuário com Id: {Id}", id);
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }
    }
}
