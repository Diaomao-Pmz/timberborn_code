using System;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.DuplicationSystem;
using Timberborn.Persistence;
using Timberborn.TemplateSystem;
using Timberborn.WorldPersistence;
using Timberborn.Yielding;

namespace Timberborn.Gathering
{
	// Token: 0x02000009 RID: 9
	public class GatherablePrioritizer : BaseComponent, IAwakableComponent, IPersistentEntity, IDuplicable<GatherablePrioritizer>, IDuplicable, IFinishedStateListener
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600001C RID: 28 RVA: 0x000023B4 File Offset: 0x000005B4
		// (remove) Token: 0x0600001D RID: 29 RVA: 0x000023EC File Offset: 0x000005EC
		public event EventHandler PrioritizedGatherableChanged;

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002421 File Offset: 0x00000621
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002429 File Offset: 0x00000629
		public GatherableSpec PrioritizedGatherable { get; private set; }

		// Token: 0x06000020 RID: 32 RVA: 0x00002432 File Offset: 0x00000632
		public void Awake()
		{
			this._yieldRemovingBuilding = base.GetComponent<YieldRemovingBuilding>();
			base.DisableComponent();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002446 File Offset: 0x00000646
		public void PrioritizeGatherable(GatherableSpec gatherableSpec)
		{
			this.PrioritizedGatherable = gatherableSpec;
			EventHandler prioritizedGatherableChanged = this.PrioritizedGatherableChanged;
			if (prioritizedGatherableChanged == null)
			{
				return;
			}
			prioritizedGatherableChanged(this, EventArgs.Empty);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002465 File Offset: 0x00000665
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000246D File Offset: 0x0000066D
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002478 File Offset: 0x00000678
		public void Save(IEntitySaver entitySaver)
		{
			if (this.PrioritizedGatherable != null)
			{
				IObjectSaver component = entitySaver.GetComponent(GatherablePrioritizer.GatherablePrioritizerKey);
				string templateName = this.PrioritizedGatherable.GetSpec<TemplateSpec>().TemplateName;
				component.Set(GatherablePrioritizer.PrioritizedGatherableKey, templateName);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024BC File Offset: 0x000006BC
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(GatherablePrioritizer.GatherablePrioritizerKey, out objectLoader) && objectLoader.Has<string>(GatherablePrioritizer.PrioritizedGatherableKey))
			{
				string templateName = objectLoader.Get(GatherablePrioritizer.PrioritizedGatherableKey);
				this.PrioritizedGatherable = this.GetGatherable(templateName);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002500 File Offset: 0x00000700
		public void DuplicateFrom(GatherablePrioritizer source)
		{
			GatherableSpec prioritizedGatherable = source.PrioritizedGatherable;
			if (prioritizedGatherable == null || this.SupportsGatherable(prioritizedGatherable))
			{
				this.PrioritizeGatherable(prioritizedGatherable);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000252D File Offset: 0x0000072D
		public bool SupportsGatherable(GatherableSpec gatherableSpec)
		{
			return this.GetGatherable(gatherableSpec.GetSpec<TemplateSpec>().TemplateName) != null;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002548 File Offset: 0x00000748
		public GatherableSpec GetGatherable(string templateName)
		{
			ComponentSpec componentSpec = this._yieldRemovingBuilding.GetAllowedYielders().Cast<ComponentSpec>().SingleOrDefault((ComponentSpec yielder) => yielder.GetSpec<TemplateSpec>().IsNamed(templateName));
			if (componentSpec == null)
			{
				return null;
			}
			return componentSpec.GetSpec<GatherableSpec>();
		}

		// Token: 0x04000011 RID: 17
		public static readonly ComponentKey GatherablePrioritizerKey = new ComponentKey("GatherablePrioritizer");

		// Token: 0x04000012 RID: 18
		public static readonly PropertyKey<string> PrioritizedGatherableKey = new PropertyKey<string>("PrioritizedGatherable");

		// Token: 0x04000015 RID: 21
		public YieldRemovingBuilding _yieldRemovingBuilding;
	}
}
