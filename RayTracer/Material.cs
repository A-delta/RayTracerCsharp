public class Material
{
    public float specularReflection,
        diffuseReflection,
        ambientReflection,
        shininess;
    public Color color;

    public Material(
        float specularReflection,
        float diffuseReflection,
        float ambientReflection,
        float shininess,
        Color color
    )
    {
        this.specularReflection = specularReflection;
        this.diffuseReflection = diffuseReflection;
        this.ambientReflection = ambientReflection;
        this.shininess = shininess;
        this.color = color;
    }
}
