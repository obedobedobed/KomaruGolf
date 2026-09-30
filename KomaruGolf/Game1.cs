using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KomaruGolf;

public class Game1 : Game
{
    public GraphicsDeviceManager Graphics { get; private set; }
    private SpriteBatch _spriteBatch;

    public static Game1 Instance { get; private set; }

    private Scenes currentScene = Scenes.Menu;

    private MenuScene menuScene = new MenuScene();
    private GameScene gameScene = new GameScene();

    public static SpriteFont Font;
    public static SpriteFont BigFont;

    private const int VIRTUAL_WIDTH = 780;
    private const int VIRTUAL_HEIGHT = 480;
    private RenderTarget2D renderTarget;

    public Game1()
    {
        LogsSystem.Log("Called Game1 class constructor");
        Graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        Graphics.PreferredBackBufferWidth = 780;
        Graphics.PreferredBackBufferHeight = 480;

        int width = Graphics.PreferredBackBufferWidth;
        int height = Graphics.PreferredBackBufferHeight;
        LogsSystem.Log($"Running in {width}x{height} window");

    }

    protected override void Initialize()
    {
        LogsSystem.Log("Initializing...");

        // TODO: Add your initialization logic here

        currentScene = Scenes.Game;
        Instance = this;
        renderTarget = new RenderTarget2D(GraphicsDevice, VIRTUAL_WIDTH, VIRTUAL_HEIGHT);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        LogsSystem.Log("Loading content...");

        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        
        TextSystem.SetFont(Content.Load<Texture2D>("Sprites/Font"));
        Font = Content.Load<SpriteFont>("Fonts/Arial");
        BigFont = Content.Load<SpriteFont>("Fonts/BigArial");
        gameScene.Load(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        switch (currentScene)
        {
            case Scenes.Menu:
                break;
            case Scenes.Game:
                gameScene.Update(gameTime);
                break;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(renderTarget);
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        _spriteBatch.Begin();

        switch (currentScene)
        {
            case Scenes.Menu:
                break;
            case Scenes.Game:
                gameScene.Draw(_spriteBatch);
                break;
        }

        _spriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.Black);

        int winHeight = Graphics.PreferredBackBufferHeight;
        int winWidth = Graphics.PreferredBackBufferWidth;

        float sizeMod = (float)winHeight / VIRTUAL_HEIGHT;
        int xOffset = (int)((winWidth - VIRTUAL_WIDTH * sizeMod) / 2);

        _spriteBatch.Begin();
        _spriteBatch.Draw(renderTarget, new Rectangle(xOffset, 0, (int)(VIRTUAL_WIDTH * sizeMod), winHeight), Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
