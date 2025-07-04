using System.Numerics;
using System.Text;

namespace Lrras
{
    /// <summary>
    /// LLRAS Scalar
    /// </summary>
    public readonly struct Scalar : IEquatable<Scalar>
    {
        /// <summary>
        /// Residue or value
        /// Cannot equal zero
        /// </summary>
        public readonly Complex V;

        /// <summary>
        /// Spacial index:
        /// -1 = -infinity with non-zero residue
        ///  0 = zero with non-zero residue
        ///  1 = real-space with non-zero value
        ///  2 = +infinity with non-zero residue
        /// </summary>
        public readonly int I;

        public static readonly Scalar Zero = new(1, 0);
        public static readonly Scalar One = new(1);
        public static readonly Scalar Infinity = new(1, 2);
        public static readonly Scalar NegativeInfinity = new(1, -1);

        private Scalar(Complex v, int i = 1)
        {
            V = v != 0 ? v : throw new ArgumentException("Cannot have a zero-valued LRRAS scalar; use i = 0 instead.");
            I = i is >= -1 and <= 2 ? i : throw new ArgumentOutOfRangeException(nameof(i), i, "Vector space index {i} not recognized");
        }
        
        /// <summary>
        /// Convert Scalar to a string.
        /// </summary>
        /// <returns>A string in the format: (v, i)</returns>
        public override string ToString()
        {
            var val = V.ToDescriptiveString();

            return $"({val}, {I})";
        }
        
        /// <summary>
        /// f^-1(s)
        /// </summary>
        /// <returns></returns>
        public Complex Evaluate()
        {
            if (I == 0)
            {
                return 0;
            }

            if (I == 1)
            {
                return V;
            }

            if (I > 1)
            {
                return Complex.Infinity;
            }

            return -Complex.Infinity;
        }
        
        /// <summary>
        /// Perform the log_<paramref name="e"/> of <paramref name="s"/>.
        /// </summary>
        /// <param name="s"><see cref="Scalar"/> radicand.</param>
        /// <param name="e"><see cref="double"/> base of the logarithm.</param>
        /// <returns>A new <see cref="Scalar"/> with the value of log_<paramref name="e"/>(<paramref name="s.V"/>) preserving <paramref name="s.I"/>.</returns>
        public static Scalar Log(Scalar s, double e)
        {
            return PostProcess(Complex.Log(s.V, e), s.I);
        }

        /// <summary>
        /// Perform the <paramref name="n"/>th root of <paramref name="s"/>.
        /// </summary>
        /// <param name="s"><see cref="Scalar"/> radicand.</param>
        /// <param name="n"><see cref="double"/> degree of the root.</param>
        /// <returns>A new <see cref="Scalar"/> with the value taken to the reciprocal of the <paramref name="n"/>th power. Or (1, 1) if <paramref name="n"/> == 0.</returns>
        public static Scalar Sqrt(Scalar s, double n)
        {
            if (n == 0)
            {
                return One;
            }

            return Pow(s, 1d / n);
        }
        
        /// <summary>
        /// Take <paramref name="s"/> to the <paramref name="n"/>th power.
        /// </summary>
        /// <param name="s"><see cref="Scalar"/> base.</param>
        /// <param name="n"><see cref="double"/> exponent.</param>
        /// <returns>A new <see cref="Scalar"/> with the value taken to the <paramref name="n"/>th power. Or (1, 1) if <paramref name="n"/> == 0.</returns>
        public static Scalar Pow(Scalar s, double n)
        {
            if (n == 0)
            {
                return One;
            }

            if (s.V.Imaginary == 0)
            {
                var r = Math.Pow(s.V.Real, n);

                return Create(r, s.I);
            }

            var c = Complex.Pow(s.V, n);

            return Create(c, s.I);
        }

        /// <summary>
        /// Computes the absolute value of the value or residue of <paramref name="s"/>.
        /// </summary>
        /// <param name="s">The <see cref="Scalar"/> to perform the absolute value calculation on.</param>
        /// <returns>A new <see cref="Scalar"/> with the absolute value of <paramref name="s.V"/> and index of <paramref name="s.I"/>.</returns>
        public static Scalar Abs(Scalar s)
        {
            return PostProcess(Complex.Abs(s.V), s.I);
        }

        /// <summary>
        /// Perform <see cref="Scalar"/> addition of <paramref name="a"/> and <paramref name="b"/>.
        /// </summary>
        /// <param name="a"><see cref="Scalar"/> value being added to.</param>
        /// <param name="b"><see cref="Scalar"/> value to add to <paramref name="a"/>.</param>
        /// <returns>A new <see cref="Scalar"/> with the sum of <paramref name="a"/> and <paramref name="b"/>.</returns>
        public static Scalar operator +(Scalar a, Scalar b)
        {
            if (a.I == 0 && b.I != 0)
            {
                return b;
            }

            if (b.I == 0 && a.I != 0)
            {
                return a;
            }

            if (b.V.IsGeometricNegative())
            {
                // perform subtraction instead
                return PostProcess(a.V - -b.V, IndexSubtract(a.I, b.I));
            }

            return PostProcess(a.V + b.V, IndexAdd(a.I, b.I));
        }
        
        /// <summary>
        /// Perform <see cref="Scalar"/> subtraction of <paramref name="b"/> from <paramref name="a"/>.
        /// </summary>
        /// <param name="a"><see cref="Scalar"/> value being subtracted from.</param>
        /// <param name="b"><see cref="Scalar"/> value subtracted from <paramref name="a"/>.</param>
        /// <returns>A new <see cref="Scalar"/> with the result of the subtraction.</returns>
        public static Scalar operator -(Scalar a, Scalar b)
        {
            if (b.I == 0 && a.I == 1)
            {
                return a;
            }
            
            if (a.I == 0 && b.I == 1)
            {
                return PostProcess(-b.V, 1);
            }

            var r = a.V - b.V;
            
            if (r == 0)
            {
                if (a.I == b.I)
                {
                    return Zero;
                }

                r = -1;
            }

            var i = IndexSubtract(a.I, b.I);

            return PostProcess(r, i);
        }
        
        /// <summary>
        /// Perform <see cref="Scalar"/> negation on the value or residue of <paramref name="s"/>.
        /// </summary>
        /// <param name="s"><see cref="Scalar"/> with value to negate.</param>
        /// <returns>A new <see cref="Scalar"/> with the negated value.</returns>
        public static Scalar operator -(Scalar s)
        {
            return Zero - s;
        }
        
        /// <summary>
        /// Perform <see cref="Scalar"/> multiplication on <paramref name="a"/> over <paramref name="b"/>.
        /// </summary>
        /// <param name="a"><see cref="Scalar"/> factor.</param>
        /// <param name="b"><see cref="Scalar"/> factor.</param>
        /// <returns>A new <see cref="Scalar"/> with the product of multiplication.</returns>
        public static Scalar operator *(Scalar a, Scalar b)
        {
            return PostProcess(a.V * b.V, IndexMultiply(a.I, b.I));
        }
        
        /// <summary>
        /// Perform <see cref="Scalar"/> division on <paramref name="a"/> over <paramref name="b"/>.
        /// </summary>
        /// <param name="a"><see cref="Scalar"/> numerator.</param>
        /// <param name="b"><see cref="Scalar"/> denominator.</param>
        /// <returns>A new <see cref="Scalar"/> with the result of the division.</returns>
        public static Scalar operator /(Scalar a, Scalar b)
        {
            return PostProcess(a.V / b.V, IndexDivide(a.I, b.I));
        }
        
        /// <summary>
        /// Perform <see cref="Scalar"/> equality checks on <paramref name="a"/> and <paramref name="b"/>.
        /// </summary>
        /// <param name="a"><see cref="Scalar"/> value to compare against <paramref name="b"/>.</param>
        /// <param name="b"><see cref="Scalar"/> value to compare against <paramref name="a"/>.</param>
        /// <returns>True if space indices and values exactly match; otherwise, false.</returns>
        public static bool operator ==(Scalar a, Scalar b)
        {
            return a.I == b.I && a.V == b.V;
        }
        
        /// <summary>
        /// Perform <see cref="Scalar"/> equality checks on <paramref name="a"/> and <paramref name="b"/>.
        /// </summary>
        /// <param name="a"><see cref="Scalar"/> value to compare against <paramref name="b"/>.</param>
        /// <param name="b"><see cref="Scalar"/> value to compare against <paramref name="a"/>.</param>
        /// <returns>True if space indices do not match or values do not match; otherwise, false.</returns>
        public static bool operator !=(Scalar a, Scalar b)
        {
            return a.I != b.I || a.V != b.V;
        }
        
        /// <summary>
        /// Perform <see cref="Complex"/> equality checks on <paramref name="s"/> and <paramref name="c"/>.
        /// </summary>
        /// <param name="s"><see cref="Scalar"/> value to evaluate.</param>
        /// <param name="c"><see cref="Complex"/> value to compare against <paramref name="s"/>.</param>
        /// <returns>True if f^-1(s) == c; otherwise, false.</returns>
        public static bool operator ==(Scalar s, Complex c)
        {
            return s.Evaluate() == c;
        }
        
        /// <summary>
        /// Perform <see cref="Complex"/> equality checks on <paramref name="s"/> and <paramref name="c"/>.
        /// </summary>
        /// <param name="s"><see cref="Scalar"/> value to evaluate.</param>
        /// <param name="c"><see cref="Complex"/> value to compare against <paramref name="s"/>.</param>
        /// <returns>True if f^-1(s) != c; otherwise, false.</returns>
        public static bool operator !=(Scalar s, Complex c)
        {
            return s.Evaluate() != c;
        }
        
        /// <summary>
        /// Implicit conversion from <see cref="Scalar"/> to <see cref="int"/>.
        /// </summary>
        /// <param name="s"><see cref="Scalar"/> to evaluate and cast to an integer.</param>
        public static implicit operator int(Scalar s)
        {
            return (int)s.Evaluate().Real;
        }
        
        /// <summary>
        /// Implicit conversion from <see cref="int"/> to <see cref="Scalar"/>.
        /// </summary>
        /// <param name="i">Value to convert.</param>
        public static implicit operator Scalar(int i)
        {
            return Create(i);
        }

        /// <summary>
        /// Implicit conversion from <see cref="double"/> to <see cref="Scalar"/>.
        /// </summary>
        /// <param name="d">Value to convert.</param>
        public static implicit operator Scalar(double d)
        {
            return Create(d);
        }

        /// <summary>
        /// Implicit conversion from <see cref="Scalar"/> to <see cref="double"/>.
        /// </summary>
        /// <param name="s">An LRRAS <see cref="Scalar"/>.</param>
        public static implicit operator double(Scalar s)
        {
            return s.Evaluate().Real;
        }

        /// <summary>
        /// Implicit conversion from <see cref="Scalar"/> to <see cref="Complex"/>.
        /// </summary>
        /// <param name="s">An LRRAS <see cref="Scalar"/>.</param>
        public static implicit operator Complex(Scalar s)
        {
            return s.Evaluate();
        }

        /// <summary>
        /// f(c)
        /// </summary>
        /// <param name="c">Value or residue.</param>
        /// <param name="i">Space index (defaults to reality).</param>
        /// <returns>A new LRRAS-compatible <see cref="Scalar"/>.</returns>
        public static Scalar Create(Complex c, int i = 1)
        {
            return PostProcess(c, i);
        }
        
        public bool Equals(Scalar other)
        {
            return I == other.I && V.Equals(other.V);
        }

        public override bool Equals(object? obj)
        {
            return obj is Scalar other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(V, I);
        }
        
        /// <summary>
        /// p(v, i)
        /// </summary>
        /// <param name="v">Value or residue.</param>
        /// <param name="i">Spacial index.</param>
        /// <returns>A <see cref="Scalar"/> conforming to the rules of LRRAS.</returns>
        internal static Scalar PostProcess(Complex v, int i)
        {
            if (v == 0)
            {
                return i switch
                {
                    0 => new Scalar(1, 0),
                    > 0 => new Scalar(1, IndexClamp(i - 1)),
                    < 0 => new Scalar(1, IndexClamp(i + 1))
                };
            }

            if (Complex.IsPositiveInfinity(v) || v == Complex.Infinity)
            {
                return new Scalar(1, IndexClamp(i + 1));
            }
            
            if (Complex.IsNegativeInfinity(v) || v == -Complex.Infinity)
            {
                return i switch
                {
                    1 => NegativeInfinity, 
                    _ => new Scalar(1, IndexClamp(i - 1))
                };
            }

            if (Complex.IsNaN(v))
            {
                throw new ArgumentException("Cannot convert NaN to an LRRAS Scalar");
            }
            
            return new Scalar(v, i);
        }
        
        /// <summary>
        /// m(i)
        /// </summary>
        /// <param name="i">Space index</param>
        /// <returns>Order of infinity of the spacial index</returns>
        internal static int IndexOrder(int i)
        {
            return i switch
            {
                -1 => -1,
                2 => 1,
                _ => 0
            };
        }

        /// <summary>
        /// mu_clamp(i)
        /// </summary>
        /// <param name="i">Spacial index to keep within -1..2</param>
        /// <returns><paramref name="i"/> clamped such that i >= -1 and i &lt;= 2</returns>
        internal static int IndexClamp(int i)
        {
            return i switch
            {
                < -1 => -1,
                > 2 => 2,
                _ => i
            };

            //return Math.Max(Math.Min(i, 2), -1);
        }
        
        /// <summary>
        /// mu_add(a, b)
        /// </summary>
        /// <param name="a">Spacial index a</param>
        /// <param name="b">Spacial index b</param>
        /// <returns>The resulting spacial index when performing addition of two scalars.</returns>
        internal static int IndexAdd(int a, int b)
        {
            if (a == 0)
            {
                return b;
            }
            
            if (b == 0)
            {
                return a;
            }

            if (a == 1)
            {
                return b;
            }

            if (b == 1)
            {
                return a;
            }

            if (a == b)
            {
                return a;
            }

            return 1;
        }
        
        /// <summary>
        /// mu_sub(a, b)
        /// </summary>
        /// <param name="a">Spacial index a</param>
        /// <param name="b">Spacial index b</param>
        /// <returns>The resulting spacial index when performing subtraction of two scalars.</returns>
        internal static int IndexSubtract(int a, int b)
        {
            return a switch
            {
                2 => b switch
                {
                    2 => 1,
                    _ => 2
                },
                1 => b switch
                {
                    2 => -1,
                    -1 => 2,
                    _ => 1
                },
                0 => b switch
                {
                    2 => -1,
                    -1 => 2,
                    _ => b
                },
                -1 => b switch
                {
                    -1 => 1,
                    _ => -1
                },
                _ => throw new ArgumentOutOfRangeException(nameof(a), a, "Index not in {-1,0,1,2}")
            };
        }
        
        /// <summary>
        /// mu_mul(a, b)
        /// </summary>
        /// <param name="a">Spacial index a</param>
        /// <param name="b">Spacial index b</param>
        /// <returns>The resulting spacial index when performing multiplication of two scalars.</returns>
        internal static int IndexMultiply(int a, int b)
        {
            return a switch
            {
                2 => b switch
                {
                    2 => 2,
                    1 => 2,
                    _ => 1
                },
                1 => b,
                0 => b switch
                {
                    2 => 1,
                    -1 => 1,
                    _ => 0
                },
                -1 => b switch
                {
                    1 => -1,
                    -1 => -1,
                    _ => 1
                },
                _ => throw new ArgumentOutOfRangeException(nameof(a), a, "Index not in {-1,0,1,2}")
            };
        }
        
        /// <summary>
        /// mu_div(a, b)
        /// </summary>
        /// <param name="a">Spacial index a</param>
        /// <param name="b">Spacial index b</param>
        /// <returns>The resulting spacial index when performing division of two scalars.</returns>
        internal static int IndexDivide(int a, int b)
        {
            return b switch
            {
                -1 => a switch
                {
                    1 or 2 => 2,
                    _ => 1
                },
                0 => a switch
                {
                    1 or 2 => 2,
                    _ => 1
                },
                1 => a,
                2 => a switch
                {
                    -1 or 1 => -1,
                    _ => 1
                },
                _ => throw new ArgumentOutOfRangeException(nameof(b), b, $"Index value of {b} was not in {{-1,0,1,2}}")
            };
        }

        /// <summary>
        /// mu_inv(i)
        /// </summary>
        /// <param name="i">Spacial index to invert</param>
        /// <returns>Converts +infinity to -infinity and vise versa. Otherwise, returns <paramref name="i"/> verbatim.</returns>
        internal static int IndexInversion(int i)
        {
            return i switch
            {
                -1 => 2,
                2 => -1,
                _ => i
            };
            // return i is 0 or 1 ? i : -i + 1;
        }
    }
}
