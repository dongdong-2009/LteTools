using System.Collections.Generic;
using System.Linq;
using Moq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.MockOperations
{
    public static class MockCellRepository
    {
        public static void MockCellRepositorySaveCell(
            this Mock<ICellRepository> repository, IEnumerable<Cell> cells)
        {
            repository.Setup(x => x.AddOneCell(It.IsAny<Cell>())).Callback<Cell>(
                e => repository.SetupGet(x => x.Cells).Returns(
                    cells.Concat(new List<Cell> { e }).AsQueryable()));
        }

        public static void MockCellRepositorySaveCell(
            this Mock<ICellRepository> repository)
        {
            repository.Setup(x => x.AddOneCell(It.IsAny<Cell>())).Callback<Cell>(
                e =>
                {
                    IEnumerable<Cell> cells = repository.Object.Cells;
                    repository.SetupGet(x => x.Cells).Returns(
                    cells.Concat(new List<Cell> { e }).AsQueryable());
                });
        }

        public static void MockCellRepositoryDeleteCell(
            this Mock<ICellRepository> repository, IEnumerable<Cell> cells)
        {
            repository.Setup(x => x.RemoveOneCell(It.IsAny<Cell>())).Returns(false);
            repository.Setup(x => x.RemoveOneCell(It.Is<Cell>(e => e != null
                && cells.FirstOrDefault(y => y == e) != null))
                ).Returns(true).Callback<Cell>(
                e => repository.Setup(x => x.Cells).Returns(
                    cells.Except(new List<Cell> { e }).AsQueryable()));
        }

        public static void MockCellRepositoryDeleteCell(
            this Mock<ICellRepository> repository)
        {
            repository.Setup(x => x.RemoveOneCell(It.IsAny<Cell>())).Returns(false);

            if (repository.Object != null)
            {
                IEnumerable<Cell> cells = repository.Object.Cells;
                repository.Setup(x => x.RemoveOneCell(It.Is<Cell>(e => e != null
                    && cells.FirstOrDefault(y => y == e) != null))
                    ).Returns(true).Callback<Cell>(
                    e => repository.Setup(x => x.Cells).Returns(
                        cells.Except(new List<Cell> { e }).AsQueryable()));
            }
        }
    }

    public static class MockCdmaCellRepository
    {
        public static void MockCdmaCellRepositorySaveCell(
            this Mock<ICdmaCellRepository> repository, IEnumerable<CdmaCell> cells)
        {
            repository.Setup(x => x.AddOneCell(It.IsAny<CdmaCell>())).Callback<CdmaCell>(
                e => repository.SetupGet(x => x.Cells).Returns(
                    cells.Concat(new List<CdmaCell> { e }).AsQueryable()));
        }

        public static void MockCdmaCellRepositorySaveCell(
            this Mock<ICdmaCellRepository> repository)
        {
            repository.Setup(x => x.AddOneCell(It.IsAny<CdmaCell>())).Callback<CdmaCell>(
                e =>
                {
                    IEnumerable<CdmaCell> cells = repository.Object.Cells;
                    repository.SetupGet(x => x.Cells).Returns(
                    cells.Concat(new List<CdmaCell> { e }).AsQueryable());
                });
        }

        public static void MockCdmaCellRepositoryDeleteCell(
            this Mock<ICdmaCellRepository> repository, IEnumerable<CdmaCell> cells)
        {
            repository.Setup(x => x.RemoveOneCell(It.IsAny<CdmaCell>())).Returns(false);
            repository.Setup(x => x.RemoveOneCell(It.Is<CdmaCell>(e => e != null
                && cells.FirstOrDefault(y => y == e) != null))
                ).Returns(true).Callback<CdmaCell>(
                e => repository.Setup(x => x.Cells).Returns(
                    cells.Except(new List<CdmaCell> { e }).AsQueryable()));
        }

        public static void MockCdmaCellRepositoryDeleteCell(
            this Mock<ICdmaCellRepository> repository)
        {
            repository.Setup(x => x.RemoveOneCell(It.IsAny<CdmaCell>())).Returns(false);

            if (repository.Object != null)
            {
                IEnumerable<CdmaCell> cells = repository.Object.Cells;
                repository.Setup(x => x.RemoveOneCell(It.Is<CdmaCell>(e => e != null
                    && cells.FirstOrDefault(y => y == e) != null))
                    ).Returns(true).Callback<CdmaCell>(
                    e => repository.Setup(x => x.Cells).Returns(
                        cells.Except(new List<CdmaCell> { e }).AsQueryable()));
            }
        }
    }
}
