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
            Renderer rd = new Renderer(
                width,
                height,
                new Vector3(0, -260, 100),
                new Vector3(0, 0, 0)
            );

            //var objects = STLReader.ReadFile("blank-elephant/body_ascii.stl");
            var objects = new List<IObject>()
            {
                new Triangle(
                    new Vector3(7.55862f, -259.288f, 4.1395f),
                    new Vector3(7.55862f, -259.288f, 4.47308f),
                    new Vector3(7.55862f, -259.306f, 4.30629f),
                    new Material("ruby")
                )
            };
            var lights = new List<LightSource>();
            lights.Add(
                new LightSource()
                {
                    position = new Vector3(0, -250, 100),
                    ambientComponent = new Vector4(.2f, .2f, .2f, .2f) * 100,
                    DiffuseComponent = new Vector4(1f, 1f, 1f, 1f) * 10,
                    SpecularComponent = new Vector4(1f, 1f, 1f, 1f) * 10
                }
            );
            //AddRectangularLight(lights, .8f, new Vector3(-20, 30, 0), new Vector3(20, 30, 0));

            foreach (var t in objects)
            {
                t.PrintInformation();
            }

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
