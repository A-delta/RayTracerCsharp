using System.Diagnostics;
using System.Numerics;

public class Renderer
{
    public double fieldViewAngle = System.Math.PI / 4;
    public Vector3 position;
    public Vector3 roation;
    public int height;
    public int width;
    private Color bgColor = Color.DarkBlue;

    public Renderer(int width, int height, Vector3 position, Vector3 roation)
    {
        this.height = height;
        this.width = width;
        this.position = position;
        this.roation = new Vector3(0, 0, 0);
    }

    public Image<Rgb24> Render(
        List<IObject> objects,
        List<LightSource> lightSources,
        Image<Rgb24> img
    )
    {
        float AspectRatio = (float)width / height;
        float theta = (float)System.Math.Tan(fieldViewAngle / 2);

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

                        img[j, i] = CastRay(Vector3.Normalize(direction), objects, lightSources);
                    }
                );
            }
        );

        return img;
    }

    private Rgb24 CastRay(Vector3 direction, List<IObject> objects, List<LightSource> lightSources)
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
            Sphere s = new Sphere(.3f, l.position, null);
            if (s.RayIntersect(position, direction))
            {
                return new Rgb24(255, 255, 255);
            }
        }

        for (int i = 0; i < objects.Count; i++)
        {
            potentialIntersection = objects[i].RayIntersectPoint(position, direction);
            if (potentialIntersection is null)
                continue;

            isInter = true;
            inter = (Vector3)potentialIntersection;
            dist = (inter - position).Length();
            if (dist < minDist)
            {
                minInter = inter;
                min = i;
                minDist = dist;
            }
        }
        if (!isInter)
            return bgColor;

        inter = minInter;

        bool inShadow = false;
        Material objectMaterial = objects[min].material;
        Vector3 N = objects[min].GetNormalVector(inter);
        Vector3 V,
            lightDir,
            R;
        float lightDotN;
        Vector4 totalLight = new Vector4(0, 0, 0, 1);
        foreach (LightSource light in lightSources)
        {
            V = Vector3.Normalize(inter - position);
            lightDir = Vector3.Normalize(inter - light.position);
            inShadow = false;
            // shadows
            for (int i = 0; i < objects.Count(); i++)
            {
                if (i != min && objects[i].RayIntersect(light.position, lightDir))
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
                objectMaterial.diffuseComponent * Math.Max(-lightDotN, 0) * light.DiffuseComponent;

            totalLight +=
                objectMaterial.specularComponent
                * (float)Math.Pow(Math.Max(0, Vector3.Dot(R, V)), objectMaterial.shininess)
                * light.SpecularComponent;

            totalLight += light.ambientComponent * objectMaterial.ambientComponent;
        }

        return (Color)(totalLight);
    }
}
