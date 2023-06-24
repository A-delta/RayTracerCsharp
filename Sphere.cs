using System.Numerics;

class Sphere : ObjectInterface {
    public int radius;
    public Vector3 center;
    public Color color=Color.Black;
    public Sphere(int radius, Vector3 center){
        this.radius = radius;
        this.center=center;
    }

    public bool RayIntersect(Vector3 origin, Vector3 direction) {

        Vector3 L = center - origin;

        if (Vector3.Dot(L, direction) <= 0) {
            return L.Length() <= radius;
        }

        Vector3 H = origin +  Vector3.Dot(L, direction) * direction / direction.LengthSquared();
        Console.WriteLine(Vector3.Distance(H, center));
        Console.WriteLine(H);
        Console.WriteLine(center);
       return Vector3.Distance(H, center)<=radius; 


    }

    public bool IsInSphere(Vector3 pos) => Vector3.Distance(center, pos) <= radius;
}