using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using Timberborn.TemplateInstantiation;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000012 RID: 18
	public class BlockObjectFactory
	{
		// Token: 0x0600008B RID: 139 RVA: 0x0000351D File Offset: 0x0000171D
		public BlockObjectFactory(EntityService entityService, TemplateInstantiator templateInstantiator)
		{
			this._entityService = entityService;
			this._templateInstantiator = templateInstantiator;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003533 File Offset: 0x00001733
		public BlockObject CreateUnfinished(BlockObjectSpec template, Placement placement)
		{
			BlockObject component = this._entityService.Instantiate(template.Blueprint).GetComponent<BlockObject>();
			component.Reposition(placement);
			return component;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003552 File Offset: 0x00001752
		public BlockObject CreateFinished(BlockObjectSpec template, Placement placement)
		{
			BlockObject blockObject = this.CreateUnfinished(template, placement);
			blockObject.MarkAsFinished();
			return blockObject;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003562 File Offset: 0x00001762
		public BlockObject CreateAsPreview(BlockObjectSpec template, Transform parent, Placement placement)
		{
			BlockObject componentSlow = this._templateInstantiator.Instantiate(template.Blueprint, parent, null).GetComponentSlow<BlockObject>();
			componentSlow.MarkAsPreview();
			componentSlow.Reposition(placement);
			return componentSlow;
		}

		// Token: 0x04000053 RID: 83
		public readonly EntityService _entityService;

		// Token: 0x04000054 RID: 84
		public readonly TemplateInstantiator _templateInstantiator;
	}
}
