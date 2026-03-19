using System;
using UnityEngine;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x02000010 RID: 16
	public class BlueprintAsset : ScriptableObject
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000044 RID: 68 RVA: 0x00002C60 File Offset: 0x00000E60
		// (remove) Token: 0x06000045 RID: 69 RVA: 0x00002C98 File Offset: 0x00000E98
		public event EventHandler OnValidated;

		// Token: 0x06000046 RID: 70 RVA: 0x00002CCD File Offset: 0x00000ECD
		public static BlueprintAsset Create(string path, string content, string source)
		{
			BlueprintAsset blueprintAsset = ScriptableObject.CreateInstance<BlueprintAsset>();
			blueprintAsset._path = path;
			blueprintAsset._content = content;
			blueprintAsset._source = source;
			return blueprintAsset;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002CE9 File Offset: 0x00000EE9
		public string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002CF1 File Offset: 0x00000EF1
		public string Content
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002CF9 File Offset: 0x00000EF9
		public string Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002D04 File Offset: 0x00000F04
		public string Name
		{
			get
			{
				string name = base.name;
				int length = BlueprintAsset.Extension.Length;
				return name.Substring(0, name.Length - length);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002D32 File Offset: 0x00000F32
		public void SetContent(string content)
		{
			this._content = content;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002D3B File Offset: 0x00000F3B
		public void OnValidate()
		{
			EventHandler onValidated = this.OnValidated;
			if (onValidated == null)
			{
				return;
			}
			onValidated(this, EventArgs.Empty);
		}

		// Token: 0x0400001A RID: 26
		public static readonly string Extension = ".blueprint";

		// Token: 0x0400001B RID: 27
		public static readonly string FullExtension = BlueprintAsset.Extension + ".json";

		// Token: 0x0400001C RID: 28
		public static readonly string OptionalExtension = ".optional" + BlueprintAsset.Extension;

		// Token: 0x0400001D RID: 29
		[SerializeField]
		public string _path;

		// Token: 0x0400001E RID: 30
		[SerializeField]
		public string _content;

		// Token: 0x0400001F RID: 31
		[SerializeField]
		public string _source;
	}
}
