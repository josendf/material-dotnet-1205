using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Sandbox
{
    /// <summary>
    /// Representa un producto
    /// </summary>
    class Product
    {
        public string Code { get; private set; }

        public Product(string code)
        {
            this.Code = code;
        }

        public override string ToString()
        {
            return Code;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Product))
                return false;
            Product other = (Product)obj;
            return Code == other.Code;
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }
    }


}
