using System.Numerics;

namespace Lrras
{
    public static class ComplexExtensions
    {
        // g(c)
        public static bool IsGeometricNegative(this Complex c)
        {
            return c.Real < 0 || c.Imaginary < 0;
        }
        
        // Rot_{270}(c)
        public static Complex Rot270(this Complex c)
        {
            return new Complex(c.Imaginary, -c.Real);
        }

        // \ssqrt(c)
        public static Complex Ssqrt(this Complex c)
        {
            if (c.IsGeometricNegative())
            {
                return -(Complex.Sqrt(Complex.Abs(c)));
            }

            return Complex.Sqrt(c);
        }

        // \sisqrt(c)
        public static Complex Sisqrt(this Complex c)
        {
            if (c.IsGeometricNegative())
            {
                return Complex.Sqrt(Complex.Abs(c)).Rot270();
            }

            return Complex.Sqrt(c);
        }
    }
}
