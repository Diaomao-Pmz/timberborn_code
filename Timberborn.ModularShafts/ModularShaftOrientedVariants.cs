using System;
using System.Collections.Generic;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.ModularShafts
{
	// Token: 0x0200000F RID: 15
	public class ModularShaftOrientedVariants
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002BD8 File Offset: 0x00000DD8
		public bool Contains(ShaftVariant variant)
		{
			return this._values.ContainsKey(ModularShaftOrientedVariants.GetIndex(variant));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002BEC File Offset: 0x00000DEC
		public void AddVariant(GameObject value, ShaftVariant variant)
		{
			this._values[ModularShaftOrientedVariants.GetIndex(variant)] = new OrientedValue<GameObject>(value, Orientation.Cw0);
			this._values[ModularShaftOrientedVariants.GetIndex(variant.Rotate(Orientation.Cw90))] = new OrientedValue<GameObject>(value, Orientation.Cw90);
			this._values[ModularShaftOrientedVariants.GetIndex(variant.Rotate(Orientation.Cw180))] = new OrientedValue<GameObject>(value, Orientation.Cw180);
			this._values[ModularShaftOrientedVariants.GetIndex(variant.Rotate(Orientation.Cw270))] = new OrientedValue<GameObject>(value, Orientation.Cw270);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002C70 File Offset: 0x00000E70
		public OrientedValue<GameObject> GetMatch(ShaftVariant variant)
		{
			OrientedValue<GameObject> result;
			if (this._values.TryGetValue(ModularShaftOrientedVariants.GetIndex(variant), out result))
			{
				return result;
			}
			throw new ArgumentOutOfRangeException("Couldn't find value for " + variant.GetName());
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002CAC File Offset: 0x00000EAC
		public static long GetIndex(ShaftVariant variant)
		{
			return (long)((ulong)variant.Down | (ulong)variant.Left << 8 | (ulong)variant.Up << 16 | (ulong)variant.Right << 24 | (ulong)variant.Bottom << 32 | (ulong)variant.Top << 40);
		}

		// Token: 0x04000031 RID: 49
		public readonly Dictionary<long, OrientedValue<GameObject>> _values = new Dictionary<long, OrientedValue<GameObject>>();
	}
}
