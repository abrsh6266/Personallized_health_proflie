using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace PersonalizedHealthCenter.Models
{
    [CollectionName("roles")]
    public class Role: MongoIdentityRole<Guid>
    {

    }
}