using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.SingletonSystem;

namespace Timberborn.TemplateSystem
{
	// Token: 0x0200000A RID: 10
	public class TemplateNameMapper : ILoadableSingleton
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000216D File Offset: 0x0000036D
		public TemplateNameMapper(TemplateService templateService)
		{
			this._templateService = templateService;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000217C File Offset: 0x0000037C
		public void Load()
		{
			this._templates = new Dictionary<string, TemplateSpec>();
			List<TemplateSpec> list = this._templateService.GetAll<TemplateSpec>().ToList<TemplateSpec>();
			foreach (TemplateSpec templateSpec in list)
			{
				this.TryAddTemplate(templateSpec.TemplateName, templateSpec, true);
			}
			foreach (TemplateSpec templateSpec2 in list)
			{
				foreach (string templateName in templateSpec2.BackwardCompatibleTemplateNames)
				{
					this.TryAddTemplate(templateName, templateSpec2, false);
				}
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002254 File Offset: 0x00000454
		public TemplateSpec GetTemplate(string templateName)
		{
			TemplateSpec result;
			if (this._templates.TryGetValue(templateName, out result))
			{
				return result;
			}
			throw new TemplateMappingException("No template found with name '" + templateName + "'");
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002288 File Offset: 0x00000488
		public bool TryGetTemplate(string templateName, out TemplateSpec templateSpec)
		{
			return this._templates.TryGetValue(templateName, out templateSpec);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002298 File Offset: 0x00000498
		public void TryAddTemplate(string templateName, TemplateSpec templateSpec, bool throwIfDuplicated)
		{
			if (!this._templates.TryAdd(templateName, templateSpec) && throwIfDuplicated)
			{
				TemplateSpec templateSpec2 = this._templates[templateName];
				throw new TemplateMappingException(string.Concat(new string[]
				{
					"Duplicate template name ",
					templateName,
					" in objects: ",
					templateSpec.Blueprint.Name,
					", ",
					templateSpec2.Blueprint.Name,
					"."
				}));
			}
		}

		// Token: 0x0400000B RID: 11
		public readonly TemplateService _templateService;

		// Token: 0x0400000C RID: 12
		public Dictionary<string, TemplateSpec> _templates;
	}
}
