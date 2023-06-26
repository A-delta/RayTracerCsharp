/*using System.Numerics;

public class Plane : IObject
{
    public Color color { get; set; } = Color.White;

    public Vector3 n;
    public Vector3 p0;

    public Plane(Vector3 n, Vector3 p0)
    {
        this.n = n;
        this.p0 = p0;
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
        return RayIntersectPoint(origin, direction) is not null;
    }

    public Vector3? RayIntersectPoint(Vector3 origin, Vector3 direction)
    {
        float D = Vector3.Dot(n, direction);
        if (D == 0)
            return null;
        Vector3 w = origin - p0;
        return p0 + w + direction * (-Vector3.Dot(n, w) / D);

        
    }
}*/
