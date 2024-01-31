using System.Collections.Generic;
using System;

namespace _Game.Scripts
{
    public static class Utils
    {
    	#region Components
    	#endregion

    	#region Variables
        #endregion
        
        private static readonly Random _random = new();

        public static void Shuffle<T>(this List<T> list)
        {
	        var n = list.Count;
	        while (n > 1)
	        {
		        n--;
		        var k = _random.Next(n + 1);
		        (list[k], list[n]) = (list[n], list[k]);
	        }
        }
    }
}

