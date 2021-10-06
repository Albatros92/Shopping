using ShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Interface
{
    interface ICalculator
    {
        double calculateFor(ShoppingCart cart);
    }
}
