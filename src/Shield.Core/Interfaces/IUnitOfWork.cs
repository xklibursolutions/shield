namespace XkliburSolutions.Shield.Core.Interfaces;

/// <summary>
/// Represents a unit of work that encapsulates a series of operations to be completed as a single transaction.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Asynchronously completes the unit of work and returns the number of state entries written to the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
    Task<int> CompleteAsync();
}
