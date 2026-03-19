using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.WorldPersistence;
using Timberborn.Yielding;
using UnityEngine;

namespace Timberborn.Ruins
{
	// Token: 0x02000007 RID: 7
	public class Ruin : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public Yielder Yielder { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002111 File Offset: 0x00000311
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002119 File Offset: 0x00000319
		public Transform ModelParent { get; private set; }

		// Token: 0x0600000B RID: 11 RVA: 0x00002122 File Offset: 0x00000322
		public Ruin(RuinReplacer ruinReplacer)
		{
			this._ruinReplacer = ruinReplacer;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002131 File Offset: 0x00000331
		public int SpecifiedHeight
		{
			get
			{
				return this._ruinSpec.RuinHeight;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000213E File Offset: 0x0000033E
		public YielderSpec YielderSpec
		{
			get
			{
				return this._ruinSpec.Yielder;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000214C File Offset: 0x0000034C
		public void Awake()
		{
			this._ruinSpec = base.GetComponent<RuinSpec>();
			this.Yielder = this.GetNamedComponent(this.YielderSpec.YielderComponentName);
			this.ModelParent = base.GameObject.FindChildTransform(this._ruinSpec.ModelParentName);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002198 File Offset: 0x00000398
		public void InitializeEntity()
		{
			this.Yielder.YieldDecreased += this.OnYieldDecreased;
			this.Yielder.Enable();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021BC File Offset: 0x000003BC
		public void OnYieldDecreased(object sender, EventArgs e)
		{
			this.UpdateHeight();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021C4 File Offset: 0x000003C4
		public void UpdateHeight()
		{
			int num = Mathf.CeilToInt((float)this.YielderSpec.Yield.Amount / (float)this.SpecifiedHeight);
			if (Mathf.CeilToInt((float)this.Yielder.Yield.Amount / (float)num) != this.SpecifiedHeight)
			{
				this._ruinReplacer.Shrink(this);
			}
		}

		// Token: 0x0400000A RID: 10
		public readonly RuinReplacer _ruinReplacer;

		// Token: 0x0400000B RID: 11
		public RuinSpec _ruinSpec;
	}
}
