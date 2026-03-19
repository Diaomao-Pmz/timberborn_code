using System;

namespace Bindito.Core.Internal
{
	// Token: 0x020000B0 RID: 176
	public class ProvisionBinding
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00004632 File Offset: 0x00002832
		public Type Type { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000463A File Offset: 0x0000283A
		public Type ProviderType { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00004642 File Offset: 0x00002842
		public IProvider<object> ProviderInstance { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000464A File Offset: 0x0000284A
		public Func<object> ProvidingMethod { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00004652 File Offset: 0x00002852
		public object Instance { get; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000465A File Offset: 0x0000285A
		public Type ExistingType { get; }

		// Token: 0x060001D6 RID: 470 RVA: 0x00004662 File Offset: 0x00002862
		private ProvisionBinding(Type type, Type providerType, IProvider<object> providerInstance, Func<object> providingMethod, object instance, Type existingType)
		{
			this.Type = type;
			this.ProviderType = providerType;
			this.ProviderInstance = providerInstance;
			this.ProvidingMethod = providingMethod;
			this.Instance = instance;
			this.ExistingType = existingType;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00004697 File Offset: 0x00002897
		public static ProvisionBinding CreateToType(Type type)
		{
			return new ProvisionBinding(type, null, null, null, null, null);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000046A4 File Offset: 0x000028A4
		public static ProvisionBinding CreateToProviderType(Type providerType)
		{
			return new ProvisionBinding(null, providerType, null, null, null, null);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000046B1 File Offset: 0x000028B1
		public static ProvisionBinding CreateToProviderInstance(IProvider<object> provider)
		{
			return new ProvisionBinding(null, null, provider, null, null, null);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000046BE File Offset: 0x000028BE
		public static ProvisionBinding CreateToProvidingMethod(Func<object> providingMethod)
		{
			return new ProvisionBinding(null, null, null, providingMethod, null, null);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000046CB File Offset: 0x000028CB
		public static ProvisionBinding CreateToInstance(object instance)
		{
			return new ProvisionBinding(null, null, null, null, instance, null);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000046D8 File Offset: 0x000028D8
		public static ProvisionBinding CreateToExisting(Type type)
		{
			return new ProvisionBinding(null, null, null, null, null, type);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000046E8 File Offset: 0x000028E8
		public override string ToString()
		{
			if (this.Type != null)
			{
				return "Type(" + TypeFormatting.Format(this.Type) + ")";
			}
			if (this.ProviderType != null)
			{
				return "ProviderType(" + TypeFormatting.Format(this.ProviderType) + ")";
			}
			if (this.ProviderInstance != null)
			{
				return string.Format("{0}({1})", "ProviderInstance", this.ProviderInstance);
			}
			if (this.ProvidingMethod != null)
			{
				return string.Format("{0}({1})", "ProvidingMethod", this.ProvidingMethod);
			}
			if (this.Instance != null)
			{
				return string.Format("{0}({1})", "Instance", this.Instance);
			}
			if (this.ExistingType != null)
			{
				return "ExistingType(" + TypeFormatting.Format(this.ExistingType) + ")";
			}
			throw new InvalidOperationException("This is an internal Bindito error!");
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000047D4 File Offset: 0x000029D4
		public bool TryGetBindingType(out Type bindingType)
		{
			if (this.ProvidingMethod != null)
			{
				bindingType = null;
				return false;
			}
			bindingType = this.GetBindingType();
			return true;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000047EC File Offset: 0x000029EC
		private Type GetBindingType()
		{
			if (this.Type != null)
			{
				return this.Type;
			}
			if (this.ProviderType != null)
			{
				return this.ProviderType;
			}
			if (this.ProviderInstance != null)
			{
				return this.ProviderInstance.GetType();
			}
			if (this.Instance != null)
			{
				return this.Instance.GetType();
			}
			if (this.ExistingType != null)
			{
				return this.ExistingType;
			}
			throw new InvalidOperationException("Binding type not found for " + this.ToString());
		}
	}
}
