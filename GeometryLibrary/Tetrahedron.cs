using System;

namespace GeometryLibrary
{
    // Inheriting from "Shape" to fulfill the abstract contract (Volume and SurfaceArea).
    public class Tetrahedron : Shape
    {
        // A tetrahedron is defined by 4 vertices in 3D space. "Private set" ensures that once created, external code cannot accidentally move one single corner and warp the shape.
        public Vertex A { get; private set; }
        public Vertex B { get; private set; }
        public Vertex C { get; private set; }
        public Vertex D { get; private set; }

        // Random number generator
        private static readonly Random rand = new Random();

        // Constructor
        public Tetrahedron()
        {
            Name = "Tetrahedron";

            // rand.NextDouble() gives a float between 0.0 and 1.0. Multiplying by 100.0 stretches out the space so coordinates range from 0.0 to 100.0.
            A = new Vertex(rand.NextDouble() * 100.0, rand.NextDouble() * 100.0, rand.NextDouble() * 100.0);
            B = new Vertex(rand.NextDouble() * 100.0, rand.NextDouble() * 100.0, rand.NextDouble() * 100.0);
            C = new Vertex(rand.NextDouble() * 100.0, rand.NextDouble() * 100.0, rand.NextDouble() * 100.0);
            D = new Vertex(rand.NextDouble() * 100.0, rand.NextDouble() * 100.0, rand.NextDouble() * 100.0);
        }

        // Copy Constructor. 
        // The assignment requires a way to clone an existing shape. We pass in an older tetrahedron ("other") and copy its exact X, Y, and Z values into new Vertices.
        public Tetrahedron(Tetrahedron other)
        {
            Name = other.Name;
            A = new Vertex(other.A.X, other.A.Y, other.A.Z);
            B = new Vertex(other.B.X, other.B.Y, other.B.Z);
            C = new Vertex(other.C.X, other.C.Y, other.C.Z);
            D = new Vertex(other.D.X, other.D.Y, other.D.Z);
        }

        // The centroid of a tetrahedron is the mathematical average of its 4 vertices. We add up all the X's and divide by 4, do the same for Y and Z, and return that new center point.
        public Vertex Centroid()
        {
            double cX = (A.X + B.X + C.X + D.X) / 4.0;
            double cY = (A.Y + B.Y + C.Y + D.Y) / 4.0;
            double cZ = (A.Z + B.Z + C.Z + D.Z) / 4.0;
            return new Vertex(cX, cY, cZ);
        }

        //Ovveriding "==" so C# knows if the tetrahedrons are the same (all vertecies mach eachother)
        public static bool operator ==(Tetrahedron? t1, Tetrahedron? t2)
        {
            // Shortcut: If they are the exact same entity in memory, they are equal.
            if (ReferenceEquals(t1, t2)) return true;

            // If one exists and the other is null, they cannot be equal.
            if (t1 is null || t2 is null) return false;

            // Check if all four vertices match using the "==" operator we wrote inside the Vertex struct.
            return t1.A == t2.A && t1.B == t2.B && t1.C == t2.C && t1.D == t2.D;
        }

        // Required companion to "==". It just returns the opposite of whatever "==" figures out.
        public static bool operator !=(Tetrahedron? t1, Tetrahedron? t2) => !(t1 == t2);


        // Mathmetical calculations
        // A tetrahedron is comprised of 4 triangular faces.  We use a custom helper method below to find the area of each face, then sum them up.
        public override double SurfaceArea()
        {
            return TriangleArea(A, B, C) +
                   TriangleArea(A, B, D) +
                   TriangleArea(A, C, D) +
                   TriangleArea(B, C, D);
        }

        // Calculating the volume of an irregular 3D shape requires the "Scalar Triple Product".
        public override double Volume()
        {
            // Step 1: Pick an anchor point (D). Create three 3D vectors shooting out from D to A, B, and C. 
            double[] AD = { A.X - D.X, A.Y - D.Y, A.Z - D.Z };
            double[] BD = { B.X - D.X, B.Y - D.Y, B.Z - D.Z };
            double[] CD = { C.X - D.X, C.Y - D.Y, C.Z - D.Z };

            // Step 2: Calculate the Cross Product of vectors BD and CD. This creates a new vector that points perfectly perpendicular to the base of the tetrahedron.
            double[] crossProduct = {
                BD[1] * CD[2] - BD[2] * CD[1],
                BD[2] * CD[0] - BD[0] * CD[2],
                BD[0] * CD[1] - BD[1] * CD[0]
            };

            // Step 3: Calculate the Dot Product of the AD vector and our new perpendicular vector. This flattens the vectors into a single number representing a parallelepiped (a 3D slanted box).
            double dotProduct = AD[0] * crossProduct[0] + AD[1] * crossProduct[1] + AD[2] * crossProduct[2];

            // Step 4: The volume of a tetrahedron is exactly 1/6th of that 3D slanted box. We use Math.Abs because volume cannot be negative.
            return Math.Abs(dotProduct) / 6.0;
        }

        // Standard formula for the area of a 3D triangle using vectors.
        private double TriangleArea(Vertex p1, Vertex p2, Vertex p3)
        {
            // Create vectors for two edges of the triangle.
            double[] v1 = { p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z };
            double[] v2 = { p3.X - p1.X, p3.Y - p1.Y, p3.Z - p1.Z };

            // The cross product gives us a vector whose length is equal to the area of a parallelogram.
            double[] cross = {
                v1[1] * v2[2] - v1[2] * v2[1],
                v1[2] * v2[0] - v1[0] * v2[2],
                v1[0] * v2[1] - v1[1] * v2[0]
            };

            // Calculate the length (magnitude) of the cross product vector using the Pythagorean theorem in 3D.
            // Multiply by 0.5 because a triangle is exactly half of a parallelogram.
            return 0.5 * Math.Sqrt(cross[0] * cross[0] + cross[1] * cross[1] + cross[2] * cross[2]);
        }

        // Required overrides whenever we create a custom "==" operator.
        public override bool Equals(object? obj) => obj is Tetrahedron t && this == t;
    }
}