using System.Diagnostics;
using System.Numerics;

public class Renderer
{
    public double fieldViewAngle = System.Math.PI / 4;
    public Vector3 position;
    public Vector3 roation;
    public int height;
    public int width;
    private Color bgColor = Color.DarkGray;

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
        float x,
            y;
        Vector3 direction;
        float AspectRatio = width / height;

        double theta = System.Math.Tan(fieldViewAngle / 2);
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
                        x = (float)((2 * (j + 0.5) / (double)width - 1) * theta * AspectRatio);
                        y = -(float)((2 * (i + 0.5) / (double)height - 1) * theta);
                        direction = new Vector3(x, y, -1);

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

        // fix to see light sources, should make it clean by attaching spheres to light sources ? or just IObjects
        /*foreach (var l in lightSources)
        {
            Sphere s = new Sphere(.3f, l.position, null);
            if (s.RayIntersect(position, direction))
            {
                return new Rgb24(255, 255, 255);
            }
        }*/

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
            lightDotN = Vector3.Dot(lightDir, N);
            R = Vector3.Normalize(2 * lightDotN * N - lightDir);

            totalLight +=
                objectMaterial.diffuseComponent * Math.Max(-lightDotN, 0) * light.DiffuseComponent;

            totalLight +=
                objectMaterial.specularComponent
                * (float)Math.Pow(Math.Max(0, Vector3.Dot(R, V)), objectMaterial.shininess)
                * light.SpecularComponent;
        }

        return (Color)(totalLight);
    }
}
