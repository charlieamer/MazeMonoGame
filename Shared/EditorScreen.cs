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
	public class EditorScreen : Screen
	{
		public EditorScreen() {
		}

		public EditorScreen(String filePath) {
			GuiObject obj = new GuiObject (filePath, "Roboto-Regular");
			obj.position = new Vector2 (100, 100);
			gui.AddObject (obj);
		}
	}
}

