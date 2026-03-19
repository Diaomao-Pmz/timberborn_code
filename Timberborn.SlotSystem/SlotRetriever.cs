using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Timberborn.SlotSystem
{
	// Token: 0x0200001A RID: 26
	public class SlotRetriever
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x0000381C File Offset: 0x00001A1C
		public IEnumerable<Transform> GetSlots(GameObject gameObject, string keyword)
		{
			return from transform in SlotRetriever.GetTransformsInChildren(gameObject)
			where SlotRetriever.IsSlot(keyword, transform)
			select transform;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003850 File Offset: 0x00001A50
		[return: TupleElementNames(new string[]
		{
			"start",
			"end"
		})]
		public ValueTuple<Transform, Transform> GetStartAndEnd(GameObject gameObject)
		{
			Transform item = SlotRetriever.GetTransformsInChildren(gameObject).SingleOrDefault(new Func<Transform, bool>(SlotRetriever.IsStart));
			Transform item2 = SlotRetriever.GetTransformsInChildren(gameObject).SingleOrDefault(new Func<Transform, bool>(SlotRetriever.IsEnd));
			return new ValueTuple<Transform, Transform>(item, item2);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003892 File Offset: 0x00001A92
		public static bool IsSlot(string keyword, Transform transform)
		{
			return transform.name.StartsWith("#Slot#" + keyword);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000038AA File Offset: 0x00001AAA
		public static bool IsStart(Transform transform)
		{
			return transform.name.StartsWith("#MiscStart");
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000038BC File Offset: 0x00001ABC
		public static bool IsEnd(Transform transform)
		{
			return transform.name.StartsWith("#MiscEnd");
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000038CE File Offset: 0x00001ACE
		public static IEnumerable<Transform> GetTransformsInChildren(GameObject gameObject)
		{
			return gameObject.GetComponentsInChildren<Transform>(true);
		}
	}
}
