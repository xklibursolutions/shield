using XkliburSolutions.Shield.Core.Interfaces;

namespace XkliburSolutions.Shield.Infrastructure.Data;

/// <summary>
/// Implements the unit of work pattern, coordinating the work of multiple repositories and managing transactions.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="users">The user repository.</param>
public class UnitOfWork(ApplicationDbContext context, IUserRepository users) : IUnitOfWork
{
    /// <summary>
    /// Gets the user repository.
    /// </summary>
    public IUserRepository Users { get; private set; } = users;

    ///<inheritdoc/>
    public async Task<int> CompleteAsync()
    {
        return await context.SaveChangesAsync();
    }

    /// <summary>
    /// Disposes the unit of work, releasing any resources it holds.
    /// </summary>
    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this);
    }
}
