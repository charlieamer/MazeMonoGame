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

//using MonoMac.Foundation;
using MonoMac.AppKit;

namespace SpaceMaze
{
	public partial class Utils
	{
		public static string OpenFile(String [] allowedFileTypes = null) {
			NSOpenPanel panel = new NSOpenPanel ();
			panel.ReleasedWhenClosed = true;
			panel.Prompt = "Open File";
			panel.ParentWindow = SpaceGame.singleton.Window.Window;
			if (allowedFileTypes != null)
				panel.AllowedFileTypes = allowedFileTypes;
			if (panel.RunModal() == 1)
				return panel.Url.AbsoluteString;
			return null;
		}
	}
}

