using System;

namespace GeometryLibrary
{
    // Inheriting from "Shape" to fulfill the abstract contract (Volume and SurfaceArea).
    public class Cuboid : Shape
    {
        // A cuboid is defined by 8 vertices in 3D space. "Private set" ensures that once created, external code cannot accidentally move one single corner and warp the shape. 
        public Vertex A { get; private set; }
        public Vertex B { get; private set; }
        public Vertex C { get; private set; }
        public Vertex D { get; private set; }
        public Vertex E { get; private set; }
        public Vertex F { get; private set; }
        public Vertex G { get; private set; }
        public Vertex H { get; private set; }

        // Random number generator
        private static readonly Random rand = new Random();

        // Constructor
        public Cuboid()
        {
            Name = "Cuboid";

            // Step 1: Generate a random "Anchor" point (Vertex A) between 0.0 and 100.0.
            double startX = rand.NextDouble() * 100.0;
            double startY = rand.NextDouble() * 100.0;
            double startZ = rand.NextDouble() * 100.0;
            A = new Vertex(startX, startY, startZ);

            // Step 2: Generate random dimensions (Width, Depth, Height). 
            // We add a small amount (1.0) to ensure the shape isn't infinitely thin.
            double w = (rand.NextDouble() * 50.0) + 1.0;
            double d = (rand.NextDouble() * 50.0) + 1.0;
            double h = (rand.NextDouble() * 50.0) + 1.0;

            // Step 3: Calculate the other 7 vertices relative to point A to ensure it stays a rectangular box.
            B = new Vertex(startX + w, startY, startZ);
            C = new Vertex(startX + w, startY + d, startZ);
            D = new Vertex(startX, startY + d, startZ);

            E = new Vertex(startX, startY, startZ + h);
            F = new Vertex(startX + w, startY, startZ + h);
            G = new Vertex(startX + w, startY + d, startZ + h);
            H = new Vertex(startX, startY + d, startZ + h);
        }

        // Copy Constructor.
        // We pass in an older cuboid ("other") and copy its exact X, Y, and Z values for all 8 vertices.
        public Cuboid(Cuboid other)
        {
            Name = other.Name;
            A = new Vertex(other.A.X, other.A.Y, other.A.Z);
            B = new Vertex(other.B.X, other.B.Y, other.B.Z);
            C = new Vertex(other.C.X, other.C.Y, other.C.Z);
            D = new Vertex(other.D.X, other.D.Y, other.D.Z);
            E = new Vertex(other.E.X, other.E.Y, other.E.Z);
            F = new Vertex(other.F.X, other.F.Y, other.F.Z);
            G = new Vertex(other.G.X, other.G.Y, other.G.Z);
            H = new Vertex(other.H.X, other.H.Y, other.H.Z);
        }

        // The centroid of a cuboid is the mathematical average of its 8 vertices. 
        public Vertex Centroid()
        {
            double cX = (A.X + B.X + C.X + D.X + E.X + F.X + G.X + H.X) / 8.0;
            double cY = (A.Y + B.Y + C.Y + D.Y + E.Y + F.Y + G.Y + H.Y) / 8.0;
            double cZ = (A.Z + B.Z + C.Z + D.Z + E.Z + F.Z + G.Z + H.Z) / 8.0;
            return new Vertex(cX, cY, cZ);
        }

        // Overriding "==" so C# knows if the cuboids are the same (all 8 vertices match).
        public static bool operator ==(Cuboid? c1, Cuboid? c2)
        {
            if (ReferenceEquals(c1, c2)) return true;
            if (c1 is null || c2 is null) return false;

            return c1.A == c2.A && c1.B == c2.B && c1.C == c2.C && c1.D == c2.D &&
                   c1.E == c2.E && c1.F == c2.F && c1.G == c2.G && c1.H == c2.H;
        }

        // Required companion to "==".
        public static bool operator !=(Cuboid? c1, Cuboid? c2) => !(c1 == c2);

        // Mathematical calculations

        // The surface area of a rectangular cuboid is the sum of the areas of its 6 faces.
        // Formula: 2 * (Width*Depth + Width*Height + Depth*Height)
        public override double SurfaceArea()
        {
            double w = GetDistance(A, B);
            double d = GetDistance(A, D);
            double h = GetDistance(A, E);
            return 2.0 * (w * d + w * h + d * h);
        }

        // The volume of a rectangular cuboid is simply the product of its three dimensions.
        // Formula: Width * Depth * Height
        public override double Volume()
        {
            double w = GetDistance(A, B);
            double d = GetDistance(A, D);
            double h = GetDistance(A, E);
            return w * d * h;
        }

        // Helper method to find the length of an edge between two vertices.
        private double GetDistance(Vertex v1, Vertex v2)
        {
            return Math.Sqrt(Math.Pow(v2.X - v1.X, 2) +
                             Math.Pow(v2.Y - v1.Y, 2) +
                             Math.Pow(v2.Z - v1.Z, 2));
        }

        // Required overrides whenever we create a custom "==" operator.
        public override bool Equals(object? obj) => obj is Cuboid c && this == c;
        public override int GetHashCode() => HashCode.Combine(A, B, C, D, E, F, G, H);
    }
}