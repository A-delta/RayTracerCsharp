using System.Numerics;

class LightSource
{
    public Vector3 position;
    public float intensity;

    public LightSource(Vector3 position, float intensity)
    {
        this.position = position;
        this.intensity = intensity;
    }
}
