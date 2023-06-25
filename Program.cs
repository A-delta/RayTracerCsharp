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
            int height = 1080;
            int width = 1920;

            List<ObjectInterface> objects = new List<ObjectInterface>();
            //objects.Add(new Sphere(radius: 3, new Vector3(x: 0, 0, -20)));
            objects.Add(new Sphere(radius: 35, new Vector3(x: 0, 0, -200)));
            //objects.Add(new Sphere(radius: 3, new Vector3(x: 3, 0 - 2, -20)));

            List<LightSource> lightSources = new List<LightSource>();
            lightSources.Add(new LightSource(new Vector3(-100, 20, 20), 1f));

            Renderer rd = new Renderer(
                width,
                height,
                new Vector3(0, 0, 0),
                new Vector3(0, 0, 0),
                new Image<Rgb24>(width, height)
            );

            rd.Render(objects, lightSources);
            rd.Save("test.png");
        }
    }
}
