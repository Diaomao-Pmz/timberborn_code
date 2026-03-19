using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BonusSystem;
using Timberborn.EnterableSystem;
using Timberborn.MechanicalSystem;
using Timberborn.TickSystem;
using Timberborn.WorkshopsEffects;
using Timberborn.WorkSystem;
using UnityEngine;

namespace Timberborn.PowerGeneration
{
	// Token: 0x0200000F RID: 15
	public class WalkerPoweredGenerator : TickableComponent, IAwakableComponent, IWorkshopAnimationSpeedModifier
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000049 RID: 73 RVA: 0x0000267C File Offset: 0x0000087C
		// (remove) Token: 0x0600004A RID: 74 RVA: 0x000026B4 File Offset: 0x000008B4
		public event EventHandler SpeedModifierChanged;

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000026E9 File Offset: 0x000008E9
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000026F1 File Offset: 0x000008F1
		public float SpeedModifier { get; private set; }

		// Token: 0x0600004D RID: 77 RVA: 0x000026FC File Offset: 0x000008FC
		public void Awake()
		{
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			this._workplace = base.GetComponent<Workplace>();
			this._enterable = base.GetComponent<Enterable>();
			this._enterable.EntererAdded += delegate(object _, EntererAddedEventArgs e)
			{
				this.AddBonusManager(e.Enterer);
			};
			this._enterable.EntererRemoved += delegate(object _, EntererRemovedEventArgs e)
			{
				this.RemoveBonusManager(e.Enterer);
			};
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000275B File Offset: 0x0000095B
		public override void StartTickable()
		{
			this.UpdateGenerator(true);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002764 File Offset: 0x00000964
		public override void Tick()
		{
			this.UpdateGenerator(false);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000276D File Offset: 0x0000096D
		public void AddBonusManager(BaseComponent baseComponent)
		{
			this._bonusManagers.Add(baseComponent.GetComponent<BonusManager>());
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002780 File Offset: 0x00000980
		public void RemoveBonusManager(BaseComponent baseComponent)
		{
			this._bonusManagers.Remove(baseComponent.GetComponent<BonusManager>());
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002794 File Offset: 0x00000994
		public void UpdateGenerator(bool forceUpdate = false)
		{
			float generatorStrength = this.GetGeneratorStrength();
			if (forceUpdate || !this._mechanicalNode.OutputMultiplier.Equals(generatorStrength))
			{
				this._mechanicalNode.SetOutputMultiplier(generatorStrength);
				this.UpdateSpeedModifier();
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000027D3 File Offset: 0x000009D3
		public float GetGeneratorStrength()
		{
			if (this._bonusManagers.Count <= 0)
			{
				return 0f;
			}
			return this.GetBonusMultiplier();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000027F0 File Offset: 0x000009F0
		public float GetBonusMultiplier()
		{
			float num = 0f;
			foreach (BonusManager bonusManager in this._bonusManagers)
			{
				num += bonusManager.Multiplier(WalkerPoweredGenerator.MovementBonusId);
			}
			return num / (float)this._workplace.MaxWorkers;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002860 File Offset: 0x00000A60
		public void UpdateSpeedModifier()
		{
			this.SpeedModifier = Mathf.Lerp(WalkerPoweredGenerator.MinSpeedModifier, WalkerPoweredGenerator.MaxSpeedModifier, this._mechanicalNode.OutputMultiplier / WalkerPoweredGenerator.MaxSpeedModifier);
			EventHandler speedModifierChanged = this.SpeedModifierChanged;
			if (speedModifierChanged == null)
			{
				return;
			}
			speedModifierChanged(this, EventArgs.Empty);
		}

		// Token: 0x04000014 RID: 20
		public static readonly string MovementBonusId = "MovementSpeed";

		// Token: 0x04000015 RID: 21
		public static readonly float MinSpeedModifier = 0.85f;

		// Token: 0x04000016 RID: 22
		public static readonly float MaxSpeedModifier = 2f;

		// Token: 0x04000019 RID: 25
		public MechanicalNode _mechanicalNode;

		// Token: 0x0400001A RID: 26
		public Workplace _workplace;

		// Token: 0x0400001B RID: 27
		public Enterable _enterable;

		// Token: 0x0400001C RID: 28
		public readonly List<BonusManager> _bonusManagers = new List<BonusManager>();
	}
}
