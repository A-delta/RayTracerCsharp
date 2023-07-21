using System.Numerics;

public class Plane : IObject
{
    public Material material { get; set; }

    public Vector3 n;
    public Vector3 p0;

    public Plane(Vector3 n, Vector3 p0, Material material)
    {
        this.n = Vector3.Normalize(n);
        this.p0 = p0;
        this.material = material;
    }

    public void ApplyForce(Vector3 force)
    {
        throw new NotImplementedException();
    }

    public Vector3 GetNormalVector(Vector3 point)
    {
        return n;
    }

    public bool RayIntersect(Vector3 origin, Vector3 direction)
    {
        return !(Vector3.Dot(-n, direction) < .0001f);
    }

    public Vector3? RayIntersectPoint(Vector3 origin, Vector3 direction)
    {
        if (!RayIntersect(origin, direction))
            return null;
        float t = Vector3.Dot((p0 - origin), n) / (Vector3.Dot(direction, n));
        return origin + t * direction;
    }

    public void SetPosition(Vector3 newPosition)
    {
        p0 = newPosition;
    }
}
