using System.Numerics;

class Renderer
{
    public Image<Rgb24> img;
    public double fieldViewAngle = System.Math.PI / 4;
    public Vector3 position;
    public Vector3 roation;
    public int height;
    public int width;

    public Renderer(int width, int height, Vector3 position, Vector3 roation, Image<Rgb24> img)
    {
        this.height = height;
        this.width = width;
        this.position = position;
        this.roation = new Vector3(0, 0, 0);
        this.img = img;
    }

    public void Render(List<ObjectInterface> objects)
    { // one sphere for now
        float AspectRatio = width / height;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                float x = (float)(
                    (2 * (i + 0.5) / (double)width - 1)
                    * (System.Math.Tan(fieldViewAngle / 2))
                    * width
                    / (double)height
                );
                float y = (float)(
                    -(2 * (j + 0.5) / (double)height - 1) * System.Math.Tan(fieldViewAngle / 2)
                );
                Vector3 direction = new Vector3(x, y, -1);

                img[j, i] = CastRay(Vector3.Normalize(direction), objects);
            }
        }
    }

    private Rgb24 CastRay(Vector3 direction, List<ObjectInterface> objects)
    {
        float dist;
        float minDist = float.MaxValue;
        int min = 0;

        for (int i = 0; i < objects.Count; i++)
        {
            dist = objects[i].RayIntersect(position, direction);
            if (dist != -1 && minDist > dist)
            {
                min = i;
                minDist = dist;
            }
        }
        if (minDist == float.MaxValue || minDist == -1)
            return new Rgb24(0, 0, 0);
        return objects[min].GetColor();
    }

    public void Save(string filename) => img.SaveAsPng(filename);
}
