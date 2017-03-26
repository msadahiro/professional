using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using network.Models;

namespace network.Controllers
{
    public class UserController : Controller
    {
        private MyContext _context;
 
        public UserController(MyContext context)
        {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [RouteAttribute("register")]
        public IActionResult Register(){
            ViewBag.errors = new List<string>();
            ViewBag.RegEmailError = "";
            return View();
        }
        [HttpGet]
        [RouteAttribute("login")]
        public IActionResult Login(){
            ViewBag.LogError = "";
            return View();
        }
        [HttpGet]
        [RouteAttribute("logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction ("Index");
        }
        [HttpGet]
        [RouteAttribute("dashboard")]
        public IActionResult Dashboard(){
            if(HttpContext.Session.GetInt32("CurrentUser")==null){
                return RedirectToAction("Login","User");
            }
            int? getUserId = HttpContext.Session.GetInt32("CurrentUser");
            User SignInUser = _context.users.Where(User => User.id == getUserId).SingleOrDefault();
            ViewBag.CurrentUser = SignInUser;
            return View();
        }
        [HttpPost]
        [RouteAttribute("create")]
        public IActionResult Create(RegisterViewModel model, User newUser){
            if(ModelState.IsValid){
                User RegCheckEmail = _context.users.Where(User => User.Email == newUser.Email).SingleOrDefault();
                if(RegCheckEmail == null){
                    newUser.CreatedAt = DateTime.Now;
                    newUser.UpdatedAt = DateTime.Now;
                    _context.Add(newUser);
                    _context.SaveChanges();
                    var CurrentUserId = newUser.id;
                    HttpContext.Session.SetInt32("CurrentUser", (int)CurrentUserId);
                    return RedirectToAction ("Dashboard");
                }
                else{
                    ViewBag.RegEmailError = "Email already used";
                }
            }
            else{
                ViewBag.RegEmailError = "";
            }
            ViewBag.errors = ModelState.Values;
            return View("Register");
        }
        [HttpPost]
        [RouteAttribute("login")]
        public IActionResult LoginUser(string Email, string Password, LoginViewModel model){
            if(ModelState.IsValid){
                User SignInUser = _context.users.Where(User => User.Email == Email).SingleOrDefault();
                if(SignInUser != null && Password != null){
                    if(SignInUser.Password == Password){
                        HttpContext.Session.SetInt32("CurrentUser",(int)SignInUser.id);
                        return RedirectToAction("Dashboard");
                    }
                }
            }
            ViewBag.LogError = "Invalid Login";
            return View("Login");
        }
        [HttpGet]
        [RouteAttribute("users")]
        public IActionResult Users(){
            if(HttpContext.Session.GetInt32("CurrentUser")==null){
                return RedirectToAction("Login","User");
            }
            int? getUserId = HttpContext.Session.GetInt32("CurrentUser");
            List<User> AllUsers = _context.users.Where(user => user.id != getUserId).ToList();
            ViewBag.AllUsers = AllUsers;
            return View ();
        }
    }
}
