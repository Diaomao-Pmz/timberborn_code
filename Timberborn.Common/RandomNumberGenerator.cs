using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Timberborn.Common
{
	// Token: 0x02000027 RID: 39
	public class RandomNumberGenerator : IRandomNumberGenerator
	{
		// Token: 0x06000089 RID: 137 RVA: 0x0000342B File Offset: 0x0000162B
		public float Range(float inclusiveMin, float inclusiveMax)
		{
			return Random.Range(inclusiveMin, inclusiveMax);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003434 File Offset: 0x00001634
		public int Range(int inclusiveMin, int exclusiveMax)
		{
			return Random.Range(inclusiveMin, exclusiveMax);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000343D File Offset: 0x0000163D
		public Vector2 InsideUnitCircle()
		{
			return Random.insideUnitCircle;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003444 File Offset: 0x00001644
		public T GetListElement<T>(IReadOnlyList<T> list)
		{
			return list[this.Range(0, list.Count)];
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003459 File Offset: 0x00001659
		public bool TryGetListElement<T>(IReadOnlyList<T> list, out T randomElement)
		{
			if (list.Count == 0)
			{
				randomElement = default(T);
				return false;
			}
			randomElement = this.GetListElement<T>(list);
			return true;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000347C File Offset: 0x0000167C
		public T GetEnumerableElement<T>(IEnumerable<T> source)
		{
			ValueTuple<T, int> valueTuple = this.RandomElement<T>(source);
			T item = valueTuple.Item1;
			if (valueTuple.Item2 == 0)
			{
				throw new ArgumentException("Provided enumerable is empty");
			}
			return item;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000034AC File Offset: 0x000016AC
		public T GetListElementOrDefault<T>(IReadOnlyList<T> list)
		{
			T result;
			if (!this.TryGetListElement<T>(list, out result))
			{
				return default(T);
			}
			return result;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000034D0 File Offset: 0x000016D0
		public bool TryGetEnumerableElement<T>(IEnumerable<T> source, out T randomElement)
		{
			ValueTuple<T, int> valueTuple = this.RandomElement<T>(source);
			T item = valueTuple.Item1;
			if (valueTuple.Item2 == 0)
			{
				randomElement = default(T);
				return false;
			}
			randomElement = item;
			return true;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003503 File Offset: 0x00001703
		public bool CheckProbability(float normalizedProbability)
		{
			return Mathf.Approximately(normalizedProbability, 1f) || this.Range(0f, 1f) < normalizedProbability;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003528 File Offset: 0x00001728
		[return: TupleElementNames(new string[]
		{
			"current",
			"count"
		})]
		public ValueTuple<T, int> RandomElement<T>(IEnumerable<T> source)
		{
			T item = default(T);
			int num = 0;
			foreach (T t in source)
			{
				num++;
				if (this.Range(0, num) == 0)
				{
					item = t;
				}
			}
			return new ValueTuple<T, int>(item, num);
		}
	}
}
