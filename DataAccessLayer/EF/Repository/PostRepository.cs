using DataAccessLayer.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EF.Repository;

public class PostRepository : Repository<Post> , IPostRepository
{
    public PostRepository(DbContext context) : base(context)
    {
    }
}