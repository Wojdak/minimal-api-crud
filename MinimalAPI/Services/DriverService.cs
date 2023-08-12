using MinimalAPI.Data;
using MinimalAPI.Models;

namespace MinimalAPI.Services
{
    public class DriverService : IDriverService
    {
        private DataContext _context;

        public DriverService(DataContext context)
        {
            _context = context;
        }

        public Driver? GetDriverById(int id)
        {
            return _context.Drivers.FirstOrDefault(d => d.Id == id);
        }

        public List<Driver> GetDrivers()
        {
            return _context.Drivers.ToList();
        }

        public Driver CreateDriver(Driver driver)
        {
            _context.Drivers.Add(driver);
            _context.SaveChanges();
            return driver;
        }

        public Driver? UpdateDriver(Driver driver, int id)
        {

            var driver_to_edit = _context.Drivers.FirstOrDefault(d => d.Id == id);

            if (driver_to_edit is not null)
            {
                driver_to_edit.Name = driver.Name;
                driver_to_edit.Nationality = driver.Nationality;
                driver_to_edit.RacingNumber = driver.RacingNumber;
                driver_to_edit.Team = driver.Team;
            }

            _context.SaveChanges();

            return driver_to_edit;
        }

        public bool DeleteDriver(int id)
        {
            var driver_to_delete = _context.Drivers.FirstOrDefault(d => d.Id == id);
            
            if(driver_to_delete is null)
                return false;

            _context.Remove(driver_to_delete);
            _context.SaveChanges();

            return true;
        }
    }
}
