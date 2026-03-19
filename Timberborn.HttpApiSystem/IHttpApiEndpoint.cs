using System;
using System.Net;
using System.Threading.Tasks;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x02000038 RID: 56
	public interface IHttpApiEndpoint
	{
		// Token: 0x0600012A RID: 298
		Task<bool> TryHandle(HttpListenerContext context);
	}
}
