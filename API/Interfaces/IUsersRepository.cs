using API.DTOs;
using API.Entities;

namespace API.Interaces;

public interface IUsersRepository
{
  void Update(AppUser user);
  Task<bool> SaveAllAsync();
  Task<IEnumerable<AppUser>> GetUsersAsync();
  Task<AppUser> GetUserByIdAsync(int id);
  Task<AppUser> GetUesrByUserNameAsync(String username);
  Task<IEnumerable<MemberDTO>> GetMembersAsync();
  Task<MemberDTO> GetMemberAsync(string username);




}
