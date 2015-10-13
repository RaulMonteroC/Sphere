using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Sphere.Core
{
    public static class SphereConfig
    {
        public static DbContext GlobalContext { get; set; }
    }
}
