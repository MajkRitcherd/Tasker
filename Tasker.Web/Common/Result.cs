namespace Tasker.Web.Common;

/// <summary>
/// Represents the result of an operation using the Result pattern with factory methods.
/// </summary>
/// <typeparam name="T">The type of the value returned when the operation succeeds.</typeparam>
public record Result<T>(bool IsSuccess, T? Value, string? ErrorMessage)
{
    /// <summary>
    /// Creates a successful result with the specified value.
    /// </summary>
    /// <param name="value">The value to return on success.</param>
    /// <returns>A <see cref="Result{T}"/> with <see cref="IsSuccess"/> set to <c>true</c>.</returns>
    public static Result<T> Success(T value) => new(true, value, null);

    /// <summary>
    /// Creates a failed result with the specified error message.
    /// </summary>
    /// <param name="errorMessage">The error message describing the failure.</param>
    /// <returns>A <see cref="Result{T}"/> with <see cref="IsSuccess"/> set to <c>false</c>.</returns>
    public static Result<T> Failure(string errorMessage) => new(false, default, errorMessage);
}

/// <summary>
/// Represents the result of an operation that does not return a value.
/// </summary>
public record Result(bool IsSuccess, string? ErrorMessage)
{
    /// <summary>
    /// Creates a successful result with no value.
    /// </summary>
    /// <returns>A <see cref="Result"/> with <see cref="IsSuccess"/> set to <c>true</c>.</returns>
    public static Result Success() => new(true, null);

    /// <summary>
    /// Creates a failed result with the specified error message.
    /// </summary>
    /// <param name="errorMessage">The error message describing the failure.</param>
    /// <returns>A <see cref="Result"/> with <see cref="IsSuccess"/> set to <c>false</c>.</returns>
    public static Result Failure(string errorMessage) => new(false, errorMessage);
}
