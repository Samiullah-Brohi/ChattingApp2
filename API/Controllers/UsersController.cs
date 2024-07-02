using System.Collections.Generic;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UsersController : BaseApiController 
{
  private readonly IUsersRepository _userRepository;
  private readonly IMapper _mapper;

  public UsersController(IUsersRepository repository, IMapper mapper)
  {
        _userRepository = repository;
        _mapper = mapper;
  }
  
  [HttpGet]
  public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
  {
        return Ok(await _userRepository.GetMembersAsync());
     // return await _userRepository.GetMembersAsync(); this line makes error due to actionresult so we should use OK()

    //  var users = await _userRepository.GetUsersAsync();
    //  var usersToReturn = _mapper.Map<IEnumerable<MemberDTO>>(users); 
    //  mapper logic is shifted in repository using a second way
    //  return Ok(usersToReturn);
  }

  
  [HttpGet("{username}")]
  public async Task<ActionResult<MemberDTO>> GetUser(string username)
  {

      return await _userRepository.GetMemberAsync(username);

   // var user =  await _userRepository.GetUesrByUserNameAsync(username);
   // return _mapper.Map<MemberDTO>(user);
   // we took this mapping logic to Repository


   // var user = await _repository.GetUesrByUserName(username);
   // if(user == null) return NotFound();
   // return user;
  }



}
