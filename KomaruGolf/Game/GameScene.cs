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
    private Texture2D pixel;

    private int score = 0;
    private Vector2 scorePos = new Vector2(6, 2);

    private int seconds;
    private Vector2 timePos = new Vector2(6, 40);
    private float timeToCountSecond = 1f;

    private Color rainbowColorNow;

    public static GameScene Instance { get; private set; }

    private float elapsedTime = 0f;

    private bool endScreen = false;

    public void Load(ContentManager Content)
    {
        Instance = this;

        ball = new Ball(Content.Load<Texture2D>("Sprites/Komaru"), Content.Load<Texture2D>("Sprites/Pixel"));
        TilesTextures.Load(Content);
        Tiles = LevelBuilder.Build(File.ReadAllLines("Content/Levels/0.lv"));

        pixel = Content.Load<Texture2D>("Sprites/Pixel");
    }

    public void Update(GameTime gameTime)
    {
        elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (!endScreen && (timeToCountSecond -= elapsedTime) <= 0)
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

    public void EndGame()
    {
        endScreen = true;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var tile in Tiles)
            tile.Draw(spriteBatch);
        
        ball.Draw(spriteBatch);

        DrawRainbowString(spriteBatch, Game1.Font, $"Score: {score}", scorePos, Color.White);
        DrawRainbowString(spriteBatch, Game1.Font, $"Time: {seconds}s", timePos, Color.White);

        if (endScreen)
        {
            spriteBatch.Draw(pixel, new Rectangle(0, 0, 1000, 1000), Color.Black * 0.5f);

            string endText = "Level Complete!";
            string endScoreText = $"Score: {score}";
            string endTimeText = $"Time: {seconds} sec";

            float endStringXOffset = Game1.BigFont.MeasureString(endText).X / 2f;
            float endScoreStringXOffset = Game1.Font.MeasureString(endScoreText).X / 2f;
            float endTimeStringXOffset = Game1.Font.MeasureString(endTimeText).X / 2f;

            DrawRainbowString(spriteBatch, Game1.BigFont, endText, new Vector2(
                Game1.Instance.Graphics.PreferredBackBufferWidth / 2f - endStringXOffset,
                Game1.Instance.Graphics.PreferredBackBufferHeight / 2f - 200),
                Color.White);

            DrawRainbowString(spriteBatch, Game1.Font, endScoreText, new Vector2(
                Game1.Instance.Graphics.PreferredBackBufferWidth / 2f - endScoreStringXOffset,
                Game1.Instance.Graphics.PreferredBackBufferHeight / 2f + 50),
                Color.White);

            DrawRainbowString(spriteBatch, Game1.Font, endTimeText, new Vector2(
                Game1.Instance.Graphics.PreferredBackBufferWidth / 2f - endTimeStringXOffset,
                Game1.Instance.Graphics.PreferredBackBufferHeight / 2f + 90),
                Color.White);
        }
    }

    public void DrawRainbowString(SpriteBatch spriteBatch, SpriteFont font, string str, Vector2 position, Color color)
    {
        spriteBatch.DrawString(font, str, position + new Vector2(2, 2), rainbowColorNow);
        spriteBatch.DrawString(font, str, position, color);
    }
}