using System.Numerics;

public static class STLReader
{
    public static List<IObject> ReadFile(string filename)
    {
        var meshes = new List<IObject>();
        var stream = new StreamReader(filename);

        string solidName = (stream.ReadLine() ?? "").Split(" ")[1];

        int limit = 26;
        while (!stream.EndOfStream && limit > 0)
        {
            limit--;
            var data = (stream.ReadLine() ?? "").Trim();
            if (data.Contains("endsolid"))
                break;

            // else data contains the normal vector coords



            data = stream.ReadLine(); // "outer loop"
            data = (stream.ReadLine() ?? "").Trim();
            var coords = data.Split(' ');
            var A = new Vector3(
                float.Parse(coords[1]),
                float.Parse(coords[2]),
                float.Parse(coords[3])
            );

            data = (stream.ReadLine() ?? "").Trim();
            coords = data.Split(' ');
            var B = new Vector3(
                float.Parse(coords[1]),
                float.Parse(coords[2]),
                float.Parse(coords[3])
            );

            data = (stream.ReadLine() ?? "").Trim();
            coords = data.Split(' ');
            var C = new Vector3(
                float.Parse(coords[1]),
                float.Parse(coords[2]),
                float.Parse(coords[3])
            );
            data = stream.ReadLine(); // jump endloop
            data = stream.ReadLine(); // jump endfacet
            data = stream.ReadLine(); // jump \n

            meshes.Add(new Triangle(A, B, C, new Material("ruby")));
        }
        return meshes;
    }
}
