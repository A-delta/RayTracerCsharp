using System.Numerics;

public class Triangle : IObject
{
    public Material material { get; set; }
    public Vector3 A,
        B,
        C;

    public Triangle(Vector3 A, Vector3 B, Vector3 C, Material m)
    {
        this.A = A;
        this.B = B;
        this.C = C;
        this.material = m;
    }

    public void ApplyForce(Vector3 force)
    {
        A += force;
        B += force;
        C += force;
    }

    public Vector3 GetNormalVector(Vector3 origin, Vector3 direction)
    {
        var n = Vector3.Normalize(Vector3.Cross((B - A), (C - A)));

        return (Vector3.Dot(direction, n) <= 1e-6) ? -n : n;
    }

    public bool RayIntersect(Vector3 origin, Vector3 direction)
    {
        var test = (Vector3 A, Vector3 B, Vector3 C, Vector3 D, Vector3 p) =>
        {
            var normal = Vector3.Cross(B - A, C - A);
            float dotD = Vector3.Dot(normal, D - A);
            float dotP = Vector3.Dot(normal, p - A);
            return Math.Sign(dotD) == Math.Sign(dotP);
        };

        return test(A, B, C, origin, origin + direction)
            && test(B, C, origin, A, origin + direction)
            && test(C, origin, A, B, origin + direction)
            && test(origin, A, B, C, origin + direction);
    }

    public Vector3? RayIntersectPoint(Vector3 origin, Vector3 direction)
    {
        if (!RayIntersect(origin, direction))
            return null;
        var n = GetNormalVector(origin, -direction);

        if (Vector3.Dot(n, -direction) < 1e-6)
            return null;
        float t = Vector3.Dot(((A + B + C) / 3 - origin), n) / (Vector3.Dot(direction, n));
        var p = origin + t * direction;

        return p;
    }

    public void SetPosition(Vector3 newPosition)
    {
        throw new NotImplementedException(); // barycentre --> newPosition
    }

    public Vector3 GetPosition() => (A + B + C) / 3;

    public void PrintInformation()
    {
        Console.WriteLine(A + "\t" + B + "\t" + C);
    }
}
