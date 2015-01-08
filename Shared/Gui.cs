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
	public class Gui : Drawable
	{
		List<GuiObject> objects;
		public GuiListener listener { get; set; }
		GuiObject selectedObject;
		public Gui ()
		{
			objects = new List<GuiObject> ();
			isUnscaled = true;
		}
		public void AddObject(GuiObject obj) {
			objects.Add (obj);
			AppendChild (obj);
		}
		public bool CallOnTouchMove(GuiObject obj, Vector2 location) {
			bool a = obj.OnMouseMove (obj, location - obj.position);
			if (a)
				return true;
			bool b = (listener != null) ? listener.OnMouseMove(obj, location - obj.position) : false;
			return b;
		}
		public bool CallOnTouchDown(GuiObject obj, Vector2 location) {
			bool a = obj.OnMouseDown (obj, location - obj.position);
			if (a)
				return true;
			bool b = (listener != null) ? listener.OnMouseDown(obj, location - obj.position) : false;
			return b;
		}
		public bool CallOnTouchUp(GuiObject obj, Vector2 location) {
			bool a = obj.OnMouseUp (obj, location - obj.position);
			if (a)
				return true;
			bool b = (listener != null) ? listener.OnMouseUp(obj, location - obj.position) : false;
			return b;
		}
		public bool OnTouchMove(Vector2 location) {
			if (selectedObject != null)
				CallOnTouchMove (selectedObject, location);
			else {
				foreach (var obj in objects) {
					if (obj.bounds.Contains (location))
						if (CallOnTouchMove (obj, location))
							return true;
				}
			}
			return false;
		}
		public bool OnTouchDown(Vector2 location) {
			Point p = new Point ((int)location.X, (int)location.Y);
			foreach (var obj in objects) {
				if (obj.bounds.Contains (p)) {
					if (CallOnTouchDown (obj, location)) {
						selectedObject = obj;
						return true;
					}
				}
			}
			return false;
		}
		public bool OnTouchUp(Vector2 location) {
			bool ret = false;
			if (selectedObject != null)
				ret = CallOnTouchUp (selectedObject, location);
			selectedObject = null;
			return ret;
		}
		public override void Draw ()
		{
			base.Draw ();
		}
	}
}

