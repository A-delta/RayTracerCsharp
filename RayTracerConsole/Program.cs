using System.Diagnostics;
using System.Numerics;

namespace RayTracer
{
    class Program
    {
        static void Main(string[] args)
        {
            int height = 800;
            int width = height * 16 / 9;

            List<IObject> objects = new List<IObject>()
            {
                new Sphere(15, new Vector3(0, 0, -60), new Material("emerald")),
                new Sphere(22, new Vector3(0, 0, 0), new Material("chrome")),
                new Sphere(28, new Vector3(0, 0, 0), new Material("blackrubber"))
            };
            List<LightSource> lightSources = new List<LightSource>()
            {
                // new LightSource()
                // {
                //     position = new Vector3(0, 0, 100),
                //     ambientComponent = new Vector4(.2f, .2f, .2f, 1f),
                //     DiffuseComponent = new Vector4(1f, 1f, 1f, 1f),
                //     SpecularComponent = new Vector4(1f, 1f, 1f, 1f)
                // },
                new LightSource()
                {
                    position = new Vector3(0, 0, 0),
                    ambientComponent = new Vector4(.2f, .2f, .2f, 1f),
                    DiffuseComponent = new Vector4(1f, 1f, 1f, 1f),
                    SpecularComponent = new Vector4(1f, 1f, 1f, 1f)
                }
            };

            SimpleAnimation(objects, lightSources, width, height);
        }

        private static void SimpleAnimation(
            List<IObject> objects,
            List<LightSource> lightSources,
            int width,
            int height
        )
        {
            Renderer rd = new Renderer(
                width,
                height,
                new Vector3(0, 50, 200),
                new Vector3(0, 0, 0)
            );

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
                objects[0].SetPosition(new Vector3(i, 0, limit / 2 - i));
                objects[1].SetPosition(new Vector3(limit / 2 - i, 0, i));
                objects[2].SetPosition(new Vector3(i, 0, i));

                img = new Image<Rgb24>(width, height);
                metadata = img.Frames.RootFrame.Metadata.GetGifMetadata();
                metadata.FrameDelay = 4;
                gif.Frames.AddFrame(rd.Render(objects, lightSources, img).Frames.RootFrame);
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine((2 * limit / step) / ts.TotalSeconds + " fps"); //total frames / total seconds

            gif.SaveAsGif("output.gif");
        }
    }
}
