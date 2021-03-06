﻿using System;
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
	public class MainMenuScreen : Screen
	{
		public MainMenuScreen() : base() {
			GuiObject newGame = new GuiObject ("New Game", "Roboto-Regular", 50);
			gui.AddObject (newGame);
			newGame.position = new Vector2 (physicalScreenSize.X / 2, 100);

			GuiObject editor = new GuiObject ("Editor", "Roboto-Regular", 50);
			gui.AddObject (editor);
			editor.position = new Vector2 (physicalScreenSize.X / 2, 170);
		}

		public override bool OnMouseDown (GuiObject obj, Vector2 p)
		{
			base.OnMouseDown (obj, p);
			if (obj.id == "New Game")
				SpaceGame.singleton.ChangeScreen (new GameScreen ());
			if (obj.id == "Editor")
				SpaceGame.singleton.ChangeScreen (new EditorMenuScreen ());
			return true;
		}
	}
}

