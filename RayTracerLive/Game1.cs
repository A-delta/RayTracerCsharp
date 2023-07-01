using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Numerics;
using System.Diagnostics;

namespace RayTracerLive;

public class Game1 : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    Texture2D image;
    int height;
    int width;
    List<IObject> objects;
    List<LightSource> lightSources;
    Renderer rd;
    System.IO.Stream stream;

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
        height = 600;
        width = 1000;

        graphics.PreferredBackBufferWidth = width; // set this value to the desired width of your window
        graphics.PreferredBackBufferHeight = height; // set this value to the desired height of your window
        graphics.ApplyChanges();

        Material m = new Material(.3f, .6f, 0f, 1f, SixLabors.ImageSharp.Color.AliceBlue);

        objects = new List<IObject>();
        objects.Add(new Sphere(radius: 15, new System.Numerics.Vector3(x: 0, y: 50, -200), m));

        lightSources = new List<LightSource>();
        lightSources.Add(new LightSource(new System.Numerics.Vector3(0, 50, 0), 1f, 1f));

        rd = new Renderer(
            width,
            height,
            new System.Numerics.Vector3(0, 0, 0),
            new System.Numerics.Vector3(0, 0, 0)
        );

        spriteBatch = new SpriteBatch(GraphicsDevice);
        this.IsFixedTimeStep = true;
        base.Initialize();
    }

    protected override void LoadContent()
    {
        var img = rd.Render(objects, lightSources, new Image<Rgb24>(width, height));
        stream = new System.IO.MemoryStream();
        img.Save(stream: stream, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder());

        image = Texture2D.FromStream(graphicsDevice: graphics.GraphicsDevice, stream);

        stream.Dispose();
    }

    protected override void UnloadContent()
    {
        //texture.Dispose(); <-- Only directly loaded
        Content.Unload();
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (Keyboard.GetState().IsKeyDown(Keys.Down))
        {
            objects[0].ApplyForce(new System.Numerics.Vector3(0, -1, 0));
            LoadContent();
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Up))
        {
            objects[0].ApplyForce(new System.Numerics.Vector3(0, +1, 0));
            LoadContent();
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Left))
        {
            objects[0].ApplyForce(new System.Numerics.Vector3(-1, 0, 0));
            LoadContent();
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Right))
        {
            objects[0].ApplyForce(new System.Numerics.Vector3(1, 0, 0));
            LoadContent();
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

        spriteBatch.Begin();
        spriteBatch.Draw(image, System.Numerics.Vector2.Zero, Microsoft.Xna.Framework.Color.White);
        spriteBatch.End();

        base.Draw(gameTime);
    }
}
