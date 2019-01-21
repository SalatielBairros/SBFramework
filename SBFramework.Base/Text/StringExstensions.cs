
using System.Text;

namespace SBFramework.Base.Text
{
  public static class StringExstensions
  {
    /// <summary>
    /// Quebra uma string em um array de strings baseados no caracter de nova linha.
    /// </summary>
    /// <param name="this">String a ser quebrada.</param>
    /// <returns>Array resultante.</returns>
    public static string[] GetLines(this string @this)
    {
      return @this.Replace("\r", string.Empty).Split('\n');
    }

    public static byte[] ToByte(this string @this)
    {
      return Encoding.UTF8.GetBytes(@this);
    }

    public static string JoinByComma(this string[] @this) => string.Join(", ", @this);

    public static string AsParameters(this string[] @this) => string.Join(", @", @this);

    public static string ToSetClausule(this string[] @this, string parameterPrefix)
    {
      for (int i = 0; i < @this.Length; i++)
      {
        @this[i] = $" {@this[i]} = {parameterPrefix}{@this[i]} ";
      }

      return @this.JoinByComma();
    }
  }
}
