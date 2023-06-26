using System.Numerics;

public class Sphere : IObject
{
    public int radius { get; }
    public Material material { get; set; }
    public Vector3 center { get; set; }
    public Color color { get; set; } = Color.AntiqueWhite;

    public Sphere(int radius, Vector3 center, Material material)
    {
        this.radius = radius;
        this.center = center;
        this.material = material;
    }

    public Sphere(int radius, Vector3 center, Material material, Color c)
    {
        this.radius = radius;
        this.center = center;
        this.material = material;
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
        return p is not null;
    }

    public bool IsInSphere(Vector3 pos) => Vector3.Distance(center, pos) <= radius;

    public Vector3 GetNormalVector(Vector3 origin)
    {
        return Vector3.Normalize(origin - center);
    }

    public void ApplyForce(Vector3 force)
    {
        this.center += force;
    }
}
