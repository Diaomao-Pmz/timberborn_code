using System;
using Timberborn.CharacterModelSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.EnterableSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.SlotSystem
{
	// Token: 0x0200001D RID: 29
	public class TransformSlot : ISlot
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000039C8 File Offset: 0x00001BC8
		// (set) Token: 0x060000BF RID: 191 RVA: 0x000039D0 File Offset: 0x00001BD0
		public Enterer AssignedEnterer { get; private set; }

		// Token: 0x060000C0 RID: 192 RVA: 0x000039D9 File Offset: 0x00001BD9
		public TransformSlot(IRandomNumberGenerator randomNumberGenerator, IThreadSafeWaterMap threadSafeWaterMap, Transform followedTransform, TransformSlotSpec transformSlotSpec)
		{
			this._randomNumberGenerator = randomNumberGenerator;
			this._threadSafeWaterMap = threadSafeWaterMap;
			this._followedTransform = followedTransform;
			this._transformSlotSpec = transformSlotSpec;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000039FE File Offset: 0x00001BFE
		public string Name
		{
			get
			{
				return this._followedTransform.name;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003A0B File Offset: 0x00001C0B
		public bool IsAvailable
		{
			get
			{
				return this._followedTransform.gameObject.activeInHierarchy;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003A20 File Offset: 0x00001C20
		public void AssignEnterer(Enterer enterer)
		{
			this.AssignedEnterer = enterer;
			CharacterModel component = this.AssignedEnterer.GetComponent<CharacterModel>();
			if (this._transformSlotSpec.RandomizeYRotation)
			{
				this._followedTransform.rotation *= Quaternion.AngleAxis((float)this._randomNumberGenerator.Range(0, 360), Vector3.up);
			}
			this.ImmediatelyMoveSlotToWaterLevel();
			if (this._transformSlotSpec.Inanimate)
			{
				component.PositionAtTarget(this._followedTransform);
				return;
			}
			component.AnimateFollowingTarget(this._followedTransform, this._transformSlotSpec.Animation);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003AB6 File Offset: 0x00001CB6
		public void UnassignEnterer()
		{
			if (this.AssignedEnterer)
			{
				this.AssignedEnterer.GetComponent<CharacterModel>().StopAnimating();
			}
			this.AssignedEnterer = null;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003ADC File Offset: 0x00001CDC
		public void Update(float deltaTime)
		{
			this.MoveSlotToWaterLevel(deltaTime);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003AE8 File Offset: 0x00001CE8
		public override string ToString()
		{
			string str = this.AssignedEnterer ? this.AssignedEnterer.Name : "Nobody";
			return "Slot: TransformSlot, assigned: " + str;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003B20 File Offset: 0x00001D20
		public void ImmediatelyMoveSlotToWaterLevel()
		{
			this.MoveSlotToWaterLevel(10000f);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003B30 File Offset: 0x00001D30
		public void MoveSlotToWaterLevel(float deltaTime)
		{
			if (this._transformSlotSpec.WaterSlot)
			{
				Vector3 position = this._followedTransform.position;
				Vector3Int coordinates = CoordinateSystem.WorldToGridInt(position);
				float num = this._threadSafeWaterMap.WaterHeightOrFloor(coordinates) - position.y;
				float num2 = 0.1f * deltaTime;
				position.y += Mathf.Clamp(num, -num2, num2);
				this._followedTransform.position = position;
			}
		}

		// Token: 0x04000039 RID: 57
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400003A RID: 58
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x0400003B RID: 59
		public readonly Transform _followedTransform;

		// Token: 0x0400003C RID: 60
		public readonly TransformSlotSpec _transformSlotSpec;
	}
}
