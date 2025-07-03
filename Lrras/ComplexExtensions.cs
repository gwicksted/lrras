using System.Numerics;
using System.Text;

namespace Lrras
{
    public static class ComplexExtensions
    {
        // g(c)
        public static bool IsGeometricNegative(this Complex c)
        {
            return c.Real < 0 || c.Imaginary < 0;
        }
        
        /// <summary>
        /// Convert Complex number to a descriptive string.
        /// </summary>
        /// <returns>A string in the format: Re(c)+/-Im(c)i</returns>
        public static string ToDescriptiveString(this Complex c)
        {
            var sb = new StringBuilder();

            if (c.Real != 0)
            {
                sb.Append(c.Real);
            }

            if (c.Imaginary > 0)
            {
                if (sb.Length > 0)
                {
                    sb.Append('+');
                }

                sb.Append(c.Imaginary);

                sb.Append('i');
            }
            else if (c.Imaginary < 0)
            {
                sb.Append(c.Imaginary);
                sb.Append('i');
            }
            
            if (sb.Length == 0)
            {
                sb.Append('0');
            }

            return sb.ToString();
        }
    }
}
