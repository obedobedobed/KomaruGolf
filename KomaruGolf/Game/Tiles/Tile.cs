using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KomaruGolf.Tiles;

public class Tile
{
    protected Texture2D texture;
    public TileType Type { get; protected set; }
    public Vector2 Position;

    public const int TILE_SIZE = 60;
    public Rectangle Rectangle { get { return new Rectangle((int)Position.X, (int)Position.Y, TILE_SIZE, TILE_SIZE); } }

    // Wall parameters
    public float RicochetStrength { get; protected set; }

    // Ground parameters
    public float SpeedMultiplier { get; protected set; }

    // Finish parameters
    public bool isFinish { get; protected set; }

    public virtual void Load(Vector2 pos)
    {
        
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, Rectangle, Color.White);
    }
}