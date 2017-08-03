using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Physics
{
	abstract class Hitbox
	{

		public List<Vector2> Corners { get; set; }

		public Hitbox()
		{
			Corners = new List<Vector2>();
		}
		
		protected void AddCorner(Vector2 corner)
		{
			Corners.Add(corner);
		}

		protected abstract bool IsVectorWithinHitbox(Vector2 vector);


		public bool CollidesWith(Hitbox otherHitbox)
		{
			foreach (Vector2 corner in otherHitbox.Corners)
			{
				if (IsVectorWithinHitbox(corner)) return true;
			}

			foreach (Vector2 corner in Corners)
			{
				if (otherHitbox.IsVectorWithinHitbox(corner)) return true;
			}

			return false;
		}

	}
}
