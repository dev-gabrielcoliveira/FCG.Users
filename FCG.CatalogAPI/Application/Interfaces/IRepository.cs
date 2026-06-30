using FGC.CatalogAPI.Application.Interfaces.Base;

namespace FGC.CatalogAPI.Application.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        void Alterar(T Entidade);
        List<T> ObterTodos();
        T? ObterPorId(int id);
        void Cadastrar(T entidade);
        void Deletar(int id);
    }
}
