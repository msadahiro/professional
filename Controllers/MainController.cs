using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using network.Models;
namespace network{
    public class MainController : Controller{
        private MyContext _context;
 
        public MainController(MyContext context)
        {
            _context = context;
        }
        [HttpGet]
        [RouteAttribute("myprofile")]
        public IActionResult MyProfile(){
            if(HttpContext.Session.GetInt32("CurrentUser")==null){
                return RedirectToAction("Login","User");
            }
            int? getUserId = HttpContext.Session.GetInt32("CurrentUser");
            User SignInUser = _context.Users.Where(User => User.id == getUserId).SingleOrDefault();
            List <Invitation> AllPendingRequest = _context.Invitations.Where(User => User.InviteeId == getUserId || User.InviterId == getUserId)
            .Include(user => user.Inviter)
            .Include(user => user.Invitee)
            .ToList();
            ViewBag.AllPendingRequests = AllPendingRequest;
            ViewBag.CurrentUser = SignInUser;
            return View();
        }
        [HttpGet]
        [RouteAttribute("users")]
        public IActionResult Users(){
            if(HttpContext.Session.GetInt32("CurrentUser")==null){
                return RedirectToAction("Login","User");
            }
            int? getUserId = HttpContext.Session.GetInt32("CurrentUser");
            User SignInUser = _context.Users.Where(User => User.id == getUserId).SingleOrDefault();
            List<User> GetAllUsers = _context.Users
                .Where(User => User.id != (int)getUserId)
                .Include(User => User.InvitationsReceived)
                .Include(User => User.InvitationsSent)
                .ToList();
            ViewBag.CurrentUser = SignInUser;
            ViewBag.AllUsers = GetAllUsers;
            return View();
        }
        [HttpGet]
        [RouteAttribute("requestConnection/{id}")]
        public IActionResult RequestConnection(int id){
             int? getUserId = HttpContext.Session.GetInt32("CurrentUser");
             Invitation newInvite = new Invitation(){
                InviterId = (int)getUserId,
                InviteeId = id,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Accepted = false
             };
             _context.Add(newInvite);
             _context.SaveChanges();
            return RedirectToAction("users");
        }
        [HttpGet]
        [RouteAttribute("accept/{id}")]
        public IActionResult Accept (int id){
            int? getUserId = HttpContext.Session.GetInt32("CurrentUser");
            Invitation addConnection = _context.Invitations.SingleOrDefault(invite => invite.id == id);
            addConnection.Accepted = true;
            _context.SaveChanges();
            return RedirectToAction("myprofile");
        }
        [HttpGet]
        [RouteAttribute("decline/{id}")]
        public IActionResult Decline (int id){
            Invitation declineConnection = _context.Invitations.SingleOrDefault(invite => invite.id == id);
            _context.Invitations.Remove(declineConnection);
            _context.SaveChanges();
            return RedirectToAction("myprofile");
        }
        [HttpGet]
        [RouteAttribute("users/{id}")]
        public IActionResult ShowOne(int id){
            if(HttpContext.Session.GetInt32("CurrentUser")==null){
                return RedirectToAction("Login","User");
            }
            User profileUser = _context.Users.SingleOrDefault(user => user.id == id);
            ViewBag.Profile = profileUser;
            return View("ShowOne");
        }
    }
}