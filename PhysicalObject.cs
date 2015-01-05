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
	class PhysicalObject : GameObject
	{
		public Vector2 velocity { get; set; }
		public float maxSpeed { get; set; }
		SortedDictionary<String, Vector2> forces;

		public PhysicalObject() : base() {}
		public PhysicalObject(String textureLocation) : base(textureLocation) {}
		public PhysicalObject(Texture2D directTexture) : base(directTexture) {}

		protected override void Init ()
		{
			base.Init ();
			forces = new SortedDictionary<string, Vector2> ();
			velocity = new Vector2 ();
			maxSpeed = float.MaxValue;
		}

		public override void Update ()
		{
			base.Update ();
			position += velocity;
			foreach (var forcePair in forces) {
				velocity += forcePair.Value;
			}
			if (velocity.Length() > maxSpeed) {
				velocity = Vector2.Multiply (Vector2.Normalize(velocity), maxSpeed);
			}
		}

		public void AddForce (String name, Vector2 force)
		{
			ModifyForce (name, force);
		}

		public void AddForce (String name, Vector2 force, float power)
		{
			ModifyForce (name, force, power);
		}

		public void RemoveForce(String name)
		{
			forces.Remove (name);
		}

		public void ModifyForce (String name, Vector2 force)
		{
			forces [name] = force;
		}

		public void ModifyForce (String name, Vector2 force, float power)
		{
			if (force.Length () > 0.0f && !float.IsNaN (power)) {
				force = Vector2.Multiply (Vector2.Normalize(force), power);
				ModifyForce (name, force);
			}
		}

		public void Stop() {
			forces.Clear ();
			velocity = new Vector2 ();
		}
	}
}

