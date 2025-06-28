using System.Text;

namespace Lrras.Tests.Utilities
{
    internal static class Tables
    {
        private const int MinI = -1;
        private const int MaxI = 2;

        public static string CreateTable(Func<int, int, int> indexFunction)
        {
            var sb = new StringBuilder();

            sb.Append("    |");
            
            for (var b = MinI; b <= MaxI; b++)
            {
                sb.Append($" {b,2} ");
            }

            sb.AppendLine();

            sb.Append("----+");
            for (var b = MinI; b <= MaxI; b++)
            {
                sb.Append("----");
            }
            
            sb.AppendLine();

            for (var a = MinI; a <= MaxI; a++)
            {
                sb.Append($" {a,2} |");

                for (var b = MinI; b <= MaxI; b++)
                {
                    sb.Append($" {indexFunction(a, b),2} ");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        public static string CreateLaTeXTable(Func<int, int, int> indexFunction)
        {
            var sb = new StringBuilder();

            sb.Append("    ");
            
            for (var b = MinI; b <= MaxI; b++)
            {
                sb.Append($"& {b,2} ");
            }
            
            sb.AppendLine(@"\\");
            sb.AppendLine("\\hline");

            for (var a = MinI; a <= MaxI; a++)
            {
                sb.Append($" {a,2} ");

                for (var b = MinI; b <= MaxI; b++)
                {
                    sb.Append($"& {indexFunction(a, b),2} ");
                }

                sb.AppendLine(@"\\");
                sb.AppendLine("\\hline");
            }

            return sb.ToString();
        }
    }
}
