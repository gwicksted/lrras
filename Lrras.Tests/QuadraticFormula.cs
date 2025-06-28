using System.Numerics;
using NUnit.Framework;

namespace Lrras.Tests
{
    [TestFixture]
    public class QuadraticFormula
    {
        public static (Complex x1, Complex x2) SolveComplexQuadratic(double a, double b, double c)
        {
            if (a == 0)
            {
                if (b == 0)
                {
                    // 0x^2 + 0x + 0 = 0
                    if (c == 0)
                    {
                        return (Complex.Zero, Complex.NaN);
                    }

                    // requires impossible numbers (outside the range of Complex, these require Scalar)
                    return (Complex.NaN, Complex.NaN);
                }

                // Linear case: bx + c = 0
                return (-c / b, Complex.NaN);
            }

            var cA = new Complex(a, 0);
            var cB = new Complex(b, 0);
            var cC = new Complex(c, 0);

            var discriminant = cB * cB - 4 * cA * cC;
            var sqrtDisc = Complex.Sqrt(discriminant);

            var x1 = (-cB + sqrtDisc) / (2 * cA);
            var x2 = (-cB - sqrtDisc) / (2 * cA);

            // returns two potential solutions because sqrt is +/-
            return (x1, x2);
        }
        
        public static Complex SolveQuadraticSisqrt(double a, double b, double c)
        {
            if (a == 0)
            {
                if (b == 0)
                {
                    // 0x^2 + 0x + 0 = 0
                    if (c == 0)
                    {
                        return Scalar.Zero;
                    }

                    // 0x^2 + 0x + 10 = 0

                    // requires impossible numbers
                    return Complex.NaN;
                }
                
                // Linear case: bx + c = 0
                return Scalar.Create(-c / b);
            }
            
            var discriminant = new Complex(b * b - 4 * a * c, 0);

            // returns a single solution because Sisqrt returns the correct sign
            return (-b + discriminant.Sisqrt()) / (2 * a);
        }
        
        
        public static Scalar SolveQuadraticSisqrtImpossible(double a, double b, double c)
        {
            if (a == 0)
            {
                if (b == 0)
                {
                    // 0x^2 + 0x + 0 = 0
                    if (c == 0)
                    {
                        return Scalar.Zero;
                    }

                    // 0x^2 + 0x + 10 = 0

                    var impossibleDiscriminant = new Complex(1 * 1 - 4 * 1 * c, 0);
                    var impossibleResult = (-1 + impossibleDiscriminant.Sisqrt()) / 2;
                    // infinity space to cause it to escape back into reality once multiplied by zero (because a, b are 0)
                    return Scalar.Create(impossibleResult, 2);
                }
                
                // Linear case: bx + c = 0
                return Scalar.Create(-c / b);
            }
            
            var discriminant = new Complex(b * b - 4 * a * c, 0);

            // Console.WriteLine("(better) (-b + sqrt(b * b - 4 * a * c)) / (2 * a)");
            // Console.WriteLine($"(better) = ({-b} + sqrt({b * b} - 4 * {a} * {c})) / (2 * {a})");
            // Console.WriteLine($"(better) = ({-b} + sqrt({b * b} - {4 * a * c})) / ({2 * a})");
            // Console.WriteLine($"(better) = ({-b} + sqrt({b * b - 4 * a * c})) / ({2 * a})");
            // Console.WriteLine($"(better) = ({-b} + {discriminant.Sisqrt()}) / ({2 * a})");
            // Console.WriteLine($"(better) = ({-b + (discriminant.Sisqrt())}) / ({2 * a})");
            // Console.WriteLine($"(better) = {(-b + (discriminant.Sisqrt())) / (2 * a)}");
            
            return Scalar.Create((-b + discriminant.Sisqrt()) / (2 * a));
        }


        [Test]
        public void SolveQuadraticInComplexSpace()
        {
            for (var a = -20; a < 20; a++)
            {
                for (var b = -20; b < 20; b++)
                {
                    for (var c = -20; c < 20; c++)
                    {
                        var traditional = SolveComplexQuadratic(a, b, c);
                        var sic = SolveQuadraticSisqrt(a, b, c);
                        var sici = SolveQuadraticSisqrtImpossible(a, b, c);

                        if (a == 0 && b == 0 && c != 0)
                        {
                            Assert.That(sici.I, Is.EqualTo(2));
                            
                            var s = a * Scalar.Pow(sici, 2) + b * sici + c;

                            if (s != Scalar.Zero && Math.Abs(s.V.Real) < 0.0000000000001 && s.V.Imaginary < 0.0000000000001)
                            {
                                // close enough
                            }
                            else
                            {
                                Assert.That(s, Is.EqualTo(Scalar.Zero),
                                    $"{a}x^2 + {b}x + {c} : x = {sici.ToString()} : {a * Scalar.Pow(sici, 2)} + {b * sici} + {c} = 0");
                            }
                        }
                        else
                        {
                            Assert.That(sic == traditional.x1 || sic == traditional.x2 || (Complex.IsNaN(sic) && Complex.IsNaN(traditional.x1)), Is.True, $"{a}x^2 + {b}x + {c} : {sic.ToString()} == {traditional.x1} || {traditional.x2}");

                            if (sici != sic)
                            {
                                Assert.That(sici.I, Is.EqualTo(1), $"{a}x^2 + {b}x + {c} : {sici.ToString()} == {sic.ToString()}");
                                Assert.That(sici.V, Is.EqualTo(sic), $"{a}x^2 + {b}x + {c} : {sici.ToString()} == {sic.ToString()}");
                            }
                        }
                    }
                }
            }
        }
    }
}
