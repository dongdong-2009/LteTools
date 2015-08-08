using Lte.Domain.Measure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lte.Domain.Test.Measure.Budget
{
    [TestClass]
    public class LinkBudgetTest
    {
        private ILinkBudget<double> budget;
        const double eps = 1E-6;

        [TestInitialize]
        public void TestInitialize()
        {
            IBroadcastModel model = new BroadcastModel();
            budget = new LinkBudget(model);
        }

        [TestMethod]
        public void TestBudgetModel()
        {
            Assert.IsNotNull(budget.Model);
        }

        [TestMethod]
        public void TestBudgetCalculate()
        {
            double x = budget.CalculateReceivedPower(1, 1);
            Assert.AreEqual(x, -126.172376, eps, x.ToString());
        }
    }
}
