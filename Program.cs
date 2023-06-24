using System;
using SixLabors.ImageSharp;
using System.Numerics;
using System.Diagnostics ;

    namespace RayTracer {
    class Program
    {
        static void Main(string[] args){
            int height=800;
            int width=1600;

            Sphere s = new Sphere(100, new Vector3(100, 0, 0));

            Renderer rd = new Renderer( 
                width, 
                height, 
                new Vector3(0,0,0), 
                new Vector3(1, 0, 0),
                new Image<Rgb24>(width, height)
                );

            rd.Render(s);
            rd.Save("test.png");

        }

        


    }
}