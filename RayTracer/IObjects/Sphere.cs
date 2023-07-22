using System.Numerics;

public class Sphere : IObject
{
    public float radius { get; }
    public Material material { get; set; }
    public Vector3 center { get; set; }

    public Sphere(float radius, Vector3 center, Material material)
    {
        this.radius = radius;
        this.center = center;
        this.material = material;
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
        return H - (float)Math.Sqrt((radius * radius - d * d)) * direction;
    }

    public bool RayIntersect(Vector3 origin, Vector3 direction)
    {
        Vector3? p = RayIntersectPoint(origin, direction);
        return p is not null;
    }

    public bool IsInSphere(Vector3 pos) => Vector3.Distance(center, pos) <= radius;

    public Vector3 GetNormalVector(Vector3 origin, Vector3 direction)
    {
        return Vector3.Normalize(origin - center);
    }

    public void ApplyForce(Vector3 force) => center += force;

    public Vector3 GetPosition() => center;

    public void SetPosition(Vector3 newPosition) => center = newPosition;

    public void PrintInformation()
    {
        Console.WriteLine(center + "\t" + radius);
    }
}
