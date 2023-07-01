using System.Diagnostics;
using System.Numerics;

namespace RayTracer
{
    class Program
    {
        static void Main(string[] args)
        {
            int height = 4080;
            int width = 4080;

            List<IObject> objects = new List<IObject>();

            List<LightSource> lightSources = new List<LightSource>();
            lightSources.Add(new LightSource(new Vector3(0, 300, 0), 1f, 1f));

            Material ivory = new Material(.5f, .7f, 0f, 2f, Color.MintCream);

            Renderer rd = new Renderer(width, height, new Vector3(0, 0, 0), new Vector3(0, 0, 0));

            Image<Rgb24> gif = rd.Render(objects, lightSources, new Image<Rgb24>(width, height));

            var gifMetaData = gif.Metadata.GetGifMetadata();

            var metadata = gif.Frames.RootFrame.Metadata.GetGifMetadata();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            int limit = 100;

            for (int i = -limit; i < limit; i += 1)
            {
                objects.Add(new Sphere(radius: 65, new Vector3(x: i, 0, -200), ivory));

                var img = new Image<Rgb24>(width, height);
                metadata = img.Frames.RootFrame.Metadata.GetGifMetadata();
                metadata.FrameDelay = 4;

                gif.Frames.AddFrame(rd.Render(objects, lightSources, img).Frames.RootFrame);

                objects.RemoveAt(0);
                Console.WriteLine((float)i / limit);
            }

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

            gif.SaveAsGif("diagonal_spheres.gif");
        }
    }
}
