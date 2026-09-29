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

    public Game1()
    {
        Graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        currentScene = Scenes.Game;
        Instance = this;

        base.Initialize();
    }

    protected override void LoadContent()
    {
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
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        _spriteBatch.Begin(blendState: BlendState.AlphaBlend);

        switch (currentScene)
        {
            case Scenes.Menu:
                break;
            case Scenes.Game:
                gameScene.Draw(_spriteBatch);
                break;
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
