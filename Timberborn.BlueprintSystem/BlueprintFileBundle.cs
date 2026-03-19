using System;
using System.Collections.Immutable;
using System.Linq;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x02000014 RID: 20
	public class BlueprintFileBundle
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000034B3 File Offset: 0x000016B3
		public string Name { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000034BB File Offset: 0x000016BB
		public string Path { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000034C3 File Offset: 0x000016C3
		public ImmutableArray<string> Jsons { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000034CB File Offset: 0x000016CB
		public ImmutableArray<string> Sources { get; }

		// Token: 0x06000075 RID: 117 RVA: 0x000034D3 File Offset: 0x000016D3
		public BlueprintFileBundle(string name, string path, ImmutableArray<string> jsons, ImmutableArray<string> sources)
		{
			this.Name = name;
			this.Path = path;
			this.Jsons = jsons;
			this.Sources = sources;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000034F8 File Offset: 0x000016F8
		public static BlueprintFileBundle CreateBundled(IGrouping<string, BlueprintAsset> assets)
		{
			return new BlueprintFileBundle(assets.First<BlueprintAsset>().Name, assets.Key, (from asset in assets
			select asset.Content).ToImmutableArray<string>(), (from asset in assets
			select asset.Source).ToImmutableArray<string>());
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000356F File Offset: 0x0000176F
		public static BlueprintFileBundle CreateSingle(BlueprintAsset asset)
		{
			return new BlueprintFileBundle(asset.Name, asset.Path, ImmutableArray.Create<string>(asset.Content), ImmutableArray.Create<string>(asset.Source));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003598 File Offset: 0x00001798
		public BlueprintFileBundle AddJson(string json, string source)
		{
			return new BlueprintFileBundle(this.Name, this.Path, this.Jsons.Add(json), this.Sources.Add(source));
		}
	}
}
