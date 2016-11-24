using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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
    }

    internal class Cell
    {
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
            return new List<Cell>();
        }
    }
}