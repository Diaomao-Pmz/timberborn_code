using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.MapStateSystem;
using Timberborn.SoilContaminationSystem;
using Timberborn.SoilMoistureSystem;
using Timberborn.TimeSystem;

namespace Timberborn.Ruins
{
	// Token: 0x0200000E RID: 14
	public class RuinModelUpdater : BaseComponent, IAwakableComponent, IStartableComponent, IPostInitializableEntity, IDeletableEntity
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002822 File Offset: 0x00000A22
		public RuinModelUpdater(ITimeTriggerFactory timeTriggerFactory, IRandomNumberGenerator randomNumberGenerator, MapEditorMode mapEditorMode, RuinModelFactory ruinModelFactory)
		{
			this._timeTriggerFactory = timeTriggerFactory;
			this._randomNumberGenerator = randomNumberGenerator;
			this._mapEditorMode = mapEditorMode;
			this._ruinModelFactory = ruinModelFactory;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002847 File Offset: 0x00000A47
		public void Awake()
		{
			this._ruin = base.GetComponent<Ruin>();
			this._ruinModels = base.GetComponent<RuinModels>();
			this._dryObject = base.GetComponent<DryObject>();
			this._contaminatedObject = base.GetComponent<ContaminatedObject>();
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002885 File Offset: 0x00000A85
		public void Start()
		{
			if (this._blockObject.IsPreview)
			{
				this.CreateModels();
				this._ruinModels.ShowWetModel();
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000028A8 File Offset: 0x00000AA8
		public void PostInitializeEntity()
		{
			this.CreateModels();
			this._dryObject.EnteredDryState += delegate(object _, EventArgs _)
			{
				this.ChangeDryState();
			};
			this._dryObject.ExitedDryState += delegate(object _, EventArgs _)
			{
				this.ChangeDryState();
			};
			this._contaminatedObject.EnteredContaminatedState += delegate(object _, EventArgs _)
			{
				this.ChangeDryState();
			};
			this._contaminatedObject.ExitedContaminatedState += delegate(object _, EventArgs _)
			{
				this.ChangeDryState();
			};
			this.UpdateModel();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000291D File Offset: 0x00000B1D
		public void DeleteEntity()
		{
			ITimeTrigger timeTrigger = this._timeTrigger;
			if (timeTrigger == null)
			{
				return;
			}
			timeTrigger.Pause();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002930 File Offset: 0x00000B30
		public void ChangeDryState()
		{
			if (this._mapEditorMode.IsMapEditor)
			{
				this.UpdateModel();
				return;
			}
			float delayInDays = this._randomNumberGenerator.Range(0f, 2f) / 24f;
			ITimeTrigger timeTrigger = this._timeTrigger;
			if (timeTrigger != null)
			{
				timeTrigger.Pause();
			}
			this._timeTrigger = this._timeTriggerFactory.Create(new Action(this.UpdateModel), delayInDays);
			this._timeTrigger.Resume();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000029A7 File Offset: 0x00000BA7
		public void UpdateModel()
		{
			if (this._dryObject.IsDry || this._contaminatedObject.IsContaminated)
			{
				this._ruinModels.ShowDryModel();
				return;
			}
			this._ruinModels.ShowWetModel();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000029DA File Offset: 0x00000BDA
		public void CreateModels()
		{
			if (!this._ruinModels.IsInitialized)
			{
				this._ruinModelFactory.CreateModels(this._ruinModels.VariantId, this._ruin);
			}
		}

		// Token: 0x0400001C RID: 28
		public readonly ITimeTriggerFactory _timeTriggerFactory;

		// Token: 0x0400001D RID: 29
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400001E RID: 30
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x0400001F RID: 31
		public readonly RuinModelFactory _ruinModelFactory;

		// Token: 0x04000020 RID: 32
		public Ruin _ruin;

		// Token: 0x04000021 RID: 33
		public RuinModels _ruinModels;

		// Token: 0x04000022 RID: 34
		public DryObject _dryObject;

		// Token: 0x04000023 RID: 35
		public ContaminatedObject _contaminatedObject;

		// Token: 0x04000024 RID: 36
		public BlockObject _blockObject;

		// Token: 0x04000025 RID: 37
		public ITimeTrigger _timeTrigger;
	}
}
