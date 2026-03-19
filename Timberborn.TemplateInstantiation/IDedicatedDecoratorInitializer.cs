using System;

namespace Timberborn.TemplateInstantiation
{
	// Token: 0x02000007 RID: 7
	public interface IDedicatedDecoratorInitializer<in TSubject, in TDecorator>
	{
		// Token: 0x06000011 RID: 17
		void Initialize(TSubject subject, TDecorator decorator);
	}
}
