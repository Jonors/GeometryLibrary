using System;
using System.Collections.Generic; // Required to use List<Shape>

namespace GeometryLibrary
{
    // Inheriting from "Shape" means a CompositeShape IS a Shape. This allows us to have a CompositeShape inside another CompositeShape!
    public class CompositeShape : Shape
    {
        // A list to store any object that inherits from Shape (Tetrahedrons, Cuboids, etc.)
        private List<Shape> _shapes = new List<Shape>();

        private static readonly Random rand = new Random();

        // Constructor CompositeShape(int n) that initializes it with n random shapes.
        public CompositeShape(int n)
        {
            Name = "Composite Object";

            for (int i = 0; i < n; i++)
            {
                // We pick a random number (0, 1, or 2) to decide which type of shape to create.
                int choice = rand.Next(3);

                if (choice == 0) _shapes.Add(new Tetrahedron());
                else if (choice == 1) _shapes.Add(new Cuboid());
                else _shapes.Add(new Cylinder());
            }
        }


        // Override the [] operator (Indexer). This allows us to type "myComposite[0]" to get the first shape in the list.
        public Shape this[int index]
        {
            get { return _shapes[index]; }
            set { _shapes[index] = value; }
        }

        // int IsIn(Shape s) tests if a shape is part of the composite. It returns the index if found, or -1 if the shape is not in the list.
        public int IsIn(Shape s)
        {
            for (int i = 0; i < _shapes.Count; i++)
            {
                // This uses the "==" operator we wrote in each specific shape class!
                if (_shapes[i] == s)
                {
                    return i;
                }
            }
            return -1; // Not found
        }

        // A helper method for the "+" operator in the base class to use.
        public void Add(Shape s)
        {
            _shapes.Add(s);
        }

        // Since Shape implements IComparable, we can just call the built-in Sort().
        public void Sort()
        {
            _shapes.Sort();
        }


        // Total Surface Area of all objects inside.
        public override double SurfaceArea()
        {
            double total = 0;
            foreach (var shape in _shapes)
            {
                total += shape.SurfaceArea();
            }
            return total;
        }

        // Total Volume of all objects inside.
        public override double Volume()
        {
            double total = 0;
            foreach (var shape in _shapes)
            {
                total += shape.Volume();
            }
            return total;
        }

        // A helper to let us know how many shapes are inside during testing.
        public int Count => _shapes.Count;
    }
}