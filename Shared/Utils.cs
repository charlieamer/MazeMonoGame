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
	class Utils
	{
		public static Texture2D CreateCircle(int radius, Color color)
		{
			int outerRadius = radius*2 + 2; // So circle doesn't go out of bounds
			Texture2D texture = new Texture2D(SpaceGame.singleton.spriteBatch.GraphicsDevice, outerRadius, outerRadius);

			Color[] data = new Color[outerRadius * outerRadius];

			// Colour the entire texture transparent first.
			for (int i = 0; i < data.Length; i++)
				data[i] = Color.TransparentBlack;

			// Work out the minimum step necessary using trigonometry + sine approximation.
			double angleStep = 1f/radius;

			for (double angle = 0; angle < Math.PI*2; angle += angleStep)
			{
				// Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
				int x = (int)Math.Round(radius + radius * Math.Cos(angle));
				int y = (int)Math.Round(radius + radius * Math.Sin(angle));

				data[y * outerRadius + x + 1] = Color.White;
			}

			texture.SetData(data);
			return texture;
		}

		public static Texture2D CreateCircle(int radius) {
			return CreateCircle (radius, Color.White);
		}

		public static Texture2D CreateRectangle( int width, int height, Color color) {
			Texture2D texture = new Texture2D(SpaceGame.singleton.spriteBatch.GraphicsDevice, width, height);
			Color[] colors = new Color[ width * height ];

			for ( int x = 0; x < texture.Width; x++ ) {
				for ( int y = 0; y < texture.Height; y++ ) {
					if ( x == 0 || y == 0 || x == texture.Width - 1 || y == texture.Height - 1 ) {
						colors[x + y * texture.Width] = color;
					} else
						colors[ x + y * texture.Width ] = Color.TransparentBlack;
				}
			}

			texture.SetData( colors );
			return texture;
		}

		public static Texture2D CreateRectangle(int width, int height) {
			return CreateRectangle (width, height, Color.White);
		}
	}
}

