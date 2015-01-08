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
	class MainMenuScreen : Screen
	{
		public MainMenuScreen() : base() {
			GuiObject newGame = new GuiObject ("New Game", "Roboto-Regular", Color.White);
			gui.AddObject (newGame);
			newGame.position = new Vector2 (physicalScreenSize.X / 2, 100);
		}

		public override bool OnMouseDown (GuiObject obj, Vector2 p)
		{
			base.OnMouseDown (obj, p);
			if (obj.id == "New Game")
				SpaceGame.singleton.ChangeScreen (new GameScreen ());
			return true;
		}
	}
}

