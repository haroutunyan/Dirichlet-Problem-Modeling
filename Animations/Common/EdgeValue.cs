using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animations.Common
{
    public static class EdgeValue
    {
        public static double GetEdgeValue(double radius, double angle)
        {
            return radius * Math.Cos(angle);
        }
    }
}
