using System;

namespace MazeMonoGameWindows
{
	class MainClass
	{
		[STAThread]
		public static void Main (string[] args)
		{
			SpaceMaze.SpaceGame game = new SpaceMaze.SpaceGame ();
			game.Run ();
		}
	}
}
