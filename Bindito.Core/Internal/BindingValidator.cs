using System;
using System.Collections.Generic;
using System.Linq;

namespace Bindito.Core.Internal
{
	// Token: 0x02000084 RID: 132
	public class BindingValidator : IBindingValidator
	{
		// Token: 0x0600013B RID: 315 RVA: 0x00003369 File Offset: 0x00001569
		public BindingValidator(IBindingAnalyser bindingAnalyser)
		{
			this._bindingAnalyser = bindingAnalyser;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00003378 File Offset: 0x00001578
		public void Validate(Type type, ProvisionBinding provisionBinding)
		{
			BindingAnalysis bindingAnalysis = this._bindingAnalyser.Analyse(type, provisionBinding);
			IReadOnlyList<Type> dependencyChain = bindingAnalysis.DependencyChain;
			if (bindingAnalysis.HasCyclicDependency)
			{
				throw new BinditoException("Cyclic dependency: " + TypeFormatting.FormatChain(dependencyChain) + ".");
			}
			if (bindingAnalysis.HasMissingDependency)
			{
				Type type2 = dependencyChain.Last<Type>();
				throw new BinditoException(string.Concat(new string[]
				{
					TypeFormatting.Format(type),
					" isn't instantiable due to missing dependency: ",
					TypeFormatting.Format(type2),
					". Dependency chain: ",
					TypeFormatting.FormatChain(dependencyChain),
					"."
				}));
			}
		}

		// Token: 0x04000082 RID: 130
		private readonly IBindingAnalyser _bindingAnalyser;
	}
}
