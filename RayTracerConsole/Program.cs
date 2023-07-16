using System.Diagnostics;
using System.Numerics;

namespace RayTracer
{
    class Program
    {
        static void Main(string[] args)
        {
            Animation5Boules();
        }

        private static void Animation5Boules()
        {
            // 3000
            // 4.1458502345971855 fps
            //Size:28360612K  Cpu:427%  Elapsed:489.47




            int height = 3000;
            int width = height * 16 / 9;

            List<IObject> objects = new List<IObject>()
            {
                new Sphere(5, new Vector3(-30, 0, 0), new Material("ruby")),
                new Sphere(5, new Vector3(-15, 0, 0), new Material("gold")),
                new Sphere(5, new Vector3(0, 0, 0), new Material("emerald")),
                new Sphere(5, new Vector3(15, 0, 0), new Material("greenrubber")),
                new Sphere(5, new Vector3(30, 0, 0), new Material("obsidian")),
            };
            List<LightSource> lightSources = new List<LightSource>()
            {
                new LightSource()
                {
                    position = new Vector3(0, 10, 0),
                    ambientComponent = new Vector4(.2f, .2f, .2f, 1f),
                    DiffuseComponent = new Vector4(1f, 1f, 1f, 1f),
                    SpecularComponent = new Vector4(1f, 1f, 1f, 1f)
                }
            };
            Renderer rd = new Renderer(width, height, new Vector3(0, 0, 50), new Vector3(0, 0, 0));
            //rd.Render(objects, lightSources, new Image<Rgb24>(width, height)).SaveAsPng("test_position_camera.png");
            SimpleAnimation(rd, objects, lightSources, width, height);
        }

        private static void SimpleAnimation(
            Renderer rd,
            List<IObject> objects,
            List<LightSource> lightSources,
            int width,
            int height
        )
        {
            Image<Rgb24> img;
            Image<Rgb24> gif = rd.Render(objects, lightSources, new Image<Rgb24>(width, height));
            var gifMetaData = gif.Metadata.GetGifMetadata();
            var metadata = gif.Frames.RootFrame.Metadata.GetGifMetadata();

            float limit = 30f;
            float step = .1f;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (float i = -limit; i < limit; i += step)
            {
                Console.WriteLine(i);
                //the actual animation :
                lightSources[0].position = new Vector3(i, 10, 0);
                // end animation

                img = new Image<Rgb24>(width, height);
                metadata = img.Frames.RootFrame.Metadata.GetGifMetadata();

                metadata.FrameDelay = 1;
                gif.Frames.AddFrame(rd.Render(objects, lightSources, img).Frames.RootFrame);
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine((2 * limit / step) / ts.TotalSeconds + " fps"); //total frames / total seconds

            //gif.SaveAsGif("output.gif");
        }
    }
}
