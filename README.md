# RayTracerCsharp
A RayTracing renderer written in C#. \
I do not intend to "finish" this project, I use it to learn more about RayTracing and to get back to C#after a while. \
I *may* add refraction to the model as well as camera rotations.

## Examples

### Simple Image

![output](https://github.com/A-delta/RayTracerCsharp/assets/55986107/bd0b1453-93ce-4c72-b485-cdfcf04f6202)
The STLReader only reads ASCII STL files, the file used is not provided (there are plenty available for free online).
```cs

int height = 1080;
int width = 1080;
Renderer rd = new Renderer(
    width,
    height,
    new Vector3(-50, 0, 300),
    new Vector3(0, 0, 0)
);

var objects = STLReader.ReadFile("lapin_ascii_max.stl");
objects.Add(new Sphere(15f, new Vector3(-45, 10, 50), new Material("emerald")));

var lights = new List<LightSource>();
lights.Add(
    new LightSource()
    {
        position = rd.position + new Vector3(0, 0, -10),
        ambientComponent = new Vector4(.2f, .2f, .2f, .2f),
        DiffuseComponent = new Vector4(1f, 1f, 1f, 1f),
        SpecularComponent = new Vector4(1f, 1f, 1f, 1f)
    }
);
AddRectangularLight(lights, 5f, new Vector3(-30, -65, 100), new Vector3(30, -65, 100));
var img = rd.Render(objects, lights, new Image<Rgb24>(width, height));
img.SaveAsPng("output.png");
```



### A more complex animation
https://github.com/A-delta/RayTracerCsharp/assets/55986107/6e54da94-d585-47bf-802f-2c359aa153d3

*(Didn't save the code)*


