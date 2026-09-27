using System;
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

    private int seconds;
    private Vector2 timePos = new Vector2(6, 40);
    private float timeToCountSecond = 1f;

    private Color rainbowColorNow;

    public static GameScene Instance { get; private set; }

    private float elapsedTime = 0f;

    public void Load(ContentManager Content)
    {
        Instance = this;

        ball = new Ball(Content.Load<Texture2D>("Sprites/Komaru"), Content.Load<Texture2D>("Sprites/Pixel"));
        TilesTextures.Load(Content);
        Tiles = LevelBuilder.Build(File.ReadAllLines("Content/Levels/0.lv"));
    }

    public void Update(GameTime gameTime)
    {
        elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        if ((timeToCountSecond -= elapsedTime) <= 0)
        {
            seconds++;
            timeToCountSecond = 1f;
        }

        ball.Update(gameTime);

        float totalTime = (float)gameTime.TotalGameTime.TotalSeconds;
        rainbowColorNow = new Color
        (
            (float)MathF.Sin(totalTime * 2) * 0.5f + 0.5f,
            (float)MathF.Sin(totalTime * 2 + 2) * 0.5f + 0.5f,
            (float)MathF.Sin(totalTime * 2 + 4) * 0.5f + 0.5f
        );
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

        // Score
        spriteBatch.DrawString(Game1.Font, $"Score: {score}", scorePos + new Vector2(2, 2), rainbowColorNow);
        spriteBatch.DrawString(Game1.Font, $"Score: {score}", scorePos, Color.White);

        // Time
        spriteBatch.DrawString(Game1.Font, $"Time: {seconds}s", timePos + new Vector2(2, 2), rainbowColorNow);
        spriteBatch.DrawString(Game1.Font, $"Time: {seconds}s", timePos, Color.White);
    }
}