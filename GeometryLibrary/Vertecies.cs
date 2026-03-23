using System;

namespace GeometryLibrary
{
    // A "struct" is used here instead of a "class" because a Vertex is just a simple container for three numbers. Structs are more lightweight and memory-efficient than using a class which is heap based while struct is "Stack" based and temporary
    public struct Vertex
    {
        // Using "get" only, to make the vertecies unchangable after creation. 
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        // Constructor
        public Vertex(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        // C# doesn't inherently know how to check if two Vertex structs are equal. We overload the "==" operator to tell C#: "Two Vertecies are equal if all three coordinates match."
        public static bool operator ==(Vertex a, Vertex b) =>
            a.X == b.X && a.Y == b.Y && a.Z == b.Z;

        // If we overload "==", we also have to overload "!=". We just return the opposite of our "==" logic using "!". 
        public static bool operator !=(Vertex a, Vertex b) => !(a == b);

        // When overriding operators, C# requires overriding the built-in Equals() method too. This checks if the object being compared is actually a Vertex, and if so, uses our "==" logic.
        public override bool Equals(object? obj) => obj is Vertex p && this == p;
    }
}