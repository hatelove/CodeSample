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
            return new List<Cell>();
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
    }
}