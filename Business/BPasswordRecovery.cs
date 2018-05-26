using System;
using SIE.Context;

namespace SIE.Business
{
    public class BPasswordRecovery
    {
        private readonly SIEContext _context;
        public BPasswordRecovery(SIEContext context) => _context = context;

        public void Save(ref PasswordRecovery passwordRecovery)
        {
            passwordRecovery.RequestDate = DateTime.Now;
            passwordRecovery.ExpirationDate = passwordRecovery.RequestDate.AddDays(1);
            passwordRecovery.Active = true;
            passwordRecovery.Token = Guid.NewGuid().ToString();

            _context.PasswordRecovery.Add(passwordRecovery);
            _context.SaveChanges();
        }

    }
}
