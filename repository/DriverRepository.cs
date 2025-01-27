using fleetsystem.entity;
using fleetsystem.config;
using Microsoft.EntityFrameworkCore;

namespace fleetsystem.repository;

public class DriverRepository : IDriverRepository
{
    private readonly TruckDbContext _context;

    public DriverRepository(TruckDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<List<Driver>> GetAllDriversAsync()
    {
        return await _context.Drivers.ToListAsync();
    }

    public async Task<Driver?> GetDriverByIdAsync(int id)
    {
        return await _context.Drivers.FindAsync(id);
    }

    public async Task<Driver?> AddDriverAsync(Driver driver)
    {
        _context.Drivers.Add(driver);
        await _context.SaveChangesAsync();
        return driver;
    }

    public async Task<Driver?> UpdateDriverAsync(Driver driver)
    {
        // _context.Drivers.Update(driver);
        // await _context.SaveChangesAsync();

        var existingDriver = await _context.Drivers.FindAsync(driver.Id);
        if (existingDriver == null) return null;

        existingDriver.Name = driver.Name;
        existingDriver.LicenseNumber = driver.LicenseNumber;
        existingDriver.ContactInformation = driver.ContactInformation;

        await _context.SaveChangesAsync();
        return existingDriver;
    }

    public async Task DeleteDriverAsync(int id)
    {
        var driver = await GetDriverByIdAsync(id);
        if (driver != null)
        {
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
        }
    }
}
