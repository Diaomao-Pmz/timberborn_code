using System;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x0200008D RID: 141
	public readonly struct WeightedCoordinates : IEquatable<WeightedCoordinates>
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00009CEC File Offset: 0x00007EEC
		public Vector3Int Coordinates { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00009CF4 File Offset: 0x00007EF4
		public float Distance { get; }

		// Token: 0x06000311 RID: 785 RVA: 0x00009CFC File Offset: 0x00007EFC
		public WeightedCoordinates(Vector3Int coordinates, float distance)
		{
			this.Coordinates = coordinates;
			this.Distance = distance;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00009D0C File Offset: 0x00007F0C
		public bool Equals(WeightedCoordinates other)
		{
			return this.Coordinates.Equals(other.Coordinates);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00009D30 File Offset: 0x00007F30
		public override bool Equals(object obj)
		{
			if (obj is WeightedCoordinates)
			{
				WeightedCoordinates other = (WeightedCoordinates)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00009D58 File Offset: 0x00007F58
		public override int GetHashCode()
		{
			return this.Coordinates.GetHashCode();
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00009D79 File Offset: 0x00007F79
		public static bool operator ==(WeightedCoordinates left, WeightedCoordinates right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00009D83 File Offset: 0x00007F83
		public static bool operator !=(WeightedCoordinates left, WeightedCoordinates right)
		{
			return !left.Equals(right);
		}
	}
}
