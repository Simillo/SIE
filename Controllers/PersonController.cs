﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIE.Context;

namespace SIE.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly SIEContext _context;
        public PersonController(SIEContext context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        public dynamic Get()
        {
            return _context.Person;
        }
    }
}
