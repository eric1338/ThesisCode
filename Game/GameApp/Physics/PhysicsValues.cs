using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Physics
{
	class PhysicsValues
	{

		public static readonly float CollectibleHitboxWidth = 0.2f;
		public static readonly float CollectibleHitboxHeight = 0.2f;

		public static readonly float PlayerHitboxWidth = 0.4f;
		public static readonly float PlayerHitboxHeight = 0.8f;

		public static float GetHalfPlayerHitboxWidth()
		{
			return PlayerHitboxWidth / 2.0f;
		}

		public static readonly float PlayerVelocity = 2.0f;
		public static readonly float GravityFactor = 1.0f;
		public static readonly float JumpForceFactor = 2.0f;

	}
}
