using Acme.OOProgramming.Shared.Domain.Model.ValueObjects;

namespace Acme.OOProgramming.Shared.Presentation;

/// <summary>
/// Provides console formatting methods for value objects. 
/// </summary>
internal static class ConsoleFormatting
{
    /// <summary>
    /// Formats a <see cref="Money"/> value object for display.
    /// </summary>
    /// <param name="money">The <see cref="Money"/> value object to format.</param>
    extension(Money money)
    {
        /// <summary>
        /// Returns a formatted string representation of the <see cref="Money"/> value object.
        /// </summary>
        public string Display => $"{money.Amount:N2} {money.Currency.Code}";
    }
}