using System.Collections.Generic;
using System.IO;
using KomaruGolf.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KomaruGolf;

public class GameScene
{
    private Ball ball;
    public List<Tile> Tiles { get; private set; } = new List<Tile>();

    private int score = 0;
    private Vector2 scorePos = new Vector2(6, 2);

    public static GameScene Instance { get; private set; }

    public void Load(ContentManager Content)
    {
        Instance = this;

        ball = new Ball(Content.Load<Texture2D>("Sprites/Komaru"), Content.Load<Texture2D>("Sprites/Pixel"));
        TilesTextures.Load(Content);
        Tiles = LevelBuilder.Build(File.ReadAllLines("Content/Levels/0.lv"));
    }

    public void Update(GameTime gameTime)
    {
        ball.Update(gameTime);
    }

    public void ScoreUp(int score)
    {
        this.score += score;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var tile in Tiles)
            tile.Draw(spriteBatch);
        
        ball.Draw(spriteBatch);
        spriteBatch.DrawString(Game1.Font, $"Score: {score}", scorePos, Color.White);
    }
}