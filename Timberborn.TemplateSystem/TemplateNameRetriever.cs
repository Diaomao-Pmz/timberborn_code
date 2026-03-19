using System;
using Timberborn.BaseComponentSystem;

namespace Timberborn.TemplateSystem
{
	// Token: 0x0200000B RID: 11
	public class TemplateNameRetriever
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002317 File Offset: 0x00000517
		public string GetTemplateName(BaseComponent instance)
		{
			return TemplateNameRetriever.GetTemplateName(instance.Name, instance.GetComponent<TemplateSpec>());
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000232A File Offset: 0x0000052A
		public static string GetTemplateName(string name, TemplateSpec persistentTemplate)
		{
			if (persistentTemplate == null)
			{
				throw new TemplateMappingException(name + " has no TemplateSpec component");
			}
			return persistentTemplate.TemplateName;
		}
	}
}
