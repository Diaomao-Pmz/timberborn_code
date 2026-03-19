using System;
using System.Collections.Generic;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.CharacterMovementSystem
{
	// Token: 0x02000012 RID: 18
	public class PathFollower
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000076 RID: 118 RVA: 0x00003110 File Offset: 0x00001310
		// (remove) Token: 0x06000077 RID: 119 RVA: 0x00003148 File Offset: 0x00001348
		public event EventHandler<MovementEventArgs> MovedAlongPath;

		// Token: 0x06000078 RID: 120 RVA: 0x0000317D File Offset: 0x0000137D
		public PathFollower(INavigationService navigationService, MovementAnimator movementAnimator, Transform transform)
		{
			this._navigationService = navigationService;
			this._movementAnimator = movementAnimator;
			this._transform = transform;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000031A7 File Offset: 0x000013A7
		public void StartMovingAlongPath(IReadOnlyList<PathCorner> pathCorners)
		{
			this._pathCorners = pathCorners;
			this._nextCornerIndex = 1;
			this._movedAlongPath = false;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000031C0 File Offset: 0x000013C0
		public void MoveAlongPath(float tickDeltaTime, string animationName, Func<float> movementSpeedProvider)
		{
			float? speedLimitIfCloseToTarget = this.GetSpeedLimitIfCloseToTarget(tickDeltaTime, movementSpeedProvider);
			float num = speedLimitIfCloseToTarget ?? this.GetMovementSpeed(movementSpeedProvider);
			int groupId = this._pathCorners[this._nextCornerIndex - 1].GroupId;
			float num2 = tickDeltaTime;
			bool flag = false;
			float timeFromLastPathPoint = this.GetTimeFromLastPathPoint();
			this._animatedPathCorners.Clear();
			this.AddAnimatedPathCorner(this._transform.position, timeFromLastPathPoint, num, groupId);
			while (num2 > PathFollower.RemainingTimeThreshold && !this.ReachedLastPathCorner())
			{
				if (flag)
				{
					this._nextCornerIndex = ((this._nextCornerIndex + 1 < this._pathCorners.Count) ? (this._nextCornerIndex + 1) : this._nextCornerIndex);
					num = (speedLimitIfCloseToTarget ?? this.GetMovementSpeed(movementSpeedProvider));
				}
				int groupId2 = this._pathCorners[this._nextCornerIndex - 1].GroupId;
				if (num < 3.4028235E+38f)
				{
					Vector3 position = this._pathCorners[this._nextCornerIndex].Position;
					Vector3 position2 = PathFollower.MoveInDirection(this._transform.position, position, num, ref num2, out flag);
					float time = timeFromLastPathPoint + tickDeltaTime - num2;
					this.AddAnimatedPathCorner(position2, time, num, groupId2);
				}
				else
				{
					Vector3 position3 = this._pathCorners[this._nextCornerIndex].Position;
					float time2 = timeFromLastPathPoint + tickDeltaTime - num2;
					this.AddAnimatedPathCorner(position3, time2, num, groupId2);
					flag = true;
				}
			}
			this.AddSmoothingAnimatedPathCorner(tickDeltaTime, flag, timeFromLastPathPoint, num);
			this._movementAnimator.AnimateMovementAlongPath(this._animatedPathCorners, animationName);
			this._movedAlongPath = true;
			this.NotifyAfterMovement();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003372 File Offset: 0x00001572
		public void StopMoving()
		{
			this._pathCorners = null;
			this._movedAlongPath = false;
			this._movementAnimator.StopAnimatingMovement();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003390 File Offset: 0x00001590
		public bool ReachedLastPathCorner()
		{
			INavigationService navigationService = this._navigationService;
			IReadOnlyList<PathCorner> pathCorners = this._pathCorners;
			return navigationService.InStoppingProximity(pathCorners[pathCorners.Count - 1].Position, this._transform.position);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000033D0 File Offset: 0x000015D0
		public float? GetSpeedLimitIfCloseToTarget(float tickDeltaTime, Func<float> movementSpeedProvider)
		{
			if (this._nextCornerIndex >= this._pathCorners.Count - PathFollower.SpeedModifyingThreshold)
			{
				float movementSpeed = this.GetMovementSpeed(movementSpeedProvider);
				float remainingDistance = this.GetRemainingDistance();
				float num = remainingDistance / movementSpeed / tickDeltaTime;
				int num2 = Mathf.Max(1, Mathf.RoundToInt(num));
				return new float?(remainingDistance / ((float)num2 * tickDeltaTime));
			}
			return null;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003430 File Offset: 0x00001630
		public float GetMovementSpeed(Func<float> movementSpeedProvider)
		{
			float speed = this._pathCorners[this._nextCornerIndex - 1].Speed;
			if (speed >= 3.4028235E+38f)
			{
				return float.MaxValue;
			}
			return speed * movementSpeedProvider();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003470 File Offset: 0x00001670
		public float GetRemainingDistance()
		{
			float num = Vector3.Distance(this._transform.position, this._pathCorners[this._nextCornerIndex].Position);
			for (int i = this._nextCornerIndex; i < this._pathCorners.Count - 1; i++)
			{
				num += Vector3.Distance(this._pathCorners[i].Position, this._pathCorners[i + 1].Position);
			}
			return num;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000034F8 File Offset: 0x000016F8
		public float GetTimeFromLastPathPoint()
		{
			if (this._movedAlongPath && this._animatedPathCorners.Count > 1)
			{
				float time = Time.time;
				List<AnimatedPathCorner> animatedPathCorners = this._animatedPathCorners;
				if (time - animatedPathCorners[animatedPathCorners.Count - 2].Time > 0f)
				{
					List<AnimatedPathCorner> animatedPathCorners2 = this._animatedPathCorners;
					return animatedPathCorners2[animatedPathCorners2.Count - 2].Time;
				}
			}
			return Time.time;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003564 File Offset: 0x00001764
		public static Vector3 MoveInDirection(Vector3 position, Vector3 target, float speed, ref float remainingTime, out bool reachedTarget)
		{
			Vector3 vector = target - position;
			float magnitude = vector.magnitude;
			if (magnitude < PathFollower.RemainingDistanceThreshold)
			{
				reachedTarget = true;
				return target;
			}
			float num = Mathf.Min(speed * remainingTime, PathFollower.MaxMovementStep);
			if (magnitude > num)
			{
				remainingTime -= num / speed;
				Vector3 vector2 = vector.normalized * num;
				reachedTarget = false;
				return position + vector2;
			}
			remainingTime -= magnitude / speed;
			reachedTarget = true;
			return target;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000035D4 File Offset: 0x000017D4
		public void AddAnimatedPathCorner(Vector3 position, float time, float speed, int groupId)
		{
			float distanceToPathCorner = Vector3.Distance(position, this._pathCorners[this._nextCornerIndex].Position);
			this._transform.position = position;
			this._animatedPathCorners.Add(new AnimatedPathCorner(position, time, speed, distanceToPathCorner, groupId));
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003624 File Offset: 0x00001824
		public void AddSmoothingAnimatedPathCorner(float tickDeltaTime, bool reachedLastTarget, float startingTime, float speed)
		{
			if (!this.ReachedLastPathCorner())
			{
				int index = Math.Min(reachedLastTarget ? (this._nextCornerIndex + 1) : this._nextCornerIndex, this._pathCorners.Count - 1);
				PathCorner pathCorner = this._pathCorners[index];
				float num = Vector3.Distance(this._transform.position, pathCorner.Position);
				float time = startingTime + tickDeltaTime + num / speed;
				this._animatedPathCorners.Add(new AnimatedPathCorner(pathCorner.Position, time, speed, num, pathCorner.GroupId));
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000036B0 File Offset: 0x000018B0
		public void NotifyAfterMovement()
		{
			PathCorner from = this._pathCorners[this._nextCornerIndex - 1];
			PathCorner to = this._pathCorners[this._nextCornerIndex];
			if (this._nextCornerIndex + 1 < this._pathCorners.Count)
			{
				EventHandler<MovementEventArgs> movedAlongPath = this.MovedAlongPath;
				if (movedAlongPath == null)
				{
					return;
				}
				movedAlongPath(this, new MovementEventArgs(from, to, new PathCorner?(this._pathCorners[this._nextCornerIndex + 1])));
				return;
			}
			else
			{
				EventHandler<MovementEventArgs> movedAlongPath2 = this.MovedAlongPath;
				if (movedAlongPath2 == null)
				{
					return;
				}
				movedAlongPath2(this, new MovementEventArgs(from, to, null));
				return;
			}
		}

		// Token: 0x0400003C RID: 60
		public static readonly float RemainingTimeThreshold = 0.0001f;

		// Token: 0x0400003D RID: 61
		public static readonly float RemainingDistanceThreshold = 0.0001f;

		// Token: 0x0400003E RID: 62
		public static readonly float MaxMovementStep = 0.1f;

		// Token: 0x0400003F RID: 63
		public static readonly int SpeedModifyingThreshold = 3;

		// Token: 0x04000041 RID: 65
		public readonly INavigationService _navigationService;

		// Token: 0x04000042 RID: 66
		public readonly MovementAnimator _movementAnimator;

		// Token: 0x04000043 RID: 67
		public readonly Transform _transform;

		// Token: 0x04000044 RID: 68
		public readonly List<AnimatedPathCorner> _animatedPathCorners = new List<AnimatedPathCorner>(100);

		// Token: 0x04000045 RID: 69
		public IReadOnlyList<PathCorner> _pathCorners;

		// Token: 0x04000046 RID: 70
		public int _nextCornerIndex;

		// Token: 0x04000047 RID: 71
		public bool _movedAlongPath;
	}
}
