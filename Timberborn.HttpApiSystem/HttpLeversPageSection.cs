using System;
using System.IO;
using System.Linq;
using HandlebarsDotNet;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x0200002B RID: 43
	public class HttpLeversPageSection : IHttpApiPageSection, ILoadableSingleton
	{
		// Token: 0x060000EE RID: 238 RVA: 0x0000572D File Offset: 0x0000392D
		public HttpLeversPageSection(HttpApiIntermediary httpApiIntermediary, HttpApiUrlGenerator httpApiUrlGenerator)
		{
			this._httpApiIntermediary = httpApiIntermediary;
			this._httpApiUrlGenerator = httpApiUrlGenerator;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00005743 File Offset: 0x00003943
		public int Order
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005747 File Offset: 0x00003947
		public void Load()
		{
			this._bodyTemplate = Handlebars.Compile(File.ReadAllText(HttpLeversPageSection.BodyTemplatePath));
			this._footer = Handlebars.Compile(File.ReadAllText(HttpLeversPageSection.FooterTemplatePath))(new object(), null);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005780 File Offset: 0x00003980
		public string BuildBody()
		{
			return this._bodyTemplate(new
			{
				leversUrl = "/api/levers",
				levers = from lever in this._httpApiIntermediary.GetLevers()
				orderby lever.Name
				select new
				{
					name = lever.Name,
					state = lever.State,
					isSpringReturn = lever.IsSpringReturn,
					url = "/api/levers/" + Uri.EscapeDataString(lever.Name),
					switchOnUrl = this._httpApiUrlGenerator.SwitchOnLeverUrlPath(lever.Name),
					switchOffUrl = this._httpApiUrlGenerator.SwitchOffLeverUrlPath(lever.Name),
					redUrl = this._httpApiUrlGenerator.ColorLeverUrlPath(lever.Name, Color.red),
					greenUrl = this._httpApiUrlGenerator.ColorLeverUrlPath(lever.Name, Color.green)
				}
			}, null);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000057E8 File Offset: 0x000039E8
		public string BuildFooter()
		{
			return this._footer;
		}

		// Token: 0x040000B9 RID: 185
		public static readonly string BodyTemplatePath = Path.Combine(HttpApi.RootPath, "index-levers.hbs");

		// Token: 0x040000BA RID: 186
		public static readonly string FooterTemplatePath = Path.Combine(HttpApi.RootPath, "index-levers-footer.hbs");

		// Token: 0x040000BB RID: 187
		public readonly HttpApiIntermediary _httpApiIntermediary;

		// Token: 0x040000BC RID: 188
		public readonly HttpApiUrlGenerator _httpApiUrlGenerator;

		// Token: 0x040000BD RID: 189
		public HandlebarsTemplate<object, object> _bodyTemplate;

		// Token: 0x040000BE RID: 190
		public string _footer;
	}
}
