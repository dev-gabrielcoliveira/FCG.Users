using FCG.CatalogAPI.Application.DTOs;
using FCG.CatalogAPI.Domain.Entities;

namespace FCG.CatalogAPI.Application.Interfaces.Service
{
    public interface IJogoService
    {
        IEnumerable<Jogo> ObterTodos(); 
        Jogo? ObterPorId(int id);       
        Jogo Criar(JogoCriarInput input);
        void Atualizar(Jogo jogo);
        void Excluir(int id);
    }
}
