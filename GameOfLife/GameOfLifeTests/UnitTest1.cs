using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLifeTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void one_cell_alive_to_death()
        {
            var cells = new List<Cell> { new Cell(0, 0) };
            var world = new World(cells);
            List<Cell> liveCells = world.NextMoment();
            Assert.AreEqual(0, liveCells.Count);
        }

        [TestMethod]
        public void only_two_near_cell_alive_to_death()
        {
            var cells = new List<Cell> { new Cell(0, 0), new Cell(1, 0) };
            var world = new World(cells);
            List<Cell> liveCells = world.NextMoment();
            Assert.AreEqual(0, liveCells.Count);
        }

        [TestMethod]
        public void three_near_cells_one_alive()
        {
            var cells = new List<Cell> { new Cell(0, 0), new Cell(1, 0), new Cell(2, 0) };
            var world = new World(cells);
            List<Cell> liveCells = world.NextMoment();
            Assert.AreEqual(1, liveCells.Count);
            Assert.AreEqual(1, liveCells[0].X);
            Assert.AreEqual(0, liveCells[0].Y);
        }

        [TestMethod]
        public void three_not_near_cells()
        {
            var cells = new List<Cell> { new Cell(-1, 0), new Cell(1, 0), new Cell(2, 0) };
            var world = new World(cells);
            List<Cell> liveCells = world.NextMoment();
            Assert.AreEqual(0, liveCells.Count);
        }

        [TestMethod]
        public void four_near_cells()
        {
            var cells = new List<Cell> { new Cell(0, 0), new Cell(1, 0), new Cell(2, 0), new Cell(3,0)};
            var world = new World(cells);
            List<Cell> liveCells = world.NextMoment();
            Assert.AreEqual(2, liveCells.Count);
            Assert.AreEqual(1, liveCells[0].X);
            Assert.AreEqual(0, liveCells[0].Y);
            Assert.AreEqual(2, liveCells[1].X);
            Assert.AreEqual(0, liveCells[1].Y);
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
            var result = new List<Cell>();
            foreach (var cell in this.cells)
            {
                //var table = GetTable(Tuple.Create(cell.X, cell.Y));
                var x_start = cell.X - 1;
                var y_start = cell.Y - 1;
                var x_end = cell.X + 1;
                var y_end = cell.Y + 1;
                var matchCount = this.cells.Count(c => c.X >= x_start && c.X <= x_end && c.Y >= y_start && c.Y <= y_end);
                if (matchCount >= 3 && matchCount <= 5)
                {
                    result.Add(new Cell(cell.X, cell.Y));
                }
            }

            return result;
        }
    }

    internal class Cell
    {
        private int _x;
        private int _y;

        public Cell(int x, int y)
        {
            this._x = x;
            this._y = y;
        }

        public bool IsAlive { get; internal set; } = true;
        public int X { get { return this._x; } }
        public int Y { get { return this._y; } }
    }
}