using System.Diagnostics;
using System.Numerics;

public class Renderer
{
    public double fieldViewAngle = System.Math.PI / 4;
    public Vector3 position;
    public Vector3 rotation;
    public int height;
    public int width;
    private Color bgColor = Color.Black;
    private int REFLEC_NB = 4;
    private float progress = 0;

    public Renderer(int width, int height, Vector3 position, Vector3 roation)
    {
        this.height = height;
        this.width = width;
        this.position = position;
        this.rotation = new Vector3(0, 0, 0);
    }

    public Image<Rgb24> Render(IObject[] objects, LightSource[] lightSources, Image<Rgb24> img)
    {
        float AspectRatio = (float)width / height;
        float theta = (float)System.Math.Tan(fieldViewAngle / 2);

        var t = new Thread(DisplayProgress);
        t.Start();

        Parallel.For(
            0,
            height,
            i =>
            {
                Parallel.For(
                    0,
                    width,
                    j =>
                    {
                        float x = ((2 * (j + 0.5f) / width - 1) * theta * AspectRatio);

                        float y = -((2 * (i + 0.5f) / height - 1) * theta);
                        Vector3 direction = new Vector3(x, y, -1);

                        img[j, i] = CastRay(position, Vector3.Normalize(direction), objects, lightSources, 0);
                        progress++;
                    }
                );
            }
        );

        progress = width * height;
        t.Join();
        return img;
    }

    private void DisplayProgress()
    {
        var c = new Stopwatch();
        c.Start();
        do
        {
            var perc = Math.Round(100 * progress / (width * height), 0);
            var eta = (int)(width * height * c.Elapsed.TotalSeconds / progress - c.Elapsed.TotalSeconds);
            Console.Write($"\r{perc}%\tETA: {eta}s            ");
            Thread.Sleep(600);
        } while (width * height - progress > 1e-6);
        c.Stop();
        Console.Write("\rComputation time: " + c.Elapsed);
        return;
    }

    private Rgb24 CastRay(
        Vector3 origin,
        Vector3 direction,
        IObject[] objects,
        LightSource[] lightSources,
        int depth
    )
    {
        Vector3? potentialIntersection;
        bool isInter = false;
        Vector3 inter;
        Vector3 minInter = new Vector3(0, 0, 0);
        float dist;
        float minDist = float.MaxValue;

        int min = 0;

        //fix to see light sources, should make it clean by attaching spheres to light sources ? or just IObjects
        foreach (var l in lightSources)
        {
            Sphere s = new Sphere((origin - l.position).Length() * 20 / 300, l.position, null);
            if (s.RayIntersect(origin, direction))
            {
                return new Rgb24(255, 255, 255);
            }
        }

        for (int i = 0; i < objects.Length; i++)
        {
            potentialIntersection = objects[i].RayIntersectPoint(origin, direction);
            if (potentialIntersection is null)

                continue;

            isInter = true;
            inter = (Vector3)potentialIntersection;
            dist = (inter - origin).Length();
            if (dist < minDist)
            {
                minInter = inter;
                min = i;
                minDist = dist;
            }
        }

        if (!isInter || depth > REFLEC_NB) // || minDist > 300)
            return bgColor;

        inter = minInter;

        bool inShadow = false;

        Material objectMaterial = objects[min].material;
        Vector3 N = objects[min].GetNormalVector(inter, -direction);
        Vector3 V,
            lightDir,
            R;
        float lightDotN;
        Vector4 totalLight = new Vector4(0, 0, 0, 1);

        // reflections
        if (depth < REFLEC_NB)
        {
            Vector3 reflectedRay = direction - 2f * N * Vector3.Dot(N, direction);
            Vector4 reflectionColor = CastRay(
                    inter,
                    Vector3.Normalize(reflectedRay),
                    objects,
                    lightSources,
                    depth + 1
                )
                .ToVector4();
            totalLight += reflectionColor * objectMaterial.albedo.X;
        }

        // lights contributions
        foreach (LightSource light in lightSources)
        {
            V = Vector3.Normalize(inter - origin);
            lightDir = Vector3.Normalize(inter - light.position);

            // shadows
            inShadow = false;
            minDist = (light.position - inter).Length();
            for (int i = 0; i < objects.Count(); i++)
            {
                if (i == min)
                    continue;
                var interPoint = objects[i].RayIntersectPoint(light.position, lightDir);
                if (interPoint == null)
                    continue;
                dist = ((Vector3)interPoint - light.position).Length();
                if (dist < minDist)
                {
                    inShadow = true;
                    break;
                }
            }
            if (inShadow)
            {
                continue;
            }

            lightDotN = Vector3.Dot(lightDir, N);
            R = Vector3.Normalize(2 * lightDotN * N - lightDir);
            totalLight +=
                objectMaterial.diffuseComponent
                * Math.Max(-lightDotN, 0)
                * light.DiffuseComponent
                * objectMaterial.albedo.Y;

            totalLight +=
                objectMaterial.specularComponent
                * (float)Math.Pow(Math.Max(0, Vector3.Dot(R, V)), objectMaterial.shininess)
                * light.SpecularComponent
                * objectMaterial.albedo.Z;

            totalLight += light.ambientComponent * objectMaterial.ambientComponent;
        }

        return (Color)(totalLight);
    }
}
