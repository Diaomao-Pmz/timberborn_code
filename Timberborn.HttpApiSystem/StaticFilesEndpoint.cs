using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Timberborn.SingletonSystem;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x0200003E RID: 62
	public class StaticFilesEndpoint : IHttpApiEndpoint, ILoadableSingleton
	{
		// Token: 0x0600013D RID: 317 RVA: 0x000067E0 File Offset: 0x000049E0
		public void Load()
		{
			string path2 = Path.Combine(HttpApi.RootPath, StaticFilesEndpoint.StaticDirectoryName);
			this._staticFiles = Directory.EnumerateFiles(path2).Where(new Func<string, bool>(StaticFilesEndpoint.AnyExtensionContentTypeMatches)).ToImmutableDictionary((string path) => "/" + Path.GetFileName(path), new Func<string, byte[]>(File.ReadAllBytes));
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000684C File Offset: 0x00004A4C
		public Task<bool> TryHandle(HttpListenerContext context)
		{
			StaticFilesEndpoint.<TryHandle>d__4 <TryHandle>d__;
			<TryHandle>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<TryHandle>d__.<>4__this = this;
			<TryHandle>d__.context = context;
			<TryHandle>d__.<>1__state = -1;
			<TryHandle>d__.<>t__builder.Start<StaticFilesEndpoint.<TryHandle>d__4>(ref <TryHandle>d__);
			return <TryHandle>d__.<>t__builder.Task;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006898 File Offset: 0x00004A98
		public static string GetContentType(string path)
		{
			foreach (StaticFilesEndpoint.ExtensionContentType extensionContentType in StaticFilesEndpoint.ExtensionsContentTypes)
			{
				if (path.EndsWith(extensionContentType.Extension))
				{
					return extensionContentType.ContentType;
				}
			}
			throw new ArgumentException(path);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000068E0 File Offset: 0x00004AE0
		public static bool AnyExtensionContentTypeMatches(string path)
		{
			return StaticFilesEndpoint.ExtensionsContentTypes.Any((StaticFilesEndpoint.ExtensionContentType extensionsContentType) => path.EndsWith(extensionsContentType.Extension));
		}

		// Token: 0x04000100 RID: 256
		public static readonly string StaticDirectoryName = "static";

		// Token: 0x04000101 RID: 257
		public static readonly ImmutableArray<StaticFilesEndpoint.ExtensionContentType> ExtensionsContentTypes = ImmutableArray.Create<StaticFilesEndpoint.ExtensionContentType>().Add(new StaticFilesEndpoint.ExtensionContentType(".css", "text/css; charset=utf-8")).Add(new StaticFilesEndpoint.ExtensionContentType(".png", "image/png; charset=utf-8"));

		// Token: 0x04000102 RID: 258
		public ImmutableDictionary<string, byte[]> _staticFiles;

		// Token: 0x0200003F RID: 63
		public class ExtensionContentType : IEquatable<StaticFilesEndpoint.ExtensionContentType>
		{
			// Token: 0x06000143 RID: 323 RVA: 0x0000695F File Offset: 0x00004B5F
			public ExtensionContentType(string Extension, string ContentType)
			{
				this.Extension = Extension;
				this.ContentType = ContentType;
				base..ctor();
			}

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x06000144 RID: 324 RVA: 0x00006975 File Offset: 0x00004B75
			[Nullable(1)]
			[CompilerGenerated]
			protected virtual Type EqualityContract
			{
				[NullableContext(1)]
				[CompilerGenerated]
				get
				{
					return typeof(StaticFilesEndpoint.ExtensionContentType);
				}
			}

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x06000145 RID: 325 RVA: 0x00006981 File Offset: 0x00004B81
			// (set) Token: 0x06000146 RID: 326 RVA: 0x00006989 File Offset: 0x00004B89
			public string Extension { get; set; }

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x06000147 RID: 327 RVA: 0x00006992 File Offset: 0x00004B92
			// (set) Token: 0x06000148 RID: 328 RVA: 0x0000699A File Offset: 0x00004B9A
			public string ContentType { get; set; }

			// Token: 0x06000149 RID: 329 RVA: 0x000069A4 File Offset: 0x00004BA4
			[NullableContext(1)]
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("ExtensionContentType");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x0600014A RID: 330 RVA: 0x000069F0 File Offset: 0x00004BF0
			[NullableContext(1)]
			[CompilerGenerated]
			protected virtual bool PrintMembers(StringBuilder builder)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				builder.Append("Extension = ");
				builder.Append(this.Extension);
				builder.Append(", ContentType = ");
				builder.Append(this.ContentType);
				return true;
			}

			// Token: 0x0600014B RID: 331 RVA: 0x00006A2A File Offset: 0x00004C2A
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator !=(StaticFilesEndpoint.ExtensionContentType left, StaticFilesEndpoint.ExtensionContentType right)
			{
				return !(left == right);
			}

			// Token: 0x0600014C RID: 332 RVA: 0x00006A36 File Offset: 0x00004C36
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator ==(StaticFilesEndpoint.ExtensionContentType left, StaticFilesEndpoint.ExtensionContentType right)
			{
				return left == right || (left != null && left.Equals(right));
			}

			// Token: 0x0600014D RID: 333 RVA: 0x00006A4A File Offset: 0x00004C4A
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Extension>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ContentType>k__BackingField);
			}

			// Token: 0x0600014E RID: 334 RVA: 0x00006A8A File Offset: 0x00004C8A
			[NullableContext(2)]
			[CompilerGenerated]
			public override bool Equals(object obj)
			{
				return this.Equals(obj as StaticFilesEndpoint.ExtensionContentType);
			}

			// Token: 0x0600014F RID: 335 RVA: 0x00006A98 File Offset: 0x00004C98
			[NullableContext(2)]
			[CompilerGenerated]
			public virtual bool Equals(StaticFilesEndpoint.ExtensionContentType other)
			{
				return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Extension>k__BackingField, other.<Extension>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ContentType>k__BackingField, other.<ContentType>k__BackingField));
			}

			// Token: 0x06000151 RID: 337 RVA: 0x00006AF9 File Offset: 0x00004CF9
			[CompilerGenerated]
			protected ExtensionContentType([Nullable(1)] StaticFilesEndpoint.ExtensionContentType original)
			{
				this.Extension = original.<Extension>k__BackingField;
				this.ContentType = original.<ContentType>k__BackingField;
			}

			// Token: 0x06000152 RID: 338 RVA: 0x00006B19 File Offset: 0x00004D19
			[CompilerGenerated]
			public void Deconstruct(out string Extension, out string ContentType)
			{
				Extension = this.Extension;
				ContentType = this.ContentType;
			}
		}
	}
}
