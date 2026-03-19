using System;
using System.Collections.Generic;
using System.Linq;

namespace Bindito.Core.Internal
{
	// Token: 0x02000088 RID: 136
	public class Container : IContainer
	{
		// Token: 0x06000146 RID: 326 RVA: 0x00003618 File Offset: 0x00001818
		public Container(IInstanceBank instanceBank, IValidatingMethodInjector validatingMethodInjector, IBoundInstanceService boundInstanceService, IContainerCreator ownCreator)
		{
			this._instanceBank = instanceBank;
			this._validatingMethodInjector = validatingMethodInjector;
			this._boundInstanceService = boundInstanceService;
			this._ownCreator = ownCreator;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000363D File Offset: 0x0000183D
		public T GetInstance<T>()
		{
			return (T)((object)this.GetInstance(typeof(T)));
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00003654 File Offset: 0x00001854
		public IEnumerable<T> GetInstances<T>()
		{
			return this.GetInstances(typeof(T)).Cast<T>();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000366C File Offset: 0x0000186C
		public object GetInstance(Type type)
		{
			object result;
			if (this._instanceBank.TryGetInstance(type, out result))
			{
				return result;
			}
			throw new BinditoException("No binding exists for type " + TypeFormatting.Format(type) + ".");
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000036A5 File Offset: 0x000018A5
		public IEnumerable<object> GetInstances(Type type)
		{
			return this._instanceBank.GetInstances(type);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000036B3 File Offset: 0x000018B3
		public IEnumerable<object> GetBoundInstances()
		{
			return this._boundInstanceService.GetBoundInstances();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000036C0 File Offset: 0x000018C0
		public void Inject(object instance)
		{
			try
			{
				this._validatingMethodInjector.Inject(instance);
			}
			catch (Exception innerException)
			{
				throw new BinditoException("Failed to inject into " + TypeFormatting.Format(instance.GetType()) + ".", innerException);
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00003710 File Offset: 0x00001910
		public IContainer CreateChildContainer(params IConfigurator[] configurators)
		{
			return this.CreateChildContainer(configurators.AsEnumerable<IConfigurator>());
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000371E File Offset: 0x0000191E
		public IContainer CreateChildContainer(IEnumerable<IConfigurator> configurators)
		{
			return this._ownCreator.CreateChildContainer(configurators);
		}

		// Token: 0x04000086 RID: 134
		private readonly IInstanceBank _instanceBank;

		// Token: 0x04000087 RID: 135
		private readonly IValidatingMethodInjector _validatingMethodInjector;

		// Token: 0x04000088 RID: 136
		private readonly IBoundInstanceService _boundInstanceService;

		// Token: 0x04000089 RID: 137
		private readonly IContainerCreator _ownCreator;
	}
}
