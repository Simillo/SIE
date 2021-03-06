﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIE.Context;

namespace SIE.Utils
{
    public class UPasswordRecovery
    {
        private readonly SIEContext _context;
        public UPasswordRecovery(SIEContext context) => _context = context;

        public PasswordRecovery GetUserCurrentActive(int personId) =>
            _context.PasswordRecovery.FirstOrDefault(r => r.Person.Id == personId && r.Active && r.ExpirationDate > DateTime.Now);

        public PasswordRecovery GetByToken(string token) => _context.PasswordRecovery.FirstOrDefault(r => r.Token == token);
        public List<PasswordRecovery> Get() => _context.PasswordRecovery.ToList();
    }
}
