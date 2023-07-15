using System.Numerics;
using System.Text.Json;

public class Material
{
    public Vector4 emissiveComponent,
        specularComponent,
        ambientComponent,
        diffuseComponent;
    public float shininess;

    public Material(string materialName)
    {
        using (
            Stream stream = File.Open("../RayTracer/materials.json", FileMode.Open, FileAccess.Read)
        )
        {
            Dictionary<string, List<List<string>>> data = JsonSerializer.Deserialize<
                Dictionary<string, List<List<string>>>
            >(stream);
            if (data.ContainsKey(materialName))
            {
                var components = data[materialName];

                ambientComponent.X = float.Parse(components[0][0]);
                ambientComponent.Z = float.Parse(components[0][2]);
                ambientComponent.Y = float.Parse(components[0][1]);
                ambientComponent.W = 0;

                diffuseComponent.X = float.Parse(components[1][0]);
                diffuseComponent.Y = float.Parse(components[1][1]);
                diffuseComponent.Z = float.Parse(components[1][2]);
                diffuseComponent.W = 0;

                specularComponent.X = float.Parse(components[2][0]);
                specularComponent.Y = float.Parse(components[2][1]);
                specularComponent.Z = float.Parse(components[2][2]);
                specularComponent.W = 0;

                shininess = float.Parse(components[3][0]);
                return;
            }
            throw new Exception("This material isn't listed");
        }
    }
}
