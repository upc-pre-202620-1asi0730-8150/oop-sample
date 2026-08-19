using Acme.OOProgramming.Shared.Domain.Model.ValueObjects;
using Acme.OOProgramming.Shared.Presentation;

namespace Acme.OOProgramming.Tests.Shared.Presentation;

/// <summary>
/// Contains unit tests for Shared Kernel presentation extension members.
/// </summary>
public class ConsoleFormattingTests
{
    /// <summary>
    /// Verifies that <see cref="ConsoleFormatting.Display"/> formats the monetary value with number separation and currency code.
    /// </summary>
    [Fact]
    public void Display_ReturnsFormattedAmountAndCurrency()
    {
        var money = new Money(1234.50m, "USD");

        var display = money.Display;

        Assert.Equal($"{1234.50m:N2} USD", display);
    }
}
