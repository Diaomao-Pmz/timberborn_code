using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.GoodStackSystem;
using Timberborn.Growing;
using Timberborn.NaturalResourcesLifecycle;
using Timberborn.WorldPersistence;
using Timberborn.Yielding;
using UnityEngine;

namespace Timberborn.Cutting
{
	// Token: 0x02000007 RID: 7
	public class Cuttable : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		public event EventHandler WasCut;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000216D File Offset: 0x0000036D
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002175 File Offset: 0x00000375
		public Yielder Yielder { get; private set; }

		// Token: 0x0600000B RID: 11 RVA: 0x0000217E File Offset: 0x0000037E
		public Cuttable(EntityService entityService)
		{
			this._entityService = entityService;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000218D File Offset: 0x0000038D
		public bool RemoveOnCut
		{
			get
			{
				return this._cuttableSpec.RemoveOnCut;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000219A File Offset: 0x0000039A
		public YielderSpec YielderSpec
		{
			get
			{
				return this._cuttableSpec.Yielder;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021A8 File Offset: 0x000003A8
		public void Awake()
		{
			this._livingNaturalResource = base.GetComponent<LivingNaturalResource>();
			this._blockObject = base.GetComponent<BlockObject>();
			this._goodStack = base.GetComponent<GoodStack>();
			this._growable = base.GetComponent<Growable>();
			this._cuttableSpec = base.GetComponent<CuttableSpec>();
			this.Yielder = this.GetNamedComponent(this.YielderSpec.YielderComponentName);
			this.Yielder.YieldDecreased += delegate(object _, EventArgs _)
			{
				this.Cut();
			};
			this._growable.HasGrown += delegate(object _, EventArgs _)
			{
				this.Yielder.Enable();
			};
			this._leftoverModel = base.GameObject.FindChildIfNameNotEmpty(this._cuttableSpec.LeftoverModelName);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002252 File Offset: 0x00000452
		public void Start()
		{
			if (this._goodStack.Enabled && this.Yielder.IsYieldRemoved)
			{
				this.InitializeDisabledAction();
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002274 File Offset: 0x00000474
		public void ShowLeftoverModel()
		{
			if (this._leftoverModel)
			{
				this._leftoverModel.SetActive(true);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000228F File Offset: 0x0000048F
		public void HideLeftoverModel()
		{
			if (this._leftoverModel)
			{
				this._leftoverModel.SetActive(false);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022AA File Offset: 0x000004AA
		public void Cut()
		{
			this.EnableGoodStack();
			this._livingNaturalResource.Die();
			this.InitializeDisabledAction();
			EventHandler wasCut = this.WasCut;
			if (wasCut == null)
			{
				return;
			}
			wasCut(this, EventArgs.Empty);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022D9 File Offset: 0x000004D9
		public void EnableGoodStack()
		{
			if (this.Yielder.IsYielding)
			{
				this._goodStack.EnableGoodStack(this.Yielder.Yield);
				this.Yielder.RemoveRemainingYield();
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002309 File Offset: 0x00000509
		public void InitializeDisabledAction()
		{
			if (this.RemoveOnCut)
			{
				this.InitializeDisabledAction(delegate()
				{
					this._entityService.Delete(this);
				});
				return;
			}
			this.InitializeDisabledAction(new Action(this.MakeOverridable));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002338 File Offset: 0x00000538
		public void InitializeDisabledAction(Action action)
		{
			if (this._goodStack.Inventory.IsEmpty)
			{
				action();
				return;
			}
			this._goodStack.GoodStackDisabled += delegate(object _, EventArgs _)
			{
				action();
			};
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002387 File Offset: 0x00000587
		public void MakeOverridable()
		{
			this._blockObject.MakeOverridable();
		}

		// Token: 0x0400000A RID: 10
		public readonly EntityService _entityService;

		// Token: 0x0400000B RID: 11
		public LivingNaturalResource _livingNaturalResource;

		// Token: 0x0400000C RID: 12
		public BlockObject _blockObject;

		// Token: 0x0400000D RID: 13
		public GoodStack _goodStack;

		// Token: 0x0400000E RID: 14
		public Growable _growable;

		// Token: 0x0400000F RID: 15
		public CuttableSpec _cuttableSpec;

		// Token: 0x04000010 RID: 16
		public GameObject _leftoverModel;
	}
}
