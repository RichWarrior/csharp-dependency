using System.Collections.Generic;
using System.Xml.Serialization;

namespace csharp.dependency.socket.client.Entity.Dotnet
{
	[XmlRoot(ElementName = "Project")]
	public class Project
	{
		[XmlElement(ElementName = "PropertyGroup")]
		public List<PropertyGroup> PropertyGroup { get; set; }
		[XmlElement(ElementName = "ItemGroup")]
		public List<ItemGroup> ItemGroup { get; set; }
		[XmlAttribute(AttributeName = "Sdk")]
		public string Sdk { get; set; }
	}
}
