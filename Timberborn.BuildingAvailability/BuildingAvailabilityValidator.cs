using System;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.TemplateSystem;
using Timberborn.UnderstructureSystem;

namespace Timberborn.BuildingAvailability
{
	// Token: 0x02000005 RID: 5
	public class BuildingAvailabilityValidator
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020E5 File Offset: 0x000002E5
		public BuildingAvailabilityValidator(EntityRegistry entityRegistry, TemplateNameMapper templateNameMapper)
		{
			this._entityRegistry = entityRegistry;
			this._templateNameMapper = templateNameMapper;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020FC File Offset: 0x000002FC
		public bool IsAvailableForPlacement(ComponentSpec spec)
		{
			UnderstructureConstraintSpec spec2 = spec.GetSpec<UnderstructureConstraintSpec>();
			return spec2 == null || this.AnyUnderstructureWasInstantiated(spec2) || this.AnyUnderstrucuteIsBuildableByPlayer(spec2);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002125 File Offset: 0x00000325
		public bool AnyUnderstructureWasInstantiated(UnderstructureConstraintSpec understructureConstraintSpec)
		{
			return understructureConstraintSpec.UnderstructureTemplateNames.FastAny((string templateName) => this._entityRegistry.WasTemplateInstantiated(templateName));
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002143 File Offset: 0x00000343
		public bool AnyUnderstrucuteIsBuildableByPlayer(UnderstructureConstraintSpec understructureConstraintSpec)
		{
			return understructureConstraintSpec.UnderstructureTemplateNames.FastAny(delegate(string templateName)
			{
				TemplateSpec templateSpec;
				if (this._templateNameMapper.TryGetTemplate(templateName, out templateSpec))
				{
					PlaceableBlockObjectSpec spec = templateSpec.GetSpec<PlaceableBlockObjectSpec>();
					return spec != null && !spec.DevModeTool;
				}
				return false;
			});
		}

		// Token: 0x04000006 RID: 6
		public readonly EntityRegistry _entityRegistry;

		// Token: 0x04000007 RID: 7
		public readonly TemplateNameMapper _templateNameMapper;
	}
}
