using fleetsystem.entity;

namespace fleetsystem.repository;

public interface IDriverRepository
{
    Task<List<Driver>> GetAllDriversAsync();
    Task<Driver?> GetDriverByIdAsync(int id);
    Task<Driver?> AddDriverAsync(Driver driver);
    Task<Driver?> UpdateDriverAsync(Driver driver);
    Task DeleteDriverAsync(int id);
}
