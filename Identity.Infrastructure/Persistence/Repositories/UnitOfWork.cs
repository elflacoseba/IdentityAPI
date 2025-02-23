using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Identity.Infrastructure.Persistence.Context;
using Identity.Infrastructure.Models;
using Identity.Domain.Interfaces;

namespace Identity.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue;
        private readonly ApplicationDbContext _context;        
        private readonly IMapper _mapper;        

        public UnitOfWork(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public async Task CommitTransactionAsync()
        {            
            await _context.Database.CommitTransactionAsync();
        }

        public async void RollbackTransaction()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();
        }
        
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Eliminar el estado administrado (objetos administrados)
                    _context.Dispose();
                }

                // Liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
                // Establecer los campos grandes como NULL
                disposedValue = true;
            }
        }

        // // Reemplazar el finalizador solo si "Dispose(bool disposing)" tiene código para liberar los recursos no administrados
        // ~UnitOfWork()

#pragma warning disable S125 // Sections of code should not be commented out
                            // {
                            //     // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
                            //     Dispose(disposing: false);
                            // }

        public void Dispose()
#pragma warning restore S125 // Sections of code should not be commented out
        {
            // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
