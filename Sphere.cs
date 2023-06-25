using System.Numerics;

class Sphere : ObjectInterface
{
    public int radius;
    public Vector3 center;
    public Color color = Color.AntiqueWhite;

    public Sphere(int radius, Vector3 center)
    {
        this.radius = radius;
        this.center = center;
    }

    public Sphere(int radius, Vector3 center, Color c)
    {
        this.radius = radius;
        this.center = center;
        this.color = c;
    }

    public Vector3? RayIntersectPoint(Vector3 origin, Vector3 direction)
    {
        Vector3 L = center - origin;

        if (Vector3.Dot(L, direction) < 0)

            return null;

        Vector3 H = origin + Vector3.Dot(L, direction) * direction / direction.LengthSquared();

        float d = (float)System.Math.Sqrt(L.LengthSquared() - (H - origin).LengthSquared());
        if (d > radius)
            return null;
        return (H - origin) - (float)Math.Sqrt((radius * radius - d * d)) * direction + origin;
    }

    public bool RayIntersect(Vector3 origin, Vector3 direction)
    {
        Vector3? p = RayIntersectPoint(origin, direction);
        return (p is not null) ? true : false;
    }

    public bool IsInSphere(Vector3 pos) => Vector3.Distance(center, pos) <= radius;

    public Color GetColor() => color;

    public Vector3 GetCenter() => this.center;
}
