using DataAccessLayer.EF.Entities;

namespace DataAccessLayer.EF.Repository;

public interface IUserRepository
{
    Task<User> FindByUsername(string username);
    
}