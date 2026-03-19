using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;
using Timberborn.Localization;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Beavers
{
	// Token: 0x0200000E RID: 14
	public class BeaverNameService : ISaveableSingleton, ILoadableSingleton
	{
		// Token: 0x06000032 RID: 50 RVA: 0x000025A6 File Offset: 0x000007A6
		public BeaverNameService(ISingletonLoader singletonLoader, IRandomNumberGenerator randomNumberGenerator, ILoc loc)
		{
			this._singletonLoader = singletonLoader;
			this._randomNumberGenerator = randomNumberGenerator;
			this._loc = loc;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000025DC File Offset: 0x000007DC
		public void Load()
		{
			this.InitializeCompleteNamePool();
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(BeaverNameService.BeaverNameServiceKey, out objectLoader))
			{
				List<string> first = objectLoader.Get(BeaverNameService.NamesKey);
				this._names.Clear();
				this._names.AddRange(first.Intersect(this._completeNamePool));
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002631 File Offset: 0x00000831
		public void Save(ISingletonSaver singletonSaver)
		{
			singletonSaver.GetSingleton(BeaverNameService.BeaverNameServiceKey).Set(BeaverNameService.NamesKey, this._names);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002650 File Offset: 0x00000850
		public string RandomName()
		{
			if (this._names.Count == 0)
			{
				this._names.AddRange(this._completeNamePool);
			}
			string listElement = this._randomNumberGenerator.GetListElement<string>(this._names);
			this._names.Remove(listElement);
			return listElement;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000269C File Offset: 0x0000089C
		public void InitializeCompleteNamePool()
		{
			IEnumerable<string> collection = from name in this._loc.T(BeaverNameService.BeaverNamePoolLocKey).Split('\n', StringSplitOptions.None).Select(new Func<string, string>(BeaverNameService.SanitizeName))
			where name.Length > 0
			select name;
			this._completeNamePool.AddRange(collection);
			if (this._completeNamePool.IsEmpty<string>())
			{
				throw new Exception("Name pool is empty.");
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000271B File Offset: 0x0000091B
		public static string SanitizeName(string name)
		{
			return name.Replace("\r", "").Trim();
		}

		// Token: 0x04000019 RID: 25
		public static readonly string BeaverNamePoolLocKey = "Beaver.NamePool";

		// Token: 0x0400001A RID: 26
		public static readonly SingletonKey BeaverNameServiceKey = new SingletonKey("BeaverNameService");

		// Token: 0x0400001B RID: 27
		public static readonly ListKey<string> NamesKey = new ListKey<string>("Names");

		// Token: 0x0400001C RID: 28
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x0400001D RID: 29
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400001E RID: 30
		public readonly ILoc _loc;

		// Token: 0x0400001F RID: 31
		public readonly List<string> _completeNamePool = new List<string>();

		// Token: 0x04000020 RID: 32
		public readonly List<string> _names = new List<string>();
	}
}
