using System;

namespace GeometryLibrary
{
    // Inheriting from "Shape" to fulfill the abstract contract (Volume and SurfaceArea).
    public class Cylinder : Shape
    {
        // A cylinder is defined by its radius and two center points (Base1 and Base2).
        // "Private set" ensures that external code cannot warp the shape after creation.
        public double Radius { get; private set; }
        public Vertex Base1 { get; private set; }
        public Vertex Base2 { get; private set; }

        // Random number generator
        private static readonly Random rand = new Random();

        // Constructor
        public Cylinder()
        {
            Name = "Cylinder";

            // Step 1: Generate a random Radius.
            Radius = (rand.NextDouble() * 49.0) + 1.0;

            // Step 2: Generate two random center points for the top and bottom bases. 
            Base1 = new Vertex(rand.NextDouble() * 100.0, rand.NextDouble() * 100.0, rand.NextDouble() * 100.0);
            Base2 = new Vertex(rand.NextDouble() * 100.0, rand.NextDouble() * 100.0, rand.NextDouble() * 100.0);
        }

        // Copy Constructor.
        // Takes an existing cylinder ("other") and copies the radius and both base vertices.
        public Cylinder(Cylinder other)
        {
            Name = other.Name;
            Radius = other.Radius;
            Base1 = new Vertex(other.Base1.X, other.Base1.Y, other.Base1.Z);
            Base2 = new Vertex(other.Base2.X, other.Base2.Y, other.Base2.Z);
        }


        // The height is simply the straight-line distance between the two base center points.
        public double Height()
        {
            return GetDistance(Base1, Base2);
        }

        // The area of the bottom circle (π * r²).
        public double BottomArea()
        {
            return Math.PI * Math.Pow(Radius, 2);
        }

        // Overriding "==" so C# knows two cylinders are equal if their radius and both bases match.
        public static bool operator ==(Cylinder? c1, Cylinder? c2)
        {
            if (ReferenceEquals(c1, c2)) return true;
            if (c1 is null || c2 is null) return false;

            return c1.Radius == c2.Radius && c1.Base1 == c2.Base1 && c1.Base2 == c2.Base2;
        }

        // Required companion to "==".
        public static bool operator !=(Cylinder? c1, Cylinder? c2) => !(c1 == c2);

        //Mathmetical calculations 

        // Surface Area = 2 * BottomArea + SideArea
        // SideArea = Circumference * Height = (2 * π * r) * h
        public override double SurfaceArea()
        {
            double sideArea = (2.0 * Math.PI * Radius) * Height();
            return (2.0 * BottomArea()) + sideArea;
        }

        // Volume = BottomArea * Height = (π * r²) * h
        public override double Volume()
        {
            return BottomArea() * Height();
        }

        // Helper method to find the distance between two vertices (identical to Cuboid logic).
        private double GetDistance(Vertex v1, Vertex v2)
        {
            return Math.Sqrt(Math.Pow(v2.X - v1.X, 2) +
                             Math.Pow(v2.Y - v1.Y, 2) +
                             Math.Pow(v2.Z - v1.Z, 2));
        }

        // Required overrides whenever we create a custom "==" operator.
        public override bool Equals(object? obj) => obj is Cylinder c && this == c;
    }
}