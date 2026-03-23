using System;

namespace GeometryLibrary

{
    //Creating a basic shape to remove any unecessary repetition when later creatig the definitive shapes. Because there will never be a a basic shape we create an abstract class for this, as we dont want to instantiate it ever. We are defining IComparable here so that we can later compare the shapes between eachother. We define by what we compare later inside the class via CompareTo().
    public abstract class Shape : IComparable<Shape>
    {
        public string Name { get; protected set; } = "BasicShape";


        //Create abstract methods for the calcuations so we can fill them out later inside the defined shapes.
        public abstract double Volume();
        public abstract double SurfaceArea();


        // Choosing with what parameter we will compare the shapes with echother. I choose Volume.
        public int CompareTo(Shape? other)
        {
            // If the other shape is null, this one is considered greater.
            if (other == null) return 1;

            // This uses C#'s built-in double comparison logic on the volumes. It automatically returns -1 (smaller), 0 (equal), or 1 (larger).
            return this.Volume().CompareTo(other.Volume());
        }

        // Creatin a CompositeShape and overloading the "+" operator.
        public static CompositeShape operator +(Shape a, Shape b)
        {
            // Create a new empty composite shape (n = 0)
            CompositeShape result = new CompositeShape(0);

            // Add both individual shapes into the container
            result.Add(a);
            result.Add(b);

            return result;
        }
    }
}