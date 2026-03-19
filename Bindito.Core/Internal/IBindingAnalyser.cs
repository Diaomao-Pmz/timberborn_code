using System;

namespace Bindito.Core.Internal
{
	// Token: 0x0200008E RID: 142
	public interface IBindingAnalyser
	{
		// Token: 0x0600016D RID: 365
		BindingAnalysis Analyse(Type suspectType, ProvisionBinding suspectProvisionBinding);
	}
}
