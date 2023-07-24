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
        return RayIntersectPoint(origin, direction) is not null;
    }

    public Vector3? RayIntersectPoint(Vector3 origin, Vector3 direction)
    {
        var n = GetNormalVector(origin, -direction);

        if (Vector3.Dot(n, -direction) < 1e-6)
            return null;
        float t = Vector3.Dot(((A + B + C) / 3 - origin), n) / (Vector3.Dot(direction, n));
        var p = origin + t * direction;

        var CA = A - C;
        var CP = p - C;

        var AB = B - A;
        var AP = p - A;

        var test = (float aA, float bA, float aP, float bP) => aA * bP - bA * aP;
        float u = 0;
        float s = 0;
        float d;
        if (
            (
                Math.Abs(A.X - B.X) <= 1e-6
                && Math.Abs(A.X - C.X) <= 1e-6
                && Math.Abs(B.X - C.X) <= 1e-6
            )
        )
        {
            s = test(CA.Z, CA.Y, CP.Z, CP.Y);
            u = test(AB.Z, AB.Y, AP.Z, AP.Y);

            d = (C.Z - B.Z) * (p.Y - B.Y) - (C.Y - B.Y) * (p.Z - B.Z);
        }
        else if (
            (
                Math.Abs(A.Y - B.Y) <= 1e-6
                && Math.Abs(A.Y - C.Y) <= 1e-6
                && Math.Abs(B.Y - C.Y) <= 1e-6
            )
        )
        {
            s = test(CA.Z, CA.X, CP.Z, CP.X);
            u = test(AB.Z, AB.X, AP.Z, AP.X);
            d = (C.Z - B.Z) * (p.X - B.X) - (C.X - B.X) * (p.Z - B.Z);
        }
        else
        {
            s = test(CA.X, CA.Y, CP.X, CP.Y);
            u = test(AB.X, AB.Y, AP.X, AP.Y);
            d = (C.X - B.X) * (p.Y - B.Y) - (C.Y - B.Y) * (p.X - B.X);
        }

        if (((s <= 1e-6) ^ (u <= 1e-6)))
            return null;

        return (!(d <= 1e-6 ^ (s + u <= 1e-6))) ? p : null;
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
