using System;
using System.Collections.Generic;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.WalkingSystem
{
	// Token: 0x02000010 RID: 16
	public class PositionDestination : IDestination, IEquatable<PositionDestination>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000267D File Offset: 0x0000087D
		public Vector3 Destination { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002685 File Offset: 0x00000885
		public float StoppingDistance { get; }

		// Token: 0x0600002F RID: 47 RVA: 0x0000268D File Offset: 0x0000088D
		public PositionDestination(INavigationService navigationService, WalkerService walkerService, Vector3 destination, float stoppingDistance)
		{
			this._navigationService = navigationService;
			this._walkerService = walkerService;
			this.Destination = destination;
			this.StoppingDistance = stoppingDistance;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026B2 File Offset: 0x000008B2
		public bool FindPath(Vector3 start, List<PathCorner> pathCorners, out float distance)
		{
			if (this._navigationService.FindPathUnlimitedRange(start, this.Destination, pathCorners, out distance))
			{
				this.OffsetLastCornerInDirectionOfSecondLastCorner(pathCorners, this.StoppingDistance);
				return true;
			}
			distance = 0f;
			return false;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000026E4 File Offset: 0x000008E4
		public bool Equals(PositionDestination other)
		{
			return other != null && (this == other || (this.Destination.Equals(other.Destination) && this.StoppingDistance.Equals(other.StoppingDistance)));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002728 File Offset: 0x00000928
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((PositionDestination)obj)));
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002758 File Offset: 0x00000958
		public override int GetHashCode()
		{
			return this.Destination.GetHashCode() * 397 ^ this.StoppingDistance.GetHashCode();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000021A0 File Offset: 0x000003A0
		public static bool operator ==(PositionDestination left, PositionDestination right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000021A9 File Offset: 0x000003A9
		public static bool operator !=(PositionDestination left, PositionDestination right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002790 File Offset: 0x00000990
		public void OffsetLastCornerInDirectionOfSecondLastCorner(List<PathCorner> pathCorners, float stoppingDistance)
		{
			if (pathCorners.Count > 1 && stoppingDistance != 0f)
			{
				if (stoppingDistance > 0.5f)
				{
					throw new ArgumentException("Stopping distance can't be bigger than " + string.Format("{0}", 0.5f));
				}
				int num = pathCorners.Count - 1;
				PathCorner pathCorner = pathCorners[num];
				Vector3 vector = pathCorners[num - 1].Position - pathCorner.Position;
				vector.y = 0f;
				Vector3 vector2 = vector.normalized * stoppingDistance;
				Vector3 vector3 = pathCorner.Position + vector2;
				if (!this._navigationService.IsOnNavMesh(vector3))
				{
					PositionDestination.ValidateHorizontalOffset(vector3 = this._walkerService.ClosestPositionOnNavMesh(vector3), vector3);
				}
				pathCorners[num] = new PathCorner(vector3, pathCorner.Speed, pathCorner.GroupId);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000287C File Offset: 0x00000A7C
		public static void ValidateHorizontalOffset(Vector3 correctedOffsetLastCorner, Vector3 offsetLastCorner)
		{
			Vector3 vector = correctedOffsetLastCorner - offsetLastCorner;
			vector.y = 0f;
			if (vector.magnitude > 0.001f)
			{
				Debug.LogWarning("Offset last corner had to be placed on nav mesh, by moving alongside x or z axis. Please report this.");
			}
		}

		// Token: 0x0400001A RID: 26
		public readonly INavigationService _navigationService;

		// Token: 0x0400001B RID: 27
		public readonly WalkerService _walkerService;
	}
}
