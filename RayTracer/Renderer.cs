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
        float dist;
        float minDist = float.MaxValue;

        int min = 0;

        for (int i = 0; i < objects.Count; i++)
        {
            inter = objects[i].RayIntersectPoint(position, direction);

            if (inter is null)
                continue;

            dist = ((Vector3)inter - position).Length();
            min = i;
            minDist = dist;
        }
        if (minDist == float.MaxValue || minDist == -1)
            return bgColor;

        inter = objects[min].RayIntersectPoint(position, direction);

        float AmbientLight = 0;
        float lightIntensity = objects[min].material.ambientReflection * AmbientLight;
        float diffuseLight = 0,
            specularIntensity = 0;
        foreach (LightSource light in lightSources)
        {
            if (inter is not null)
            {
                // float factor = Math.Max(
                //     Vector3.Dot(
                //         objects[min].GetNormalVector((Vector3)inter),
                //         Vector3.Normalize(light.position - (Vector3)inter)
                //     ),
                //     0
                // );

                //lightIntensity += light.intensity * factor;

                Vector3 N = objects[min].GetNormalVector((Vector3)inter);
                Vector3 lightDir = Vector3.Normalize((Vector3)inter - light.position);
                diffuseLight +=
                    light.diffuseIntensity
                    * objects[min].material.diffuseReflection
                    * Math.Max(0, -Vector3.Dot(lightDir, N));

                Vector3 R = 2 * Math.Min(0, Vector3.Dot(lightDir, N)) * N - lightDir;

                specularIntensity +=
                    light.specularIntensity
                    * objects[min].material.specularReflection
                    * (float)
                        Math.Pow((double)Vector3.Dot(lightDir, R), objects[min].material.shininess);
            }
        }
        return (Color)(
            (Vector4)objects[min].material.color * diffuseLight
            + new Vector4(1, 1, 1, 1) * specularIntensity
        );
    }
}
