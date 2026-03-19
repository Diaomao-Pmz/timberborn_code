using System;
using Timberborn.CharacterModelSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.EnterableSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.SlotSystem
{
	// Token: 0x0200000C RID: 12
	public class PatrollingSlot : ISlot
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000247E File Offset: 0x0000067E
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002486 File Offset: 0x00000686
		public Enterer AssignedEnterer { get; private set; }

		// Token: 0x06000034 RID: 52 RVA: 0x0000248F File Offset: 0x0000068F
		public PatrollingSlot(IRandomNumberGenerator randomNumberGenerator, Transform slotTransform, Transform start, Transform end, PatrollingSlotSpec patrollingSlotSpec, IThreadSafeWaterMap threadSafeWaterMap)
		{
			this._randomNumberGenerator = randomNumberGenerator;
			this._slotTransform = slotTransform;
			this._start = start;
			this._end = end;
			this._patrollingSlotSpec = patrollingSlotSpec;
			this._threadSafeWaterMap = threadSafeWaterMap;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000024C4 File Offset: 0x000006C4
		public string Name
		{
			get
			{
				return this._slotTransform.name;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000024D1 File Offset: 0x000006D1
		public bool IsAvailable
		{
			get
			{
				return this._slotTransform.gameObject.activeInHierarchy;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000024E3 File Offset: 0x000006E3
		public void AssignEnterer(Enterer enterer)
		{
			this.AssignedEnterer = enterer;
			enterer.GetComponent<CharacterModel>().AnimateFollowingTarget(this._slotTransform, this._patrollingSlotSpec.Animation);
			this._movementSpeed = this.RandomMovementSpeed();
			this.RandomizePositionToStartOrEnd();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000251A File Offset: 0x0000071A
		public void UnassignEnterer()
		{
			if (this.AssignedEnterer)
			{
				this.AssignedEnterer.GetComponent<CharacterModel>().StopAnimating();
			}
			this.AssignedEnterer = null;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002540 File Offset: 0x00000740
		public void Update(float deltaTime)
		{
			this.MoveDestinationToWaterLevel();
			float num = this._movementSpeed * deltaTime;
			if (Vector2.Distance(this._slotTransform.position.XZ(), this._destination.XZ()) < num)
			{
				this.FlipDirection();
				return;
			}
			this.MoveToDestination(num);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002590 File Offset: 0x00000790
		public override string ToString()
		{
			string str = this.AssignedEnterer ? this.AssignedEnterer.Name : "Nobody";
			return "Slot: PatrollingSlot, assigned: " + str;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000025C8 File Offset: 0x000007C8
		public float RandomMovementSpeed()
		{
			float num = this._randomNumberGenerator.Range(1f - this._patrollingSlotSpec.MaxRandomDeviationOfMovementSpeed, 1f + this._patrollingSlotSpec.MaxRandomDeviationOfMovementSpeed);
			return this._patrollingSlotSpec.BaseMovementSpeed * num;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002610 File Offset: 0x00000810
		public void FlipDirection()
		{
			this._slotTransform.position = this._destination;
			this._destination = ((this._destination.XZ() == this._start.position.XZ()) ? this._end.position : this._start.position);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002670 File Offset: 0x00000870
		public void MoveToDestination(float distanceToTravel)
		{
			Vector3 vector = (this._destination - this._slotTransform.position).normalized * distanceToTravel;
			this._slotTransform.position += vector;
			this._slotTransform.LookAt(this._destination);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000026CC File Offset: 0x000008CC
		public void RandomizePositionToStartOrEnd()
		{
			if (this._randomNumberGenerator.Range(0f, 1f) > 0.5f)
			{
				this._destination = this._start.position;
				this._slotTransform.position = this._end.position;
			}
			else
			{
				this._destination = this._end.position;
				this._slotTransform.position = this._start.position;
			}
			this._slotTransform.LookAt(this._end);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002758 File Offset: 0x00000958
		public void MoveDestinationToWaterLevel()
		{
			if (this._patrollingSlotSpec.WaterSlot)
			{
				Vector3Int coordinates = CoordinateSystem.WorldToGridInt(this._destination);
				float y = this._threadSafeWaterMap.WaterHeightOrFloor(coordinates);
				this._destination.y = y;
			}
		}

		// Token: 0x0400000B RID: 11
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400000C RID: 12
		public readonly Transform _slotTransform;

		// Token: 0x0400000D RID: 13
		public readonly Transform _start;

		// Token: 0x0400000E RID: 14
		public readonly Transform _end;

		// Token: 0x0400000F RID: 15
		public readonly PatrollingSlotSpec _patrollingSlotSpec;

		// Token: 0x04000010 RID: 16
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x04000011 RID: 17
		public Vector3 _destination;

		// Token: 0x04000012 RID: 18
		public float _movementSpeed;
	}
}
