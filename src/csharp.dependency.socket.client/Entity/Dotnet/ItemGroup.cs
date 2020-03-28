using System.Collections.Generic;
using System.Xml.Serialization;

namespace csharp.dependency.socket.client.Entity.Dotnet
{
	[XmlRoot(ElementName = "ItemGroup")]
	public class ItemGroup
	{
		[XmlElement(ElementName = "PackageReference")]
		public List<PackageReference> PackageReference { get; set; }
		[XmlElement(ElementName = "ProjectReference")]
		public List<ProjectReference> ProjectReference { get; set; }
	}
}
