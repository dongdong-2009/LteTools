using System.Collections.Generic;
using System.Linq;
using Lte.Parameters.Region.Abstract;
using Lte.Parameters.Region.Entities;
using Moq;

namespace Lte.Parameters.MockOperations
{
    public static class MockTownRepository
    {
        public static void MockAddOneTownOperation(this Mock<ITownRepository> townRepository,
            IEnumerable<Town> towns)
        {
            townRepository.Setup(x => x.AddOneTown(It.Is<Town>(t => t != null))).Callback<Town>(
                town => townRepository.SetupGet(y => y.Towns).Returns(
                    towns.Concat(new List<Town> { town }).AsQueryable()));
        }

        public static void MockAddOneTownOperation(this Mock<ITownRepository> townRepository)
        {
            townRepository.MockAddOneTownOperation(townRepository.Object.Towns);
        }

        public static void MockRemoveOneTownOperation(this Mock<ITownRepository> townRepository,
            IEnumerable<Town> towns)
        {
            townRepository.Setup(x => x.RemoveOneTown(It.IsAny<Town>())).Returns(false);
            townRepository.Setup(x => x.RemoveOneTown(It.Is<Town>(
                t => t != null && towns.FirstOrDefault(y => y == t) != null))).Returns(true).Callback<Town>(
                t1 => townRepository.SetupGet(z => z.Towns).Returns(towns.Where(t2 => t2 != t1).AsQueryable()));
        }

        public static void MockRemoveOneTownOperation(this Mock<ITownRepository> townRepository)
        {
            townRepository.MockRemoveOneTownOperation(townRepository.Object.Towns);
        }
    }

    public static class MockOptimizeRegionRepository
    {
        public static void MockAddOneRegionOperation(this Mock<IRegionRepository> regionRepository,
            IEnumerable<OptimizeRegion> regions)
        {
            regionRepository.Setup(x => x.AddOneRegion(It.Is<OptimizeRegion>(o => o != null))).Callback<OptimizeRegion>(
                region => regionRepository.SetupGet(y => y.OptimizeRegions).Returns(
                    regions.Concat(new List<OptimizeRegion> { region }).AsQueryable()));
        }

        public static void MockAddOneRegionOperation(this Mock<IRegionRepository> regionRepository)
        {
            regionRepository.MockAddOneRegionOperation(regionRepository.Object.OptimizeRegions);
        }

        public static void MockRemoveOneRegionOperation(this Mock<IRegionRepository> regionRepository,
            IEnumerable<OptimizeRegion> regions)
        {
            regionRepository.Setup(x => x.RemoveOneRegion(It.Is<OptimizeRegion>(
                t => t != null && regions.FirstOrDefault(y => y == t) != null))).Callback<OptimizeRegion>(
                t1 => regionRepository.SetupGet(z => z.OptimizeRegions).Returns(
                    regions.Where(t2 => t2 != t1).AsQueryable()));
        }

        public static void MockRemoveOneRegionOperation(this Mock<IRegionRepository> regionRepository)
        {
            regionRepository.MockRemoveOneRegionOperation(regionRepository.Object.OptimizeRegions);
        }
    }
}
