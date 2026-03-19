using System;
using System.Resources;
using System.Runtime.CompilerServices;
using FxResources.System.Collections.Immutable;

namespace System
{
	// Token: 0x0200000B RID: 11
	internal static class SR
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000020C4 File Offset: 0x000002C4
		private static bool GetUsingResourceKeysSwitchValue()
		{
			bool flag;
			return AppContext.TryGetSwitch("System.Resources.UseSystemResourceKeys", out flag) && flag;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020E2 File Offset: 0x000002E2
		internal static bool UsingResourceKeys()
		{
			return System.SR.s_usingResourceKeys;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020EC File Offset: 0x000002EC
		private static string GetResourceString(string resourceKey)
		{
			if (System.SR.UsingResourceKeys())
			{
				return resourceKey;
			}
			string result = null;
			try
			{
				result = System.SR.ResourceManager.GetString(resourceKey);
			}
			catch (MissingManifestResourceException)
			{
			}
			return result;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002128 File Offset: 0x00000328
		private static string GetResourceString(string resourceKey, string defaultString)
		{
			string resourceString = System.SR.GetResourceString(resourceKey);
			if (!(resourceKey == resourceString) && resourceString != null)
			{
				return resourceString;
			}
			return defaultString;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000214B File Offset: 0x0000034B
		[NullableContext(1)]
		internal static string Format(string resourceFormat, [Nullable(2)] object p1)
		{
			if (System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1
				});
			}
			return string.Format(resourceFormat, p1);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002174 File Offset: 0x00000374
		[NullableContext(1)]
		internal static string Format(string resourceFormat, [Nullable(2)] object p1, [Nullable(2)] object p2)
		{
			if (System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1,
					p2
				});
			}
			return string.Format(resourceFormat, p1, p2);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021A2 File Offset: 0x000003A2
		[NullableContext(2)]
		[return: Nullable(1)]
		internal static string Format([Nullable(1)] string resourceFormat, object p1, object p2, object p3)
		{
			if (System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1,
					p2,
					p3
				});
			}
			return string.Format(resourceFormat, p1, p2, p3);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021D5 File Offset: 0x000003D5
		[NullableContext(1)]
		internal static string Format(string resourceFormat, [Nullable(2)] params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (System.SR.UsingResourceKeys())
			{
				return resourceFormat + ", " + string.Join(", ", args);
			}
			return string.Format(resourceFormat, args);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002201 File Offset: 0x00000401
		[NullableContext(1)]
		internal static string Format([Nullable(2)] IFormatProvider provider, string resourceFormat, [Nullable(2)] object p1)
		{
			if (System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1
				});
			}
			return string.Format(provider, resourceFormat, p1);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000222B File Offset: 0x0000042B
		[NullableContext(2)]
		[return: Nullable(1)]
		internal static string Format(IFormatProvider provider, [Nullable(1)] string resourceFormat, object p1, object p2)
		{
			if (System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1,
					p2
				});
			}
			return string.Format(provider, resourceFormat, p1, p2);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000225A File Offset: 0x0000045A
		[NullableContext(2)]
		[return: Nullable(1)]
		internal static string Format(IFormatProvider provider, [Nullable(1)] string resourceFormat, object p1, object p2, object p3)
		{
			if (System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1,
					p2,
					p3
				});
			}
			return string.Format(provider, resourceFormat, p1, p2, p3);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002290 File Offset: 0x00000490
		[NullableContext(1)]
		internal static string Format([Nullable(2)] IFormatProvider provider, string resourceFormat, [Nullable(2)] params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (System.SR.UsingResourceKeys())
			{
				return resourceFormat + ", " + string.Join(", ", args);
			}
			return string.Format(provider, resourceFormat, args);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000022BD File Offset: 0x000004BD
		internal static ResourceManager ResourceManager
		{
			get
			{
				ResourceManager result;
				if ((result = System.SR.s_resourceManager) == null)
				{
					result = (System.SR.s_resourceManager = new ResourceManager(typeof(FxResources.System.Collections.Immutable.SR)));
				}
				return result;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022DD File Offset: 0x000004DD
		internal static string Arg_KeyNotFoundWithKey
		{
			get
			{
				return System.SR.GetResourceString("Arg_KeyNotFoundWithKey");
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000022E9 File Offset: 0x000004E9
		internal static string ArrayInitializedStateNotEqual
		{
			get
			{
				return System.SR.GetResourceString("ArrayInitializedStateNotEqual");
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000022F5 File Offset: 0x000004F5
		internal static string ArrayLengthsNotEqual
		{
			get
			{
				return System.SR.GetResourceString("ArrayLengthsNotEqual");
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002301 File Offset: 0x00000501
		internal static string CannotFindOldValue
		{
			get
			{
				return System.SR.GetResourceString("CannotFindOldValue");
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000230D File Offset: 0x0000050D
		internal static string CapacityMustBeGreaterThanOrEqualToCount
		{
			get
			{
				return System.SR.GetResourceString("CapacityMustBeGreaterThanOrEqualToCount");
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002319 File Offset: 0x00000519
		internal static string CapacityMustEqualCountOnMove
		{
			get
			{
				return System.SR.GetResourceString("CapacityMustEqualCountOnMove");
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002325 File Offset: 0x00000525
		internal static string CollectionModifiedDuringEnumeration
		{
			get
			{
				return System.SR.GetResourceString("CollectionModifiedDuringEnumeration");
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002331 File Offset: 0x00000531
		internal static string DuplicateKey
		{
			get
			{
				return System.SR.GetResourceString("DuplicateKey");
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000233D File Offset: 0x0000053D
		internal static string InvalidEmptyOperation
		{
			get
			{
				return System.SR.GetResourceString("InvalidEmptyOperation");
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002349 File Offset: 0x00000549
		internal static string InvalidOperationOnDefaultArray
		{
			get
			{
				return System.SR.GetResourceString("InvalidOperationOnDefaultArray");
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002355 File Offset: 0x00000555
		internal static string Arg_HTCapacityOverflow
		{
			get
			{
				return System.SR.GetResourceString("Arg_HTCapacityOverflow");
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002361 File Offset: 0x00000561
		internal static string Arg_RankMultiDimNotSupported
		{
			get
			{
				return System.SR.GetResourceString("Arg_RankMultiDimNotSupported");
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000236D File Offset: 0x0000056D
		internal static string Arg_NonZeroLowerBound
		{
			get
			{
				return System.SR.GetResourceString("Arg_NonZeroLowerBound");
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002379 File Offset: 0x00000579
		internal static string Arg_ArrayPlusOffTooSmall
		{
			get
			{
				return System.SR.GetResourceString("Arg_ArrayPlusOffTooSmall");
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002385 File Offset: 0x00000585
		internal static string Argument_IncompatibleArrayType
		{
			get
			{
				return System.SR.GetResourceString("Argument_IncompatibleArrayType");
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002391 File Offset: 0x00000591
		internal static string ArgumentOutOfRange_NeedNonNegNum
		{
			get
			{
				return System.SR.GetResourceString("ArgumentOutOfRange_NeedNonNegNum");
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000239D File Offset: 0x0000059D
		internal static string InvalidOperation_IncompatibleComparer
		{
			get
			{
				return System.SR.GetResourceString("InvalidOperation_IncompatibleComparer");
			}
		}

		// Token: 0x04000005 RID: 5
		private static readonly bool s_usingResourceKeys = System.SR.GetUsingResourceKeysSwitchValue();

		// Token: 0x04000006 RID: 6
		private static ResourceManager s_resourceManager;
	}
}
