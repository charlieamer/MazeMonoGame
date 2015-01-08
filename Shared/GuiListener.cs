using System;

using Microsoft.Xna.Framework;
namespace SpaceMaze
{
	public interface GuiListener
	{
		bool OnMouseDown(GuiObject obj, Vector2 p);
		bool OnMouseMove(GuiObject obj, Vector2 p);
		bool OnMouseUp(GuiObject obj, Vector2 p);
	}
}

