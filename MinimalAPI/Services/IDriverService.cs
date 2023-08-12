using MinimalAPI.Data;
using MinimalAPI.Models;

namespace MinimalAPI.Services
{
    public interface IDriverService
    {
        public List<Driver> GetDrivers();
        public Driver? GetDriverById(int id);
        public Driver CreateDriver(Driver driver);
        public Driver? UpdateDriver(Driver driver, int id);
        public bool DeleteDriver(int id);
    }
}
