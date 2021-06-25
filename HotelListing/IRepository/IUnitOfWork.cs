using System;
using System.Threading.Tasks;
using HotelListing.Domains;

namespace HotelListing.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Country> Countries { get; }

        IGenericRepository<Hotel> Hotels { get; }

        void Save();
        Task SaveAsync();
    }
}