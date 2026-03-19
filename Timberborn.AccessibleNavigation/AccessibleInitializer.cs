using System;
using Timberborn.Navigation;
using Timberborn.TemplateInstantiation;

namespace Timberborn.AccessibleNavigation
{
	// Token: 0x02000004 RID: 4
	public class AccessibleInitializer : IDedicatedDecoratorInitializer<IAccessibleNeeder, Accessible>
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public void Initialize(IAccessibleNeeder subject, Accessible decorator)
		{
			decorator.Initialize(subject.AccessibleComponentName);
			subject.SetAccessible(decorator);
		}
	}
}
