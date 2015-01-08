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
	public class GuiObject : GameObject, GuiListener
	{
		ImageFont font;
		public String id { get; protected set; }
		public GuiObject (String text, String fontName, Color color, float size = 50.0f) : base() {
			font = ImageFont.Load (fontName);
			texture = font.WriteText (text, size);
			isUnscaled = true;
			id = text;
		}
		public GuiObject(String text, String fontName, float size = 50.0f) : this(text, fontName, Color.White, size) {
		}

		public bool selected { get; protected set; }

		#region GuiListener implementation
		public bool OnMouseDown (GuiObject obj, Vector2 p)
		{
			selected = true;
			return false;
		}
		public bool OnMouseMove (GuiObject obj, Vector2 p)
		{
			return false;
		}
		public bool OnMouseUp (GuiObject obj, Vector2 p)
		{
			selected = false;
			return false;
		}
		#endregion
	}
}

