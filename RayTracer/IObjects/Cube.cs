/*using System.Numerics;

class Cube : IObject
{
    public Color color { get; set; }
    public Vector3[] vertices { get; set; }

    public Cube(Vector3[] vertices)
    {
        this.vertices = vertices;
    }

    public Vector3 GetNormalVector(Vector3 origin)
    {
        throw new NotImplementedException();
    }

    public bool RayIntersect(Vector3 origin, Vector3 direction) =>
        RayIntersectPoint(origin, direction) is not null;

    public Vector3? RayIntersectPoint(Vector3 origin, Vector3 direction)
    {
        Vector3 closest = vertices[0];
        float min = (closest - origin).Length();

        foreach (Vector3 vert in vertices)
        {
            if ((vert - origin).Length() < min)
            {
                closest = vert;
                min = (vert - origin).Length();
            }
        }

        if (Vector3.Dot(closest - origin, direction) < 0)
            return null;

        
    }

    public void ApplyForce(Vector3 force)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] += force;
        }
    }
}
*/
