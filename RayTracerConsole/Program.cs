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

            Material emerald = new Material()
            {
                ambientComponent = new Vector4(.0215f, .1745f, .0215f, 0),
                diffuseComponent = new Vector4(0.07568f, 0.61424f, 0.07568f, 0),
                specularComponent = new Vector4(0.633f, 0.727811f, 0.633f, 0),
                shininess = .6f * 128
            };

            Renderer rd = new Renderer(width, height, new Vector3(0, 0, 0), new Vector3(0, 0, 0));
            List<IObject> objects = new List<IObject>();
            List<LightSource> lightSources = new List<LightSource>();

            objects.Add(new Sphere(15, new Vector3(0, 0, -60), emerald));
            lightSources.Add(
                new LightSource()
                {
                    position = new Vector3(0, 0, 0),
                    ambientComponent = new Vector4(.2f, .2f, .2f, 1f),
                    DiffuseComponent = new Vector4(1f, 1f, 1f, 1f),
                    SpecularComponent = new Vector4(1f, 1f, 1f, 1f)
                }
            );

            Image<Rgb24> img = rd.Render(objects, lightSources, new Image<Rgb24>(width, height));
            img.SaveAsPng("output.png");
        }
    }
}
