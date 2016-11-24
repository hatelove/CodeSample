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
                new Cell(0,0),
            };

            var world = new World(cells);

            var result = world.NextMoment();

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void two_cell_near()
        {
            var cells = new List<Cell>
            {
                new Cell(0,0),
                new Cell(0,1)
            };

            var world = new World(cells);

            var result = world.NextMoment();

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void three_cells()
        {
            var cells = new List<Cell>
            {
                new Cell(0,0),
                new Cell(-1,0),
                new Cell(1,0),
            };

            var world = new World(cells);

            var result = world.NextMoment();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(0, result[0].X);
            Assert.AreEqual(0, result[0].Y);
        }
    }

    internal class Cell
    {
        private bool isAlive = true;
        private int _x;
        private int _y;

        public Cell(int x, int y)
        {
            this._x = x;
            this._y = y;
        }

        public bool IsAlive
        {
            get { return this.isAlive; }
            set { this.isAlive = value; }
        }

        public int X { get { return this._x; } }

        public int Y
        {
            get { return this._y; }
        }
    }

    internal class World
    {
        private List<Cell> cells;

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

            foreach (var cell in this.cells)
            {
                if (cell.IsAlive)
                {
                    cell.IsAlive = false;
                }
            }

            return this.cells.Where(x => x.IsAlive).ToList();
        }
    }
}