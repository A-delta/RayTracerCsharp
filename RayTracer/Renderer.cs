using System.Diagnostics;
using System.Numerics;

public class Renderer
{
    public double fieldViewAngle = System.Math.PI / 4;
    public Vector3 position;
    public Vector3 roation;
    public int height;
    public int width;
    private Color bgColor = Color.Black;

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
        float AspectRatio = width / height;

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
                        float x = (float)(
                            (2 * (j + 0.5) / (double)width - 1)
                            * (System.Math.Tan(fieldViewAngle / 2))
                            * width
                            / (float)height
                        );

                        float y = -(float)(
                            (2 * (i + 0.5) / (double)height - 1)
                            * System.Math.Tan(fieldViewAngle / 2)
                        );
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
        Vector3? inter;
        Vector3? minInter = null;
        float dist;
        float minDist = float.MaxValue;

        int min = 0;

        for (int i = 0; i < objects.Count; i++)
        {
            inter = objects[i].RayIntersectPoint(position, direction);

            if (inter is null)
                continue;

            dist = ((Vector3)inter - position).Length();
            if (dist < minDist)
            {
                minInter = inter;
                min = i;
                minDist = dist;
            }
        }
        if (minDist == float.MaxValue || minDist == -1)
            return bgColor;

        inter = minInter;
        Material objectMaterial = objects[min].material;

        Vector3 N = objects[min].GetNormalVector(position);

        Vector4 totalLight = new Vector4(0, 0, 0, 1);
        foreach (LightSource light in lightSources)
        {
            if (inter is not null)
            {
                Vector3 V = Vector3.Normalize((Vector3)inter - position);
                Vector3 lightDir = Vector3.Normalize((Vector3)inter - light.position);
                Vector3 R = Vector3.Normalize(2 * Vector3.Dot(lightDir, N) * N - lightDir);

                totalLight +=
                    objectMaterial.diffuseComponent
                    * Math.Max(Vector3.Dot(-lightDir, N), 0)
                    * light.DiffuseComponent;

                totalLight +=
                    objectMaterial.specularComponent
                    * (float)Math.Pow(Math.Max(0, Vector3.Dot(R, V)), objectMaterial.shininess)
                    * light.SpecularComponent;
            }
        }
        return (Color)(totalLight);
    }
}
