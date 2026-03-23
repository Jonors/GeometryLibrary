# Report

Course: C# Development SS2026 (4 ECTS, 3 SWS)

Student ID: cc241032

BCC Group: C

Name: Jona Jozef Zeichen

## Methodology

My approach to this assignment was guided by a philosophy of creating a "Frictionless" and "Tactile" codebase. The architecture relies heavily on Object-Oriented Programming (OOP) principles to keep complex mathematical operations "Invisible" to the end user of the library.

* **The Struct vs. Class Decision:** I created a custom `Vertex` struct instead of using a class for 3D coordinates. Because a point in 3D space is just a lightweight container for three numbers (X, Y, Z), a struct is significantly more memory-efficient. It allocates to the stack rather than the heap, preventing unnecessary garbage collection overhead when generating hundreds of shapes.
* **Hierarchical Inheritance & Abstract Classes:** I implemented a strict inheritance tree with `Shape` as the base class. I explicitly marked `Shape` as an `abstract` class because a generic "Shape" does not exist in physical space and should never be directly instantiated. By declaring `Volume()` and `SurfaceArea()` as `abstract` methods within this base class, I created a strict rule: every child class (`Tetrahedron`, `Cuboid`, `Cylinder`) is forced to provide its own specific geometric formula using the `override` keyword.
* **Interfaces for Frictionless Sorting:** The `Shape` class implements the `IComparable<Shape>` interface. By defining a `CompareTo()` method based on the object's Volume, the `CompositeShape` class can natively use C#'s built-in `.Sort()` method. 
* **Access Modifiers & Immutability:** To ensure structural integrity, I heavily utilized `public` properties with `private set` modifiers. External code needs to read a shape's vertices or radius, but allowing public write access would let a user accidentally move a single corner of a cuboid, warping the geometry. This encapsulation acts as an "Invisible UI," preventing bugs by making the shapes immutable after creation.
* **Operator Overloading:** Overloading `==` allows for intuitive equality checks between complex objects. Overloading `+` in the base class seamlessly merges two distinct shapes into a new `CompositeShape` instance, abstracting away the underlying list management.

## Additional Features

* **Custom Coordinate Struct:** The aforementioned `Vertex` struct includes its own overloaded operators (`==` and `!=`) and overrides `Equals()` and `GetHashCode()`. This prevents redundant coordinate-checking code in the main shape classes.
* **Dynamic Pattern Matching:** In the `Computation` execution script, I implemented C# pattern matching (e.g., `if (s is Tetrahedron t)`) inside a universal print helper. This safely checks the runtime type of a shape and dynamically calls shape-specific methods (like `Centroid()`) without breaking the polymorphic flow.
S
## Discussion/Conclusion

The development process presented two primary challenges, both of which were solved through mathematical logic rather than brute-force coding:

1.  **Maintaining Cuboid Integrity:** The prompt requested a random default constructor for the `Cuboid`. Simply generating 8 random points in 3D space would result in a warped, irregular hexahedron, not a rectangular cuboid. I solved this by generating a single random "Anchor" vertex, randomly generating three dimensions (Width, Depth, and Height), and mathematically calculating the remaining 7 vertices relative to the anchor to guarantee perfect 90-degree angles.
2.  **Tetrahedron Volume Calculation:** Calculating the volume of an irregular 3D polyhedron required moving beyond basic geometry into linear algebra. I implemented the Scalar Triple Product formula. By converting the vertices into 3D vectors and calculating the absolute value of the dot product of one vector and the cross product of the other two, I was able to accurately compute the volume regardless of the tetrahedron's random orientation.

## Reference:
* Microsoft C# Documentation (Operator Overloading, Interfaces, Pattern Matching)
* Linear Algebra Mathematics (Scalar Triple Product for Tetrahedron Volume)
* Standard Geometric Formulas (Surface Area and Volume for Prisms and Cylinders)