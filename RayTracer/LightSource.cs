using System.Numerics;

public class LightSource
{
    public Vector4 AmbientComponent,
        SpecularComponent,
        DiffuseComponent;

    public IObject Body;

    public LightSource(Vector4 lightLevel, Vector3 position, Material mat)
    {
        AmbientComponent = lightLevel * .1f;
        SpecularComponent = lightLevel;
        DiffuseComponent = lightLevel;
        Body = new Sphere(5f, position, mat);
    }
}
