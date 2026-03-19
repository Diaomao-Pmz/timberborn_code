using System;
using System.Collections.Generic;
using System.Linq;

namespace Bindito.Core.Internal
{
	// Token: 0x0200007F RID: 127
	public class BindingAnalyser : IBindingAnalyser
	{
		// Token: 0x0600011A RID: 282 RVA: 0x00002D3A File Offset: 0x00000F3A
		public BindingAnalyser(IDependencyRetriever dependencyRetriever, IBindingResolver bindingResolver)
		{
			this._dependencyRetriever = dependencyRetriever;
			this._bindingResolver = bindingResolver;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00002D68 File Offset: 0x00000F68
		public BindingAnalysis Analyse(Type suspectType, ProvisionBinding suspectProvisionBinding)
		{
			this._dependencyStack.Clear();
			this._dependencyStack.Push(suspectType);
			BindingAnalysis result;
			switch (this.CheckForProblemsCached(suspectProvisionBinding))
			{
			case BindingAnalyser.CheckResult.Ok:
				result = BindingAnalysis.Ok();
				break;
			case BindingAnalyser.CheckResult.CyclicDependency:
				result = BindingAnalysis.CyclicDependency(this.CreateDependencyChain());
				break;
			case BindingAnalyser.CheckResult.MissingDependency:
				result = BindingAnalysis.MissingDependency(this.CreateDependencyChain());
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return result;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00002DD4 File Offset: 0x00000FD4
		private BindingAnalyser.CheckResult CheckForProblemsCached(ProvisionBinding suspect)
		{
			Type key;
			if (suspect.TryGetBindingType(out key))
			{
				if (!this._cachedResults.ContainsKey(key))
				{
					this._cachedResults[key] = this.CheckForProblems(suspect);
				}
				return this._cachedResults[key];
			}
			return BindingAnalyser.CheckResult.Ok;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00002E1C File Offset: 0x0000101C
		private BindingAnalyser.CheckResult CheckForProblems(ProvisionBinding suspect)
		{
			using (IEnumerator<Type> enumerator = this._dependencyRetriever.GetDependencies(suspect).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Type dependency = enumerator.Current;
					this._dependencyStack.Push(dependency);
					if (this._dependencyStack.Count((Type dependencyInChain) => dependencyInChain == dependency) > 1)
					{
						return BindingAnalyser.CheckResult.CyclicDependency;
					}
					IEnumerable<Binding> enumerable;
					if (!this._bindingResolver.ResolveBindings(dependency, out enumerable))
					{
						return BindingAnalyser.CheckResult.MissingDependency;
					}
					foreach (Binding binding in enumerable)
					{
						BindingAnalyser.CheckResult checkResult = this.CheckForProblemsCached(binding.ProvisionBinding);
						if (checkResult != BindingAnalyser.CheckResult.Ok)
						{
							return checkResult;
						}
					}
					this._dependencyStack.Pop();
				}
			}
			return BindingAnalyser.CheckResult.Ok;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00002F24 File Offset: 0x00001124
		private IEnumerable<Type> CreateDependencyChain()
		{
			return this._dependencyStack.Reverse<Type>().ToList<Type>();
		}

		// Token: 0x04000072 RID: 114
		private readonly IDependencyRetriever _dependencyRetriever;

		// Token: 0x04000073 RID: 115
		private readonly IBindingResolver _bindingResolver;

		// Token: 0x04000074 RID: 116
		private readonly Stack<Type> _dependencyStack = new Stack<Type>();

		// Token: 0x04000075 RID: 117
		private readonly Dictionary<Type, BindingAnalyser.CheckResult> _cachedResults = new Dictionary<Type, BindingAnalyser.CheckResult>();

		// Token: 0x020000B6 RID: 182
		private enum CheckResult
		{
			// Token: 0x040000BF RID: 191
			Ok,
			// Token: 0x040000C0 RID: 192
			CyclicDependency,
			// Token: 0x040000C1 RID: 193
			MissingDependency
		}
	}
}
