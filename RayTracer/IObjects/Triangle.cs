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
        //Console.WriteLine(            Vector3.Dot(direction, n) + " " + ((Vector3.Dot(direction, n) > .0001f) ? n : -n));
        return (Vector3.Dot(direction, n) > .0001f) ? n : -n;
    }

    public bool RayIntersect(Vector3 origin, Vector3 direction)
    {
        return RayIntersectPoint(origin, direction) is not null;
    }

    public Vector3? RayIntersectPoint(Vector3 origin, Vector3 direction)
    {
        var n = GetNormalVector(origin, -direction);
        if (Vector3.Dot(n, direction) > .0001f)
            return null;
        float t = Vector3.Dot(((A + B + C) / 3 - origin), n) / (Vector3.Dot(direction, n));
        var p = origin + t * direction;

        var s = (A.X - C.X) * (p.Y - C.Y) - (A.Y - C.Y) * (p.X - C.X);
        var u = (B.X - A.X) * (p.Y - A.Y) - (B.Y - A.Y) * (p.X - A.X);

        if ((s < 0) != (u < 0) && s != 0 && u != 0)
            return null;

        var d = (C.X - B.X) * (p.Y - B.Y) - (C.Y - B.Y) * (p.X - B.X);
        return (d == 0 || (d < 0) == (s + u <= 0)) ? p : null;
    }

    public void SetPosition(Vector3 newPosition)
    {
        throw new NotImplementedException(); // barycentre --> newPosition
    }
}
