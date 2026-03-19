using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200001A RID: 26
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	internal sealed class LibraryImportAttribute : Attribute
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002D00 File Offset: 0x00000F00
		[NullableContext(1)]
		public LibraryImportAttribute(string libraryName)
		{
			this.LibraryName = libraryName;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002D0F File Offset: 0x00000F0F
		[Nullable(1)]
		public string LibraryName { [NullableContext(1)] get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002D17 File Offset: 0x00000F17
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002D1F File Offset: 0x00000F1F
		public string EntryPoint { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002D28 File Offset: 0x00000F28
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002D30 File Offset: 0x00000F30
		public StringMarshalling StringMarshalling { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002D39 File Offset: 0x00000F39
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002D41 File Offset: 0x00000F41
		public Type StringMarshallingCustomType { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002D4A File Offset: 0x00000F4A
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002D52 File Offset: 0x00000F52
		public bool SetLastError { get; set; }
	}
}
