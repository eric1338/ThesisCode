using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Physics
{
	class BoxHitbox : Hitbox
	{

		private Vector2 topLeftCorner;
		private Vector2 topRightCorner;
		private Vector2 bottomLeftCorner;
		private Vector2 bottomRightCorner;

		public BoxHitbox(Vector2 topLeftCorner, Vector2 bottomRightCorner)
		{
			this.topLeftCorner = topLeftCorner;
			this.bottomRightCorner = bottomRightCorner;

			topRightCorner = new Vector2(bottomRightCorner.X, topLeftCorner.Y);
			bottomLeftCorner = new Vector2(topLeftCorner.X, bottomRightCorner.Y);

			AddCorners();
		}

		public BoxHitbox(Vector2 center, float width, float height)
		{
			float halfWidth = width / 2;
			float halfHeight = height / 2;

			topLeftCorner = center + new Vector2(-halfWidth, halfHeight);
			topRightCorner = center + new Vector2(halfWidth, halfHeight);
			bottomLeftCorner = center + new Vector2(-halfWidth, -halfHeight);
			bottomRightCorner = center + new Vector2(halfWidth, -halfHeight);

			AddCorners();
		}

		private void AddCorners()
		{
			AddCorner(topLeftCorner);
			AddCorner(topRightCorner);
			AddCorner(bottomRightCorner);
			AddCorner(bottomLeftCorner);
		}

		protected override bool IsVectorWithinHitbox(Vector2 vector)
		{
			return vector.X > topLeftCorner.X && vector.X < bottomRightCorner.X && vector.Y < topLeftCorner.Y && vector.Y > bottomRightCorner.Y;
		}
	}
}
