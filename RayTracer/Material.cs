using System.Numerics;
using System.Text.Json;

public class Material
{
    public Vector4 emissiveComponent,
        specularComponent,
        ambientComponent,
        diffuseComponent,
        albedo;
    public float shininess;

    public Material(string materialName, string materialsPath)
    {
        try
        {
            using (Stream stream = File.Open(materialsPath, FileMode.Open, FileAccess.Read))
            {
                var data = JsonSerializer.Deserialize<Dictionary<string, List<List<string>>>>(stream);
                if (data.ContainsKey(materialName))
                {
                    var components = data[materialName];

                    ambientComponent.X = float.Parse(components[0][0]);
                    ambientComponent.Z = float.Parse(components[0][2]);
                    ambientComponent.Y = float.Parse(components[0][1]);
                    ambientComponent.W = 0;

                    specularComponent.X = float.Parse(components[1][0]);
                    specularComponent.Y = float.Parse(components[1][1]);
                    specularComponent.Z = float.Parse(components[1][2]);
                    specularComponent.W = 0;

                    diffuseComponent.X = float.Parse(components[2][0]);
                    diffuseComponent.Y = float.Parse(components[2][1]);
                    diffuseComponent.Z = float.Parse(components[2][2]);
                    diffuseComponent.W = 0;

                    albedo.X = float.Parse(components[3][0]); // reflec
                    albedo.Y = float.Parse(components[3][1]); // diff
                    albedo.Z = float.Parse(components[3][2]); // spec
                    albedo.W = 0; // refrac

                    shininess = 128 * float.Parse(components[4][0]);
                    return;
                }
                Console.Error.WriteLine("This material isn't listed in the provided file");
                Environment.Exit(20);
            }
        }
        catch (DirectoryNotFoundException)
        {
            Console.Error.WriteLine("Materials file not found");
            Environment.Exit(20);
        }
    }
}
