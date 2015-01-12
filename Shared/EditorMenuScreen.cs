using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

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
	public class EditorMenuScreen : Screen
	{
		public EditorMenuScreen() {
			GuiObject openButton = new GuiObject ("New", "Roboto-Regular");
			openButton.position = new Vector2 (300, 100);
			gui.AddObject (openButton);

			GuiObject newButton = new GuiObject ("Open", "Roboto-Regular");
			newButton.position = new Vector2 (500, 100);
			gui.AddObject (newButton);
		}

		public override bool OnMouseDown (GuiObject obj, Vector2 p)
		{
			base.OnMouseDown (obj, p);
			if (obj.id == "New")
				SpaceGame.singleton.ChangeScreen (new EditorScreen ());
			if (obj.id == "Open") {
				String str = Utils.OpenFile ();
				if (str != null)
					SpaceGame.singleton.ChangeScreen (new EditorScreen (str));
			}
			return true;
		}
	}
}

