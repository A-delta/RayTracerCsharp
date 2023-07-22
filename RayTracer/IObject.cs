using System.Numerics;

public interface IObject
{
    public Material material { get; set; }
    public abstract Vector3? RayIntersectPoint(Vector3 origin, Vector3 direction);
    public abstract bool RayIntersect(Vector3 origin, Vector3 direction);
    public abstract Vector3 GetNormalVector(Vector3 origin, Vector3 direction);
    public abstract void ApplyForce(Vector3 force);
    public abstract void SetPosition(Vector3 newPosition);
}
