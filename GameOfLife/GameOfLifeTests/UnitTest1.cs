using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
        public void three_near_cells_3_alive()
        {
            var cells = new List<Cell> { new Cell(0, 0), new Cell(1, 0), new Cell(2, 0) };
            var world = new World(cells);
            List<Cell> liveCells = world.NextMoment();
            Assert.AreEqual(3, liveCells.Count);

            var expected = new List<Cell> { new Cell(1, 0), new Cell(1, -1), new Cell(1, 1) };
            expected.ToExpectedObject().ShouldEqual(liveCells);
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
            var cells = new List<Cell> { new Cell(0, 0), new Cell(1, 0), new Cell(2, 0), new Cell(3, 0) };
            var world = new World(cells);
            List<Cell> liveCells = world.NextMoment();
            Assert.AreEqual(6, liveCells.Count);
        }

        [TestMethod]
        public void three_near_cells_with_y()
        {
            var cells = new List<Cell> { new Cell(1, 1), new Cell(1, 0), new Cell(2, 0) };
            var world = new World(cells);
            List<Cell> liveCells = world.NextMoment();
            Assert.AreEqual(4, liveCells.Count);
        }

        [TestMethod]
        public void five_near_cells()
        {
            var cells = new List<Cell> { new Cell(-1, 0), new Cell(-1, 1), new Cell(0, 0), new Cell(0, 1), new Cell(1, 1) };
            var world = new World(cells);
            List<Cell> liveCells = world.NextMoment();

            Assert.AreEqual(5, liveCells.Count);
        }
    }

    internal class World
    {
        private List<Cell> cells;

        public World(List<Cell> cells)
        {
            this.cells = cells;
        }

        private Dictionary<bool, IPointChanger> changers = new Dictionary<bool, IPointChanger>
        {
            {true, new LivePointChanger() },
            {false, new DeathPointChanger() },
        };

        internal List<Cell> NextMoment()
        {
            //以最小x,y與最大x,y兩點長出二維table, 巡覽每一個點

            var pointsOfTable = new HashSet<Point>();
            cells.ForEach(cell => pointsOfTable.Add(new Point(cell.X, cell.Y) { IsLive = true }));

            var minX = this.cells.Min(cell => cell.X) - 1;
            var minY = this.cells.Min(cell => cell.Y) - 1;

            var maxX = this.cells.Max(cell => cell.X) + 1;
            var maxY = this.cells.Max(cell => cell.Y) + 1;
            var points = new List<Point>();
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    pointsOfTable.Add(new Point(x, y) { IsLive = false });
                }
            }

            var result = new List<Cell>();
            foreach (var point in pointsOfTable)
            {
                var changer = changers[point.IsLive];
                changer.Handle(point, cells.Select(x => new Point(x.X, x.Y)).ToList(), () => result.Add(new Cell(point.x, point.y)));
            }

            return result;
        }
    }

    internal class DeathPointChanger : IPointChanger
    {
        public void Handle(Point point, List<Point> list, Action handler)
        {
            var x_start = point.x - 1;
            var y_start = point.y - 1;
            var x_end = point.x + 1;
            var y_end = point.y + 1;
            var matchCount = list.Count(c => c.x >= x_start && c.x <= x_end && c.y >= y_start && c.y <= y_end);
            if (matchCount == 3) { handler.Invoke(); }
        }
    }

    internal class LivePointChanger : IPointChanger
    {
        public void Handle(Point point, List<Point> list, Action handler)
        {
            var x_start = point.x - 1;
            var y_start = point.y - 1;
            var x_end = point.x + 1;
            var y_end = point.y + 1;
            var matchCount = list.Count(c => c.x >= x_start && c.x <= x_end && c.y >= y_start && c.y <= y_end);
            if (matchCount >= 3 && matchCount < 5) { handler.Invoke(); }
        }
    }

    internal interface IPointChanger
    {
        void Handle(Point point, List<Point> list, Action handler);
    }

    internal class Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            var point = (Point)obj;
            return this.x == point.x && this.y == point.y;
        }

        public override int GetHashCode()
        {
            return Tuple.Create(this.x, this.y).GetHashCode();
        }

        public bool IsLive { get; internal set; }
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