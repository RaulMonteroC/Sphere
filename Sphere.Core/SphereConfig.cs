using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Sphere.Core
{
    /// <summary>
    /// Configuration class containing Sphere's dependencies.
    /// </summary>
    public static class SphereConfig
    {
        /// <summary>
        /// Global context instance to be used across all repositories.
        /// </summary>
        public static DbContext GlobalContext { get; set; }
    }
}
