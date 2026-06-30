using FGC.CatalogAPI.Application.Interfaces;
using FGC.CatalogAPI.Application.Interfaces.Base;
using FGC.CatalogAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FGC.CatalogAPI.Infrastructure.Repositories.EF
{
    public class EFRepository<T>: IRepository<T> where T : EntityBase
    {
        protected ApplicationDbContext _context { get; set; }
        protected DbSet<T> _dbSet;

        public EFRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        void IRepository<T>.Alterar(T Entidade)
        {
            throw new NotImplementedException();
        }

        List<T> IRepository<T>.ObterTodos()
        {
            throw new NotImplementedException();
        }

        T IRepository<T>.ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

        void IRepository<T>.Cadastrar(T entidade)
        {
            throw new NotImplementedException();
        }

        void IRepository<T>.Deletar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
