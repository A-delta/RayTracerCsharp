﻿using System.Numerics;
using RayTracer;

namespace RayTracer
{
    class Program
    {
        static void Main(string[] args)
        {
            int height = 1080;
            int width = 1920;

            List<ObjectInterface> objects = new List<ObjectInterface>();

            List<LightSource> lightSources = new List<LightSource>();
            lightSources.Add(new LightSource(new Vector3(0, 50, -200), 1f));
            objects.Add(new Sphere(radius: 20, new Vector3(x: 0, y: 71, -200), Color.Yellow));

            Renderer rd = new Renderer(width, height, new Vector3(0, 0, 0), new Vector3(0, 0, 0));

            /*Image<Rgb24> img = new Image<Rgb24>(width, height);
            objects.Add(new Sphere(30, new Vector3(0, 0, -200), Color.White));
            rd.Render(objects, lightSources, img);
            img.SaveAsPng("test.png");*/



            Image<Rgb24> gif = rd.Render(objects, lightSources, new Image<Rgb24>(width, height));

            var gifMetaData = gif.Metadata.GetGifMetadata();

            var metadata = gif.Frames.RootFrame.Metadata.GetGifMetadata();

            int limit = 100;

            for (int i = -limit; i < limit; i += 1)
            {
                objects.Add(new Sphere(radius: 16, new Vector3(x: i, -i, -200)));
                objects.Add(new Sphere(radius: 32, new Vector3(x: -i, -i, -200)));

                var img = new Image<Rgb24>(width, height);
                metadata = img.Frames.RootFrame.Metadata.GetGifMetadata();
                metadata.FrameDelay = 4;

                gif.Frames.AddFrame(rd.Render(objects, lightSources, img).Frames.RootFrame);

                objects.RemoveAt(1);
                objects.RemoveAt(1);
                Console.WriteLine((float)i / limit);
            }
            gif.SaveAsGif("diagonal_spheres.gif");
        }
    }
}