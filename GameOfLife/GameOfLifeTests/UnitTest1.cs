using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GameOfLifeTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void one_cell()
        {
            var cells = new List<Cell> { new Cell(0, 0) };

            var world = new World(cells);

            List<Cell> liveCells = world.NextMoment();

            Assert.AreEqual(0, liveCells.Count);
        }

        [TestMethod]
        public void three_cells_near()
        {
            var cells = new List<Cell> { new Cell(-1, 0), new Cell(0, 0), new Cell(1, 0) };
            var world = new World(cells);
            var liveCells = world.NextMoment();
            Assert.AreEqual(1, liveCells.Count);
            Assert.AreEqual(0, liveCells[0].X);
            Assert.AreEqual(0, liveCells[0].Y);
        }

        [TestMethod]
        public void three_cells_not_near()
        {
            var cells = new List<Cell> { new Cell(-2, 0), new Cell(0, 0), new Cell(1, 0) };
            var world = new World(cells);
            var liveCells = world.NextMoment();
            Assert.AreEqual(0, liveCells.Count);
        }

        [TestMethod]
        public void four_cells_not_near()
        {
            var cells = new List<Cell> { new Cell(-1, 1), new Cell(1, 1), new Cell(0, 0), new Cell(-1, -1) };
            var world = new World(cells);
            var liveCells = world.NextMoment();
            Assert.AreEqual(1, liveCells.Count);
            Assert.AreEqual(0, liveCells[0].X);
            Assert.AreEqual(0, liveCells[0].Y);
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
            foreach (var cell in this.cells)
            {
                cell.IsAlive = CheckCellShouldLive(cell, this.cells);
            }
            return this.cells.Where(x => x.IsAlive).ToList();
        }

        private bool CheckCellShouldLive(Cell cell, List<Cell> cells)
        {
            var count = cells.Count(item => isNear(cell, item));
            return count >= 3;
        }

        private static bool isNear(Cell cell, Cell another)
        {
            var isNotNear = another.X > (cell.X + 1) || another.Y > (cell.Y + 1);
            var isNotNear_2 = another.X < (cell.X - 1) || another.Y < (cell.Y - 1);
            return !(isNotNear || isNotNear_2);
        }
    }

    internal class Cell
    {
        private int x;
        private int y;
        private bool _isAlive = true;

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool IsAlive
        {
            get { return this._isAlive; }
            set { this._isAlive = value; }
        }

        public int X { get { return this.x; } }
        public int Y { get { return this.y; } }
    }
}