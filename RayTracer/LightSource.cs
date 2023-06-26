using System.Numerics;

public class LightSource
{
    public Vector3 position;
    public float specularIntensity,
        diffuseIntensity;

    public LightSource(Vector3 position, float specularIntensity, float diffuseIntensity)
    {
        this.position = position;
        this.specularIntensity = specularIntensity;
        this.diffuseIntensity = diffuseIntensity;
    }
}
