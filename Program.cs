using System;
using SixLabors.ImageSharp;
using System.Numerics;
using System.Diagnostics ;

    namespace RayTracer {
    class Program
    {
        static void Main(string[] args){
            int height=800;
            int width=1200;

            Sphere s = new Sphere(radius: 3, new Vector3(x: 0, 0, -20));

            Renderer rd = new Renderer( 
                width, 
                height, 
                new Vector3(0,0,0), 
                new Vector3(0, 0, 0),
                new Image<Rgb24>(width, height)
                );

            rd.Render(s);
            rd.Save("test.png");

        }

        


    }
}