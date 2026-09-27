using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KomaruGolf;

public static class TextSystem
{
    private static Dictionary<char, int> charsIndexes = new Dictionary<char, int>
    {
        {'a', 0},
        {'b', 1},
        {'c', 2},
        {'d', 3},
        {'e', 4},
        {'f', 5},
        {'g', 6},
        {'h', 7},
        {'i', 8},
        {'j', 9},
        {'k', 10},
        {'l', 11},
        {'m', 12},
        {'n', 13},
        {'o', 14},
        {'p', 15},
        {'q', 16},
        {'r', 17},
        {'s', 18},
        {'t', 19},
        {'u', 20},
        {'v', 21},
        {'w', 22},
        {'x', 23},
        {'y', 24},
        {'z', 25},
        {'0', 26},
        {'1', 27},
        {'2', 28},
        {'3', 29},
        {'4', 30},
        {'5', 31},
        {'6', 32},
        {'7', 33},
        {'8', 34},
        {'9', 35}
    };

    private static Dictionary<char, int> charsPixelsWidth = new Dictionary<char, int>
    {
        {'a', 13},
        {'b', 12},
        {'c', 12},
        {'d', 12},
        {'e', 11},
        {'f', 10},
        {'g', 13},
        {'h', 12},
        {'i', 2},
        {'j', 9},
        {'k', 13},
        {'l', 10},
        {'m', 14},
        {'n', 11},
        {'o', 13},
        {'p', 11},
        {'q', 10},
        {'r', 12},
        {'s', 12},
        {'t', 12},
        {'u', 12},
        {'v', 12},
        {'w', 18},
        {'x', 12},
        {'y', 12},
        {'z', 11},
        {'0', 10},
        {'1', 9},
        {'2', 10},
        {'3', 10},
        {'4', 10},
        {'5', 10},
        {'6', 10},
        {'7', 9},
        {'8', 10},
        {'9', 10}
    };

    private const int TEXT_SCALE = 2;
    private static Texture2D fontTexture;
    public static void SetFont(Texture2D texture) => fontTexture = texture;
    private const int CHARS_Y_SIZE = 14;
    private const int MAX_CHAR_X_SIZE = 18;
    private const int CHARS_SPACE = 2 * TEXT_SCALE;
    private const int SPACE_WIDTH = 8 * TEXT_SCALE;

    public static void DrawString(string str, SpriteBatch spriteBatch, Vector2 position)
    {
        float xOffset = 0f;

        foreach (var chr in str.ToLower())
        {
            if (chr != ' ')
            {
                int index = charsIndexes[chr];
                int width = charsPixelsWidth[chr] * TEXT_SCALE;

                spriteBatch.Draw(fontTexture, new Rectangle
                ((int)(position.X + xOffset), (int)position.Y, width, CHARS_Y_SIZE * TEXT_SCALE), new Rectangle
                (0, CHARS_Y_SIZE * index + index, MAX_CHAR_X_SIZE, CHARS_Y_SIZE), Color.White);
                xOffset += width + CHARS_SPACE;
            }
            else xOffset += SPACE_WIDTH;
        }        
    }
}