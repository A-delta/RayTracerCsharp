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

            List<LightSource> lightSources = new List<LightSource>();
            lightSources.Add(new LightSource(new Vector3(0, 0, 100), 1f));

            Renderer rd = new Renderer(width, height, new Vector3(0, 0, 0), new Vector3(0, 0, 0));

            Image<Rgb24> gif = rd.Render(objects, lightSources, new Image<Rgb24>(width, height));

            var gifMetaData = gif.Metadata.GetGifMetadata();
            //gifMetaData.RepeatCount = 5;

            var metadata = gif.Frames.RootFrame.Metadata.GetGifMetadata();

            int limit = 200;
            for (int i = 0; i < limit; i += 20)
            {
                objects.Add(new Sphere(radius: 35, new Vector3(x: i, 0, -200)));

                var img = new Image<Rgb24>(width, height);
                metadata = img.Frames.RootFrame.Metadata.GetGifMetadata();
                metadata.FrameDelay = 5;
                gif.Frames.AddFrame(rd.Render(objects, lightSources, img).Frames.RootFrame);

                objects.RemoveAt(0);
                Console.WriteLine((float)i / limit);
            }
            gif.SaveAsGif("test.gif");
        }
    }
}
