namespace System;

public static class PriceExtensions
{
    public static string GetFormattedPrice(this decimal price) => price.ToString("0.00");
}