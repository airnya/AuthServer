using AuthServer.Models;
using AuthServer.Repo;
using AuthServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Controllers
{
    [ApiController]
    [Route( "[controller]" )]
    public class CloseController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public CloseController( IUserRepository userRepository )
        {
            _userRepository = userRepository;
        }
    }
}
