using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Utils
{
	class MyMath
	{

		public static float Smoothstep(float edge0, float edge1, float x)
		{
			x = Clamp((x - edge0) / (edge1 - edge0), 0.0f, 1.0f);

			//return x;
			
			return x * x * (3 - 2 * x);
		}

		public static float Clamp(float x, float lowerlimit, float upperlimit)
		{
			if (x < lowerlimit) return lowerlimit;
			if (x > upperlimit) return upperlimit;

			return x;
		}

	}
}
