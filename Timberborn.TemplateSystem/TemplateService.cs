using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.TemplateCollectionSystem;

namespace Timberborn.TemplateSystem
{
	// Token: 0x0200000C RID: 12
	public class TemplateService
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000234C File Offset: 0x0000054C
		public TemplateService(TemplateCollectionService templateCollectionService)
		{
			this._templateCollectionService = templateCollectionService;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000235C File Offset: 0x0000055C
		public IEnumerable<T> GetAll<T>()
		{
			List<T> list = new List<T>();
			foreach (Blueprint blueprint in this._templateCollectionService.AllTemplates)
			{
				blueprint.GetSpecs<T>(list);
			}
			return list;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000239C File Offset: 0x0000059C
		public T GetSingle<T>()
		{
			List<T> list = this.GetAll<T>().ToList<T>();
			if (list.Count == 1)
			{
				return list[0];
			}
			throw new InvalidOperationException("Blueprint with component " + typeof(T).Name + " not found or multiple templates/components found");
		}

		// Token: 0x0400000D RID: 13
		public readonly TemplateCollectionService _templateCollectionService;
	}
}
