using System.Diagnostics;
using System.Numerics;

namespace RayTracer
{
    class Program
    {
        static void Main(string[] args)
        {
            int height = 1080;
            int width = 1920;

            Material ivory = new Material(.5f, .7f, 0f, 2f, Color.MintCream);

            Renderer rd = new Renderer(width, height, new Vector3(0, 0, 0), new Vector3(0, 0, 0));
            List<IObject> objects = new List<IObject>();
            List<LightSource> lightSources = new List<LightSource>();

            objects.Add(new Sphere(15, new Vector3(0, 0, -60), ivory));
            lightSources.Add(new LightSource(new Vector3(0, 300, 0), 1f, 1f));

            Image<Rgb24> img = rd.Render(objects, lightSources, new Image<Rgb24>(width, height));
            img.SaveAsPng("output.png");
        }
    }
}
