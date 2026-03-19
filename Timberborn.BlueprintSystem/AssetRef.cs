using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x02000009 RID: 9
	public class AssetRef<T> : IEquatable<AssetRef<T>> where T : Object
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000236B File Offset: 0x0000056B
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(AssetRef<T>);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002377 File Offset: 0x00000577
		public string Path { get; }

		// Token: 0x06000015 RID: 21 RVA: 0x0000237F File Offset: 0x0000057F
		public AssetRef(string path, Lazy<T> lazyAsset)
		{
			this.Path = path;
			this._lazyAsset = lazyAsset;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002395 File Offset: 0x00000595
		public T Asset
		{
			get
			{
				return this._lazyAsset.Value;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023A4 File Offset: 0x000005A4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AssetRef");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023F0 File Offset: 0x000005F0
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Path = ");
			builder.Append(this.Path);
			builder.Append(", Asset = ");
			builder.Append(this.Asset);
			return true;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000242F File Offset: 0x0000062F
		[CompilerGenerated]
		public static bool operator !=([Nullable(new byte[]
		{
			2,
			0
		})] AssetRef<T> left, [Nullable(new byte[]
		{
			2,
			0
		})] AssetRef<T> right)
		{
			return !(left == right);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000243B File Offset: 0x0000063B
		[CompilerGenerated]
		public static bool operator ==([Nullable(new byte[]
		{
			2,
			0
		})] AssetRef<T> left, [Nullable(new byte[]
		{
			2,
			0
		})] AssetRef<T> right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000244F File Offset: 0x0000064F
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Path>k__BackingField)) * -1521134295 + EqualityComparer<Lazy<T>>.Default.GetHashCode(this._lazyAsset);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000248F File Offset: 0x0000068F
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AssetRef<T>);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024A0 File Offset: 0x000006A0
		[CompilerGenerated]
		public virtual bool Equals([Nullable(new byte[]
		{
			2,
			0
		})] AssetRef<T> other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Path>k__BackingField, other.<Path>k__BackingField) && EqualityComparer<Lazy<T>>.Default.Equals(this._lazyAsset, other._lazyAsset));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002501 File Offset: 0x00000701
		[CompilerGenerated]
		protected AssetRef([Nullable(new byte[]
		{
			1,
			0
		})] AssetRef<T> original)
		{
			this.Path = original.<Path>k__BackingField;
			this._lazyAsset = original._lazyAsset;
		}

		// Token: 0x0400000D RID: 13
		public readonly Lazy<T> _lazyAsset;
	}
}
