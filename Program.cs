using System;
using SixLabors.ImageSharp;
using System.Numerics;
using System.Diagnostics ;

    namespace RayTracer {
    class Program
    {


        /*private Rgb24 castRay(Vector3 origin, Vector3 direction, List<ObjectInterface> objects) {
            var objMin = objects[0];
            double min;

            
        }*/

        static void Main(string[] args){
            Image<Rgb24> img = new Image<Rgb24>(800, 800);


            for (int i = 0; i < 800; i++)
            {
                for (int j = 0; j < 800; j++)
                {
                    img[i, j] = new Rgb24((byte)(255*i/800), (byte)(0), (byte)(255*j/800));
                }
            }




        img.SaveAsPng("test.png");

        }

        


    }
}