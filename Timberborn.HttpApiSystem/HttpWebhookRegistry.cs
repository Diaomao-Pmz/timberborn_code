using System;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.EntitySystem;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x02000036 RID: 54
	public class HttpWebhookRegistry
	{
		// Token: 0x06000120 RID: 288 RVA: 0x000062DA File Offset: 0x000044DA
		public HttpWebhookRegistry(EntityComponentRegistry entityComponentRegistry)
		{
			this._entityComponentRegistry = entityComponentRegistry;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000062EC File Offset: 0x000044EC
		public ImmutableArray<string> FindUnsafeAddresses()
		{
			return (from address in (from url in this._entityComponentRegistry.GetAll<HttpAdapter>().SelectMany((HttpAdapter client) => client.AllWebhookUrls)
			where !string.IsNullOrWhiteSpace(url)
			where !HttpWebhookRegistry.UrlIsSafe(url)
			select url).Select(new Func<string, string>(HttpWebhookRegistry.GetHostIfPossible)).Distinct<string>()
			orderby address
			select address).ToImmutableArray<string>();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000063B0 File Offset: 0x000045B0
		public static bool UrlIsSafe(string url)
		{
			Uri uri;
			return Uri.TryCreate(url, UriKind.Absolute, out uri) && uri.IsLoopback;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000063D0 File Offset: 0x000045D0
		public static string GetHostIfPossible(string url)
		{
			Uri uri;
			if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
			{
				return url;
			}
			return uri.Host;
		}

		// Token: 0x040000E6 RID: 230
		public readonly EntityComponentRegistry _entityComponentRegistry;
	}
}
