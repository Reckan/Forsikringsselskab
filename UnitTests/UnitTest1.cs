using DataClasses;
using FuncLayer;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        public Func Func { get; set; } = new();
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestIngenForsikringPeriode()
        {
            DateTime date = new DateTime();
            ForsikringAftaler forsikring = new(Func.KundeList[0], Func.BilmodelList[0], "00110sse", "Test", 40, 45,  date);
            Func.NyForsikring(forsikring);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSammeTelefonNummer()
        {

            Kunde kunde = new("TestFornavn", "TestEdternavn", "TestAdresse", 8270, 28513594);
            Func.NyKunde(kunde);
        }
    }
}