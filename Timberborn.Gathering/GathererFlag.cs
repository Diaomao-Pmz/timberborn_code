using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.TemplateSystem;
using Timberborn.Yielding;

namespace Timberborn.Gathering
{
	// Token: 0x0200000D RID: 13
	public class GathererFlag : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002A53 File Offset: 0x00000C53
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002A5B File Offset: 0x00000C5B
		public ImmutableArray<GatherableSpec> AllowedGatherables { get; private set; }

		// Token: 0x06000055 RID: 85 RVA: 0x00002A64 File Offset: 0x00000C64
		public void Awake()
		{
			this._yieldRemovingBuilding = base.GetComponent<YieldRemovingBuilding>();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002A74 File Offset: 0x00000C74
		public void InitializeEntity()
		{
			this.AllowedGatherables = this.GetAllowedGatherables().ToImmutableArray<GatherableSpec>();
			IEnumerable<string> values = from gatherable in this.AllowedGatherables
			select gatherable.GetSpec<TemplateSpec>().TemplateName;
			this._allowedGatherables.AddRange(values);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002AC9 File Offset: 0x00000CC9
		public bool CanGather(string templateName)
		{
			return this._allowedGatherables.Contains(templateName);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002AD7 File Offset: 0x00000CD7
		public IEnumerable<GatherableSpec> GetAllowedGatherables()
		{
			return from yielder in this._yieldRemovingBuilding.GetAllowedYielders()
			select ((ComponentSpec)yielder).GetSpec<GatherableSpec>();
		}

		// Token: 0x04000023 RID: 35
		public YieldRemovingBuilding _yieldRemovingBuilding;

		// Token: 0x04000024 RID: 36
		public readonly HashSet<string> _allowedGatherables = new HashSet<string>();
	}
}
