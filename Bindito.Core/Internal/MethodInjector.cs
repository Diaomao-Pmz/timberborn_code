using System;
using System.Reflection;

namespace Bindito.Core.Internal
{
	// Token: 0x020000AC RID: 172
	public class MethodInjector : IMethodInjector
	{
		// Token: 0x060001BF RID: 447 RVA: 0x00004354 File Offset: 0x00002554
		public MethodInjector(IParameterProvider parameterProvider, IMethodRetriever methodRetriever, IInjectionListenerNotifier injectionListenerNotifier)
		{
			this._parameterProvider = parameterProvider;
			this._methodRetriever = methodRetriever;
			this._injectionListenerNotifier = injectionListenerNotifier;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00004374 File Offset: 0x00002574
		public void Inject(object injectee)
		{
			Type type = injectee.GetType();
			foreach (MethodInfo method in this._methodRetriever.GetInjectedMethods(type))
			{
				this.InjectMethod(injectee, method);
			}
			this._injectionListenerNotifier.NotifyAllListeners(injectee);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000043DC File Offset: 0x000025DC
		private void InjectMethod(object injectee, MethodBase method)
		{
			object[] parameters = this._parameterProvider.GetParameters(method);
			method.Invoke(injectee, parameters);
		}

		// Token: 0x040000A9 RID: 169
		private readonly IParameterProvider _parameterProvider;

		// Token: 0x040000AA RID: 170
		private readonly IMethodRetriever _methodRetriever;

		// Token: 0x040000AB RID: 171
		private readonly IInjectionListenerNotifier _injectionListenerNotifier;
	}
}
