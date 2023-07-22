using System.Diagnostics;
using System.Numerics;

namespace RayTracer
{
    class Program
    {
        static void Main(string[] args)
        {
            int height = 1920 * 2;
            int width = height * 16 / 9;
            Renderer rd = new Renderer(
                width,
                height,
                new Vector3(14, 2.5f, 14),
                new Vector3(0, 0, 0)
            );

            //var objects = STLReader.ReadFile("blank-elephant/body_ascii.stl");
            var objects = new List<IObject>()
            {
                new Sphere(.1f, new Vector3(5 + 7.55862f, 0, 4.1395f), new Material("emerald")),
                new Sphere(.1f, new Vector3(5 + 7.55862f, 0, 4.47308f), new Material("copper")),
                new Sphere(.1f, new Vector3(5 + 7.55862f, 5, 4.30629f), new Material("brass")),
                new Triangle(
                    new Vector3(5 + 7.55862f, 0, 4.1395f),
                    new Vector3(5 + 7.55862f, 0, 4.47308f),
                    new Vector3(5 + 7.55862f, 5, 4.30629f),
                    new Material("ruby")
                ),
                new Triangle(
                    new Vector3(14, 0, 2),
                    new Vector3(15, 2.5f, 2),
                    new Vector3(16, 1, 0),
                    new Material("ruby")
                )
            };
            var lights = new List<LightSource>();
            lights.Add(
                new LightSource()
                {
                    position = new Vector3(21, 0, 0),
                    ambientComponent = new Vector4(.2f, .2f, .2f, .2f),
                    DiffuseComponent = new Vector4(1f, 1f, 1f, 1f),
                    SpecularComponent = new Vector4(1f, 1f, 1f, 1f)
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
