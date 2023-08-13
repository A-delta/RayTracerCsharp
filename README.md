# RayTracerCsharp
A RayTracing renderer written in C#. \
I do not intend to "finish" this project, I used it to learn more about RayTracing and to get back to C# after a while. \
I *may* add refraction to the model as well as camera rotations later.

## Examples

### Simple image

![output](https://github.com/A-delta/RayTracerCsharp/assets/55986107/bd0b1453-93ce-4c72-b485-cdfcf04f6202) \
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

// scene objects
var objects = STLReader.ReadFile("lapin_ascii_max.stl");
objects.Add(new Sphere(15f, new Vector3(-45, 10, 50), new Material("emerald")));

//scene lights
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

## A illustrated evolution of the program

![](https://github.com/A-delta/RayTracerCsharp/blob/b662bd1d163d486fe75fc69e6113c5669ba47205/test.png?raw=true)
![](https://github.com/A-delta/RayTracerCsharp/blob/1dda5aea6d3228cafababe0ef6232605ef58e423/test.png?raw=true)
![image](https://github.com/A-delta/RayTracerCsharp/assets/55986107/cac182d4-304e-4b9a-a668-502000573a2f)
![image](https://github.com/A-delta/RayTracerCsharp/assets/55986107/3d30a098-b460-480e-b539-d69a4bad58b9)

![image](https://raw.githubusercontent.com/A-delta/RayTracerCsharp/776a0f3c34e9e4020f5f5450318b72e5d07171bb/test.gif)
![](https://raw.githubusercontent.com/A-delta/RayTracerCsharp/d719525921a29da665240e429fe5e6d3bb5b2926/test.gif)
![image](https://github.com/A-delta/RayTracerCsharp/assets/55986107/e4e71268-c712-4ffd-92b4-7c15139f15f3)

![](https://github.com/A-delta/RayTracerCsharp/blob/e490add93f8e2ef77c66027b68747a1aa7cad2a7/test.gif)


![variable_light](https://github.com/A-delta/RayTracerCsharp/assets/55986107/ddc3db3c-a723-4da7-b59c-ee16e2839bf1)


![](https://github.com/A-delta/RayTracerCsharp/blob/c781746ac761a62ce25e14031a499d0e372c1c36/RayTracerConsole/test.png?raw=true)


![](https://github.com/A-delta/RayTracerCsharp/blob/6b4117455dbac626baaca3c7076b738d9150d019/RayTracerConsole/test_position_camera.png?raw=true)


![image](https://github.com/A-delta/RayTracerCsharp/assets/55986107/d68a657b-bd6d-4fb7-aa39-510f01c9e8ba)
![image](https://github.com/A-delta/RayTracerCsharp/assets/55986107/8ab93c84-0cdb-4994-8c9c-51d88a88cf8e)
![image](https://github.com/A-delta/RayTracerCsharp/assets/55986107/27484128-7330-438d-af86-21ff457cf178)
![image](https://github.com/A-delta/RayTracerCsharp/assets/55986107/42df23c3-b2a4-4a7a-aad4-4001c4f2c0b6)
![image](https://github.com/A-delta/RayTracerCsharp/assets/55986107/a4f5f988-2832-4f4c-8e1d-0882e5cd57b5)
*b u g s*
![image](https://github.com/A-delta/RayTracerCsharp/assets/55986107/2510838f-99b2-41ae-b01c-89e1cfd1c96d)
![image](https://github.com/A-delta/RayTracerCsharp/assets/55986107/b3ca122e-844c-4775-bdfa-0da645f24661)



