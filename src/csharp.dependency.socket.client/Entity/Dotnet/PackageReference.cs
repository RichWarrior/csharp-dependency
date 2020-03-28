using System.Xml.Serialization;

namespace csharp.dependency.socket.client.Entity.Dotnet
{
	[XmlRoot(ElementName = "PackageReference")]
	public class PackageReference
	{
		[XmlAttribute(AttributeName = "Include")]
		public string Include { get; set; }
		[XmlAttribute(AttributeName = "Version")]
		public string Version { get; set; }
	}

}
