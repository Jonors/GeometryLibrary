using System;
using GeometryLibrary;

namespace Computation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("====================================================");
            Console.WriteLine("   ASSIGNMENT 2: GEOMETRIC OBJECT COMPUTATION");
            Console.WriteLine("====================================================\n");

            // Creating one of each kind using our random default constructors.
            Tetrahedron tet = new Tetrahedron();
            Cuboid cub = new Cuboid();
            Cylinder cyl = new Cylinder();

            Console.WriteLine(">>> 1. BASIC OBJECTS CREATED:");
            PrintShapeSummary(tet);
            PrintShapeSummary(cub);
            PrintShapeSummary(cyl);

            // Adding two shapes together returns a brand new CompositeShape.
            // This is "Invisible" logic—the user just uses '+' and the library handles the rest.
            Console.WriteLine("\n>>> 2. COMBINING SHAPES:");
            CompositeShape myComposite = tet + cub;

            // We can even add a third one to the composite we just made!
            myComposite.Add(cyl);

            Console.WriteLine($"Composite now contains {myComposite.Count} shapes.");
            Console.WriteLine($"Total Composite Volume: {myComposite.Volume():F2}");
            Console.WriteLine($"Total Composite Surface Area: {myComposite.SurfaceArea():F2}\n");

            // Before sorting, the shapes are in the order we added them.
            // After calling .Sort(), they will be ordered by Volume (smallest to largest).
            Console.WriteLine(">>> 3. SORTING SHAPES BY VOLUME:");
            myComposite.Sort();
            for (int i = 0; i < myComposite.Count; i++)
            {
                Console.WriteLine(
                    $"Index [{i}]: {myComposite[i].Name} (Vol: {myComposite[i].Volume():F2})"
                );
            }

            // We search for our original 'cub' in the now-sorted list.
            Console.WriteLine("\n>>> 4. SEARCHING & ACCESSING:");
            int foundIndex = myComposite.IsIn(cub);

            if (foundIndex != -1)
            {
                Console.WriteLine($"Found the original Cuboid at sorted index: {foundIndex}");

                // Accessing the object using our overloaded [] bracket operator.
                Shape foundShape = myComposite[foundIndex];

                // Now we create a perfect clone of that found shape.
                // We must cast it back to Cuboid because the Indexer returns a generic 'Shape'.
                if (foundShape is Cuboid originalCuboid)
                {
                    Cuboid cubClone = new Cuboid(originalCuboid);
                    Console.WriteLine($"Successfully cloned the Cuboid from index {foundIndex}.");
                    Console.WriteLine(
                        $"Clone Centroid: ({cubClone.Centroid().X:F2}, {cubClone.Centroid().Y:F2}, {cubClone.Centroid().Z:F2})"
                    );
                }
            }

            Console.WriteLine("\n====================================================");
            Console.WriteLine("             PROGRAM EXECUTION COMPLETE");
            Console.WriteLine("====================================================");
        }

        /// A helper to print the shared attributes of any Shape.
        static void PrintShapeSummary(Shape s)
        {
            Console.WriteLine(
                $"- {s.Name, -12} | Vol: {s.Volume(), 10:F2} | Area: {s.SurfaceArea(), 10:F2}"
            );

            // Using pattern matching to dynamically access the Centroid method,
            // since the base Shape class doesn't enforce a Centroid.
            if (s is Tetrahedron t)
            {
                Vertex c = t.Centroid();
                Console.WriteLine($"  -> Centroid: ({c.X:F2}, {c.Y:F2}, {c.Z:F2})");
            }
            else if (s is Cuboid cub)
            {
                Vertex c = cub.Centroid();
                Console.WriteLine($"  -> Centroid: ({c.X:F2}, {c.Y:F2}, {c.Z:F2})");
            }
        }
    }
}
