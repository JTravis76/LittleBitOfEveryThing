namespace WebAppFunction.Api.Extension
{
  public static class StringExtension
  {
    /// <summary>
    /// Returns null if empty string. Primary use would be for preserving
    /// null values when writing to SQL databases (i.e. IHistoryService), instead
    /// of ToString() which will convert a null value to an empty string.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string ToNullableString<T>(this T value) =>
        value == null ? null : value.ToString();
  }
}