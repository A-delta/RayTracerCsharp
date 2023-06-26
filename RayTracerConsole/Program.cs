using System.Diagnostics;
using System.Numerics;

namespace RayTracer
{
    class Program
    {
        static void Main(string[] args)
        {
            int height = 1000;
            int width = 1000;

            List<IObject> objects = new List<IObject>();

            List<LightSource> lightSources = new List<LightSource>();
            lightSources.Add(new LightSource(new Vector3(0, y: 100, 0), 1.5f, 1.5f));

            Material m = new Material(.3f, .6f, 0f, 1f, Color.Blue);

            objects.Add(new Sphere(radius: 16, new Vector3(x: 0, -15, -80), m));

            Renderer rd = new Renderer(width, height, new Vector3(0, 0, 0), new Vector3(0, 0, 0));

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var img = rd.Render(objects, lightSources, new Image<Rgb24>(width, height));

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = String.Format(
                "{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours,
                ts.Minutes,
                ts.Seconds,
                ts.Milliseconds / 10
            );
            Console.WriteLine("Rendering : " + elapsedTime);

            img.SaveAsPng("test.png");

            /*objects.Add(new Sphere(radius: 20, new Vector3(x: 0, y: 71, -200), Color.Yellow));

            Renderer rd = new Renderer(width, height, new Vector3(0, 0, 0), new Vector3(0, 0, 0));

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
            gif.SaveAsGif("diagonal_spheres.gif");*/
        }
    }
}
