using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.CharacterMovementSystem
{
	// Token: 0x02000008 RID: 8
	public class AnimatedPathFollower
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000214F File Offset: 0x0000034F
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002157 File Offset: 0x00000357
		public Vector3 CurrentPosition { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002160 File Offset: 0x00000360
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002168 File Offset: 0x00000368
		public Vector3 CurrentDirection { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002171 File Offset: 0x00000371
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002179 File Offset: 0x00000379
		public float CurrentSpeed { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002182 File Offset: 0x00000382
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000218A File Offset: 0x0000038A
		public float CurrentXRotation { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002193 File Offset: 0x00000393
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000219B File Offset: 0x0000039B
		public float CurrentDistanceToPathCorner { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000021A4 File Offset: 0x000003A4
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000021AC File Offset: 0x000003AC
		public int CurrentGroupId { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000021B5 File Offset: 0x000003B5
		public bool Stopped
		{
			get
			{
				return this._pathCorners.Count == 0;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000021C5 File Offset: 0x000003C5
		public void SetNewPath(IEnumerable<AnimatedPathCorner> pathCorners)
		{
			this.ClearCurrentPath();
			this._pathCorners.AddRange(pathCorners);
			if (!this._isPositionedBeforeMovement)
			{
				this._isPositionedBeforeMovement = true;
				this.PlaceAtCorner(this._pathCorners[0]);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000021FA File Offset: 0x000003FA
		public void Stop()
		{
			this._isPositionedBeforeMovement = false;
			this.ClearCurrentPath();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002209 File Offset: 0x00000409
		public void Update(float timeInSeconds)
		{
			this.MoveNextCornerIndex(timeInSeconds);
			if (!this.ReachedDestination())
			{
				this.PlaceAtCorrectPosition(timeInSeconds);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002221 File Offset: 0x00000421
		public bool ReachedDestination()
		{
			return this._nextCornerIndex >= this._pathCorners.Count;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000223C File Offset: 0x0000043C
		public float LastCornerTime()
		{
			if (!this._pathCorners.IsEmpty<AnimatedPathCorner>())
			{
				return this._pathCorners.Last<AnimatedPathCorner>().Time;
			}
			return 0f;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000226F File Offset: 0x0000046F
		public void ClearCurrentPath()
		{
			this._pathCorners.Clear();
			this._nextCornerIndex = 0;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002284 File Offset: 0x00000484
		public void MoveNextCornerIndex(float timeInSeconds)
		{
			for (int i = this._nextCornerIndex; i < this._pathCorners.Count; i++)
			{
				if (timeInSeconds < this._pathCorners[i].Time)
				{
					this._nextCornerIndex = i;
					return;
				}
			}
			this._nextCornerIndex = this._pathCorners.Count + 1;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000022E0 File Offset: 0x000004E0
		public void PlaceAtCorrectPosition(float timeInSeconds)
		{
			AnimatedPathCorner animatedPathCorner = this._pathCorners[this._nextCornerIndex];
			if (this._nextCornerIndex == 0)
			{
				this.PlaceAtCorner(animatedPathCorner);
				return;
			}
			AnimatedPathCorner previousCorner = this._pathCorners[this._nextCornerIndex - 1];
			this.PlaceBetweenCorners(previousCorner, animatedPathCorner, timeInSeconds);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000232C File Offset: 0x0000052C
		public void PlaceAtCorner(AnimatedPathCorner animatedPathCorner)
		{
			this.CurrentPosition = animatedPathCorner.Position;
			this.CurrentDirection = Vector3.zero;
			this.CurrentSpeed = animatedPathCorner.Speed;
			this.CurrentDistanceToPathCorner = animatedPathCorner.DistanceToPathCorner;
			this.CurrentGroupId = animatedPathCorner.GroupId;
			this.CurrentXRotation = 0f;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002384 File Offset: 0x00000584
		public void PlaceBetweenCorners(AnimatedPathCorner previousCorner, AnimatedPathCorner nextCorner, float timeInSeconds)
		{
			Vector3 vector = nextCorner.Position - previousCorner.Position;
			float num = nextCorner.Time - previousCorner.Time;
			float num2 = (timeInSeconds - previousCorner.Time) / num;
			Vector3 vector2 = vector * num2;
			this.CurrentPosition = previousCorner.Position + vector2;
			this.CurrentDirection = vector.normalized;
			this.CurrentXRotation = ((vector == Vector3.zero) ? 0f : Quaternion.LookRotation(vector).eulerAngles.x);
			this.CurrentDistanceToPathCorner = nextCorner.DistanceToPathCorner;
			this.CurrentSpeed = previousCorner.Speed;
			this.CurrentGroupId = previousCorner.GroupId;
		}

		// Token: 0x04000013 RID: 19
		public readonly List<AnimatedPathCorner> _pathCorners = new List<AnimatedPathCorner>();

		// Token: 0x04000014 RID: 20
		public int _nextCornerIndex;

		// Token: 0x04000015 RID: 21
		public bool _isPositionedBeforeMovement;
	}
}
