using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TallerIdentity.Application.Interfaces.Persistence;
using TallerIdentity.Domain.Entities;
using TallerIdentity.Infrastructure.Persistence.Context;

namespace TallerIdentity.Infrastructure.Persistence.Repositories;

public class UserRepository(ApplicationDbContext context) : GenericRepository<User>(context), IUserRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<User> UserByEmailAsync(string email)
    {
        var user = await _context.Users
             .AsNoTracking()
             .FirstOrDefaultAsync(u => u.Email == email && u.State == "1");

        return user!;
    }

    public async Task<User?> UserByEmailAsyncDapper(string email)
    {
        using var connection = _context.CreateConnection();

        var query = "select * from public.\"Users\" where \"Email\" = @Email";
        var parameters = new DynamicParameters();
        parameters.Add("Email", email);

        return await connection.QueryFirstOrDefaultAsync<User>(query, param: parameters, commandType: CommandType.Text);
    }
}
