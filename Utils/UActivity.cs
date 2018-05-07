﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIE.Context;

namespace SIE.Utils
{
    public class UActivity
    {
        private readonly SIEContext _context;

        public UActivity(SIEContext context) => _context = context;

        public Activity GetById(int id) => _context.Activity.Find(id);
    }
}