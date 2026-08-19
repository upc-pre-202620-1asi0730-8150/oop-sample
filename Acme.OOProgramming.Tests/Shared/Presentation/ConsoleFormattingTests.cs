using Acme.OOProgramming.Shared.Domain.Model.ValueObjects;
using Acme.OOProgramming.Shared.Presentation;

namespace Acme.OOProgramming.Tests.Shared.Presentation;

public class ConsoleFormattingTests
{
    [Fact]
    public void Display_ReturnsFormattedAmountAndCurrency()
    {
        var money = new Money(1234.50m, "USD");

        var display = money.Display;

        Assert.Equal($"{1234.50m:N2} USD", display);
    }
}
