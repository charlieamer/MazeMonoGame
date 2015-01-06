using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SpaceMaze;

using Microsoft.Xna.Framework;

namespace MazeMonoGameAndroid
{
	[Activity (Label = "MazeMonoGameAndroid", 
		MainLauncher = true,
		Icon = "@drawable/icon",
		Theme = "@style/Theme.Splash",
		AlwaysRetainTaskState = true,
		LaunchMode = Android.Content.PM.LaunchMode.SingleInstance,
		ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation |
		Android.Content.PM.ConfigChanges.KeyboardHidden |
		Android.Content.PM.ConfigChanges.Keyboard)]
	public class MazeGameActivity : AndroidGameActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create our OpenGL view, and display it
			//SpaceGame.Activity = this;
			Game.Activity = this;
			SpaceGame g = new SpaceGame ();
			SetContentView (g.Window);
			g.Run ();
		}
		
	}
}


