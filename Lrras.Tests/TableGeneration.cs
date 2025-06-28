using Lrras.Tests.Utilities;
using NUnit.Framework;

namespace Lrras.Tests
{
    [TestFixture]
    public class TableGeneration
    {
        [Test]
        public void CreateIndexAdditionTable()
        {
            Console.WriteLine(Tables.CreateTable(Scalar.IndexAdd));
        }
        
        [Test]
        public void CreateIndexSubtractionTable()
        {
            Console.WriteLine(Tables.CreateTable(Scalar.IndexSubtract));
        }

        [Test]
        public void CreateIndexMultiplicationTable()
        {
            Console.WriteLine(Tables.CreateTable(Scalar.IndexMultiply));
        }
        
        [Test]
        public void CreateIndexDivisionTable()
        {
            Console.WriteLine(Tables.CreateTable(Scalar.IndexDivide));
        }

        
        [Test]
        public void CreateIndexAdditionLaTeXTable()
        {
            Console.WriteLine(Tables.CreateLaTeXTable(Scalar.IndexAdd));
        }
        
        [Test]
        public void CreateIndexSubtractionLaTeXTable()
        {
            Console.WriteLine(Tables.CreateLaTeXTable(Scalar.IndexSubtract));
        }

        [Test]
        public void CreateIndexMultiplicationLaTeXTable()
        {
            Console.WriteLine(Tables.CreateLaTeXTable(Scalar.IndexMultiply));
        }
        
        [Test]
        public void CreateIndexDivisionLaTeXTable()
        {
            Console.WriteLine(Tables.CreateLaTeXTable(Scalar.IndexDivide));
        }
    }
}
