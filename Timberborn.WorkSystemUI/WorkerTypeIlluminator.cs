using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Bots;
using Timberborn.EntitySystem;
using Timberborn.Illumination;
using Timberborn.WorkerTypesUI;
using Timberborn.WorkSystem;

namespace Timberborn.WorkSystemUI
{
	// Token: 0x02000008 RID: 8
	public class WorkerTypeIlluminator : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002296 File Offset: 0x00000496
		public WorkerTypeIlluminator(WorkerTypeHelper workerTypeHelper, BotColors botColors)
		{
			this._workerTypeHelper = workerTypeHelper;
			this._botColors = botColors;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022AC File Offset: 0x000004AC
		public void Awake()
		{
			this._illuminator = base.GetComponent<Illuminator>();
			this._workplaceWorkerType = base.GetComponent<WorkplaceWorkerType>();
			this._workplaceWorkerType.WorkerTypeChanged += this.OnWorkerTypeChanged;
			this._illuminatorColorizer = this._illuminator.CreateColorizer(20);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022FB File Offset: 0x000004FB
		public void InitializeEntity()
		{
			this.UpdateIlluminator();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002303 File Offset: 0x00000503
		public void UpdateIlluminator()
		{
			if (this._workerTypeHelper.IsBotWorkerType(this._workplaceWorkerType.WorkerType))
			{
				this._illuminatorColorizer.SetColor(this._botColors.BotIlluminationColor);
				return;
			}
			this._illuminatorColorizer.ClearColor();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022FB File Offset: 0x000004FB
		public void OnWorkerTypeChanged(object sender, WorkerTypeChangedEventArgs e)
		{
			this.UpdateIlluminator();
		}

		// Token: 0x0400000F RID: 15
		public readonly WorkerTypeHelper _workerTypeHelper;

		// Token: 0x04000010 RID: 16
		public readonly BotColors _botColors;

		// Token: 0x04000011 RID: 17
		public Illuminator _illuminator;

		// Token: 0x04000012 RID: 18
		public WorkplaceWorkerType _workplaceWorkerType;

		// Token: 0x04000013 RID: 19
		public bool _lightingEnabled;

		// Token: 0x04000014 RID: 20
		public IlluminatorColorizer _illuminatorColorizer;
	}
}
