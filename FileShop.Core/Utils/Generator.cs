using System;
using System.Collections.Generic;
using System.Text;

namespace TopCoderZ.Core.Generator
{
    public class NameGenerator
    {
        public static string GenerateUniqCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}