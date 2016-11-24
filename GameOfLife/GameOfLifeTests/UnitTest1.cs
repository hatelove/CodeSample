using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLifeTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void without_any_cell()
        {
            var cells = new List<Cell>();
            var world = new World(cells);

            List<Cell> result = world.NextMoment();

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void only_one_cell()
        {
            var cells = new List<Cell>
            {
                new Cell(0,0,Status.Live),
            };

            var world = new World(cells);

            var result = world.NextMoment();

            Assert.AreEqual(Status.Death, result.First().Status);
        }
    }

    internal enum Status
    {
        None,
        Live,
        Death
    }

    internal class Cell
    {
        private Status status;
        private int _x;
        private int _y;

        public Cell(int x, int y, Status status)
        {
            this._x = x;
            this._y = y;
            this.status = status;
        }

        public Status Status
        {
            get { return this.status; }
            set { this.status = value; }
        }
    }

    internal class World
    {
        private List<Cell> cells;

        public World()
        {
        }

        public World(List<Cell> cells)
        {
            this.cells = cells;
        }

        internal List<Cell> NextMoment()
        {
            if (!this.cells.Any())
            {
                return this.cells;
            }

            if (this.cells.First().Status == Status.Live)
            {
                this.cells.First().Status = Status.Death;
            }

            return this.cells;
        }
    }
}