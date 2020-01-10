using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finance.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CascadeDeleteAttribute : Attribute { }
}