using System;
using System.Collections.Generic;
using SIE.Utils;

namespace SIE.Models
{
    public class MNewRoom
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Description { get; set; }

        public void ValidRoom(URoom uRoom, ref List<MModelError> errors)
        {
            var roomExisting = uRoom.GetByCode(Code);
            if (string.IsNullOrEmpty(Name))
            {
                errors.Add(new MModelError
                {
                    HasError = true,
                    MessageError = "Obrigatório",
                    Property = "Name"
                });
            }

            if (roomExisting != null)
            {
                errors.Add(new MModelError
                {
                    HasError = true,
                    MessageError = "Já existe uma sala com esse código!",
                    Property = "Code"
                });
            }
        }
    }
}
