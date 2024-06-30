using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;

public class BuggyController : BaseApiController
{
  private readonly DataContext _context;
  public BuggyController(DataContext context)
  {
        _context = context;
  }

   
   [Authorize]
   [HttpGet("auth")]
   public ActionResult<String> GetSecret()
   {
      return "Secret text";
   }

   [HttpGet("not-found")]
   public ActionResult<AppUser> GetNotFound()
   {
      var obj = _context.Users.Find(-1);
      if(obj == null) return NotFound();
      return obj;

   }

   [HttpGet("server-error")]
   public ActionResult<String> GetServerError()
   {
    //try{
       var obj = _context.Users.Find(-1);
       var myreturn = obj.ToString();
       return myreturn;
   // }
   // catch(Exception ex)
    //{
     // return StatusCode(500,"Computer Says No" + ex.Message);
    //}

   }

   [HttpGet("bad-request")]
   public ActionResult<String> GetBadRequest()
   {
      return BadRequest("this was not good request");
   }


}
