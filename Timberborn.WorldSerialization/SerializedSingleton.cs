using System;
using Timberborn.SerializationSystem;

namespace Timberborn.WorldSerialization
{
	// Token: 0x02000007 RID: 7
	public class SerializedSingleton
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022D5 File Offset: 0x000004D5
		public string Name { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000022DD File Offset: 0x000004DD
		public SerializedObject Value { get; }

		// Token: 0x06000016 RID: 22 RVA: 0x000022E5 File Offset: 0x000004E5
		public SerializedSingleton(string name) : this(name, new SerializedObject())
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022F3 File Offset: 0x000004F3
		public SerializedSingleton(string name, SerializedObject value)
		{
			this.Name = name;
			this.Value = value;
		}
	}
}
