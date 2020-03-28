using System.Xml.Serialization;

namespace csharp.dependency.socket.client.Entity.Dotnet
{
	[XmlRoot(ElementName = "ProjectReference")]
	public class ProjectReference
	{
		[XmlAttribute(AttributeName = "Include")]
		public string Include { get; set; }
	}
}
