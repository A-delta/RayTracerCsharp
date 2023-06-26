using System.Numerics;

public interface IObject
{
    public Color color { get; set; }
    public abstract Vector3? RayIntersectPoint(Vector3 origin, Vector3 direction);
    public abstract bool RayIntersect(Vector3 origin, Vector3 direction);
    public abstract Vector3 GetNormalVector(Vector3 origin);
    public abstract void ApplyForce(Vector3 force);
}
