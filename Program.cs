using System;
using SixLabors.ImageSharp;
using System.Numerics;
using System.Diagnostics;

namespace RayTracer
{
    class Program
    {
        static void Main(string[] args)
        {
            int height = 800;
            int width = 1200;

            List<ObjectInterface> l = new List<ObjectInterface>();
            l.Add(new Sphere(radius: 3, new Vector3(x: 0, 0, -20)));
            l.Add(new Sphere(radius: 8, new Vector3(x: 0, 0, -50)));
            l.Add(new Sphere(radius: 3, new Vector3(x: 3, 0 - 2, -20)));

            Renderer rd = new Renderer(
                width,
                height,
                new Vector3(0, 0, 0),
                new Vector3(0, 0, 0),
                new Image<Rgb24>(width, height)
            );

            rd.Render(l);
            rd.Save("test.png");
        }
    }
}
