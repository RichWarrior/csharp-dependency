using Dapper.Contrib.Extensions;
using System;

namespace csharp.dependency.core.Entity
{
    public class BaseEntity
    {
        [ExplicitKey]
        public long id { get; set; }
        public DateTime creation_date { get; set; }
        public long creator_id { get; set; }
        public int status_id { get; set; }
    }
}
