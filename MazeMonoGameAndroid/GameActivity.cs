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
	public class GameActivity : AndroidGameActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SpaceGame.Activity = this;
			var g = new SpaceMaze.SpaceGame ();
			SetContentView (g.Window);
			g.Run ();
		}
		
	}
}


