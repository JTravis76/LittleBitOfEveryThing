using System;

namespace Modifier.Demo
{
    interface IShape
    {
        int GetArea();

        int GetArea(int x, int y);
    }

    abstract class Shape : IShape
    {
        public abstract int GetArea();

        public abstract int GetArea(int x, int y);

        public bool Valid() { return true; }

        public virtual int Total() { return 0; }
    }

    class Square : Shape
    {
        readonly int _side;

        public Square() { }

        public Square(int n) => _side = n;

        public override int GetArea()
        {
            return _side * _side;
        }

        public override int GetArea(int x, int y)
        {
            return x * y;
        }


        public virtual int Area()
        {
            // this function is already included by inhertiance
            if (this.Valid())
            {
                // Do something
            }

            return 0;
        }
    }

    //class MyObject : Square
    //{

    //}


    class SomeEntityTable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // to override this virtual, must be inherited by a different class
        public virtual AnotherEntityTable AnotherEntityTable { get; set; }
    }

    partial class AnotherEntityTable
    {
        public int Id { get; set; }
        public string Address { get; set; }
    }

    class Program : Square
    {
        // overriding the virtual in the Square class
        public override int Area()
        {
            return base.Area() + 44;
        }


        static void Main(string[] args)
        {
            var square1 = new Square(8);
            Console.WriteLine($"Area of the square = {square1.GetArea()}");

            var square2 = new Square();
            Console.WriteLine($"Area of the square = {square2.GetArea(4, 6)}");

            var square3 = new Square();
            Console.WriteLine($"Area of the square = {square3.Area()}");

            Console.WriteLine($"new area = {new Program().Area()}");

            var model = new SomeEntityTable 
            { 
                Id = 1, 
                Name = "Blaze, Johnny", 
                AnotherEntityTable = new AnotherEntityTable 
                { 
                    Address = "123 Main St, New York, NY" 
                } 
            };

            Console.ReadKey();
        }
    }
}
