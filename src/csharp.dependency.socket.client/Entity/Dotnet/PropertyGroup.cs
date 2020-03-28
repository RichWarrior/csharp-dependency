using System.Xml.Serialization;

namespace csharp.dependency.socket.client.Entity.Dotnet
{
	[XmlRoot(ElementName = "PropertyGroup")]
	public class PropertyGroup
	{
		[XmlElement(ElementName = "TargetFramework")]
		public string TargetFramework { get; set; }
		[XmlElement(ElementName = "DocumentationFile")]
		public string DocumentationFile { get; set; }
		[XmlAttribute(AttributeName = "Condition")]
		public string Condition { get; set; }
	}
}
