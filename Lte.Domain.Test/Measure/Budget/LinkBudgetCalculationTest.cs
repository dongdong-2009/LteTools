using Lte.Domain.Measure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lte.Domain.Test.Measure.Budget
{
    [TestClass]
    public class LinkBudgetCalculationTest
    {
        private IBroadcastModel model;
        private ILinkBudget<double> budget;
        const double eps = 1E-6;

        [TestInitialize]
        public void TestInitialize()
        {
            model = new BroadcastModel();
            budget = new LinkBudget(model);
        }

        [TestMethod]
        public void TestCalculation_FixedHeight()
        {
            double x1 = budget.CalculateReceivedPower(0.01, 30);
            double x2 = budget.CalculateReceivedPower(0.02, 30);
            Assert.IsTrue(x1 > x2, x1 + "," + x2);
        }
    }
}
