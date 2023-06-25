using System.Numerics;

interface ObjectInterface
{
    public abstract Vector3? RayIntersectPoint(Vector3 origin, Vector3 direction);
    public abstract bool RayIntersect(Vector3 origin, Vector3 direction);
    public abstract Color GetColor();
}
