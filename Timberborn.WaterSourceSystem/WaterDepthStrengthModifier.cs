using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.WaterSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x02000015 RID: 21
	public class WaterDepthStrengthModifier : BaseComponent, IAwakableComponent, IInitializableEntity, IDeletableEntity, IPersistentEntity, IWaterStrengthModifier
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00002E24 File Offset: 0x00001024
		public WaterDepthStrengthModifier(IThreadSafeWaterMap threadSafeWaterMap)
		{
			this._threadSafeWaterMap = threadSafeWaterMap;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002E33 File Offset: 0x00001033
		public void Awake()
		{
			this._spec = base.GetComponent<WaterDepthStrengthModifierSpec>();
			this._blockObject = base.GetComponent<BlockObject>();
			this._waterSource = base.GetComponent<WaterSource>();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002E59 File Offset: 0x00001059
		public void InitializeEntity()
		{
			this._waterSource.AddWaterStrengthModifier(this);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002E67 File Offset: 0x00001067
		public void DeleteEntity()
		{
			this._waterSource.RemoveWaterStrengthModifier(this);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002E75 File Offset: 0x00001075
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(WaterDepthStrengthModifier.WaterDepthStrengthModifierKey).Set(WaterDepthStrengthModifier.CurrentModifierKey, this._currentModifier);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002E94 File Offset: 0x00001094
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(WaterDepthStrengthModifier.WaterDepthStrengthModifierKey);
			this._currentModifier = component.Get(WaterDepthStrengthModifier.CurrentModifierKey);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002EBE File Offset: 0x000010BE
		public float GetStrengthModifier()
		{
			this.UpdateEnabledState();
			this._currentModifier = (this._isEnabled ? Mathf.MoveTowards(this._currentModifier, 1f, WaterDepthStrengthModifier.FadeInSpeed * Time.deltaTime) : 0f);
			return this._currentModifier;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002EFC File Offset: 0x000010FC
		public void UpdateEnabledState()
		{
			float num = this._threadSafeWaterMap.WaterDepth(this._blockObject.CoordinatesAtBaseZ);
			if (this._isEnabled && num > this._spec.DepthLimit)
			{
				this._isEnabled = false;
				return;
			}
			if (!this._isEnabled && num < this._spec.DepthLimit * WaterDepthStrengthModifier.HysteresisBottomScale)
			{
				this._isEnabled = true;
			}
		}

		// Token: 0x0400002B RID: 43
		public static readonly float HysteresisBottomScale = 0.9f;

		// Token: 0x0400002C RID: 44
		public static readonly float FadeInSpeed = 0.5f;

		// Token: 0x0400002D RID: 45
		public static readonly ComponentKey WaterDepthStrengthModifierKey = new ComponentKey("WaterDepthStrengthModifier");

		// Token: 0x0400002E RID: 46
		public static readonly PropertyKey<float> CurrentModifierKey = new PropertyKey<float>("CurrentModifier");

		// Token: 0x0400002F RID: 47
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x04000030 RID: 48
		public WaterDepthStrengthModifierSpec _spec;

		// Token: 0x04000031 RID: 49
		public BlockObject _blockObject;

		// Token: 0x04000032 RID: 50
		public WaterSource _waterSource;

		// Token: 0x04000033 RID: 51
		public bool _isEnabled;

		// Token: 0x04000034 RID: 52
		public float _currentModifier;
	}
}
