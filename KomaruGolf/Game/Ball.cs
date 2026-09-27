using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KomaruGolf;

public class Ball (Texture2D texture, Texture2D pixelTexture)
{
    public Vector2 Position { get; private set; } = new Vector2(400, 200);
    public Vector2 Size { get; private set; } = new Vector2(60, 60);
    private Texture2D texture = texture;
    private Texture2D pixelTexture = pixelTexture;
    private int dirLineWidth = 2;

    private bool targeting = false;

    public Rectangle Rectangle { get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y); } }
    public Vector2 CenteredPosition { get { return new Vector2(Position.X + Size.X / 2, Position.Y + Size.Y / 2); } }

    private float speed = 0f;
    private float distance = 0f;
    private float finalDistance = 0f;
    private float maxDistance = 200f;
    private Vector2 direction;
    private float rotation = 0f;

    private float speedMod = 2f;
    private float speedLoseMode = 100f;

    private float elapsedTime = 0f;

    private MouseState lastMouse;
    private bool controllsEnabled = true;

    private bool finishing = false;
    private float alpha = 1f;

    private Point ricochetScoreRange = new Point(10, 15);
    private Point finishScoreRange = new Point(100, 110);

    public void Update(GameTime gameTime)
    {
        elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (speed != 0f)
            Move();

        if (finishing)
            FinishAnim();

        var mouse = Mouse.GetState();

        var mousePos = new Vector2(mouse.X, mouse.Y);
        var lastMousePos = new Vector2(lastMouse.X, lastMouse.Y);

        if (controllsEnabled)
        {
            if (mouse.LeftButton == ButtonState.Pressed && lastMouse.LeftButton == ButtonState.Pressed)
            { 
                if (mousePos != lastMousePos && speed == 0f)
                {
                    direction = Vector2.Normalize(CenteredPosition - mousePos);
                    distance = Vector2.Distance(CenteredPosition, mousePos);
                    finalDistance = distance < maxDistance ? distance : maxDistance;
                    rotation = MathF.Atan2(direction.Y, direction.X) - (float)Math.PI / 2;
                }
            }
            else if (mouse.LeftButton == ButtonState.Released && lastMouse.LeftButton == ButtonState.Pressed && speed == 0f)
            {
                speed = finalDistance * speedMod;
                LogsSystem.Log($"Ball Knock [Speed: {speed}; Direction: x{direction.X}, y{direction.Y}]");
            }
        }

        lastMouse = mouse;
    }

    private void Move()
    {
        float tileSpeedMod = 1f;
        float constantTileSpeedMod = 1f;

        foreach (var tile in GameScene.Instance.Tiles)
        {
            if (tile.Rectangle.Intersects(Rectangle) && tile.Type == Tiles.TileType.Ground)
            {
                // tileSpeedMod = tile.SpeedMultiplier <= tileSpeedMod ? tile.SpeedMultiplier : tileSpeedMod;
                constantTileSpeedMod = tile.SpeedMultiplier;
            }
        }

        speed *= constantTileSpeedMod;

        var nextPos = Position + direction * speed * elapsedTime * tileSpeedMod;
        var nextRect = new Rectangle(nextPos.ToPoint(), Size.ToPoint());

        bool checkedRicochet = false;

        foreach (var tile in GameScene.Instance.Tiles)
        {
            if (tile.Rectangle.Intersects(nextRect))
            {
                switch (tile.Type)
                {
                    case Tiles.TileType.Wall:
                        if (checkedRicochet)
                            break;

                        BallDir ballDir = BallDir.None;

                        if (direction.X >= 0 && direction.Y >= 0)
                            ballDir = BallDir.BottomRight;
                        else if (direction.X <= 0 && direction.Y >= 0)
                            ballDir = BallDir.BottomLeft;
                        else if (direction.X >= 0 && direction.Y <= 0)
                            ballDir = BallDir.TopRight;
                        else if (direction.X <= 0 && direction.Y <= 0)
                            ballDir = BallDir.TopLeft;

                        LogsSystem.Log($"Collision with wall. Calculating ricochet (current dir: x{direction.X} y{direction.Y})");
                        WallCollisionSide colSide = WallCollisionSide.None;

                        float WallXIntersection = 0f;
                        float WallYIntersection = 0f;

                        switch (ballDir)
                        {
                            case BallDir.TopRight:
                                WallXIntersection = tile.Rectangle.Left - Rectangle.Right;
                                WallYIntersection = Rectangle.Top - tile.Rectangle.Bottom;
                                break;
                            case BallDir.TopLeft:
                                WallXIntersection = Rectangle.Left - tile.Rectangle.Right;
                                WallYIntersection = Rectangle.Top - tile.Rectangle.Bottom;
                                break;
                            case BallDir.BottomRight:
                                WallXIntersection = tile.Rectangle.Left - Rectangle.Right;
                                WallYIntersection = tile.Rectangle.Top - Rectangle.Bottom;
                                break;
                            case BallDir.BottomLeft:
                                WallXIntersection = Rectangle.Left - tile.Rectangle.Right;
                                WallYIntersection = tile.Rectangle.Top - Rectangle.Bottom;
                                break;
                        }

                        if (WallXIntersection > WallYIntersection)
                            colSide = WallCollisionSide.Horizontal;
                        else colSide = WallCollisionSide.Vertical;

                        direction = CalculateRicochet(direction, colSide);

                        string side = string.Empty;
                        switch (colSide)
                        {
                            case WallCollisionSide.Vertical:
                                side = "Vertical";
                                break;
                            case WallCollisionSide.Horizontal:
                                side = "Horizontal";
                                break;
                            case WallCollisionSide.None:
                                side = "Unknown";
                                break;
                        }
                        LogsSystem.Log($"Calculated ricochet: x{direction.X} y{direction.Y}; side: {side}");
                        GameScene.Instance.ScoreUp(Random.Shared.Next(ricochetScoreRange.X, ricochetScoreRange.Y));

                        checkedRicochet = true;
                        break;
                    case Tiles.TileType.Ground:
                        if (tile.isFinish)
                        {
                            StartFinishAnim(tile.Position);
                            return;
                        }
                        break;
                }
            }
        }

        Position += direction * speed * elapsedTime * tileSpeedMod;

        speed -= speedLoseMode * elapsedTime;
        if (speed < 0f) speed = 0f;
    }

    private Vector2 CalculateRicochet(Vector2 dir, WallCollisionSide colSide)
    {
        switch (colSide)
        {
            case WallCollisionSide.Vertical:
                return new Vector2(dir.X, -dir.Y);
            case WallCollisionSide.Horizontal:
                return new Vector2(-dir.X, dir.Y);
            default:
                return Vector2.Zero;
        }
    }

    private void StartFinishAnim(Vector2 finishPos)
    {
        speed = 0;
        Position = finishPos;
        finishing = true;
        controllsEnabled = false;
        GameScene.Instance.ScoreUp(Random.Shared.Next(finishScoreRange.X, finishScoreRange.Y));
        Console.WriteLine("Finish entered");
    }

    private void FinishAnim()
    {
        alpha -= 1f * elapsedTime;
        if (alpha <= 0f)
            alpha = 0f;

        Size -= new Vector2(1f, 1f);
        Position += new Vector2(0.5f, 0.5f);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, Rectangle, Color.White * alpha);

        var mouse = Mouse.GetState();
        if (distance != 0 && mouse.LeftButton == ButtonState.Pressed && speed == 0f && controllsEnabled)
            spriteBatch.Draw(pixelTexture, new Rectangle((int)CenteredPosition.X, (int)CenteredPosition.Y, dirLineWidth,
            (int)(finalDistance / 2)), null, Color.Red, rotation, new Vector2(-0.6f), SpriteEffects.None, 0f);
    }
}