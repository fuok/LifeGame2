using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{

	//获取不重复随机数

	public static List<int> GetRandomNumbers (int length, int required)
	{
//		int[] result = new int[required];
		List<int> result = new List<int> ();
		do {

			int num = Random.Range (0, length);
			if (!result.Contains (num)) {
				result.Add (num);
			}

		} while (result.Count < required);

		return result;
	}
}
