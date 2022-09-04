using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace miniRPG
{
    public class Brevno
    {
        Vector2 origin;
        public static Rectangle brevnoRectangle;
        public static Texture2D _texture1;
        public Vector2 _position;
        public Brevno(Vector2 position)
        {
            _position = position;
        }
        public static void LoadContent(ContentManager content)
        {
            _texture1 = content.Load<Texture2D>("Day/Brevno");
        }
        public void Update()
        {
           brevnoRectangle = new Rectangle((int)_position.X - _texture1.Width / 2, (int)_position.Y - _texture1.Height / 2, _texture1.Width, _texture1.Height);
            if (_position.X < 0)
                _position.X = 0;
            if (_position.Y < 0)
                _position.Y = 0;
            if (_position.X > Game1.ScreenWidth - _texture1.Width)
                _position.X = Game1.ScreenWidth - _texture1.Width;
            if (_position.Y > Game1.ScreenHeight - _texture1.Height)
                _position.Y = Game1.ScreenHeight - _texture1.Height;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture1, _position, null, Color.White, 0, origin, 1f, SpriteEffects.None, 0);
        }

    }
}
