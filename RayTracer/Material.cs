public class Material
{
    public float specularReflection,
        diffuseReflection,
        ambientReflection,
        shininess;

    public Material(
        float specularReflection,
        float diffuseReflection,
        float ambientReflection,
        float shininess
    )
    {
        this.specularReflection = specularReflection;
        this.diffuseReflection = diffuseReflection;
        this.ambientReflection = ambientReflection;
        this.shininess = shininess;
    }
}
