using System.Threading;
using System.Threading.Tasks;

namespace Kt.Data.Authorization.Objects;


/// <summary>
/// Defines an interface for object access policies, allowing for the determination of read and property creation permissions based on the session, table name, object ID, and role ID.
/// </summary>
public interface IKtObjectAccessPolicy {

    /// <summary>
    /// Determines whether the specified session can read the object with the given ID from the specified table.
    /// </summary>
    /// <param name="session">The database session.</param>
    /// <param name="tableName">The name of the table.</param>
    /// <param name="id">The ID of the object.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the session can read the object.</returns>
    Task<bool> CanReadAsync(KtDbSession session, string tableName, ulong id, CancellationToken cancellationToken);


    /// <summary>
    /// Determines whether the specified role can create a property for the specified master object.
    /// </summary>
    /// <param name="session">The database session.</param>
    /// <param name="masterId">The ID of the master object.</param>
    /// <param name="roleId">The ID of the role.</param>
    /// <param name="valueTable">The name of the value table.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the role can create the property.</returns>
    Task<bool> CanCreatePropertyAsync(KtDbSession session, ulong masterId, ulong roleId, string valueTable, CancellationToken cancellationToken);
}