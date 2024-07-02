using API.Data;
using API.DTOs;
using API.Entities;
using API.Interaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly IMapper _mapper;
    public DataContext _dbcontext { get; }
    public UsersRepository(DataContext context, IMapper mapper)
    {
        _dbcontext = context;
        _mapper = mapper;
    }

   
    public async Task<AppUser> GetUserByIdAsync(int id)
    {
       return await _dbcontext.Users.FindAsync(id);
    }

    public async Task<AppUser> GetUesrByUserNameAsync(string username)
    {
         return await _dbcontext.Users
            .Include(p=> p.Photos)
            .SingleOrDefaultAsync(x=> x.UserName == username);
    }

   
    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await _dbcontext.Users
            .Include(p=> p.Photos)
            .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
       return await _dbcontext.SaveChangesAsync() > 0;
    }

    public void Update(AppUser user)
    {
       _dbcontext.Entry(user).State = EntityState.Modified;
    }

    public async Task<IEnumerable<MemberDTO>> GetMembersAsync()
    {
        return await _dbcontext.Users
            .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<MemberDTO> GetMemberAsync(string username)
    {
        return await _dbcontext.Users
            .Where(x=> x.UserName == username)
            .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider) // this automatically finds his configuration from automapper class
            .SingleOrDefaultAsync();
    }
}
