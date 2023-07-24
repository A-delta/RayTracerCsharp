using System.Diagnostics;
using System.Numerics;

namespace RayTracer
{
    class Program
    {
        static void Main(string[] args)
        {
            int height = 1080;
            int width = height * 16 / 9;
            Renderer rd = new Renderer(
                width,
                height,
                new Vector3(-50, 0, 300),
                new Vector3(0, 0, 0)
            );

            var objects = STLReader.ReadFile("lapin_ascii_42.stl");

            objects.Add(new Sphere(15f, new Vector3(25, 10, 100), new Material("emerald")));

            var lights = new List<LightSource>();

            AddRectangularLight(lights, 5f, new Vector3(-30, -65, 100), new Vector3(30, -65, 100));

            var img = rd.Render(objects, lights, new Image<Rgb24>(width, height));
            img.SaveAsPng("output.png");
        }

        private static void AddRectangularLight(
            List<LightSource> lights,
            //float lightlevel,
            float step,
            Vector3 start,
            Vector3 end
        )
        {
            Vector3 direction = (end - start);

            float lightlevel = step * .1f;

            direction = direction * step / direction.Length();

            Vector3 tmp = start;
            while ((end - tmp).Length() > step)
            {
                lights.Add(
                    new LightSource()
                    {
                        position = tmp,
                        ambientComponent = new Vector4(
                            lightlevel / 10,
                            lightlevel / 10,
                            lightlevel / 10,
                            lightlevel / 10
                        ),
                        DiffuseComponent = new Vector4(
                            lightlevel,
                            lightlevel,
                            lightlevel,
                            lightlevel
                        ),
                        SpecularComponent = new Vector4(
                            lightlevel,
                            lightlevel,
                            lightlevel,
                            lightlevel
                        ),
                    }
                );
                tmp += direction;
            }
        }

        private static List<IObject> GetAxis(float point)
        {
            return new List<IObject>()
            {
                new Sphere(1f, new Vector3(0, 0, 0), new Material("blackrubber")),
                new Sphere(1f, new Vector3(point, 0, 0), new Material("blackrubber")),
                new Sphere(1f, new Vector3(-point, 0, 0), new Material("blackrubber")),
                new Sphere(1f, new Vector3(0, point / 2, 0), new Material("blackrubber")),
                new Sphere(1f, new Vector3(0, -point / 2, 0), new Material("blackrubber")),
                new Sphere(1f, new Vector3(0, 0, -point), new Material("blackrubber")),
                new Sphere(1f, new Vector3(0, 0, point), new Material("blackrubber")),
            };
        }
    }
}
