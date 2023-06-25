using System.Numerics;

public interface ObjectInterface
{
    public Color color { get; set; }
    public Vector3 center { get; set; }
    public abstract Vector3? RayIntersectPoint(Vector3 origin, Vector3 direction);
    public abstract bool RayIntersect(Vector3 origin, Vector3 direction);
    public abstract Vector3 GetNormalVector(Vector3 origin);
}
