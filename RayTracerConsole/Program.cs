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
            var rd = new Renderer(width, height, new Vector3(0, 0, 100), Vector3.Zero);

            var objects = STLReader.ReadFile(
                "treefrog_45_cut.stl",
                new Material("ruby", "../RayTracer/materials.json")
            );
            var l = Vector4.One * 10;
            var lights = new List<LightSource>()
            {
                new LightSource()
                {
                    ambientComponent = l * .1f,
                    SpecularComponent = l,
                    DiffuseComponent = l,
                    Body = new Sphere(
                        5f,
                        new Vector3(-30, 0, 60),
                        new Material("reflect_only", "../RayTracer/materials.json")
                    )
                },
                new LightSource()
                {
                    ambientComponent = l * .1f,
                    SpecularComponent = l,
                    DiffuseComponent = l,
                    Body = new Sphere(
                        5f,
                        new Vector3(50, 0, 0),
                        new Material("reflect_only", "../RayTracer/materials.json")
                    )
                },
            };
            rd.Render(objects.ToArray(), lights.ToArray(), new Image<Rgb24>(width, height))
                .SaveAsPng("output.png");
        }

        private static void AddRectangularLight(
            List<LightSource> lights,
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
                        ambientComponent = new Vector4(lightlevel, lightlevel, lightlevel, lightlevel) * .1f,
                        DiffuseComponent = new Vector4(lightlevel, lightlevel, lightlevel, lightlevel),
                        SpecularComponent = new Vector4(lightlevel, lightlevel, lightlevel, lightlevel),
                        Body = new Sphere(
                            5f,
                            tmp,
                            new Material("relfect_only", "../RayTracer/materials.json")
                        )
                    }
                );
                tmp += direction;
            }
        }

        private static List<IObject> GetAxis(float point, Material mat)
        {
            return new List<IObject>()
            {
                new Sphere(1f, new Vector3(0, 0, 0), mat),
                new Sphere(1f, new Vector3(point, 0, 0), mat),
                new Sphere(1f, new Vector3(-point, 0, 0), mat),
                new Sphere(1f, new Vector3(0, point / 2, 0), mat),
                new Sphere(1f, new Vector3(0, -point / 2, 0), mat),
                new Sphere(1f, new Vector3(0, 0, -point), mat),
                new Sphere(1f, new Vector3(0, 0, point), mat),
            };
        }
    }
}
