using System.Numerics;

class Renderer {
    public Image<Rgb24> img;
    public double fieldViewAngle =  35f;
    public Vector3 position;
    public Vector3 roation;
    public int height;
    public int width;

    public Renderer(int width, int height, Vector3 position, Vector3 roation, Image<Rgb24> img) {
        this.height=height;
        this.width=width;
        this.position=position;
        this.roation=roation;
        this.img=img;
    }

    public void Render(Sphere s) { // one sphere for now
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                img[j, i] = new Rgb24((byte)(255*i/width), (byte)(0), (byte)(255*j/width));

            }
            
        }
    }

    public void Save(string filename) => img.SaveAsPng(filename);

}