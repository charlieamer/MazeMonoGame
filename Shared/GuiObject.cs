using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace SpaceMaze
{
	class GuiObject : GameObject
	{
		ImageFont font;
		public GuiObject (Texture2D tex) : base(tex) {}
		public GuiObject (String textureLocation) : base(textureLocation) {}
		public GuiObject (String text, SpriteFont font) : base() {
			texture = Utils.TextToTexture (text, font);
		}
		public GuiObject (String text, SpriteFont font, Color color) : base() {
			texture = Utils.TextToTexture (text, font, color);
		}
		public GuiObject (String text, String fontName, Color color, float size = 50.0f) : base() {
			font = new ImageFont (fontName);
			texture = font.WriteText (text, size);
		}
	}
}

