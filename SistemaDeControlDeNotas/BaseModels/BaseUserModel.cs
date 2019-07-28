using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDeControlDeNotas.BaseModels
{
    public abstract class BaseUserModel
    {
        public abstract string Email { get; set; }
        public abstract string Password { get; set; }
        public abstract byte UserProfile { get; set; }
    }
}