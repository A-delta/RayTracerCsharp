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
            Renderer rd = new Renderer(width, height, new Vector3(0, 0, 100), new Vector3(0, 0, 0));

            List<IObject> objects = GetAxis(50f);
            objects.Add(new Sphere(15f, new Vector3(50f, 20f, -16f), new Material("greenrubber")));
            objects.Add(new Sphere(15f, new Vector3(-20f, 10f, -5f), new Material("emerald")));
            objects.Add(new Sphere(4f, new Vector3(0, 0, 0), new Material("ruby")));

            List<LightSource> lights = new List<LightSource>();

            lights.Add(
                new LightSource()
                {
                    position = new Vector3(50, -50 / 2, 0),
                    ambientComponent = new Vector4(.2f, .2f, .2f, .2f),
                    DiffuseComponent = new Vector4(1f, 1f, 1f, 1f),
                    SpecularComponent = new Vector4(1f, 1f, 1f, 1f),
                }
            );
            lights.Add(
                new LightSource()
                {
                    position = new Vector3(0, -30 / 2, 0),
                    ambientComponent = new Vector4(.2f, .2f, .2f, .2f),
                    DiffuseComponent = new Vector4(1f, 1f, 1f, 1f),
                    SpecularComponent = new Vector4(1f, 1f, 1f, 1f),
                }
            );

            lights.Add(
                new LightSource()
                {
                    position = new Vector3(-4f, 10f, -5f),
                    ambientComponent = new Vector4(.2f, .2f, .2f, .2f),
                    DiffuseComponent = new Vector4(1f, 1f, 1f, 1f),
                    SpecularComponent = new Vector4(1f, 1f, 1f, 1f),
                }
            );

            //AddRectangularLight(lights, .01f, .1f, new Vector3(-30, 15, 0), new Vector3(0, 15, 0));

            Image<Rgb24> img = rd.Render(objects, lights, new Image<Rgb24>(width, height));
            img.SaveAsPng("output.png");
        }

        private static void AddRectangularLight(
            List<LightSource> lights,
            float lightlevel,
            float step,
            Vector3 start,
            Vector3 end
        )
        {
            Vector3 direction = (end - start);

            direction = direction * step / direction.Length();

            Vector3 tmp = start;
            while ((end - tmp).Length() > .1f)
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
