using System.Numerics;

class Sphere : ObjectInterface {
    public int radius;
    public Vector3 center;
    public Color color=Color.Black;
    public Sphere(int radius, Vector3 center){
        this.radius = radius;
        this.center=center;
    }

    public float RayIntersect(Vector3 origin, Vector3 direction) { // negative output = no intersection

        Vector3 L = center - origin;

        if (Vector3.Dot(L, direction) < 0) return -1;
        
        

        Vector3 H = origin +  Vector3.Dot(L, direction) * direction / direction.LengthSquared();

        float d= (float)System.Math.Sqrt(L.LengthSquared() - (H-origin).LengthSquared());
        if (d>radius) return -1;
        return (H-origin).Length() - (radius*radius - d*d);
/*
       if (Vector3.Distance(H, center)>radius) return -1;

        float d = (float)System.Math.Sqrt(radius*radius - H.LengthSquared());

        return (H+d*direction - center).Length();
        */
    }

    public bool IsInSphere(Vector3 pos) => Vector3.Distance(center, pos) <= radius;
}