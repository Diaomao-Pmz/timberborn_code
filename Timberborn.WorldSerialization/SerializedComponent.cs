using System;
using Timberborn.SerializationSystem;

namespace Timberborn.WorldSerialization
{
	// Token: 0x02000004 RID: 4
	public class SerializedComponent : IEquatable<SerializedComponent>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public string Name { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public SerializedObject Value { get; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020CE File Offset: 0x000002CE
		public SerializedComponent(string name) : this(name, new SerializedObject())
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020DC File Offset: 0x000002DC
		public SerializedComponent(string name, SerializedObject value)
		{
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020F2 File Offset: 0x000002F2
		public bool Equals(SerializedComponent other)
		{
			return other != null && this.Name == other.Name && this.Value.Equals(other.Value);
		}
	}
}
